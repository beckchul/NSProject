using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    // Use this for initialization
    MeshRenderer render;
    public GameObject SelectEffect;
    private GameObject PlayerObject;
    public bool IsWall = false;
    private int State = 0;
    public GameObject[] OtherBox;
    private float fTime;
    public float fTimer = 2.0f;

    void Start () {
        render = GetComponent<MeshRenderer>();
        OtherBox = GameObject.FindGameObjectsWithTag("Box");
        PlayerObject = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        ColorTime();
    }

    void LateUpdate()
    {
        PlayerCheck();
    }

    void PlayerCheck()
    {
        if(1 == State)
            PlayerObject.GetComponent<PlayerController>().SetPlayerPullOption(transform.position, true);
        else if(2 == State)
            PlayerObject.GetComponent<PlayerController>().SetPlayerPullOption(transform.position, false);
    }

    void ColorTime()
    {
        if(0 != State)
        {
            fTime += Time.deltaTime;
            if (fTime >= fTimer)
                ResetColor();
        }
    }
    public void ResetColor()
    {
        State = 0;
        render.material.color = Color.white;
    }
    public void ChangeColor(int _Color, Vector3 vecPosition)
    {
        if(State != _Color)
        {
            foreach (GameObject Boxs in OtherBox)
            {
                Boxs.GetComponent<BoxController>().ResetColor();
            }
            fTime = 0.0f;
            switch (_Color)
            {
                case 1:
                    State = 1;
                    render.material.color = Color.red;
                    break;
                case 2:
                    State = 2;
                    render.material.color = Color.blue;
                    break;
            }
        }
        else
        {
            State = 0;
            render.material.color = Color.white;
        }
       
        Vector3 vecDir = vecPosition - transform.position;
        vecDir = vecDir.normalized;
        Instantiate(SelectEffect, transform.position + vecDir, transform.rotation);
    }
}
