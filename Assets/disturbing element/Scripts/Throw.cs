using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throw : MonoBehaviour
{
    
    [SerializeField] private float speed = 1;

    
    [SerializeField] private float time;
    private bool shotcan = true;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

        if (shotcan == true)
        {
            
            if (Playercontroller.Instance.right == true)
            {
                StartCoroutine("ShotThrow");
                Move();
                
            }
            if (Playercontroller.Instance.right == false)
            {
                StartCoroutine("ShotThrow");
                Shot();
                
            }

        }
    }

    public void Move()
    {
        
        
        rb.AddForce(transform.right * speed);
        shotcan = false;
        
    }

    public void Shot()
    {

        rb.AddForce(-transform.right * speed);
        shotcan = false;
    }

   private void OnTriggerEnter2D(Collider2D collision2D)
    {
        
        
        if (collision2D.gameObject.tag == "partner")
        {
            
            Destroy(gameObject);
        }
        if (collision2D.gameObject.tag == "Player")
        {
            
            Destroy(gameObject);
        }
        if (collision2D.gameObject.tag == "Ground")
        {
            
            Destroy(gameObject);
        }
        if (collision2D.gameObject.tag == "Groundup")
        {
            
            Destroy(gameObject);
        }
        if (collision2D.gameObject.tag == "Groundown")
        {
            
            Destroy(gameObject);
        }
    }

    IEnumerator ShotThrow()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }


}
