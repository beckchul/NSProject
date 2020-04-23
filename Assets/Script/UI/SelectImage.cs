using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectImage : MonoBehaviour {

    // Use this for initialization
    public Sprite[] StageImage;
    private Image mySprite;

    public GameObject Score1;
    public GameObject Score2;
    public GameObject Score3;
    public Sprite[] StarImage;

    PlayerData myData;
    void Start ()
    {
        mySprite = GetComponent<Image>();
        myData = GetComponent<PlayerData>();
        Score1.gameObject.SetActive(false);
        Score2.gameObject.SetActive(false);
        Score3.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeStageImage(int iNumber)
    {
        Score1.gameObject.SetActive(true);
        Score2.gameObject.SetActive(true);
        Score3.gameObject.SetActive(true);
        mySprite.sprite = StageImage[iNumber];
        int iScore = myData.GetStageScore(iNumber + 1);
        if (iScore >= 1)
            Score1.GetComponent<Image>().sprite = StarImage[0];
        else
            Score1.GetComponent<Image>().sprite = StarImage[1];

        if (iScore >= 2)
            Score2.GetComponent<Image>().sprite = StarImage[0];
        else
            Score2.GetComponent<Image>().sprite = StarImage[1];

        if (iScore >= 3)
            Score3.GetComponent<Image>().sprite = StarImage[0];
        else
            Score3.GetComponent<Image>().sprite = StarImage[1];
    }
}
