using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;
using TMPro;
public class LeaderBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<LeaderboardEntry> lbe;
    public int amount;
    public GameObject[] entryObjects;


    void Start()
    {
         
    }
    private void Awake()
    {
        try
        {
            Core.AsyncInitialize();
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallBack);
            
        }
        catch (UnityException e)
        {
            Debug.LogError("Failed to intiliaze exception");
            Debug.LogException(e);
            UnityEngine.Application.Quit();
            
        }
    }
    void EntitlementCallBack(Message msg)
    {
        
        if (msg.IsError)
        {
           
            Debug.LogError("You are not entitled to use this app");
           // UnityEngine.Application.Quit();
        }
        else
        {
            Debug.Log("You are entitled to use this app");
            Debug.Log("?");
        }
    }
    public void SubmitScore(string leaderboardname, int score)
        {
        if(score < 0)
        {
            Debug.Log("Error, score cant be negative");
            return;
        }
        Leaderboards.WriteEntry(leaderboardname, score);
        Debug.Log("Data saved to leaderboard");
        }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetLeaderboardData(string leaderboardname)
    {
        lbe = new List<LeaderboardEntry>(); //clear the old ones
        Leaderboards.GetEntries(leaderboardname, amount, LeaderboardFilterType.None, LeaderboardStartAt.Top).OnComplete(LeaderBoardGetCallBack);
    }
    void LeaderBoardGetCallBack(Message<LeaderboardEntryList> msg)
    {
        if (!msg.IsError)
        {
            var entries = msg.Data;
            foreach(var entry in entries)
            {
                lbe.Add(entry);
                
            }
            Debug.Log("Leaderbords fetched successfully");
            UpdateUI();
        }
        else
        {
            Debug.Log("Error getting the leaderbords");
        }
    }

    void UpdateUI()
    {
        for (int i=0;i<entryObjects.Length;i++)
        {
            if (i<lbe.Count)
            {
                entryObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " " + lbe[i].Rank;
                entryObjects[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " " + lbe[i].Score;
                entryObjects[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = " " + lbe[i].User.OculusID;
            }
            else
            {
                entryObjects[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " " + (i+1);
                entryObjects[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " "  ;
                entryObjects[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = " "  ;

            }
        }
    }
}
