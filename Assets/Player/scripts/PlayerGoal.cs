using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerGoal : MonoBehaviour
{
    [SerializeField, Header("パートナー")] private GameObject Partner;
    

    private Rigidbody2D rb;
     private  Vector3 jumpForce;
    [SerializeField] private float x;
    [SerializeField] private float y;
    private bool jump;
    private bool Lose;
    private bool Loses;
    private Animator animator = null;
    [SerializeField, Header("ジャンプ音")] private AudioClip clip;
    [SerializeField, Header("ゴール")] private AudioClip clip2;
    [SerializeField, Header("ゲームオーバー")] private AudioClip clip3;
    [SerializeField, Header("サウンドコントローラー")] private SoundController sound;
    public static bool Playergoal;
    public static PlayerGoal playergoal;
    [SerializeField] private GameObject goalUI;
    [SerializeField] private GameObject gameovUI;
    [SerializeField] private GameObject botton;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject Quit;
    [SerializeField] private GameObject Ritry;
    public bool landing;
    [SerializeField] private Text HitCountText;
    [SerializeField] private Text HitCountText2;
    [SerializeField] private GameObject PlayerHitCountText;
    [SerializeField] private GameObject PartnerHitCountText;
    [SerializeField] private GameObject LoseBackGround;
    [SerializeField] private GameObject target;
    Quaternion Target;


    [SerializeField]private  GameObject PS;
      EventSystem ES;

    [SerializeField] Image[] _image;
    [SerializeField] Sprite[] _sprite;
    
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ES= GetComponent<EventSystem>();    
        
        
        
        if(Playergoal)
        {
            sound.PlayOneShotSound(clip2);  //ゴール
            StartCoroutine("Jump");
        }
        else
        {
            //ES.firstSelectedGameObject = botton;
            Partner.transform.position=new Vector3(1,2,0);
            transform.position = new Vector3(-4.2f,0,0);
            PS.SetActive(false);
            LoseBackGround.SetActive(true);
            _image[0].sprite = _sprite[0];
            _image[1].sprite = _sprite[1];
            StartCoroutine("Jump");
            sound.PlayOneShotSound(clip3);  //ゲームオ－バー
            Target=Quaternion.AngleAxis(90,new Vector3(0,0,-1));
        }
        HitCountText.text = "";
        HitCountText2.text = "";
    }

   
    void Update()
    {
        float hit =Playercontroller.HitCount;
        HitCountText.text = string.Format(hit.ToString("n0"));
        float Partnerhit = partner.PartnerHitCount;
        HitCountText2.text = string.Format(Partnerhit.ToString("n0"));


        if (jump)
        {
            animator.SetTrigger("Jump");
            sound.PlayOneShotSound(clip);  //ジャンプ音 
            jumpForce = new Vector3(x, y, 0);
            rb.AddForce(jumpForce);
            jump = false;
            animator.SetTrigger("Idol");
            
           
        }
        if(!Playergoal && Lose == true)
        {


            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Target, 2);
           
            StartCoroutine("Idol");
            if (Loses)
            {
                gameovUI.GetComponent<Text>();
                gameovUI.SetActive(true);
                Quit.SetActive(true);
                Ritry.SetActive(true);
                PlayerHitCountText.SetActive(true);
                PartnerHitCountText.SetActive(true);
            }
        }
        
        if (landing&&Playergoal)
        {
            
            Debug.Log("aa");
            
            
            goalUI.GetComponent<Text>();
            goalUI.SetActive(true);
            Quit.SetActive(true);
            Ritry.SetActive(true);
            PlayerHitCountText.SetActive(true);
            PartnerHitCountText.SetActive(true);

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Ground")
       {
            StartCoroutine("Idol");
           
            
        }
    }


    IEnumerator Jump()
    {
       
        //秒数待つ
        yield return new WaitForSeconds(3.0f);
        if (Playergoal)
        {
            jump = true;
        }
        else
        {
            Lose=true;
        }
    }
    IEnumerator Idol()
    {
        yield return new WaitForSeconds(2);
        landing = true;
        Loses = true;
    }
}
