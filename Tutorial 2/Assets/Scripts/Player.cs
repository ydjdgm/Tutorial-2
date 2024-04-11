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
    void SetGameStatus0()//Set1은 OnTriggerEnter에
    {
        if(transform.position.y < -15 || rDown || gameManager.timeLimit <= 0f)
        {
            gameStatus = 0;
        }
    }
    public void Respawn()
    {
        SceneManager.LoadScene("Stage" + gameManager.stage);
    }
}
