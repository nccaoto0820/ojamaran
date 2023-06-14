using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCon : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float speed;

    

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
    }

    
}
