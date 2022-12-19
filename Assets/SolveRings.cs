using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolveRings : MonoBehaviour
{
    public Slider ringSlider;
    public Button solveButton;
    public GameObject mainEdge;
    public GameObject secondaryEdge;
    public GameObject finalEdge;
    public Text status;
    public Button resetSim;
    public List<GameObject> mainPoleRings = new List<GameObject>();
    public List<GameObject> secondaryPoleRings = new List<GameObject>();
    public static List<GameObject> finalPoleRings = new List<GameObject>();
    public static List<int> tasksToExecute = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        Button btn = solveButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

        status.text = "SIMULATING...\nPLEASE WAIT";
        status.color = Color.yellow;
        solveButton.interactable = false;
        ringSlider.interactable = false;
        TransferToList();
        StartCoroutine(DelayRingSolve(1));
    }

    void TransferToList()
    {
        for(int i = 0; i < RefreshRings.rings.Count; i++)
        {
            mainPoleRings.Add(RefreshRings.rings[i]);
        }
        RefreshRings.rings.Clear();
    }

    IEnumerator DelayRingSolve(float time)
    {
        yield return new WaitForSeconds(time);
        mainPoleRings.Reverse();
        char startPeg = 'A';
        char tempPeg = 'B';
        char endPeg = 'C';        
        int totalDisks = mainPoleRings.Count;
        SolveTowers(totalDisks, startPeg, endPeg, tempPeg);
        StartCoroutine(StartAnimation());
    }

    void SolveTowers(int n, char startPeg, char endPeg, char tempPeg)
    {
        if (n > 0)
        {
            SolveTowers(n - 1, startPeg, tempPeg, endPeg);
            Debug.Log("Move disk from " + startPeg + ' ' + endPeg);
            AlgorithmList.AddTask(startPeg, endPeg);
            SolveTowers(n - 1, tempPeg, endPeg, startPeg);
        }
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < tasksToExecute.Count; i++)
        {
            yield return new WaitForSeconds(1f);
            switch (tasksToExecute[i])
            {
                case 1: // A --> B
                    secondaryPoleRings.Insert(0, mainPoleRings[0]);
                    mainPoleRings.RemoveAt(0);
                    secondaryPoleRings[0].GetComponent<Rigidbody>().isKinematic = true;
                    secondaryPoleRings[0].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);
                    Vector3 ring = secondaryPoleRings[0].GetComponent<Transform>().position;
                    for (float j = secondaryPoleRings[0].GetComponent<Transform>().position.y; j < mainEdge.GetComponent<Transform>().position.y; j+=0.1f)
                    {
                        secondaryPoleRings[0].GetComponent<Transform>().position = new Vector3(ring.x, j, ring.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    ring = secondaryPoleRings[0].GetComponent<Transform>().position;
                    for (float j = secondaryPoleRings[0].GetComponent<Transform>().position.x; j < secondaryEdge.GetComponent<Transform>().position.x; j += 0.1f)
                    {
                        secondaryPoleRings[0].GetComponent<Transform>().position = new Vector3(j, ring.y, ring.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.05f);
                    secondaryPoleRings[0].GetComponent<Rigidbody>().isKinematic = false;
                    break;

                case 2: // A --> C
                    finalPoleRings.Insert(0, mainPoleRings[0]);
                    mainPoleRings.RemoveAt(0);
                    finalPoleRings[0].GetComponent<Rigidbody>().isKinematic = true;
                    finalPoleRings[0].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);
                    Vector3 ring2 = finalPoleRings[0].GetComponent<Transform>().position;
                    for (float j = finalPoleRings[0].GetComponent<Transform>().position.y; j < mainEdge.GetComponent<Transform>().position.y; j += 0.1f)
                    {
                        finalPoleRings[0].GetComponent<Transform>().position = new Vector3(ring2.x, j, ring2.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    ring2 = finalPoleRings[0].GetComponent<Transform>().position;
                    for (float j = finalPoleRings[0].GetComponent<Transform>().position.x; j < finalEdge.GetComponent<Transform>().position.x; j += 0.1f)
                    {
                        finalPoleRings[0].GetComponent<Transform>().position = new Vector3(j, ring2.y, ring2.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.05f);
                    finalPoleRings[0].GetComponent<Rigidbody>().isKinematic = false;
                    break;

                case 3: // B --> C
                    finalPoleRings.Insert(0, secondaryPoleRings[0]);
                    secondaryPoleRings.RemoveAt(0);
                    finalPoleRings[0].GetComponent<Rigidbody>().isKinematic = true;
                    finalPoleRings[0].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);
                    Vector3 ring3 = finalPoleRings[0].GetComponent<Transform>().position;
                    for (float j = finalPoleRings[0].GetComponent<Transform>().position.y; j < secondaryEdge.GetComponent<Transform>().position.y; j += 0.1f)
                    {
                        finalPoleRings[0].GetComponent<Transform>().position = new Vector3(ring3.x, j, ring3.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    ring3 = finalPoleRings[0].GetComponent<Transform>().position;
                    for (float j = finalPoleRings[0].GetComponent<Transform>().position.x; j < finalEdge.GetComponent<Transform>().position.x; j += 0.1f)
                    {
                        finalPoleRings[0].GetComponent<Transform>().position = new Vector3(j, ring3.y, ring3.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.05f);
                    finalPoleRings[0].GetComponent<Rigidbody>().isKinematic = false;
                    break;

                case 4: // C --> A
                    mainPoleRings.Insert(0, finalPoleRings[0]);
                    finalPoleRings.RemoveAt(0);
                    mainPoleRings[0].GetComponent<Rigidbody>().isKinematic = true;
                    mainPoleRings[0].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);
                    Vector3 ring4 = mainPoleRings[0].GetComponent<Transform>().position;
                    for (float j = mainPoleRings[0].GetComponent<Transform>().position.y; j < finalEdge.GetComponent<Transform>().position.y; j += 0.1f)
                    {
                        mainPoleRings[0].GetComponent<Transform>().position = new Vector3(ring4.x, j, ring4.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    ring4 = mainPoleRings[0].GetComponent<Transform>().position;
                    for (float j = mainPoleRings[0].GetComponent<Transform>().position.x; j > mainEdge.GetComponent<Transform>().position.x; j -= 0.1f)
                    {
                        mainPoleRings[0].GetComponent<Transform>().position = new Vector3(j, ring4.y, ring4.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.05f);
                    mainPoleRings[0].GetComponent<Rigidbody>().isKinematic = false;
                    break;

                case 5: // C --> B
                    secondaryPoleRings.Insert(0, finalPoleRings[0]);
                    finalPoleRings.RemoveAt(0);
                    secondaryPoleRings[0].GetComponent<Rigidbody>().isKinematic = true;
                    secondaryPoleRings[0].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);
                    Vector3 ring5 = secondaryPoleRings[0].GetComponent<Transform>().position;
                    for (float j = secondaryPoleRings[0].GetComponent<Transform>().position.y; j < finalEdge.GetComponent<Transform>().position.y; j += 0.1f)
                    {
                        secondaryPoleRings[0].GetComponent<Transform>().position = new Vector3(ring5.x, j, ring5.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    ring5 = secondaryPoleRings[0].GetComponent<Transform>().position;
                    for (float j = secondaryPoleRings[0].GetComponent<Transform>().position.x; j > secondaryEdge.GetComponent<Transform>().position.x; j -= 0.1f)
                    {
                        secondaryPoleRings[0].GetComponent<Transform>().position = new Vector3(j, ring5.y, ring5.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.05f);
                    secondaryPoleRings[0].GetComponent<Rigidbody>().isKinematic = false;
                    break;

                case 6: // B --> A
                    mainPoleRings.Insert(0, secondaryPoleRings[0]);
                    secondaryPoleRings.RemoveAt(0);
                    mainPoleRings[0].GetComponent<Rigidbody>().isKinematic = true;
                    mainPoleRings[0].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);
                    Vector3 ring6 = mainPoleRings[0].GetComponent<Transform>().position;
                    for (float j = mainPoleRings[0].GetComponent<Transform>().position.y; j < secondaryEdge.GetComponent<Transform>().position.y; j += 0.1f)
                    {
                        mainPoleRings[0].GetComponent<Transform>().position = new Vector3(ring6.x, j, ring6.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    ring6 = mainPoleRings[0].GetComponent<Transform>().position;
                    for (float j = mainPoleRings[0].GetComponent<Transform>().position.x; j > mainEdge.GetComponent<Transform>().position.x; j -= 0.1f)
                    {
                        mainPoleRings[0].GetComponent<Transform>().position = new Vector3(j, ring6.y, ring6.z);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.05f);
                    mainPoleRings[0].GetComponent<Rigidbody>().isKinematic = false;
                    break;
            }
        }
        status.text = "DONE";
        status.color = Color.green;
        resetSim.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
