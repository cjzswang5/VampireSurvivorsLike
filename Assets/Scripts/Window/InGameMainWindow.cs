using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameMainWindow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI debugText;

    [SerializeField] private GameObject arrowKeyRoot;
    private Image upImage;
    private Image downImage;
    private Image leftImage;
    private Image rightImage;

    void Awake()
    {
        upImage     = arrowKeyRoot.transform.Find("arrowkey-up").GetComponent<Image>();
        downImage   = arrowKeyRoot.transform.Find("arrowkey-down").GetComponent<Image>();
        leftImage   = arrowKeyRoot.transform.Find("arrowkey-left").GetComponent<Image>();
        rightImage  = arrowKeyRoot.transform.Find("arrowkey-right").GetComponent<Image>();
    }

    // 切换显示按下按键的效果
    void ImageButtonDown(Image image, bool isKeyDown = false)
    {
        if (isKeyDown) {
            image.color = Color.gray;
        } else {
            image.color = Color.white;
        }
    }

    void RefreshKeyCode(KeyCode keyCode, Image image)
    {
        if (Input.GetKeyDown(keyCode))
            ImageButtonDown(image, true);
        if (Input.GetKeyUp(keyCode))
            ImageButtonDown(image, false);
    }

    void Start()
    {

    }

    void Update()
    {
        //string str = string.Format("{0} {1}", Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //debugText.text = str;
        RefreshKeyCode(KeyCode.W, upImage);
        RefreshKeyCode(KeyCode.S, downImage);
        RefreshKeyCode(KeyCode.A, leftImage);
        RefreshKeyCode(KeyCode.D, rightImage);
    }
}
