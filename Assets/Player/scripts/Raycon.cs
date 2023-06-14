using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycon : MonoBehaviour
{
    [SerializeField] private GameObject Parther;
     private Rigidbody2D rb = null;
    private Collider2D col = null;
    //�W�����v�J�E���g
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
        //�p��������
        rb = Parther.GetComponent<Rigidbody2D>();
        col = Parther.GetComponent<Collider2D>();
        aniani = GetComponent<Animator>();
    }

    
    void Update()
    {
      
        
        Vector2 origin = new Vector2(0, 0);  //���_

        Vector2 direction = new Vector2(1, 0);  //x��������\���x�N�g��
        Ray2D ray2D = new Ray2D(transform.position, direction);
        Debug.DrawRay(transform.position, ray2D.direction , Color.red, 0.5f);

        

        
                                                                                            //Ray����
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
                        // ���x���N���A����2��ڂ̃W�����v��1��ڂƓ��������ɂ���B
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
                        // ���x���N���A����2��ڂ̃W�����v��1��ڂƓ��������ɂ���B
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
                    // ���x���N���A����2��ڂ̃W�����v��1��ڂƓ��������ɂ���B
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
                    // ���x���N���A����2��ڂ̃W�����v��1��ڂƓ��������ɂ���B
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
        //�b���҂�
        yield return new WaitForSeconds(0.1f);
        
        isJump = true;
    }
    IEnumerator ReturnBullet2()
    {
        toka = false;
        //�b���҂�
        yield return new WaitForSeconds(1f);
        
        toka = true;
    }
    IEnumerator ReturnBullet3()
    {
        toka = false;
        //�b���҂�
        yield return new WaitForSeconds(2f);
        
        toka = true;
    }
    
}
