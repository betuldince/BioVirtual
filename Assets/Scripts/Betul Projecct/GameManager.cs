using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
 
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Blue;
    public int Green;
    public int Yellow;
     
    public TextMeshProUGUI timeGUI;
    public TextMeshProUGUI countGUI;

    public TextMeshProUGUI blueGUI;
    public TextMeshProUGUI finalPoint;

    public GameObject countdownscreen;
    public GameObject finishedscreen;
    public float time;
    public int timeInt;
    GameInfo info;
    private bool isStarted=false;

    public GameObject[] pumpkins;
    public GameObject pumpCanvas;
    GameObject myGO;

    public GameObject[] images;

    public GameObject gameOver;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Blue = 0;
        Green = 0;
        Yellow = 0;
        time = 40;
        info = GameObject.Find("LeaderBoardManager").GetComponent<GameInfo>();
 



    }

    void CreateCanvas()
    {

        
        GameObject myText;
        Canvas myCanvas;
        Text text;
        RectTransform rectTransform;

        // Canvas
        myGO = new GameObject();
        myGO.name = "TestCanvas";
        myGO.AddComponent<Canvas>();
        
        myCanvas = myGO.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.WorldSpace;
        
        myGO.AddComponent<CanvasScaler>();
        myGO.AddComponent<GraphicRaycaster>();

        // Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "wibble";

        text = myText.AddComponent<Text>();
        text.font = (Font)Resources.Load("MyFont");
        text.text = "wobble";
        text.fontSize = 100;

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        finalPoint.text = blueGUI.text;
        if (isStarted)
        {
            if (time >= 0)
            {
                timeGUI.text = timeInt.ToString() + " s";
                time = time - Time.deltaTime;
                timeInt = (int)time;
                if (time<=6)
                {
                    countdownscreen.SetActive(true);
                    //countGUI.text = timeInt.ToString();


                    int l = 0;
                    for(int k = 5; k >= 0; k--)
                    {
                        if (timeInt == k && l!=5)
                        {
                            
                            images[l].SetActive(true);

                        }

                        l = l + 1;
                    }


                    if (time<=0)
                    {
                        countdownscreen.SetActive(false);
                        // finalPointGUI.text= _pointManager.finalCounter.ToString();
                        
                        finishedscreen.SetActive(true);
                        gameOver.SetActive(true);
                    }
                }
            }
        }

    }
    public void SetScore()
    {
        info.SetScoreInt();
    }
    public void GetScore()
    {
        info.GetScoreInt();
    }
    public void StartTime()
    {
        isStarted = true;   
    }
}
