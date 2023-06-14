using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] GroundCheck ground;
    private string groundTag = "Ground";
    private bool isGroundEnter, isGroundStay, isGroundExit;
    private Animator anims = null;

    [SerializeField] private GameObject shot; //shotプレハブを格納
    [SerializeField] private Transform attackPoint;//アタックポイントを格納
    [SerializeField] private Transform attackPoint2;//アタックポイントを格納

    [SerializeField] private float attackTime = 0.2f; //攻撃の間隔
    private float currentAttackTime; //攻撃の間隔を管理
    public bool canAttack; //攻撃可能状態かを指定するフラグ

    [SerializeField] private Text TimeText;
    [SerializeField] private static float CountDownTime;


    //ジャンプカウント
    [SerializeField] private float jumpForce;
    public int jumpCount = 0;

    private Rigidbody2D rbs = null;
    private bool isGround = false;
    private bool topspeed = false;
    private bool badspped = false;
    public static TutorialPlayer Instancee;
    public bool change = false;

    private bool canjump = false;
    public float time;

    public bool right;

    public GameObject fall;

    [SerializeField] private GameObject partner;
    [SerializeField] private GameObject player;
    [SerializeField, Header("ワープ場所")] private GameObject[] warppos;
    private float distancewarp;  //ワープ時の距離
    [SerializeField] private float warpspeed;
    //[SerializeField] private GameObject Startbutton;
    public bool start = false;
    private GameObject Item1;
    private GameObject Item2;
    private GameObject Item3;
    [SerializeField, Header("ジャンプ音")] private AudioClip clip;
    [SerializeField, Header("ぶつかる音")] private AudioClip clip2;
    [SerializeField, Header("発射音")] private AudioClip clip3;
    [SerializeField, Header("ワープ音")] private AudioClip clip4;
    [SerializeField, Header("スピードアップ音")] private AudioClip clip5;
    [SerializeField, Header("サウンドコントローラー")] private SoundController sound;
    protected AudioSource _source;

    private BoxCollider2D bc;


    void Start()
    {
        rbs = GetComponent<Rigidbody2D>();
        anims = GetComponent<Animator>();
        currentAttackTime = attackTime;
        _source = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (start == true)
        {
            Debug.Log("走っちゃう");
            //接地判定
            isGround = ground.IsGround();

            float xspeed = 0.0f;

            float yspeed = -gravity;
            xspeed = speed;

            time += Time.deltaTime;
            if (time >= 3)
            {
                if (topspeed == true)
                {
                    rbs.velocity = new Vector2(xspeed + 4, rbs.velocity.y);

                    anims.SetBool("hit", false);
                    StartCoroutine("Tspeed");
                    //Debug.Log("トップスピードだ");

                }
                else if (badspped == true)
                {
                    rbs.velocity = new Vector2(xspeed - 3, rbs.velocity.y);
                    StartCoroutine("Badspeed");
                }
                else
                {
                    rbs.velocity = new Vector2(xspeed, rbs.velocity.y);
                    anims.SetBool("hit", false);
                    //Debug.Log("走っちゃう");
                }
            }
            else
            {

            }

            if (parther.Instances.changes == true)
            {
                //Transform tran = Instantiate(Before);
                Item2 = GameObject.Find("Item2(Clone)");

                //transform.position = Item2.transform.position;
                parther.Instances.changes = false;
                Debug.Log("tureじゃないよ");
                Destroy(Item2);
            }


            attackTime += Time.deltaTime;
            if (attackTime > currentAttackTime)
            {
                canAttack = true;
                Debug.Log("hasyayaayayayay");
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


            Attack();
            if (change)
            {
                bc.isTrigger = true;
                distancewarp = Vector2.Distance(player.transform.position, warppos[0].transform.position);
                float currentposition = (Time.deltaTime * warpspeed) / distancewarp;
                transform.position = Vector2.Lerp(player.transform.position, warppos[0].transform.position, currentposition);
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
       
        if (!context.performed) return;
        //Debug.Log("o-----------i");
        start = true;
        Debug.Log("o-----------i");
        if (canjump)
        {
            if (this.jumpCount < 2 && time >= 3)
            {
                // 速度をクリアして2回目のジャンプも1回目と同じ挙動にする。
                rbs.velocity = Vector3.zero;
                this.rbs.AddForce(transform.up * jumpForce);
                //yspeed = jumpForce;
                anims.SetBool("run", false);
                anims.SetBool("jump", true);
                sound.PlayOneShotSound(clip);  //ジャンプ音 

                Debug.Log("o--------");
                jumpCount++;

            }
            if (rbs.velocity.y < -2)
            {
                anims.SetBool("fall", true);
            }
        }
    }

    public void Awake()
    {
        if (Instancee == null)
        {
            Instancee = this;
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
                Debug.Log("ジャンプカウント回復");
                anims.SetBool("run", true);
                anims.SetBool("jump", false);
                anims.SetBool("fall", false);

            }
            if (collision.gameObject.tag == "jama")
            {
                anims.SetBool("run", false);
                anims.SetBool("hit", true);
                sound.PlayOneShotSound(clip2);  //ぶつかる音 
                rbs.velocity = new Vector2(-5, 0);

                Debug.Log("ワッ");
                time = 0;
            }
            if (collision.gameObject.tag == "jamaup")
            {
                jumpCount = 0;
                Debug.Log("ジャンプカウント回復2");
                anims.SetBool("run", true);
                anims.SetBool("jump", false);
                anims.SetBool("fall", false);
            }

            if (collision.gameObject.tag == "needle")
            {
                anims.SetBool("run", false);
                anims.SetBool("hit", true);
                sound.PlayOneShotSound(clip2);  //ぶつかる音 
                rbs.velocity = new Vector2(-7, 0);

                Debug.Log("ワッ");
                time = 0;
            }
            //if (collision.gameObject.tag == "jama")
            //{
            //    anim.SetBool("run", false);
            //    anim.SetBool("hit", true);
            //    rb.velocity = new Vector2(-5,0); 
            //    Debug.Log("ワッ");
            //    time = 0;
            //}
            if (enemy.Instance3.funda == false)
            {
                if (collision.gameObject.tag == "enemy")
                {
                    anims.SetBool("run", false);
                    anims.SetBool("hit", true);
                    sound.PlayOneShotSound(clip2);  //ぶつかる音 
                    rbs.velocity = new Vector2(-5, 0);
                    Debug.Log("やめろ");
                    time = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "jama")
        //{
        //    anim.SetBool("run", false);
        //    anim.SetBool("hit", true);
        //    rb.velocity = new Vector2(-5, 0);
        //    Debug.Log("ワッ");
        //    time = 0;
        //}

        if (enemy.Instance3.funda == false)
        {
            if (collision.gameObject.tag == "enemybefore")
            {
                anims.SetBool("run", false);
                anims.SetBool("hit", true);
                rbs.velocity = new Vector2(-5, 0);
                Debug.Log("ワッ");
                time = 0;
            }
        }

        if (collision.gameObject.tag == "shot")
        {
            anims.SetBool("run", false);
            anims.SetBool("hit", true);
            sound.PlayOneShotSound(clip2);  //ぶつかる音 
            rbs.velocity = new Vector2(-5, 0);
            Debug.Log("クッソ");
            time = 0;
        }

        if (collision.gameObject.tag == "pitfall")
        {
            //    transform.position = fall.transform.position;
            //    Debug.Log("主人公");
            anims.SetBool("run", false);
            anims.SetBool("hit", true);
            time = 0;
        }
        if (collision.gameObject.tag == "Item1")
        {
            sound.PlayOneShotSound(clip5);  //スピードアップ音 
            topspeed = true;
            Debug.Log("ゲットだぜ");
            Item1 = GameObject.Find("Item1(Clone)");
            Destroy(Item1);
        }
        if (collision.gameObject.tag == "Item2")
        {
            distancewarp = Vector2.Distance(player.transform.position, warppos[0].transform.position);
            float currentposition = (Time.deltaTime * warpspeed) / distancewarp;
            transform.position = Vector2.Lerp(player.transform.position, warppos[0].transform.position, currentposition);
            //transform.position = warppos[0].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //ワープ音 

            Debug.Log("ポケモン");

        }
        if (collision.gameObject.tag == "Item2(1)")
        {

            transform.position = warppos[1].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //ワープ音 
            //partner.transform.position = transform.position;
            Debug.Log("ポケモン");

        }
        if (collision.gameObject.tag == "Item2(2)")
        {

            transform.position = warppos[2].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //ワープ音 
            //partner.transform.position = transform.position;
            Debug.Log("ポケモン");

        }
        if (collision.gameObject.tag == "Item2(3)")
        {

            transform.position = warppos[3].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //ワープ音 
            //partner.transform.position = transform.position;
            Debug.Log("ポケモン");

        }
        if (collision.gameObject.tag == "Item3")
        {
            badspped = true;
            Item3 = GameObject.Find("Item3(Clone)");
            Destroy(Item3);
            Debug.Log("おっそ");
        }
        if(collision.gameObject.tag=="CanJump")
        {
            canjump = true;
            Debug.Log("llllplp");
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (time >= 3)
        {

            right = true;
            if (canAttack)
            {
                Instantiate(shot, attackPoint.position, Quaternion.identity);
                sound.PlayOneShotSound(clip3);  //発射音 
                canAttack = false;
                CountDownTime = 3.0F; //カウントダウン開始
                attackTime = 0;
            }

        }
    }

    public void LeftAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (time >= 3)
        {
            right = false;
            if (canAttack)
            {
                Instantiate(shot, attackPoint2.position, Quaternion.identity);
                sound.PlayOneShotSound(clip3);  //発射音 
                canAttack = false;
                CountDownTime = 3.0F; //カウントダウン開始
                attackTime = 0;
            }
        }
    }
    void Attack()
    {


    }

    public void OnClickStartButton()
    {
        start = true;
        
    }


}
