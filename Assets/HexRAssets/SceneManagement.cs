using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
public class SceneManagement : MonoBehaviour
{
    // Call this function to change to a specific scene
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void BasicScene()
    {
        SceneManager.LoadScene("Basic Tutorial");
    }
    public void UseInteractableScene()
    {
        SceneManager.LoadScene("Basic Tutorial");
    }
    public void RainScene()
    {
        SceneManager.LoadScene("Rain and fountain tutorial");
    }
    // Optional: Call this function to reload the current scene
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
