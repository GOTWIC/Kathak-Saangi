using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
  public void LoadByName(string sceneName) => SceneManager.LoadScene(sceneName);
  public void LoadMain() => SceneManager.LoadScene("Main"); // convenience
}