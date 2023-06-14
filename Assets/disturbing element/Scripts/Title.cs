using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField, Header("���艹")] private AudioClip clip;
    [SerializeField, Header("�T�E���h�R���g���[���[")] private SoundController sound;
    public void OnClickStartButton()
    {
        sound.PlayOneShotSound(clip);  //���艹 
        SceneManager.LoadScene("StageSelect");
    }
}
