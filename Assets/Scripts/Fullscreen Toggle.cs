using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    // Reference to desired resolution for windowed mode
    public int windowedWidth = 1280;
    public int windowedHeight = 720;

    // Setting the fullscreen mode in the inspector
    public FullScreenMode targetFullscreenMode = FullScreenMode.ExclusiveFullScreen;

    // A variable to keep track of the current fullscreen state
    private bool isFullscreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // On start, set the internal state based on the current screen state
        isFullscreen = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle fullscreen when 'F11' key is pressed
        if (Input.GetKeyDown(KeyCode.F11))
        {
            // Toggle the boolean state
            isFullscreen = !isFullscreen;

            // Apply the new resolution and fullscreen state
            if (isFullscreen)
            {
                // Switch to fullscreen mode
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, targetFullscreenMode);
            }
            else
            {
                // Switch to windowed mode
                Screen.SetResolution(windowedWidth, windowedHeight, FullScreenMode.Windowed);
            }
        }
    }
}
