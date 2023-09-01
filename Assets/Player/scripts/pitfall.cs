using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitfall : MonoBehaviour
{
    public GameObject fall;
    [SerializeField] private GameObject Player;

    private Rigidbody2D rb = null;
    private Animator anim = null;

    public static Playercontroller instance;

    [SerializeField] private GameObject Partner;

    private Rigidbody2D rb2 = null;
    private Animator anim2 = null;

    public static partner instance2;

    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        anim = Player.GetComponent<Animator>();
        rb2 = Partner.GetComponent<Rigidbody2D>();
        anim2 = Partner.GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            
           Player. transform.position = fall.transform.position;
           
        }
        if (collision.gameObject.tag == "partner")
        {

            Partner.transform.position = fall.transform.position;
            
        }
    }
}
