using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("A button pressed");
        }

    }
    public void LoadUnderWaterScene()
    {
        SceneManager.LoadScene("UnderwaterScene");
    }
    public void LoadGardenScene()
    {
        SceneManager.LoadScene("GardenScene");
    }
    public void LoadSwimScene()
    {
        SceneManager.LoadScene("SwimScene");
    }
}
