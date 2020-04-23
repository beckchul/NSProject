using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    // Use this for initialization
    private int SelectSceneNumber;
	void Start () {
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSelectSceneNumber(int iNumber)
    {
        SelectSceneNumber = iNumber;
    }

    public void StartStage()
    {
        SceneManager.LoadScene("Stage" + (SelectSceneNumber).ToString());
    }
}
