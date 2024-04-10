using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int gameStatus = 2;//0 = 주금, 1 = 클리어, 2 = 진행 중

    public float moveSpeed;
    public float jumpPower;

    public int gotItem;

    public GameObject respawn;
    public GameObject[] items;
    public GameManager gameManager;

    Rigidbody rb;
    AudioSource audioSource;

    int maxItem;

    float hAxis;
    float vAxis;

    bool jumpDown;
    bool rDown;

    bool isJump;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        maxItem = gameManager.maxItem;
        gameStatus = 2;
    }
    private void FixedUpdate()
    {
        GetInputForFixedUpdate();
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            isJump = false;
        }else if (other.tag == "Finish" && gotItem >= maxItem)
        {
            gameStatus = 1;
            SceneManager.LoadScene("Stage"+(gameManager.stage+1));
        }else if(other.tag == "Item")
        {
            gotItem++;
            gameManager.GotItemTextUpdate(gotItem);
            audioSource.Play();
        }
    }
    private void Update()
    {
        GetInput();
        Jump();
        SetGameStatus0();
        if(gameStatus == 0)
        {
            Respawn();
        }
        Win();//미완성 함수
    }
    void GetInputForFixedUpdate()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis ("Vertical");
    }
    void GetInput()
    {
        rDown = Input.GetKeyDown(KeyCode.R);
        jumpDown = Input.GetButtonDown("Jump");
    }
    void Move()
    {
        rb.AddForce(new Vector3(hAxis * moveSpeed, 0, vAxis * moveSpeed), ForceMode.Impulse);
    }
    void Jump()
    {
        if (jumpDown && !isJump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }
    }
    void SetGameStatus0()//Set1은 OnTriggerEnter에 Set2는 
    {
        if(transform.position.y < -15 || rDown)
        {
            gameStatus = 0;
        }
    }
    void SetGameStatus2()
    {
        gameStatus = 2;
        rb.constraints = RigidbodyConstraints.None;
        gotItem = 0;
        for (int i = 0; i < maxItem; i++)
        {
            items[i].gameObject.SetActive(true);
        }
    }
    void Win()
    {
        //클리어 시 로직
    }
    public void Respawn()
    {
        transform.position = respawn.transform.position;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Invoke("SetGameStatus2", 0.5f);
    }
}
