using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBefore : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;

    private string groundTag = "jama";
    private string enemyTag = "partner";
    private string playerTag = "Player";
    private string bacTag = "bac";
    private string jamaGroundTag = "jamaGround";
    private string needletag = "needle";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == enemyTag || collision.tag == playerTag || collision.tag == bacTag || collision.tag==groundTag||collision.tag==jamaGroundTag||collision.tag==needletag)
        {
            gameObject.transform.parent.gameObject.GetComponent<enemy>().right = !gameObject.transform.parent.gameObject.GetComponent<enemy>().right;
        }
       

    }
    

}
