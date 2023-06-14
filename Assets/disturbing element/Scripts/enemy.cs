using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random= UnityEngine.Random;

public class enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    private Rigidbody2D rb = null;
    private BoxCollider2D col = null;
    public bool isDead = false;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Partner;
    [SerializeField] private GameObject enemybefore;
    public bool right = false;
    private bool start = false;

    public GameObject[] itemPrefabs;
    private int number;

    public EnemyBefore checkCol;
    private Animator animanim = null;
    public bool funda=false;
    public static enemy Instance3;

    public bool tutorialenemy=false;

    //チュートリアル
    public bool tutorial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animanim=GetComponent<Animator>();
    }

    public void Awake()
    {
        if (Instance3 == null)
        {
            Instance3 = this;
        }
    }

    void Update()
    {
        if (tutorialenemy == true)
        {
            if (Playercontroller.Instance.start == true)
            {
                
                int xVector = -1;
                if (right)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-5, 5, 1);
                }
                else
                {
                    transform.localScale = new Vector3(5, 5, 1);
                }

                float xspeed = 0.0f;

                float yspeed = -gravity;
                xspeed = speed;
                rb.velocity = new Vector2(xVector * xspeed, rb.velocity.y);
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="shot")
        {
            if (!isDead)
            {
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;



                if (tutorial == false)
                {
                    Vector3 pos = Player.transform.position;


                    //ランダム数字選択
                    number = Random.Range(1, 100);

                    print(number);

                    if (number <= 40)
                    {
                        Instantiate(itemPrefabs[0], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                    }
                    else if (number <= 70)
                    {
                        Instantiate(itemPrefabs[1], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(itemPrefabs[2], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                    }
                }
            }
        }
        if (collision.gameObject.tag == "partnershot")
        {
            if (!isDead)
            {
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
                
                
                Vector3 pos = Partner.transform.position;
                

                //ランダム数字選択
                number = Random.Range(1, 100);

                print(number);

                if (number <= 40)
                {
                    Instantiate(itemPrefabs[0], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
                else if (number <= 70)
                {
                    Instantiate(itemPrefabs[1], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
                else
                {
                    Instantiate(itemPrefabs[2], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
            }
        }
        if (collision.gameObject.tag == "fumu")
        {
            if (!isDead)
            {
                funda = true;
                Destroy(enemybefore);
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
                
                Vector3 pos = Player.transform.position;
                
                //ランダム数字選択
                number = Random.Range(1, 100);

                print(number);

                if (number <=40)
                {
                    Instantiate(itemPrefabs[0], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
                else if (number <= 70)
                {
                    Instantiate(itemPrefabs[1], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
                else
                {
                    Instantiate(itemPrefabs[2], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
                StartCoroutine("ite");
            }
        }
        if (collision.gameObject.tag == "fumu2")
        {
            if (!isDead)
            {
                funda = true;
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
                
                Vector3 pos = Partner.transform.position;
                
                //ランダム数字選択
                number = Random.Range(1, 100);

                print(number);

                if (number <= 40)
                {
                    Instantiate(itemPrefabs[0], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
                else if (number <= 70)
                {
                    Instantiate(itemPrefabs[1], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
                else
                {
                    Instantiate(itemPrefabs[2], new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
                }
                StartCoroutine("ite");
            }
        }
    }

    IEnumerator ite()
    {
        //秒数待つ
        yield return new WaitForSeconds(3.0f);
        funda = false;
    }
    public void OnClickStartButton()
    {
        start = true;
    }
}
