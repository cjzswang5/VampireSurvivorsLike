using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    [SerializeField] private PauseWindow pauseWindow;

    void OpenPauseWindow()
    {
        pauseWindow.Open();
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
