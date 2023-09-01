using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject goalUI;
    [SerializeField] private GameObject gameovUI;
    [SerializeField] private GameObject botton;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Partner;
    [SerializeField] private GameObject PartnerWall;
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject enemy;
    [SerializeField, Header("サウンドコントローラー")] private SoundController sound;
    [SerializeField, Header("クリア音")] private AudioClip clip;
    [SerializeField, Header("ゲームオーバー音")] private AudioClip clip2;
    private EventSystem ES;
    [SerializeField] private GameObject Text;
    [SerializeField] private string GoalScenes;
    [SerializeField]public PlayerGoal pg;


    //チュートリアル
    [SerializeField] public GameObject tuto;
    public TutorialText tutorialtext;

    [SerializeField] public bool Stage2;
    [SerializeField]public static bool stage;

    [SerializeField] public bool Stagetutorial;
    [SerializeField] public static bool tutorialstage;
    private void Update()
    {
        if(Stage2 == true)
        {
            stage = true;
        }
        else
        {
            stage = false;
        }
        if(Stagetutorial==true)
        {
            tutorialstage = true;
        }
        else
        {
            tutorialstage = false; 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerGoal.Playergoal= true;
            SceneManager.LoadScene(GoalScenes);
           
            goalUI.GetComponent<Text>();
            
            
        }
        if (collision.gameObject.tag == "partner")
        {
            PlayerGoal.Playergoal = false;
            SceneManager.LoadScene(GoalScenes);
            

        }
    }
}
