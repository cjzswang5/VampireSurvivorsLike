using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        mainCamera.orthographicSize = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
