// TehaiCalculator.cs
// Reusable Unity component for all 3 tehai calculators.
// - Mode is chosen via INSPECTOR checkboxes (bools), not UI Toggles.
// - Auto-calculates whenever relevant fields change.
// - If required inputs are empty/invalid, it does NOTHING (keeps existing output).

using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TehaiCalculator : MonoBehaviour
{
    private enum Mode
    {
        SimpleTehai,
        KamaliChakradhar,
        FarmaishiChakradhar
    }

    [Header("Mode Select (Inspector Checkboxes)")]
    [SerializeField] private bool useSimpleTehai = true;
    [SerializeField] private bool useKamali;
    [SerializeField] private bool useFarmaishi;

    [Header("Inputs - Simple Tehai (2 fields)")]
    [SerializeField] private TMP_InputField simpleMaatrasInTaalInput;
    [SerializeField] private TMP_InputField simpleAvartaansInput;

    [Header("Inputs - Kamali (tracker reference)")]
    [SerializeField] private ButtonMaatrasTracker kamaliMaatrasTracker;

    [Header("Inputs - Farmaishi (1 field)")]
    [SerializeField] private TMP_InputField farmaishiMaatrasInput;

    [Header("Output")]
    [SerializeField] private TMP_Text outputText;

    // Optional: last computed result (only updates when inputs are valid)
    public string LastResult { get; private set; } = "";

    // Same supported taal list as your Swift Kamali view
    private static readonly int[] KamaliSupportedTaals = { 7, 10, 12, 14, 15, 16 };

    private struct KamaliParts
    {
        public int PartA;     // p1
        public int PartB;     // tb1
        public double PartC;  // t1 (can be 1.5)
        public int Gap;       // g1
    }

    // Matches Swift mapping exactly (for displayed values)
    private static readonly Dictionary<int, KamaliParts> KamaliMap = new Dictionary<int, KamaliParts>
    {
        {  7, new KamaliParts { PartA =  5, PartB = 2, PartC = 1.5, Gap = 1 } },
        { 10, new KamaliParts { PartA =  8, PartB = 2, PartC = 3.0, Gap = 1 } },
        { 12, new KamaliParts { PartA = 10, PartB = 2, PartC = 3.0, Gap = 5 } },
        { 14, new KamaliParts { PartA =  9, PartB = 5, PartC = 3.0, Gap = 0 } },
        { 15, new KamaliParts { PartA = 11, PartB = 4, PartC = 3.0, Gap = 5 } },
        { 16, new KamaliParts { PartA = 10, PartB = 6, PartC = 3.0, Gap = 1 } },
    };

    // ---------------- Unity lifecycle ----------------

    private void OnValidate()
    {
        // Allow single selection only - if multiple selected, priority: Simple > Kamali > Farmaishi
        int count = (useSimpleTehai ? 1 : 0) + (useKamali ? 1 : 0) + (useFarmaishi ? 1 : 0);

        if (count > 1)
        {
            if (useSimpleTehai)
            {
                useKamali = false;
                useFarmaishi = false;
            }
            else if (useKamali)
            {
                useFarmaishi = false;
            }
        }

#if UNITY_EDITOR
        // In-editor, you often want the output to update immediately when you flip modes.
        // It still respects "invalid/empty => do nothing".
        if (!Application.isPlaying)
            TryAutoCalculate();
#endif
    }

    // Track last kamali value for change detection
    private int lastKamaliMaatras = -1;

    private void OnEnable()
    {
        // Simple inputs
        if (simpleMaatrasInTaalInput != null) simpleMaatrasInTaalInput.onValueChanged.AddListener(OnAnyTextChanged);
        if (simpleAvartaansInput != null) simpleAvartaansInput.onValueChanged.AddListener(OnAnyTextChanged);

        // Farmaishi input
        if (farmaishiMaatrasInput != null) farmaishiMaatrasInput.onValueChanged.AddListener(OnAnyTextChanged);

        // Kamali tracker - no listener needed, will poll in Update()
        if (kamaliMaatrasTracker != null)
        {
            lastKamaliMaatras = kamaliMaatrasTracker.maatras;
        }

        TryAutoCalculate();
    }

    private void OnDisable()
    {
        if (simpleMaatrasInTaalInput != null) simpleMaatrasInTaalInput.onValueChanged.RemoveListener(OnAnyTextChanged);
        if (simpleAvartaansInput != null) simpleAvartaansInput.onValueChanged.RemoveListener(OnAnyTextChanged);

        if (farmaishiMaatrasInput != null) farmaishiMaatrasInput.onValueChanged.RemoveListener(OnAnyTextChanged);
    }

    private void Update()
    {
        // Poll the Kamali tracker for changes (only when in Kamali mode)
        if (useKamali && kamaliMaatrasTracker != null)
        {
            int currentMaatras = kamaliMaatrasTracker.maatras;
            if (currentMaatras != lastKamaliMaatras)
            {
                lastKamaliMaatras = currentMaatras;
                TryAutoCalculate();
            }
        }
    }

    // ---------------- Public API ----------------

    /// <summary>
    /// Manually trigger compute (optional if you still want a button).
    /// If inputs are invalid/empty, does nothing.
    /// </summary>
    public void CalculateAndApply()
    {
        TryAutoCalculate();
    }

    /// <summary>
    /// Returns what WOULD be displayed, but returns null if inputs are invalid/empty.
    /// (And does not modify outputText.)
    /// </summary>
    public string CalculateSelectedStringOrNull()
    {
        Mode mode = GetSelectedMode();

        switch (mode)
        {
            case Mode.SimpleTehai:
            {
                if (!TryParsePositiveInt(simpleMaatrasInTaalInput, out int maatras)) return null;
                if (!TryParsePositiveInt(simpleAvartaansInput, out int avartaans)) return null;
                return CalculateSimpleTehaiString(maatras, avartaans);
            }
            case Mode.KamaliChakradhar:
            {
                if (!TryGetKamaliTaal(out int taal))
                {
                    // If taal is 0 (no button selected), show instruction message
                    if (kamaliMaatrasTracker != null && kamaliMaatrasTracker.maatras == 0)
                        return "Please Set Value Above to See Result";
                    return null;
                }
                if (!KamaliMap.ContainsKey(taal)) return null;
                return CalculateKamaliChakradharString(taal);
            }
            case Mode.FarmaishiChakradhar:
            {
                if (!TryParsePositiveInt(farmaishiMaatrasInput, out int maatras)) return null;
                return CalculateFarmaishiChakradharString(maatras);
            }
            default:
                return null;
        }
    }

    // ---------------- Event handlers ----------------

    private void OnAnyTextChanged(string _)
    {
        TryAutoCalculate();
    }

    // ---------------- Core auto-calc ----------------

    private void TryAutoCalculate()
    {
        // If there's nowhere to put it, respect "don't do anything".
        if (outputText == null) return;

        string result = CalculateSelectedStringOrNull();
        if (string.IsNullOrEmpty(result)) return; // invalid/empty -> do nothing

        LastResult = result;
        outputText.text = result;
    }

    private Mode GetSelectedMode()
    {
        // OnValidate keeps this consistent, but keep a safe fallback.
        if (useSimpleTehai) return Mode.SimpleTehai;
        if (useKamali) return Mode.KamaliChakradhar;
        if (useFarmaishi) return Mode.FarmaishiChakradhar;
        return Mode.SimpleTehai;
    }

    // ---------------- Input parsing helpers ----------------

    private static bool TryParsePositiveInt(TMP_InputField field, out int value)
    {
        value = 0;
        if (field == null) return false;

        string s = field.text;
        if (string.IsNullOrWhiteSpace(s)) return false;

        if (!int.TryParse(s, out value)) return false;
        if (value <= 0) return false;

        return true;
    }

    private bool TryGetKamaliTaal(out int taal)
    {
        taal = 0;
        if (kamaliMaatrasTracker == null) return false;

        // Get the maatras value directly from the tracker
        taal = kamaliMaatrasTracker.maatras;

        // Only accept supported taal values (and reject 0)
        if (taal <= 0) return false;
        return KamaliMap.ContainsKey(taal);
    }

    // ---------------- Calculators (formula-equivalent) ----------------

    // 1) Simple Tehai (matches Swift exactly)
    private static string CalculateSimpleTehaiString(int maatrasInTaal, int avartaans)
    {
        long L = (long)maatrasInTaal * (long)avartaans;
        long total = L + 1;          // Swift: (num1*num2 + 1)

        long palla = total / 3;      // Swift: Int((...)/3) for positive
        long rem = total % 3;        // 0,1,2

        // Swift: g1 = ((...) % 3) / 2  -> {0, 0.5, 1}
        string gapStr = (rem == 1) ? "0.5" : (rem / 2).ToString(CultureInfo.InvariantCulture);

        return $"Palla size: {palla}\nGap size: {gapStr}";
    }

    // 2) Kamali Chakradhar (lookup table)
    private static string CalculateKamaliChakradharString(int maatrasInTaal)
    {
        KamaliParts parts = KamaliMap[maatrasInTaal]; // <-- fixed (no unsupported named-arg nonsense)

        // Mimic Swift String(t1): show "3" not "3.0", but keep "1.5"
        string partCStr = (parts.PartC % 1.0 == 0.0)
            ? ((int)parts.PartC).ToString(CultureInfo.InvariantCulture)
            : parts.PartC.ToString(CultureInfo.InvariantCulture);

        return $"Part A: {parts.PartA}\nPart B: {parts.PartB}\nPart C: {partCStr}\nGap size: {parts.Gap}";
    }

    // 3) Farmaishi Chakradhar (piecewise simplified; matches Swift outputs)
    private static string CalculateFarmaishiChakradharString(int N)
    {
        int k = N / 3;
        int r = N % 3;

        int p1, t1, gap;

        if (r == 0)
        {
            p1 = 2 * k + 2;
            t1 = k - 1;
            gap = 2;
        }
        else if (r == 1)
        {
            p1 = 2 * k + 2;
            t1 = k;
            gap = 0;
        }
        else // r == 2
        {
            p1 = 2 * k + 3;
            t1 = k;
            gap = 1;
        }

        int p2 = p1 + N;
        int p3 = p2 + N;

        return $"Part A: {p1} {p2} {p3}\nPart B: {t1}\nGap size: {gap}";
    }
}
