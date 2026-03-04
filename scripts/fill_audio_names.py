#!/usr/bin/env python3
"""
Fill audioFileName in Main.unity for each Audio item, based on displayName and page context.
Run from project root.
"""
import re

SCENE_PATH = "Assets/Scenes/Main.unity"
TARGET_GUID = "99ec488e0143c49339384deebeb4e632"
PAGE_CONTROLLER_TARGET = "5884056710426590124"

# Maps: (page_name, display_name_value) -> audio file name (no extension)
DISPLAY_TO_FILE = {
    ("Octapad", "Composition 1"): "octapad_comp1",
    ("Octapad", "Composition 2"): "octapad_comp2",
    ("Octapad", "Composition 3"): "octapad_comp3",
    ("Ladi and Upaj", "Example Ladi 1: Tabla"): "lu_lari_comp1_tabla",
    ("Ladi and Upaj", "Example Ladi 1: Recitation"): "lu_lari_comp1_rec",
    ("Ladi and Upaj", "Example Ladi 2: Tabla"): "lu_lari_comp2_tabla",
    ("Ladi and Upaj", "Example Ladi 2: Recitation"): "lu_lari_comp2_rec",
    ("Ladi and Upaj", "Example Upaj 1"): "lu_upaj_1",
    ("Ladi and Upaj", "Example Upaj 2"): "lu_upaj_2",
    ("Kramalaya", "Tabla: 1, 2, 4, Tehai"): "kramalaya_1_2_4_T_tabla",
    ("Kramalaya", "Recitation: 3 goon"): "kramalaya_3",
    ("Kramalaya", "Tabla: 1 to 4, 8, Tehai"): "kramalaya_1_4_8_T_tabla",
    ("Kramalaya", "Recitation: 5 goon"): "kramalaya_5",
    ("Kramalaya", "Recitation: 6 goon"): "kramalaya_6",
    ("Kramalaya", "Recitation: 7 goon"): "kramalaya_7",
    ("Kramalaya", "Recitation: 1 to 8, Tehai"): "kramalaya_1_8_T",
    ("Kramalaya", "Tabla: 1 to 8, Tehai"): "kramalaya_1_8_T_tabla",
    ("Padhant", "Recitation 1"): "parhant_01",
    ("Padhant", "Recitation 2"): "parhant_02",
    ("Padhant", "Recitation 3"): "parhant_03",
    ("Padhant", "Recitation 4"): "parhant_04",
    ("Padhant", "Recitation 5"): "parhant_05",
    ("Padhant", "Recitation 6"): "parhant_06",
    ("Padhant", "Recitation 7"): "parhant_07",
    ("Padhant", "Recitation 8"): "parhant_08",
    ("Padhant", "Recitation 9"): "parhant_09",
    ("Padhant", "Recitation 10"): "parhant_10",
    ("Riyaz", "2 Beat"): "riyaz_hastak_2_beat",
    ("Riyaz", "3 Beat"): "riyaz_hastak_3_beat",
    ("Riyaz", "4 Beat"): "riyaz_hastak_4_beat",
    ("Riyaz", "100 BPM"): "riyaz_taatkar_100_bpm",
    ("Riyaz", "150 BPM"): "riyaz_taatkar_150_bpm",
    ("Riyaz", "200 BPM"): "riyaz_taatkar_200_bpm",
    ("Riyaz", "220 BPM"): "riyaz_taatkar_220_bpm",
    ("Riyaz", "250 BPM"): "riyaz_taatkar_250_bpm",
    ("Riyaz", "300 BPM"): "riyaz_taatkar_300_bpm",
    ("Riyaz", "350 BPM"): "riyaz_taatkar_350_bpm",
    ("Riyaz", "400 BPM"): "riyaz_taatkar_400_bpm",
    ("Riyaz", "440 BPM"): "riyaz_taatkar_440_bpm",
    ("Riyaz", "520 BPM"): "riyaz_taatkar_520_bpm",
    ("Riyaz", "45 BPM"): "riyaz_takita_takita_dhin_45_bpm",
    ("Riyaz", "50 BPM"): "riyaz_takita_takita_dhin_50_bpm",
    ("Riyaz", "110 BPM"): "riyaz_takita_takita_dhin_110_bpm",
    ("Riyaz", "130 BPM"): "riyaz_takita_takita_dhin_130_bpm",
    ("Riyaz", "175 BPM"): "riyaz_takita_takita_dhin_175_bpm",
    ("Riyaz", "1 Beat - 70 BPM"): "riyaz_chakkar_1_beat_70_bpm",
    ("Riyaz", "1 Beat - 80 BPM"): "riyaz_chakkar_1_beat_80_bpm",
    ("Riyaz", "1 Beat - 90 BPM"): "riyaz_chakkar_1_beat_90_bpm",
    ("Riyaz", "2 Beat - 70 BPM"): "riyaz_chakkar_2_beat_70_bpm",
    ("Riyaz", "2 Beat - 80 BPM"): "riyaz_chakkar_2_beat_80_bpm",
    ("Riyaz", "5 Beat"): "riyaz_chakkar_5_beat",
}
# When (page, display) is ambiguous, use (page_name, index) -> fileName
INDEX_OVERRIDE = {
    ("Riyaz", 4): "riyaz_hastak_3_beat",
    ("Riyaz", 29): "riyaz_chakkar_3_beat",
}


