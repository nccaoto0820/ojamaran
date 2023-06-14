using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject fireoff;
    [SerializeField] private float time;
    private bool set=true;
   
    void Update()
    {
       
        time += Time.deltaTime;
        if(time>=3)
        {
            fire.SetActive(true);
            fireoff.SetActive(false);
           
            if (set == true)
            {
                StartCoroutine("UP");
            }
           
        }
        else
        {
            fire.SetActive(false);
            fireoff.SetActive(true);
        }
    }
    IEnumerator UP()
    {
        set = false;
        //ïbêîë“Ç¬
        yield return new WaitForSeconds(9.0f);
        set = true;
        time = 0;
    }
}
