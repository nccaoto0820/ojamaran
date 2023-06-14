using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    //チュートリアル
    [SerializeField] public GameObject tuto;
    public TutorialText tutorialtext;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            goalUI.GetComponent<Text>();
            goalUI.SetActive(true);
            botton.SetActive(true);
            title.SetActive(true);
            Player.SetActive(false);
            Partner.SetActive(false);
            tutorialtext.goalText = true;
            
            enemy.SetActive(false);
            sound.PlayOneShotSound(clip);  //クリア音 
            sound.BGM_Stop();   //　BGMを止める 
            EventSystem.current.SetSelectedGameObject(Text);
            
        }
        if (collision.gameObject.tag == "partner")
        {
            gameovUI.GetComponent<Text>();
            gameovUI.SetActive(true);
            botton.SetActive(true);
            title.SetActive(true);
            Player.SetActive(false);
            Partner.SetActive(false);
            
            enemy.SetActive(false);
            sound.PlayOneShotSound(clip2);  //ゲームオーバー音 
            sound.BGM_Stop();   //　BGMを止める 
            EventSystem.current.SetSelectedGameObject(Text);
            
        }
    }
}
