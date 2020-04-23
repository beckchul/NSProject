using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour {
    private GameObject PlayerObject;
    public Text CountText;
    // Use this for initialization
    void Start () {
        PlayerObject = GameObject.Find("Player");
        CountText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        int count = PlayerObject.GetComponent<PlayerController>().GetPlayerBulletCount();
        int MaxCount = PlayerObject.GetComponent<PlayerController>().BulletMaxCount;
        CountText.text = "Bullet : " + count.ToString() + " / " + MaxCount.ToString();
	}
}
