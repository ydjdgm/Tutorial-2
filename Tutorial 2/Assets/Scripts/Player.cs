using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    public int GameStatue { get; set;} //0은 클리어 실패, 1은 클리어, 2는 플레이 중
    
    public GameObject respawn;
    public GameObject[] items;
    public float moveSpeed;
    public float jumpPower;
    public int gotItem;
    public int maxItem;

    bool isJump = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GameStatue = 2;
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SelfRespawn();
        }
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            isJump = true;
        }
        if(transform.position.y < -15)
        {
            GameStatue = 0;
        }
        if(GameStatue == 1)
        {

        }else if(GameStatue == 0)
        {
            for (int i = 0; i < maxItem; i++)//으아아아아
            {
                items[i].SetActive(true);
            }
            transform.position = respawn.transform.position;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            Invoke("SetGameStatus2", 0.5f);
        }else if(GameStatue == 2)
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
            GameStatue = 1;
        }
        if(other.tag == "Item")
        {
            gotItem++;
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
        GameStatue = 2;
    }

    public void SelfRespawn()
    {
        GameStatue = 0;
    }
}
