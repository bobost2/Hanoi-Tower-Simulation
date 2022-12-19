using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = quitButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
