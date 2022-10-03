using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] private Button quitGameButton;

    private DOTweenAnimation tween;
    private CanvasGroup canvasGroup;

    void Start()
    {
        tween = gameObject.GetComponent<DOTweenAnimation>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void RegisterEvent()
    {
        quitGameButton.onClick.AddListener(() => {
            Debug.Log("QuitGameButton");
            Application.Quit();
        });
    }

    public void Open()
    {
        gameObject.SetActive(true);
        RegisterEvent();
        Debug.LogFormat("Open activeSelf={0}", gameObject.activeSelf);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        quitGameButton.onClick.RemoveAllListeners();
    }

}
