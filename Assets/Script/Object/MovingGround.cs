using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{

    // Use this for initialization
    public float DirX = 0.1f;
    public float DirY = 0.1f;
    public float DirZ = 0.1f;
    CharacterController TargetObjectController;
    float TimeCount = 0.0f;
    public float MoveTime = 5.0f;
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {
        TimeCount+= Time.deltaTime;
        if (TimeCount >= MoveTime)
        {
            TimeCount = 0.0f;
            DirX *= -1f;
            DirY *= -1f;
            DirZ *= -1f;
        }
        Vector3 vecMove = new Vector3(DirX, DirY, DirZ);
        transform.Translate(vecMove * Time.deltaTime);

        if (TargetObjectController)
        {
            TargetObjectController.Move(vecMove * Time.deltaTime);
        }
    }

    public void SetPlayerObject(GameObject _Object)
    {
        if (_Object)
            TargetObjectController = _Object.GetComponent<CharacterController>();
        else
            TargetObjectController = null;
    }

}
