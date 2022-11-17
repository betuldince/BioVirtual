using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    // Start is called before the first frame update
    LeaderBoardManager lbe;
    void Start()
    {
        lbe = GetComponent<LeaderBoardManager>();
    }

    public void SetScoreInt()
        {
        lbe.SubmitScore("PointsInt", 25);
        }

    public void GetScoreInt()
    {
        lbe.GetLeaderboardData("PointsInt");
    }
}