def main():
    with open(SCENE_PATH, "r") as f:
        full = f.read()

    if re.search(r"propertyPath: items\.Array\.data\[\d+\]\.audioFileName\n      value: \w+", full):
        print("audioFileName already has values; skipping.")
        return

    # Split into blocks: each "--- !u!1001 &ID" starts a new object
    parts = re.split(r'(?=--- !u!1001 &\d+\nPrefabInstance:)', full)
    result_parts = []
    for part in parts:
        if not part.strip():
            continue
        if "PrefabInstance:" not in part[:100]:
            result_parts.append(part)
            continue
        if TARGET_GUID not in part or "items.Array.data" not in part or "audioFolderName" not in part:
            result_parts.append(part)
            continue
        page_name = None
        for m in re.finditer(r"propertyPath: m_Name\n      value: ([^\n]+)", part):
            v = m.group(1).strip().strip("'\"")
            if v in ("Octapad", "Ladi and Upaj", "Kramalaya", "Padhant", "Riyaz"):
                page_name = v
                break
        if not page_name:
            result_parts.append(part)
            continue
        display_by_index = {}
        for m in re.finditer(r"propertyPath: items\.Array\.data\[(\d+)\]\.displayName\n      value: ([^\n]+)", part):
            idx = int(m.group(1))
            val = m.group(2).strip().strip("'\"")
            display_by_index[idx] = val
        pattern = (
            r'(\s+- target: \{fileID: ' + PAGE_CONTROLLER_TARGET + r', guid: ' + TARGET_GUID + r', type: 3\}\n'
            r'      propertyPath: items\.Array\.data\[(\d+)\]\.audioFolderName\n'
            r'      value: Assets/Audio/tracks_app\n'
            r'      objectReference: \{fileID: 0\})'
        )
        def repl(m):
            idx = int(m.group(2))
            display = display_by_index.get(idx, "")
            file_name = INDEX_OVERRIDE.get((page_name, idx)) or DISPLAY_TO_FILE.get((page_name, display))
            if not file_name:
                return m.group(1)
            return (
                m.group(1)
                + f"\n    - target: {{fileID: {PAGE_CONTROLLER_TARGET}, guid: {TARGET_GUID}, type: 3}}\n"
                + f"      propertyPath: items.Array.data[{idx}].audioFileName\n"
                + f"      value: {file_name}\n"
                + "      objectReference: {fileID: 0}"
            )
        new_part = re.sub(pattern, repl, part)
        result_parts.append(new_part)

    new_content = "".join(result_parts)
    if new_content != full:
        with open(SCENE_PATH, "w") as f:
            f.write(new_content)
        print("Written audioFileName entries to", SCENE_PATH)
    else:
        print("No changes.")


if __name__ == "__main__":
    main()
