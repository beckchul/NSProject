using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float mouseSensitivity = 5f;
    public float upDownRange = 90;
    public float jumpSpeed = 6;

    private Vector3 speed;
    private float forwardSpeed;
    private float sideSpeed;

    private float rotLeftRight;
    private float rotUpDown;
    private float verticalRotation = 0f;

    private float verticalVelocity = 0f;

    private CharacterController cc;

    private Vector3 vecPullPosition;
    public float fPullPower = 30.0f;
    private bool bPullCheck = false;
    private bool bCheck = false;
    // Use this for initialization

    public int iStageNumber = 1;
    private int iStageObjectCount = 0;  // 스테이지 내 오브젝트 획득 개수
    GameObject finishline;              // FinishLine 오브젝트
    GameObject MoveObject = null;              // 움직이는 발판
    public int BulletMaxCount = 10;  // 총알 최대 발사수
    private int BulletCount;    // 현재 총알 수

    PlayerData m_PlayerData;       // PlayerData

    public AudioClip m_ShotClip;
    public AudioClip m_ItemClip;
    public AudioClip m_PotalClip;
    public AudioClip m_JumpClip;

    public int GetStageObjectCount()
    {
        return iStageObjectCount;
    }
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        finishline = GameObject.Find("FinishLine");
        finishline.SetActive(false);
        BulletCount = BulletMaxCount;
        m_PlayerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        vecPullPosition = new Vector3(0.0f, 0.0f, 0.0f);
        FPMove();
        FPRotate();
        SelectObject();
        Fall();
    }

    private void FixedUpdate()
    {
        if (vecPullPosition != new Vector3(0.0f, 0.0f, 0.0f))
        {
            Physics.gravity = new Vector3(0, 0.0f, 0);
            bCheck = true;
            if (MoveObject)
            {
                MoveObject.GetComponent<MovingGround>().SetPlayerObject(null);
                MoveObject = null;
            }
            if (bPullCheck)
            {
                Vector3 vecPos = Vector3.MoveTowards(transform.position, vecPullPosition, Time.fixedDeltaTime * fPullPower);
                cc.Move(vecPos - transform.position);

            }
            else
            {
                Vector3 vecDir = transform.position - vecPullPosition;
                vecDir = vecDir.normalized;
                Vector3 vecPosition = vecDir + transform.position;
                Vector3 vecPos = Vector3.MoveTowards(transform.position, vecPosition, Time.fixedDeltaTime * fPullPower);
                cc.Move(vecPos - transform.position);
            }
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.8f, 0);
            bCheck = false;
        }
    }

    public void SetPlayerPullOption(Vector3 vecPosition, bool _Option)
    {
        vecPullPosition = vecPosition;
        bPullCheck = _Option;
    }

    public int GetPlayerBulletCount()
    {
        return BulletCount;
    }

    private void SelectObject()
    {
        if (Input.GetMouseButtonDown(0) && BulletCount > 0)
        {
            SoundManager.instance.PlaySingle(m_ShotClip);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Coin")));

            Physics.Raycast(ray, out hit, 100.0f, layerMask);

            if (hit.collider != null && hit.transform.gameObject.tag == "Box")
            {
                --BulletCount;
                hit.transform.gameObject.GetComponent<BoxController>().ChangeColor(1, transform.position);
            }
        }
        if (Input.GetMouseButtonDown(1) && BulletCount > 0)
        {
            SoundManager.instance.PlaySingle(m_ShotClip);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = (-1) - ((1 << LayerMask.NameToLayer("Coin")));

            Physics.Raycast(ray, out hit, 100.0f, layerMask);

            if (hit.collider != null && hit.transform.gameObject.tag == "Box")
            {
                --BulletCount;
                hit.transform.gameObject.GetComponent<BoxController>().ChangeColor(2, transform.position);
            }
        }
    }


    //Player의 x축, z축 움직임을 담당
    void FPMove()
    {

        forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        //점프
        if (cc.isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpSpeed;
            if (MoveObject)
            {
                MoveObject.GetComponent<MovingGround>().SetPlayerObject(null);
                MoveObject = null;
            }
            SoundManager.instance.PlaySingle(m_JumpClip);
        }
        // 총알 장전
        if (!bCheck && cc.isGrounded)
            BulletCount = BulletMaxCount;

        if (!bCheck)
        {
            if (!cc.isGrounded)
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        }
        else
        {
            verticalVelocity = 0.0f;
            speed = new Vector3(sideSpeed, 0.0f, forwardSpeed);
        }
        speed = transform.rotation * speed;

        cc.Move(speed * Time.deltaTime);
    }

    //Player의 회전을 담당
    void FPRotate()
    {
        //좌우 회전
        rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, rotLeftRight, 0f);

        //상하 회전
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void Fall()
    {
        if (transform.position.y <= -40.0f)
            SceneManager.LoadScene(Application.loadedLevelName);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Coin")
        {
            SoundManager.instance.PlaySingle(m_ItemClip);
            if (!finishline.active)
                finishline.SetActive(true);
            Destroy(hit.gameObject);
            ++iStageObjectCount;
        }
        else if (hit.gameObject.tag == "Finish")
        {
            SoundManager.instance.PlaySingle(m_PotalClip);
            m_PlayerData.SaveData(iStageNumber, iStageObjectCount);
            SceneManager.LoadScene("Menu");
        }
        else if (hit.gameObject.tag == "ReStart")
            SceneManager.LoadScene(Application.loadedLevelName);
        else if (hit.gameObject.tag == "MoveBox" && cc.isGrounded && !bCheck)
        {
            MoveObject = hit.gameObject;
            MoveObject.GetComponent<MovingGround>().SetPlayerObject(gameObject);
        }
    }
}
