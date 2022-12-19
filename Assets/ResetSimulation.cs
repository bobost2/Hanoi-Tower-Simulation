using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSimulation : MonoBehaviour
{
    // Start is called before the first frame update
    public Button resetSim;
    public Text status;
    public Slider ringSlider;
    public Button solveButton;
    void Start()
    {
        Button btn = resetSim.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        status.text = "PENDING...";
        status.color = Color.red;
        resetSim.interactable = false;
        for(int i = 0; i < SolveRings.finalPoleRings.Count; i++)
        {
            Destroy(SolveRings.finalPoleRings[i]);
        }
        SolveRings.finalPoleRings.Clear();
        ringSlider.interactable = true;
        solveButton.interactable = true;
    }
}
