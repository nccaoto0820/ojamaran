using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private GameObject desGO;
    [SerializeField] public GameObject Text;
    [SerializeField] private GameObject Text2;
    [SerializeField] private GameObject Text3;
    [SerializeField] private GameObject Text4;
    [SerializeField] private GameObject Text5;
    [SerializeField] private GameObject Text6;
    [SerializeField] private GameObject Text7;
    [SerializeField] private GameObject Text8;
    private bool texttrue;
    public static TutorialText TextInstance;
    private bool destroy = false;
    public bool ON;
    public bool isShot = false;
    public bool speedUP = false;
    public bool warpText = false;
    public bool speeddown = false;
    public bool lastText = false;
    public bool goalText = false;
    public bool walltext = false;

    [SerializeField] public GameObject Player;
    public Playercontroller playercontroller;

   
    void Update()
    {
        
        if (destroy)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Gamepad.current.aButton.wasPressedThisFrame)
            {
                Time.timeScale = 1;
                Text.SetActive(false);
                destroy = false;
                Destroy(desGO);
            }
        }
        if (Wall.InstanceEX2.Onewall == true && walltext == true)
        {


            Text.SetActive(false);

            Text2.SetActive(true);
            walltext = false;
            Wall.InstanceEX2.Onewall = false;
            Playercontroller.Instance.canShot = true;
            
        }
        if (Playercontroller.Instance.cantext == false)
        {

            Time.timeScale = 0;
            Text.SetActive(true);
            Wall.InstanceEX2.CanWall = true;
            Playercontroller.Instance.cantext = true;
        }
        if (isShot == true && playercontroller.rightShot == true)
        {

            Time.timeScale = 1;
            Text2.SetActive(false);
            Text3.SetActive(false);
            
            isShot = false;
            playercontroller.rightShot = false;
        }
        if (playercontroller.getitem == true)
        {

            playercontroller.canAttack = true;
            Time.timeScale = 0;
            Text3.SetActive(true);
            playercontroller.getitem = false;
        }
        if (enemy.Instance3.isDead == true)
        {
            Text4.SetActive(true);
            enemy.Instance3.isDead = false;
        }
        if (speedUP == true)
        {
            Text4.SetActive(false);
            Text5.SetActive(true);
        }
        if (warpText == true)
        {
            Text5.SetActive(false);
            Text6.SetActive(true);
        }
        if (speeddown == true)
        {
            Text6.SetActive(false);
            Text7.SetActive(true);
        }
        if (lastText == true)
        {
            Text7.SetActive(false);
            Text8.SetActive(true);
        }
        if (goalText == true)
        {
            Text8.SetActive(false);
        }


    }
    public void Awake()
    {
        if (TextInstance == null)
        {
            TextInstance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ON == true && collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            Text.SetActive(true);
            destroy = true;

        }
    }
   

}
