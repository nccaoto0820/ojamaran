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

    [SerializeField] private GameObject shot; //shot�v���n�u���i�[
    [SerializeField] private Transform attackPoint;//�A�^�b�N�|�C���g���i�[
    [SerializeField] private Transform attackPoint2;//�A�^�b�N�|�C���g���i�[

    [SerializeField] private float attackTime = 0.2f; //�U���̊Ԋu
    private float currentAttackTime; //�U���̊Ԋu���Ǘ�
    public bool canAttack; //�U���\��Ԃ����w�肷��t���O

    [SerializeField] private Text TimeText;
    [SerializeField] private static float CountDownTime;


    //�W�����v�J�E���g
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
    [SerializeField, Header("���[�v�ꏊ")] private GameObject[] warppos;
    private float distancewarp;  //���[�v���̋���
    [SerializeField] private float warpspeed;
    //[SerializeField] private GameObject Startbutton;
    public bool start = false;
    private GameObject Item1;
    private GameObject Item2;
    private GameObject Item3;
    [SerializeField, Header("�W�����v��")] private AudioClip clip;
    [SerializeField, Header("�Ԃ��鉹")] private AudioClip clip2;
    [SerializeField, Header("���ˉ�")] private AudioClip clip3;
    [SerializeField, Header("���[�v��")] private AudioClip clip4;
    [SerializeField, Header("�X�s�[�h�A�b�v��")] private AudioClip clip5;
    [SerializeField, Header("�T�E���h�R���g���[���[")] private SoundController sound;
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
            Debug.Log("�������Ⴄ");
            //�ڒn����
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
                    //Debug.Log("�g�b�v�X�s�[�h��");

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
                    //Debug.Log("�������Ⴄ");
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
                Debug.Log("ture����Ȃ���");
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
                TimeText.text = string.Format("�N�[���^�C��:{0:0}", CountDownTime);
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
                // ���x���N���A����2��ڂ̃W�����v��1��ڂƓ��������ɂ���B
                rbs.velocity = Vector3.zero;
                this.rbs.AddForce(transform.up * jumpForce);
                //yspeed = jumpForce;
                anims.SetBool("run", false);
                anims.SetBool("jump", true);
                sound.PlayOneShotSound(clip);  //�W�����v�� 

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
                Debug.Log("�W�����v�J�E���g��");
                anims.SetBool("run", true);
                anims.SetBool("jump", false);
                anims.SetBool("fall", false);

            }
            if (collision.gameObject.tag == "jama")
            {
                anims.SetBool("run", false);
                anims.SetBool("hit", true);
                sound.PlayOneShotSound(clip2);  //�Ԃ��鉹 
                rbs.velocity = new Vector2(-5, 0);

                Debug.Log("���b");
                time = 0;
            }
            if (collision.gameObject.tag == "jamaup")
            {
                jumpCount = 0;
                Debug.Log("�W�����v�J�E���g��2");
                anims.SetBool("run", true);
                anims.SetBool("jump", false);
                anims.SetBool("fall", false);
            }

            if (collision.gameObject.tag == "needle")
            {
                anims.SetBool("run", false);
                anims.SetBool("hit", true);
                sound.PlayOneShotSound(clip2);  //�Ԃ��鉹 
                rbs.velocity = new Vector2(-7, 0);

                Debug.Log("���b");
                time = 0;
            }
            //if (collision.gameObject.tag == "jama")
            //{
            //    anim.SetBool("run", false);
            //    anim.SetBool("hit", true);
            //    rb.velocity = new Vector2(-5,0); 
            //    Debug.Log("���b");
            //    time = 0;
            //}
            if (enemy.Instance3.funda == false)
            {
                if (collision.gameObject.tag == "enemy")
                {
                    anims.SetBool("run", false);
                    anims.SetBool("hit", true);
                    sound.PlayOneShotSound(clip2);  //�Ԃ��鉹 
                    rbs.velocity = new Vector2(-5, 0);
                    Debug.Log("��߂�");
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
        //    Debug.Log("���b");
        //    time = 0;
        //}

        if (enemy.Instance3.funda == false)
        {
            if (collision.gameObject.tag == "enemybefore")
            {
                anims.SetBool("run", false);
                anims.SetBool("hit", true);
                rbs.velocity = new Vector2(-5, 0);
                Debug.Log("���b");
                time = 0;
            }
        }

        if (collision.gameObject.tag == "shot")
        {
            anims.SetBool("run", false);
            anims.SetBool("hit", true);
            sound.PlayOneShotSound(clip2);  //�Ԃ��鉹 
            rbs.velocity = new Vector2(-5, 0);
            Debug.Log("�N�b�\");
            time = 0;
        }

        if (collision.gameObject.tag == "pitfall")
        {
            //    transform.position = fall.transform.position;
            //    Debug.Log("��l��");
            anims.SetBool("run", false);
            anims.SetBool("hit", true);
            time = 0;
        }
        if (collision.gameObject.tag == "Item1")
        {
            sound.PlayOneShotSound(clip5);  //�X�s�[�h�A�b�v�� 
            topspeed = true;
            Debug.Log("�Q�b�g����");
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
            sound.PlayOneShotSound(clip4);  //���[�v�� 

            Debug.Log("�|�P����");

        }
        if (collision.gameObject.tag == "Item2(1)")
        {

            transform.position = warppos[1].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //���[�v�� 
            //partner.transform.position = transform.position;
            Debug.Log("�|�P����");

        }
        if (collision.gameObject.tag == "Item2(2)")
        {

            transform.position = warppos[2].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //���[�v�� 
            //partner.transform.position = transform.position;
            Debug.Log("�|�P����");

        }
        if (collision.gameObject.tag == "Item2(3)")
        {

            transform.position = warppos[3].transform.position;
            change = true;
            sound.PlayOneShotSound(clip4);  //���[�v�� 
            //partner.transform.position = transform.position;
            Debug.Log("�|�P����");

        }
        if (collision.gameObject.tag == "Item3")
        {
            badspped = true;
            Item3 = GameObject.Find("Item3(Clone)");
            Destroy(Item3);
            Debug.Log("������");
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
                sound.PlayOneShotSound(clip3);  //���ˉ� 
                canAttack = false;
                CountDownTime = 3.0F; //�J�E���g�_�E���J�n
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
                sound.PlayOneShotSound(clip3);  //���ˉ� 
                canAttack = false;
                CountDownTime = 3.0F; //�J�E���g�_�E���J�n
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
