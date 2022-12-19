using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshRings : MonoBehaviour
{
    public Slider ringSlider;
    public Text ringCountText;
    public GameObject mainStand;
    public GameObject secondaryStand;
    public GameObject finalStand;
    public GameObject hanoiPlatform;
    public GameObject mainPole;
    public GameObject secondaryPole;
    public GameObject finalPole;
    public GameObject mainCamera;
    public static List<GameObject> rings = new List<GameObject>();
    public GameObject defaultRing;
    public Quaternion test;
    public GameObject mainStandEdge;
    public Material OtherColor;
    public Button solveButton;
    public Text actionsText;
    public Text timeText;
    public GameObject warningText;

    private Vector3 defaultMainStandValueVector3;
    private Vector3 defaultMainStandValueVector3Position;
    private float defaultMainStandValue;
    private float defaultMainStandPosX;

    private Vector3 defaultMainPoleValueVector3;
    private Vector3 defaultMainPoleValueVector3Position;
    private float defaultMainPoleValue;
    private float defaultMainPolePosX;

    private Vector3 defaultSecondaryStandValueVector3;
    private float defaultSecondaryStandValue;

    private Vector3 defaultSecondaryPoleValueVector3;
    private float defaultSecondaryPoleValue;

    private Vector3 defaultFinalStandValueVector3;
    private Vector3 defaultFinalStandValueVector3Position;
    private float defaultFinalStandValue;
    private float defaultFinalStandPosX;

    private Vector3 defaultFinalPoleValueVector3;
    private Vector3 defaultFinalPoleValueVector3Position;
    private float defaultFinalPoleValue;
    private float defaultFinalPolePosX;

    private Vector3 defaultHanoiPlatformValueVector3;
    private float defaultHanoiPlatformValueX;
    private float defaultHanoiPlatformValueZ;

    private Vector3 defaultCameraPosition;
    private Quaternion defaultCameraRotation;
    private float defaultCameraPositionY;
    private float defaultCameraPositionZ;
    private float defaultCameraRotationX;


    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        ringSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        InitializeDefaultComponents();
        UpdatePanel(8);
        StartCoroutine(DelayRingGeneration(0.5f, 8));
    }

    void InitializeDefaultComponents()
    {
        // Main Pos X
        defaultMainStandValueVector3Position = mainStand.GetComponent<Transform>().position;
        defaultMainStandPosX = defaultMainStandValueVector3Position.x;

        defaultMainPoleValueVector3Position = mainPole.GetComponent<Transform>().position;
        defaultMainPolePosX = defaultMainPoleValueVector3Position.x;

        // Final Pos X
        defaultFinalStandValueVector3Position = finalStand.GetComponent<Transform>().position;
        defaultFinalStandPosX = defaultFinalStandValueVector3Position.x;

        defaultFinalPoleValueVector3Position = finalPole.GetComponent<Transform>().position;
        defaultFinalPolePosX = defaultFinalPoleValueVector3Position.x;

        // Main Size
        defaultMainStandValueVector3 = mainStand.GetComponent<Transform>().localScale;
        defaultMainStandValue = defaultMainStandValueVector3.z;

        defaultMainPoleValueVector3 = mainPole.GetComponent<Transform>().localScale;
        defaultMainPoleValue = defaultMainPoleValueVector3.y;
        
        // Secondary Size
        defaultSecondaryStandValueVector3 = secondaryStand.GetComponent<Transform>().localScale;
        defaultSecondaryStandValue = defaultSecondaryStandValueVector3.z;

        defaultSecondaryPoleValueVector3 = secondaryPole.GetComponent<Transform>().localScale;
        defaultSecondaryPoleValue = defaultSecondaryPoleValueVector3.y;
        
        // Final Size
        defaultFinalStandValueVector3 = finalStand.GetComponent<Transform>().localScale;
        defaultFinalStandValue = defaultFinalStandValueVector3.z;

        defaultFinalPoleValueVector3 = finalPole.GetComponent<Transform>().localScale;
        defaultFinalPoleValue = defaultFinalPoleValueVector3.y;
        
        // Hanoi Platform Size
        defaultHanoiPlatformValueVector3 = hanoiPlatform.GetComponent<Transform>().localScale;
        defaultHanoiPlatformValueX = defaultHanoiPlatformValueVector3.x;
        defaultHanoiPlatformValueZ = defaultHanoiPlatformValueVector3.z;

        // Camera 
        defaultCameraPosition = mainCamera.GetComponent<Transform>().position;
        defaultCameraRotation = mainCamera.GetComponent<Transform>().rotation;
        defaultCameraPositionY = defaultCameraPosition.y;
        defaultCameraPositionZ = defaultCameraPosition.z;
        defaultCameraRotationX = defaultCameraRotation.x;
    }

    public void ValueChangeCheck()
    {
        ClearRings();
        StopAllCoroutines();
        solveButton.interactable = false;
        ringCountText.text = "Hanoi Rings - " + ringSlider.value;
        float a = ringSlider.value;
        UpdatePanel(a);
        float standRadius = defaultMainStandValue;
        float poleHeight = defaultMainPoleValue;
        float calculateDistance = defaultFinalStandValueVector3Position.x;
        float hanoiPlatformSizeX = defaultHanoiPlatformValueX;
        float hanoiPlatformSizeZ = defaultHanoiPlatformValueZ;
        float cameraPosY = defaultCameraPositionY;
        float cameraPosZ = defaultCameraPositionZ;
        float cameraRotX = defaultCameraRotationX;
        if (a > 8)
        {
            float b = a - 8;
            for(int i = 0; i < b; i++)
            {
                standRadius += 0.3f;
                poleHeight += 1f;
                hanoiPlatformSizeX += 0.4f;
                hanoiPlatformSizeZ += 1.3f;
                calculateDistance += 0.5f;

                cameraRotX += 1f;
                cameraPosY += 1f;
                cameraPosZ -= 1f;
            }
        }
        
        // Main Scale
        mainStand.GetComponent<Transform>().localScale = new Vector3(standRadius, defaultMainStandValueVector3.y , standRadius);
        mainPole.GetComponent<Transform>().localScale = new Vector3(defaultMainPoleValueVector3.x, poleHeight, defaultMainPoleValueVector3.z);

        // Main Pos
        mainStand.GetComponent<Transform>().position = new Vector3(calculateDistance * -1 - 1, defaultMainStandValueVector3Position.y, defaultMainStandValueVector3Position.z);
        mainPole.GetComponent<Transform>().position = new Vector3(calculateDistance * -1 - 1, defaultMainPoleValueVector3Position.y, defaultMainPoleValueVector3Position.z);

        // Secondary Scale
        secondaryStand.GetComponent<Transform>().localScale = new Vector3(standRadius, defaultSecondaryStandValueVector3.y, standRadius);
        secondaryPole.GetComponent<Transform>().localScale = new Vector3(defaultSecondaryPoleValueVector3.x, poleHeight, defaultSecondaryPoleValueVector3.z);

        // Final Pos
        finalStand.GetComponent<Transform>().position = new Vector3(calculateDistance, defaultFinalStandValueVector3Position.y, defaultFinalStandValueVector3Position.z);
        finalPole.GetComponent<Transform>().position = new Vector3(calculateDistance, defaultFinalPoleValueVector3Position.y, defaultFinalPoleValueVector3Position.z);

        // Final Scale
        finalStand.GetComponent<Transform>().localScale = new Vector3(standRadius, defaultFinalStandValueVector3.y, standRadius);
        finalPole.GetComponent<Transform>().localScale = new Vector3(defaultFinalPoleValueVector3.x, poleHeight, defaultFinalPoleValueVector3.z);

        // Hanoi Platform Scale
        hanoiPlatform.GetComponent<Transform>().localScale = new Vector3(hanoiPlatformSizeX, defaultHanoiPlatformValueVector3.y, hanoiPlatformSizeZ);

        // Camera
        mainCamera.GetComponent<Transform>().position = new Vector3(defaultCameraPosition.x, cameraPosY, cameraPosZ);
        //mainCamera.GetComponent<Transform>().rotation = new Quaternion(cameraRotX, defaultCameraRotation.y, defaultCameraRotation.z, defaultCameraRotation.w);
        mainCamera.GetComponent<Transform>().eulerAngles = new Vector3(cameraRotX, defaultCameraRotation.y, defaultCameraRotation.z);

        // Start Generating Rings
        StartCoroutine(DelayRingGeneration(0.5f, (int)a));
    }

    void UpdatePanel(float a)
    {
        // Shows Warning Message 
        if (a >= 16)
        {
            warningText.GetComponent<Text>().enabled = true;
        }
        else
        {
            warningText.GetComponent<Text>().enabled = false;
        }

        // Calculate Steps
        int counter = 3;
        string actionsTextString = "Amount of actions: ";
        if (a == 1)
        {
            counter = 1;
            actionsTextString += 1;
            actionsText.text = actionsTextString;
        }
        else if (a == 2)
        {
            actionsTextString += 3;
            actionsText.text = actionsTextString;
        }
        else if (a > 2)
        {
            for (int i = 0; i < a - 2; i++)
            {
                counter = (counter * 2) + 1;
            }
            actionsTextString += counter;
            actionsText.text = actionsTextString;
        }

        // Calculate Time
        string timeTextString = "Time for execution: ~";
        long averageTimeInSecondsPerMove = 3;
        long calculateTimeInSeconds = averageTimeInSecondsPerMove * counter;
        if (calculateTimeInSeconds > 99)
        {
            long calculateTimeInMinutes = calculateTimeInSeconds / 60;
            if (calculateTimeInMinutes > 99)
            {
                long calculateTimeInHours = calculateTimeInMinutes / 60;
                if (calculateTimeInHours > 24)
                {
                    long calculateTimeInDays = calculateTimeInHours / 24;
                    if (calculateTimeInDays > 30)
                    {
                        long calculateTimeInMonths = calculateTimeInDays / 30;
                        if (calculateTimeInMonths >= 12)
                        {
                            long calculateTimeInYears = calculateTimeInMonths / 12;
                            timeTextString += calculateTimeInYears + " years";
                            timeText.text = timeTextString;
                        }
                        else
                        {
                            timeTextString += calculateTimeInMonths + " months";
                            timeText.text = timeTextString;
                        }
                    }
                    else
                    {
                        timeTextString += calculateTimeInDays + " days";
                        timeText.text = timeTextString;
                    }
                }
                else
                {
                    timeTextString += calculateTimeInHours + " hours";
                    timeText.text = timeTextString;
                }
            }
            else
            {
                timeTextString += calculateTimeInMinutes + " minutes";
                timeText.text = timeTextString;
            }
        }
        else
        {
            timeTextString += calculateTimeInSeconds + " seconds";
            timeText.text = timeTextString;
        }
    }

    void ClearRings()
    {
        for(int i = 0; i < rings.Count; i++)
        {
            Destroy(rings[i]);
        }
        rings.Clear();
    }

    IEnumerator DelayRingGeneration(float time, int amount)
    {
        float a = 0.7f;
        for (int i = 0; i < amount; i++)
        {
            a += 0.1f;
        }
        bool toggleColor = false;
        for (int i = 0; i < amount; i++)
        {
            if (toggleColor)
                toggleColor = false;
            else
                toggleColor = true;
            yield return new WaitForSeconds(time);
            a -= 0.1f;
            if (toggleColor)
            {
                GenerateRing(a, true);
            }
            else
            {
                GenerateRing(a, false);
            }
        }
        solveButton.interactable = true;
    }

    void GenerateRing(float Radius, bool otherColor)
    {
        float r = Radius * 100;
        Vector3 pos = mainStandEdge.GetComponent<Transform>().position;
        rings.Add(Instantiate(defaultRing, pos, test));
        rings[rings.Count - 1].GetComponent<Transform>().localScale = new Vector3(r, r, r);
        if (otherColor == true)
        {
            rings[rings.Count - 1].GetComponent<MeshRenderer>().material = OtherColor;
        }
    }
}
