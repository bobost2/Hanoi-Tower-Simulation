using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpeed : MonoBehaviour
{
    public Slider speedSlider;
    public Text textStatus;
    // Start is called before the first frame update
    void Start()
    {
        speedSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        string textString = "Simulation speed - x" + speedSlider.value;
        textStatus.text = textString;
        Time.timeScale = speedSlider.value;
    }

}
