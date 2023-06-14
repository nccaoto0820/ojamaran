using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerWall : MonoBehaviour
{
    
    private int numberm;
    private bool toka = true;
    public float time;
    //private bool start2 ;
    public bool walldes=false;
    [SerializeField] private GameObject wall;

    public static PartnerWall InstanceEX;
    private bool start = false;
    public LayerMask _layermask;
    private Vector2 rayhitpos;
    public bool parynerrayhitflag = false;

    void Update()
    {
        if (parther.Instances.tutoStart == true)
        {
            Vector2 origin = new Vector2(0, 0);
            Vector2 direction = new Vector2(0, -1);
            if (Playercontroller.Instance.start == true)
            {

                time += Time.deltaTime;
                if (time >= 7)
                {
                    if (toka == true)
                    {

                        numberm = Random.Range(1, 100);
                        
                        if (numberm <= 100 && toka)
                        {

                            Ray2D ray2D = new Ray2D(transform.position, direction);
                            Debug.DrawRay(transform.position, ray2D.direction, Color.red, 1f * Time.deltaTime);
                            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray2D.origin, (Vector2)ray2D.direction, 150f, _layermask);

                            if (!hit)
                            {

                                return;
                            }
                            else
                            {
                                if (!parynerrayhitflag)
                                {
                                    rayhitpos = hit.point;
                                    rayhitpos.y += 1.15f;
                                    parynerrayhitflag = true;





                                    GameObject hits = Instantiate(wall, rayhitpos, Quaternion.identity);

                                    time = 0;

                                    StartCoroutine("ReturnBullet4");

                                }
                            }


                        }
                        else
                        {
                            time = 0;
                            StartCoroutine("ReturnBullet4");
                        }
                    }
                }

            }
        }
    }

    public void Awake()
    {
        if (InstanceEX == null)
        {
            InstanceEX = this;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    
    IEnumerator ReturnBullet4()
    {
        //•Ç‚ğ‚Å‚È‚­‚·‚é
        toka = false;
        //•b”‘Ò‚Â
        yield return new WaitForSeconds(5.0f);
        //•Ço‚é
        toka = true;
        parynerrayhitflag = false;
    }
    public void OnClickStartButton()
    {
        start = true;
    }
}
