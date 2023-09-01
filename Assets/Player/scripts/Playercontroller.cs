using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{
    //-------プレイヤー-------
    [SerializeField, Header("プレイヤー")] private GameObject player;
    [SerializeField, Header("パートナー")] private GameObject partner;
    [SerializeField, Header("プレイヤースピード")] private float speed;
    [SerializeField, Header("重力")] private float gravity;
    [SerializeField, Header("ジャンプ力")] private float jumpForce; 
    public int jumpCount = 0;//ジャンプカウント
    [SerializeField] GroundCheck ground;
    private Rigidbody2D rb = null;
    public static Playercontroller Instance;
    private Animator anim = null;
    float xspeed = 0.0f;
    float yspeed = 0.0f;

    //-------アタック-------
    [SerializeField, Header("SHOTプレハブ")] private GameObject shot; 
    [SerializeField, Header("アタックポイント")] private Transform attackPoint;
    [SerializeField, Header("アタックポイント2")] private Transform attackPoint2;
    [SerializeField, Header("攻撃の間隔")] public float attackTime = 0.2f; 
    //攻撃の間隔を管理
    private float currentAttackTime;
    //攻撃可能状態かを指定するフラグ
    public bool canAttack;
    //当たった回数
    public static int HitCount = 0;　　

    [SerializeField] private Text TimeText;
    [SerializeField] private static float CountDownTime;

    private bool isGround=false;
    private bool topspeed = false;
    private bool badspped = false;   
    public bool change = false;
    public float time;
    public bool right;
    public GameObject fall;
    
    [SerializeField, Header("ワープ場所")] private GameObject[] warppos;
    private float distancewarp;  //ワープ時の距離
    [SerializeField] private float warpspeed;
    
    public bool start = false;
    private BoxCollider2D bc;

    //壁、球クールタイムUI
    [SerializeField] private Timer booltimer;
    [SerializeField] private Timer walltimer;
    private bool Starttimer=true;


    //-------アイテム-------
    private GameObject Item1;
    private GameObject Item2;
    private GameObject Item3;
   

    

    //[SerializeField] private GameObject Startbutton;
    //private string groundTag = "Ground";
    //private bool isGroundEnter, isGroundStay, isGroundExit;

    //-------音-------
    [SerializeField, Header("ジャンプ音")] private AudioClip clip;
    [SerializeField, Header("ぶつかる音")] private AudioClip clip2;
    [SerializeField, Header("発射音")] private AudioClip clip3;
    [SerializeField, Header("ワープ音")] private AudioClip clip4;
    [SerializeField, Header("スピードアップ音")] private AudioClip clip5;
    [SerializeField, Header("サウンドコントローラー")] private SoundController sound;
    protected AudioSource _source;

    //-------チュートリアル-------
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
            
            //接地判定
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
        if (topspeed == true)//トップスピード
        {
            rb.velocity = new Vector2(xspeed + 4, rb.velocity.y);

            anim.SetBool("hit", false);
            StartCoroutine("Tspeed");


        }
        else if (badspped == true)//足が遅くなる
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
            TimeText.text = string.Format("クールタイム:{0:0}", CountDownTime);
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
                    // 速度をクリアして2回目のジャンプも1回目と同じ挙動にする。
                    rb.velocity = Vector3.zero;
                    this.rb.AddForce(transform.up * jumpForce);

                    anim.SetBool("run", false);
                    anim.SetBool("doublejump", true);
                    sound.PlayOneShotSound(clip);  //ジャンプ音 
                    ONGround = false;

                    jumpCount++;
                }
                if (jumpCount == 0)
                {
                    // 速度をクリアして2回目のジャンプも1回目と同じ挙動にする。
                    rb.velocity = Vector3.zero;
                    this.rb.AddForce(transform.up * jumpForce);

                    anim.SetBool("run", false);
                    anim.SetBool("jump", true);
                    sound.PlayOneShotSound(clip);  //ジャンプ音 
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
        sound.PlayOneShotSound(clip2);  //ぶつかる音 
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
        //秒数待つ
        yield return new WaitForSeconds(4.0f);
        topspeed = false;
    }

    IEnumerator Badspeed()
    {
        //秒数待つ
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
                sound.PlayOneShotSound(clip2);  //ぶつかる音 
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
            sound.PlayOneShotSound(clip5);  //スピードアップ音 
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
            sound.PlayOneShotSound(clip4);  //ワープ音 

            

        }
        if (collision.gameObject.tag == "Item2(1)")
        {
            
            anim.SetTrigger("warpa");
            
            transform.position = warppos[1].transform.position;
            change = true;
            tutorialtext.warpText = true;
            sound.PlayOneShotSound(clip4);  //ワープ音 

            

        }
        if (collision.gameObject.tag == "Item2(2)")
        {
            
            anim.SetTrigger("warpa");
           
            transform.position = warppos[2].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //ワープ音 
           


        }
        if (collision.gameObject.tag == "Item2(3)")
        {
            anim.SetTrigger("warpa");
            
            transform.position = warppos[3].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //ワープ音 
            


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
                   
                    sound.PlayOneShotSound(clip3);  //発射音 
                    canAttack = false;
                    CountDownTime = 3.0F; //カウントダウン開始
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


                        sound.PlayOneShotSound(clip3);  //発射音 
                        canAttack = false;
                        CountDownTime = 3.0F; //カウントダウン開始
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
