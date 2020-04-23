using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    // Use this for initialization
    PlayerData myData;
    public int MaxStageCount = 10;
    
    void Awake()
    {
        myData = GameObject.Find("StageScore").GetComponent<PlayerData>();
    }
	void Start () {
        
        int iDonStageNumber = -1;
        for (int i = 1; i <= MaxStageCount; ++i)
        {
            int iScore = myData.GetStageScore(i);
            if (iScore == 0)
            {
                iDonStageNumber = i + 1;
                for (int j = iDonStageNumber; j <= MaxStageCount; ++j)
                {
                    GameObject.Find("Stage" + j.ToString()).gameObject.SetActive(false);
                }
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
