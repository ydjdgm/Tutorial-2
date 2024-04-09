using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    
    public int gameStatue = 2; //0은 클리어 실패, 1은 클리어, 2는 플레이 중
    public GameObject respawn;
    public GameObject startWall;
    public float moveSpeed;
    public float jumpPower;
    public int gotItem;
    public int maxItem;

    bool isJump = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        //if(isJump != false) { }
        rb.AddForce(new Vector3(hAxis * moveSpeed * Time.deltaTime, 0, vAxis * moveSpeed * Time.deltaTime), ForceMode.Impulse);
        //transform.position = new Vector3(hAxis * moveSpeed * Time.deltaTime, 0, vAxis * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            isJump = true;
        }
        if(transform.position.y < -15)
        {
            gameStatue = 0;
        }
        if(gameStatue == 1)
        {

        }else if(gameStatue == 0)
        {
            transform.position = respawn.transform.position;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            Invoke("SetGameStatus2", 0.5f);
        }else if(gameStatue == 2)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor")
        {
            isJump = false;
        }
        if (other.tag == "Finish" && gotItem >= maxItem)
        {
            gameStatue = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Floor")
        {
            isJump = true;
        }
    }
    void SetGameStatus2()
    {
        gameStatue = 2;
    }
}
