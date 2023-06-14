using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = -2.0f;
        transform.position = pos;
    
    }
}
