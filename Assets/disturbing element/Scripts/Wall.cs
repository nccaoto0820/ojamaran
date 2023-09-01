using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject WallPrefab;

    [SerializeField] private Transform rangeA;
    [SerializeField] private Transform rangeB;
    [SerializeField] private Text WallTime;
    [SerializeField] private static float CountDownTime;
    private bool start = false;

    private bool isWall = true;

    public bool walldes2 = false;
    [SerializeField] private GameObject Partner;

    public static Wall InstanceEX2;
    

    public bool rayhitflag=false;
    public LayerMask _layermask;
    private Vector2 rayhitpos;
    Vector2 newrayhitpos;
    public bool CanWall;
    public float time;

    [SerializeField] private Timer timer;

    //チュートリアル
    public bool Onewall=false;
    
    void Update()
    {
        
        if (isWall == false)
        {
            
            
            WallTime.text = string.Format("クールタイム:{0:0}",CountDownTime);
            CountDownTime -= Time.deltaTime;
        }
        else
        {
            WallTime.text = string.Format("let go", CountDownTime);
        }

        if (Playercontroller.Instance.start == true)
        {
            
            time += Time.deltaTime;
        }



    }
    

    public void Wallinstant(InputAction.CallbackContext context)
    {
        if (Playercontroller.Instance.start == true)
        {
            time += Time.deltaTime;
            if (time >= 11.5)
            {
                if (CanWall == true)
                {
                    //ray生成
                    Vector2 origin = new Vector2(0, 0);
                    Vector2 direction = new Vector2(0, -1);

                    Ray2D ray2D = new Ray2D(transform.position, direction);



                    Debug.DrawRay(/*Parther.*/transform.position, ray2D.direction, Color.red, 1f * Time.deltaTime);
                    RaycastHit2D hit = Physics2D.Raycast((Vector2)ray2D.origin, (Vector2)ray2D.direction, 150f, _layermask);
                    if (!hit)
                    {

                        return;
                    }
                    else
                    {
                        if (!rayhitflag)
                        {
                            rayhitpos = hit.point;
                            rayhitpos.y += 1.15f;
                            rayhitflag = true;

                            Onewall = true;
                            GameObject hits = Instantiate(WallPrefab, rayhitpos, Quaternion.identity);
                            

                            StartCoroutine("ReturnBullet");

                        }
                    }
                }
            }
        }
    }

   
    public void Awake()
    {
        if (InstanceEX2 == null)
        {
            InstanceEX2 = this;
        }
    }
    
    

    public void OnClickStartButton()
    {
        start = true;
    }

    IEnumerator ReturnBullet()
    {
        
        //壁をでなくする
        isWall = false;
        CountDownTime = 10.0F; //カウントダウン開始
        timer.StartTimer();
        //秒数待つ
        yield return new WaitForSeconds(7.5f);
        
        //壁出る
        isWall = true;
        rayhitflag = false;
    }
}
