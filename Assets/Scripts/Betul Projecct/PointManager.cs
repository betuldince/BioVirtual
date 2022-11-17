using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class PointManager : MonoBehaviour
{
 
    public TextMeshProUGUI point_blue;


    public int frPoint;
    static int counter;
    AudioSource check;


    void Start()
    {
        check = GameObject.FindGameObjectWithTag("checksound").GetComponent<AudioSource>();
               

    }

    void CreateCanvas()
    {
        GameObject myGO;
        GameObject myText;
        Canvas myCanvas;
        Text text;
        RectTransform rectTransform;

        // Canvas
        myGO = new GameObject();
        myGO.name = "TestCanvas";
        myGO.AddComponent<Canvas>();

        myCanvas = myGO.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
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
        rectTransform.sizeDelta = new Vector2(400, 200);
    }

    // Update is called once per frame
    void Update()
    {
        // 1 basamak 20 iki basamak 30

        if (gameObject.tag == "gr_orange")
        {
            frPoint = 20;
        }else if (gameObject.tag == "tr_orange")
        {
            frPoint = 30;
        }
        else if (gameObject.tag == "tr_orange_1")
        {
            frPoint = 50;
        }
        else if (gameObject.tag == "tr_orange_2")
        {
            frPoint = 80;
        }
        else if (gameObject.tag == "gr_banana")
        {
            frPoint = 25;
        }
        else if (gameObject.tag == "tr_banana")
        {
            frPoint = 50;
        }
        else if (gameObject.tag == "tr_banana_2")
        {
            frPoint = 80;
        }

        else if (gameObject.tag == "gr_coconut")
        {
            frPoint = 25;
        }
        else if (gameObject.tag == "tr_coconut")
        {
            frPoint = 85;
        }
        else if (gameObject.tag == "gr_apple")
        {
            frPoint = 15;
        }
        else if (gameObject.tag == "tr_apple")
        {
            frPoint = 25;
        }
        else if (gameObject.tag == "tr_apple_1")
        {
            frPoint = 40;
        }

        else if (gameObject.tag == "tr_apple_2")
        {
            frPoint = 60;
        }

        else if (gameObject.tag == "tr_apple_3")
        {
            frPoint = 120;
        }



        else if (gameObject.tag == "pump")
        {
            frPoint = 10;

        }
        else if (gameObject.tag == "gr_corn")
        {
            frPoint = 15;
        }
        else if (gameObject.tag == "tr_corn")
        {
            frPoint = 20;
        }
        else if (gameObject.tag == "cake")
        {
            frPoint = 150;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Basket")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            
            counter= counter+frPoint;
            point_blue.text = counter.ToString();
            
            check.Play(0);
        }

        if (collision.gameObject.layer == 9)
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
    }



}