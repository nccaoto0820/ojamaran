using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField, Header("決定音")] private AudioClip clip;
    [SerializeField, Header("サウンドコントローラー")] private SoundController sound;
    public void OnClickStartButton()
    {
        sound.PlayOneShotSound(clip);  //決定音 
        SceneManager.LoadScene("StageSelect");
    }
}
