using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoal : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float x;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        //rb.AddForce(jumpForce,x,0);
    }
}
