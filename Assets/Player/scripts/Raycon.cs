using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycon : MonoBehaviour
{
    [SerializeField] private GameObject Parther;
     private Rigidbody2D rb = null;
    private Collider2D col = null;
    //ジャンプカウント
    [SerializeField] private float jumpForce;
    public int jumpCount = 0;

    private bool isGround = false;

    private bool isJump = true;

    private Animator aniani = null;

    private int numberm;

    private bool toka =true;
   public bool Jumptrue=false;

    public static Raycon RayInstance;


    void Start()
    {
        //恥ずかしい
        rb = Parther.GetComponent<Rigidbody2D>();
        col = Parther.GetComponent<Collider2D>();
        aniani = GetComponent<Animator>();
    }

    
    void Update()
    {
      
        
        Vector2 origin = new Vector2(0, 0);  //原点

        Vector2 direction = new Vector2(1, 0);  //x軸方向を表すベクトル
        Ray2D ray2D = new Ray2D(transform.position, direction);
        Debug.DrawRay(transform.position, ray2D.direction , Color.red, 0.5f);

        

        
                                                                                            //Ray長さ
        RaycastHit2D hit = Physics2D.Raycast((Vector2)ray2D.origin, (Vector2)ray2D.direction,0.5f);

        
        if (!hit)
            return;
        
        if (hit.collider.CompareTag("jama") && isJump&&hit.collider.isTrigger==false|| hit.collider.CompareTag("jamaGround") && isJump )
        {
           
            if (parther.Instances.time >= 3)
            {

                if (toka == true)
                {
                    

                    numberm = Random.Range(1, 100);
                    
                    

                    if (numberm <= 85)
                    {
                        // 速度をクリアして2回目のジャンプも1回目と同じ挙動にする。
                        rb.velocity = Vector3.zero;
                        this.rb.AddForce(transform.up * jumpForce);
                        aniani.SetBool("partner.run", false);
                        aniani.SetBool("partner.jump", true);
                        
                        jumpCount++;
                        Jumptrue = true;
                        StartCoroutine("ReturnBullet");

                    }
                    else
                    {
                        StartCoroutine("ReturnBullet2");
                    }
                }
            }
            
            
        }
        if (hit.collider.CompareTag("needle") && isJump && hit.collider.isTrigger == false)
        {

            if (parther.Instances.time >= 3)
            {

                if (toka == true)
                {
                    

                    numberm = Random.Range(1, 100);
                    
                    

                    if (numberm <= 85)
                    {
                        // 速度をクリアして2回目のジャンプも1回目と同じ挙動にする。
                        rb.velocity = Vector3.zero;
                        this.rb.AddForce(transform.up * jumpForce);
                        aniani.SetBool("partner.run", false);
                        aniani.SetBool("partner.jump", true);
                       
                        jumpCount++;
                        Jumptrue = true;
                        StartCoroutine("ReturnBullet");

                    }
                    else
                    {
                        StartCoroutine("ReturnBullet2");
                    }
                }
            }


        }
        if (hit.collider.CompareTag("jamaana") && isJump)
        {
            if (toka == true)
            {
                numberm = Random.Range(1, 100);
                

                if (numberm <= 85 && toka)
                {
                    // 速度をクリアして2回目のジャンプも1回目と同じ挙動にする。
                    rb.velocity = Vector3.zero;
                    this.rb.AddForce(transform.up * jumpForce);
                    aniani.SetBool("partner.run", false);
                    aniani.SetBool("partner.jump", true);
                    
                    jumpCount++;
                    Jumptrue = true;
                    StartCoroutine("ReturnBullet");
                }
                else
                {
                    StartCoroutine("ReturnBullet2");
                }
            }
        }
        if (hit.collider.CompareTag("enemybefore") && isJump)
        {
            
            if (toka == true)
            {
                numberm = Random.Range(1, 100);
                

                if (numberm <= 85 && toka)
                {
                    // 速度をクリアして2回目のジャンプも1回目と同じ挙動にする。
                    rb.velocity = Vector3.zero;
                    this.rb.AddForce(transform.up * jumpForce);
                    aniani.SetBool("partner.run", false);
                    aniani.SetBool("partner.jump", true);
                    
                    jumpCount++;
                    Jumptrue = true;
                    StartCoroutine("ReturnBullet");

                }
                else
                {
                    StartCoroutine("ReturnBullet3");
                }
            }
        }
    }

    public void Awake()
    {
        if (RayInstance == null)
        {
            RayInstance = this;
        }
    }
    IEnumerator ReturnBullet()
    {
        isJump = false;
        //秒数待つ
        yield return new WaitForSeconds(0.1f);
        
        isJump = true;
    }
    IEnumerator ReturnBullet2()
    {
        toka = false;
        //秒数待つ
        yield return new WaitForSeconds(1f);
        
        toka = true;
    }
    IEnumerator ReturnBullet3()
    {
        toka = false;
        //秒数待つ
        yield return new WaitForSeconds(2f);
        
        toka = true;
    }
    
}
