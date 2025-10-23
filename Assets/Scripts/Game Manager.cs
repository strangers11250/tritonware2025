using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // The static instance of the GameManager
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // If an instance already exists, and it's not this one, destroy this new instance
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Set this as the singleton instance
            Instance = this;

            // Make this object persistent across scene changes
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // This method will be called every frame to check for input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleFullscreen();
        }
    }

    // The method to toggle the fullscreen state
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
