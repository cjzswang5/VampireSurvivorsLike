using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    [SerializeField] private PauseWindow pauseWindow;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseWindow.Open();
        }
    }
}
