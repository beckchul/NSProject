using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour {

    // Use this for initialization
    private GameObject PlayerObject;
    public GameObject Score1;
    public GameObject Score2;
    public GameObject Score3;
    public Sprite[] StarImage;
    void Start () {
        PlayerObject = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        int iCount = PlayerObject.GetComponent<PlayerController>().GetStageObjectCount();
        if (iCount >= 1)
            Score1.GetComponent<Image>().sprite = StarImage[0];
        else
            Score1.GetComponent<Image>().sprite = StarImage[1];

        if (iCount >= 2)
            Score2.GetComponent<Image>().sprite = StarImage[0];
        else
            Score2.GetComponent<Image>().sprite = StarImage[1];

        if (iCount >= 3)
            Score3.GetComponent<Image>().sprite = StarImage[0];
        else
            Score3.GetComponent<Image>().sprite = StarImage[1];
    }
}
