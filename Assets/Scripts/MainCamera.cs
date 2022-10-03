using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private static MainCamera _Instance;
    public static MainCamera Instance
    {
        get
        {
            if (_Instance == null) {
                _Instance = new MainCamera();
            }
            return _Instance;
        }
    }
    Camera mainCamera;
    private bool isFullScreen = false;

    public MainCamera()
    {
        _Instance = this;
    }

    bool SetFullScreen(int width, int height, bool isFullScreen, FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen)
    {
        Screen.SetResolution(width, height, isFullScreen);
        Screen.fullScreenMode = fullScreenMode;
        Screen.fullScreen = isFullScreen;
        return isFullScreen;
    }

    void Start()
    {
        //mainCamera = gameObject.GetComponent<Camera>();
        //mainCamera.orthographicSize = Screen.height / 2;
        SetFullScreen(1366, 768, isFullScreen);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            isFullScreen = SetFullScreen(1366, 768, !isFullScreen);
        }
    }
}
