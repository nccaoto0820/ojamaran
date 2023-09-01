using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{
    //-------�v���C���[-------
    [SerializeField, Header("�v���C���[")] private GameObject player;
    [SerializeField, Header("�p�[�g�i�[")] private GameObject partner;
    [SerializeField, Header("�v���C���[�X�s�[�h")] private float speed;
    [SerializeField, Header("�d��")] private float gravity;
    [SerializeField, Header("�W�����v��")] private float jumpForce; 
    public int jumpCount = 0;//�W�����v�J�E���g
    [SerializeField] GroundCheck ground;
    private Rigidbody2D rb = null;
    public static Playercontroller Instance;
    private Animator anim = null;
    float xspeed = 0.0f;
    float yspeed = 0.0f;

    //-------�A�^�b�N-------
    [SerializeField, Header("SHOT�v���n�u")] private GameObject shot; 
    [SerializeField, Header("�A�^�b�N�|�C���g")] private Transform attackPoint;
    [SerializeField, Header("�A�^�b�N�|�C���g2")] private Transform attackPoint2;
    [SerializeField, Header("�U���̊Ԋu")] public float attackTime = 0.2f; 
    //�U���̊Ԋu���Ǘ�
    private float currentAttackTime;
    //�U���\��Ԃ����w�肷��t���O
    public bool canAttack;
    //����������
    public static int HitCount = 0;�@�@

    [SerializeField] private Text TimeText;
    [SerializeField] private static float CountDownTime;

    private bool isGround=false;
    private bool topspeed = false;
    private bool badspped = false;   
    public bool change = false;
    public float time;
    public bool right;
    public GameObject fall;
    
    [SerializeField, Header("���[�v�ꏊ")] private GameObject[] warppos;
    private float distancewarp;  //���[�v���̋���
    [SerializeField] private float warpspeed;
    
    public bool start = false;
    private BoxCollider2D bc;

    //�ǁA���N�[���^�C��UI
    [SerializeField] private Timer booltimer;
    [SerializeField] private Timer walltimer;
    private bool Starttimer=true;


    //-------�A�C�e��-------
    private GameObject Item1;
    private GameObject Item2;
    private GameObject Item3;
   

    

    //[SerializeField] private GameObject Startbutton;
    //private string groundTag = "Ground";
    //private bool isGroundEnter, isGroundStay, isGroundExit;

    //-------��-------
    [SerializeField, Header("�W�����v��")] private AudioClip clip;
    [SerializeField, Header("�Ԃ��鉹")] private AudioClip clip2;
    [SerializeField, Header("���ˉ�")] private AudioClip clip3;
    [SerializeField, Header("���[�v��")] private AudioClip clip4;
    [SerializeField, Header("�X�s�[�h�A�b�v��")] private AudioClip clip5;
    [SerializeField, Header("�T�E���h�R���g���[���[")] private SoundController sound;
    protected AudioSource _source;

    //-------�`���[�g���A��-------
    public bool canjump;
    private bool ONGround=false;
    public bool cantext=true;
    public bool canShot=false;
    public bool getitem=false;
    private bool one=true;
    private bool one2 = true;
    public bool rightShot = false;
    public bool leftShot = false;
    [SerializeField] public GameObject tuto;
    public TutorialText tutorialtext;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentAttackTime = attackTime;
        _source = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider2D>();
        Time.timeScale = 1f;
    }

    
    void Update()
    {
        if (start == true)

        {
            
            //�ڒn����
            isGround = ground.IsGround();
            yspeed = -gravity;
            xspeed = speed;
            time += Time.deltaTime;
            if (time >= 3)
            {
                Speed();
                if (Starttimer)
                {
                    booltimer.StartTimer();
                    walltimer.StartTimer();
                    Starttimer=false;
                    Debug.Log("a");
                }
            }
            else
            {}
            ItemDestroy();
            
            Canon();

            if(rb.velocity.y<0)
            {
                anim.SetBool("jump", false);
                anim.SetBool("doublejump", false);
                anim.SetBool("fall", true);
            }
        }
    }
    private void Speed()
    {
        if (topspeed == true)//�g�b�v�X�s�[�h
        {
            rb.velocity = new Vector2(xspeed + 4, rb.velocity.y);

            anim.SetBool("hit", false);
            StartCoroutine("Tspeed");


        }
        else if (badspped == true)//�����x���Ȃ�
        {
            rb.velocity = new Vector2(xspeed - 3, rb.velocity.y);
            StartCoroutine("Badspeed");
        }
        else
        {
            rb.velocity = new Vector2(xspeed, rb.velocity.y);
            anim.SetBool("hit", false);

        }
    }

    private void Canon()
    {
        attackTime += Time.deltaTime;
        if (attackTime > currentAttackTime)
        {
            canAttack = true;

        }
        if (canAttack == false)
        {
            TimeText.text = string.Format("�N�[���^�C��:{0:0}", CountDownTime);
            CountDownTime -= Time.deltaTime;
        }
        else
        {
            TimeText.text = string.Format("let go", CountDownTime);
        }
    }

    private void ItemDestroy()
    {
        if (global::partner.Instances.changes == true)
        {

            Item2 = GameObject.Find("Item2(Clone)");


            global::partner.Instances.changes = false;

            Destroy(Item2);
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        start = true;
        if (canjump)
        {
            if (this.jumpCount < 2 && time >= 3)
            {
                if(jumpCount==1)
                {
                    // ���x���N���A����2��ڂ̃W�����v��1��ڂƓ��������ɂ���B
                    rb.velocity = Vector3.zero;
                    this.rb.AddForce(transform.up * jumpForce);

                    anim.SetBool("run", false);
                    anim.SetBool("doublejump", true);
                    sound.PlayOneShotSound(clip);  //�W�����v�� 
                    ONGround = false;

                    jumpCount++;
                }
                if (jumpCount == 0)
                {
                    // ���x���N���A����2��ڂ̃W�����v��1��ڂƓ��������ɂ���B
                    rb.velocity = Vector3.zero;
                    this.rb.AddForce(transform.up * jumpForce);

                    anim.SetBool("run", false);
                    anim.SetBool("jump", true);
                    sound.PlayOneShotSound(clip);  //�W�����v�� 
                    ONGround = false;

                    jumpCount++;
                }
            }
        }
        
    }

    private void HIT()
    {
        anim.SetBool("run", false);
        anim.SetBool("hit", true);
        sound.PlayOneShotSound(clip2);  //�Ԃ��鉹 
        rb.velocity = new Vector2(-5, 0);
        HitCount++;
        time = 0;
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    IEnumerator Tspeed()
    {
        //�b���҂�
        yield return new WaitForSeconds(4.0f);
        topspeed = false;
    }

    IEnumerator Badspeed()
    {
        //�b���҂�
        yield return new WaitForSeconds(5.0f);
        badspped = false;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (start == true)
        {
            if (collision.gameObject.tag == "Ground")
            {
                jumpCount = 0;
                
                anim.SetBool("run", true);
                anim.SetBool("jump", false);
                anim.SetBool("fall", false);
                ONGround = true;

            }
            if (collision.gameObject.tag == "jama")
            {
                HIT();
            }

            if (collision.gameObject.tag == "jamaup")
            {
                jumpCount = 0;
                
                anim.SetBool("run", true);
                anim.SetBool("jump", false);
                anim.SetBool("fall", false);
                HitCount++;
            }

            if(collision.gameObject.tag== "needle")
            {
                anim.SetBool("run", false);
                anim.SetBool("hit", true);
                sound.PlayOneShotSound(clip2);  //�Ԃ��鉹 
                rb.velocity = new Vector2(-7, 0);
                HitCount++;

                time = 0;
            }
            
            if (enemy.Instance3.funda == false)
            {
                if (collision.gameObject.tag == "enemy")
                {
                    HIT();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag=="jamaGround")
        {
            HIT();
        }
        
        if (enemy.Instance3.funda == false)
        {
            if (collision.gameObject.tag == "enemybefore")
            {
                anim.SetBool("run", false);
                anim.SetBool("hit", true);
                rb.velocity = new Vector2(-5, 0);
                
                time = 0;
            }
        }

        if (collision.gameObject.tag == "shot")
        {
            HIT();
        }

        if (collision.gameObject.tag == "pitfall")
        {

            HitCount++;
            anim.SetBool("run", false);
            anim.SetBool("hit", true);
            time = 0;
        }
        if(collision.gameObject.tag == "Item1")
        {
            sound.PlayOneShotSound(clip5);  //�X�s�[�h�A�b�v�� 
            topspeed = true;
            tutorialtext.speedUP = true;
           
            Item1 = GameObject.Find("Item1(Clone)");
            Destroy(Item1);
        }
        if(collision.gameObject.tag == "Item2")
        {
            Debug.Log("r");
            anim.SetTrigger("warpa");
            
            transform.position = warppos[0].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //���[�v�� 

            

        }
        if (collision.gameObject.tag == "Item2(1)")
        {
            
            anim.SetTrigger("warpa");
            
            transform.position = warppos[1].transform.position;
            change = true;
            tutorialtext.warpText = true;
            sound.PlayOneShotSound(clip4);  //���[�v�� 

            

        }
        if (collision.gameObject.tag == "Item2(2)")
        {
            
            anim.SetTrigger("warpa");
           
            transform.position = warppos[2].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //���[�v�� 
           


        }
        if (collision.gameObject.tag == "Item2(3)")
        {
            anim.SetTrigger("warpa");
            
            transform.position = warppos[3].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //���[�v�� 
            


        }
        if (collision.gameObject.tag=="Item3")
        {
            badspped = true;
            tutorialtext.speeddown = true;
            Item3 = GameObject.Find("Item3(Clone)");
            Destroy(Item3);
            
        }
        if (collision.gameObject.tag == "CanJump")
        {
            canjump = true;
           
        }
        if(collision.gameObject.tag=="notjump")
        {
            canjump = false;
            canShot = false;
        }
        if(collision.gameObject.tag=="LastText")
        {
            tutorialtext.lastText = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CanWall" &&ONGround==true&&cantext==true&&one==true)
        {
            canjump = true;
            cantext = false;
            one = false;
            tutorialtext.walltext = true;
        }
        if (collision.gameObject.tag == "GetItem"&&ONGround==true&&one2==true)
        {

            getitem = true;
            one2 = false;
            canjump = true;
            canShot = true;
        }
    }

   

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (canShot == true)
        {
            if (time >= 3)
            {
                
                right = true;
                if (canAttack)
                {
                    
                    tutorialtext.isShot = true;
                    Instantiate(shot, attackPoint.position, Quaternion.identity);
                    rightShot = true;
                   
                    sound.PlayOneShotSound(clip3);  //���ˉ� 
                    canAttack = false;
                    CountDownTime = 3.0F; //�J�E���g�_�E���J�n
                    attackTime = 0;
                    
                    booltimer.StartTimer();
                }

            }
        }
    }

    public void LeftAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (leftShot == true)
        {
            if (canShot == true)
            {
                if (time >= 3)
                {
                    right = false;
                    if (canAttack)
                    {
                        tutorialtext.isShot = true;
                        Instantiate(shot, attackPoint2.position, Quaternion.identity);


                        sound.PlayOneShotSound(clip3);  //���ˉ� 
                        canAttack = false;
                        CountDownTime = 3.0F; //�J�E���g�_�E���J�n
                        attackTime = 0;
                        booltimer.StartTimer();
                    }
                }
            }
        }
    }
    

    public void OnClickStartButton()
    {
        start = true;
        
    }

    
}
