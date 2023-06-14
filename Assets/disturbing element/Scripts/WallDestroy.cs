using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    [SerializeField] private float deleteTime;
    [SerializeField] private float deleteTime2;
    

    public GameObject ob;
    public BoxCollider2D boxcol;
    public SpriteRenderer ren;

    public bool destroyflag;

    void Start()
    {
       
        boxcol = GetComponent<BoxCollider2D>();
        ren = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
                
        if(Wall.InstanceEX2.rayhitflag==true)
        {
            deleteTime -= Time.deltaTime;
            if (deleteTime <= 0)
            {
                Destroy(ob);
                deleteTime = 3;
            }
        }
        if(PartnerWall.InstanceEX.parynerrayhitflag==true)
        {
            deleteTime2 -= Time.deltaTime;
            if (deleteTime2 <= 0)
            {
                Destroy(ob);
                deleteTime2 = 3;
            }
        }

        
    }
}
