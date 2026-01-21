using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void OnGoToScene(string name)
    {
        SceneManager.LoadScene(sceneName: name);
    }
    public void OnQuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
