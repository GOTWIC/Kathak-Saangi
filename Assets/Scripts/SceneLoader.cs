using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName = "Main";

    private void Awake()
    {
        SceneManager.LoadScene(sceneName);
    }
}
