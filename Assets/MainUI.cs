using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public MainCharacterControl mainCharacterControl;

    public Slider horizontalSlider;
    public Slider verticalSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalSlider.value = (Input.GetAxis("Horizontal") + 1f) / 2f;
        verticalSlider.value = (Input.GetAxis("Vertical") + 1f) / 2f;
    }
}
