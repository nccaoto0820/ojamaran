using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class parther : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb = null;
    [SerializeField] private GameObject Parther;
    [SerializeField] private GameObject Player;
    private GameObject Item1;
     private GameObject Item2;
    private GameObject Item3;

    
    
    //ジャンプカウント
    [SerializeField] private float jumpForce;


    [SerializeField] private GameObject shot; //shotプレハブを格納
    [SerializeField] private Transform attackPoint;//アタックポイントを格納

    [SerializeField] private float attackTime = 5f; //攻撃の間隔
    private float currentAttackTime; //攻撃の間隔を管理
    private bool canAttack; //攻撃可能状態かを指定するフラグ
    [SerializeField, Header("ワープ場所")] private GameObject[] warppos;

    

    public static Raycon juc;

    [SerializeField] private GameObject rayObject;
    public static parther Instances;

    public Raycon raycon;

    private Animator aniani = null;

    public float time;
    private int numberm;
    private bool toka = false;
    private bool start = false;
    private bool topspeed = false;
    private bool badspeed = false;
    public bool changes = false;

    float z = 40;
    float x = -40;

    //チュートリアル
    public bool tutoStart=false;
    private bool hit;

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentAttackTime = attackTime;
        aniani = GetComponent<Animator>();
    }
    
    void Update()
    {

        if (tutoStart == true)
        {

            if (Playercontroller.Instance.start == true)
            {



                float xspeed = 0.0f;
                xspeed = speed;

                time += Time.deltaTime;
                if (time >= 3)
                {
                    if (topspeed == true)
                    {
                        rb.velocity = new Vector2(xspeed + 4, rb.velocity.y);
                        aniani.SetBool("partner.hit", false);
                        StartCoroutine("Tspeed");
                        

                    }
                    else if (badspeed == true)
                    {
                        rb.velocity = new Vector2(xspeed - 3, rb.velocity.y);
                        StartCoroutine("Badspeed");
                    }
                    else
                    {


                        rb.velocity = new Vector2(xspeed, rb.velocity.y);
                        aniani.SetBool("partner.hit", false);
                    }
                }
                else
                {

                }

                if (Playercontroller.Instance.change == true)
                {
                    
                    Item2 = GameObject.Find("Item2(Clone)");

                  
                    
                    Destroy(Item2);
                }

                if (Raycon.RayInstance.Jumptrue == true)
                {
                    
                    aniani.SetBool("partner.run", false);
                    aniani.SetBool("partner.jump", true);
                }

               

                Attack();
            }
        }
        if (hit == true)
        {
            time += Time.deltaTime;
            if (time >= 3)
            {
                aniani.SetBool("partner.hit", false);
            }

        }

    }

    public void Awake()
    {
        if (Instances == null)
        {
            Instances = this;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Playercontroller.Instance.start == true)
        {
            if (collision.gameObject.tag == "Ground")
            {
                raycon.jumpCount = 0;
                
                aniani.SetBool("run", true);
                aniani.SetBool("partner.jump", false);
                Raycon.RayInstance.Jumptrue = false;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            if (collision.gameObject.tag == "Groundup")
            {
                raycon.jumpCount = 0;
                
                aniani.SetBool("run", true);
                aniani.SetBool("partner.jump", false);
                Raycon.RayInstance.Jumptrue = false;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, z);
            }
            if (collision.gameObject.tag == "Groundown")
            {
                raycon.jumpCount = 0;
                
                aniani.SetBool("run", true);
                aniani.SetBool("partner.jump", false);
                Raycon.RayInstance.Jumptrue = false;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, x);
            }
            if (collision.gameObject.tag == "jamaup")
            {
                raycon.jumpCount = 0;
                
                aniani.SetBool("run", true);
                aniani.SetBool("partner.jump", false);
                Raycon.RayInstance.Jumptrue = false;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            if (collision.gameObject.tag == "jama")
            {
                aniani.SetBool("partner.run", false);
                aniani.SetBool("partner.hit", true);
                rb.velocity = new Vector2(-5, 0);
                
                time = 0;
            }
            if (collision.gameObject.tag == "enemy")
            {
                aniani.SetBool("partner.run", false);
                aniani.SetBool("partner.hit", true);
                rb.velocity = new Vector2(-5, 0);
               
                time = 0;
            }
            if (collision.gameObject.tag == "needle")
            {
                aniani.SetBool("partner.run", false);
                aniani.SetBool("partner.hit", true);
                rb.velocity = new Vector2(-5, 0);
                
                time = 0;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jamaGround")
        {
            aniani.SetBool("partner.run", false);
            aniani.SetBool("partner.hit", true);
            rb.velocity = new Vector2(-5, 0);
            
            time = 0;
        }
            if (collision.gameObject.tag == "shot")
        {
            aniani.SetBool("partner.run", false);
            aniani.SetBool("partner.hit", true);
            rb.velocity = new Vector2(-5, 0);
            
            hit = true;
            time = 0;
        }
        if (collision.gameObject.tag == "pitfall")
        {
            
            aniani.SetBool("partner.run", false);
            aniani.SetBool("partner.hit", true);
            time = 0;
        }
        if (collision.gameObject.tag == "enemybefore")
        {
            aniani.SetBool("partner.run", false);
            aniani.SetBool("partner.hit", true);
            rb.velocity = new Vector2(-5, 0);
            
            time = 0;
        }
        if (collision.gameObject.tag == "Item1")
        {
            topspeed = true;
           
            Item1 = GameObject.Find("Item1(Clone)");
            Destroy(Item1);
        }
       
        if (collision.gameObject.tag == "Item2")
        {

            transform.position = warppos[0].transform.position;
            changes = true;
            
            

        }
        if (collision.gameObject.tag == "Item2(1)")
        {

            transform.position = warppos[1].transform.position;
            changes = true;
            
            
           

        }
        if (collision.gameObject.tag == "Item2(2)")
        {

            transform.position = warppos[2].transform.position;
            changes = true;
            
            
            

        }
        if (collision.gameObject.tag == "Item2(3)")
        {

            transform.position = warppos[3].transform.position;
            changes = true;
            
           
        }
        if (collision.gameObject.tag=="Item3")
        {
            badspeed = true;
            Item3 = GameObject.Find("Item3(Clone)");
            Destroy(Item3);
            
        }
    }

    IEnumerator Tspeed()
    {
        //秒数待つ
        yield return new WaitForSeconds(3.0f);
        topspeed = false;
    }

    IEnumerator Badspeed()
    {
        //秒数待つ
        yield return new WaitForSeconds(5.0f);

        badspeed = false;
    }

    void Attack()
    {
        attackTime += Time.deltaTime;

        if (attackTime > currentAttackTime)
        {
            if (toka == true)
            {
                numberm = Random.Range(1, 100);
                print(numberm);
                if (numberm <= 50 && toka)
                {
                    
                    canAttack = true;
                }
                else
                {
                    attackTime = 0;
                    
                    StartCoroutine("ReturnBullet3");
                }
            }
            else
            {
                StartCoroutine("ReturnBullet3");
            }
        }

        
        {
            if (canAttack)
            {
                Instantiate(shot, attackPoint.position, Quaternion.identity);
                canAttack = false;
                attackTime = 0;
            }
        }
    }
    IEnumerator ReturnBullet3()
    {
        toka = false;
        //秒数待つ
        yield return new WaitForSeconds(5f);
        
        toka = true;
        attackTime = 0;
    }
    public void OnClickStartButton()
    {
        start = true;
    }
}
