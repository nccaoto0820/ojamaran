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
    [SerializeField, Header("�T�E���h�R���g���[���[")] private SoundController sound;
    [SerializeField, Header("�N���A��")] private AudioClip clip;
    [SerializeField, Header("�Q�[���I�[�o�[��")] private AudioClip clip2;
    private EventSystem ES;
    [SerializeField] private GameObject Text;

    //�`���[�g���A��
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
            sound.PlayOneShotSound(clip);  //�N���A�� 
            sound.BGM_Stop();   //�@BGM���~�߂� 
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
            sound.PlayOneShotSound(clip2);  //�Q�[���I�[�o�[�� 
            sound.BGM_Stop();   //�@BGM���~�߂� 
            EventSystem.current.SetSelectedGameObject(Text);
            
        }
    }
}
