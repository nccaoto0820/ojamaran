using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Countdown : MonoBehaviour
{
    [SerializeField] private Text _Countdown;
    [SerializeField] private GameObject button;
    private bool one=true;

    [SerializeField, Header("決定音")] private AudioClip clip;
    [SerializeField, Header("サウンドコントローラー")] private SoundController sound;

    void Start()
    {
        _Countdown.text = "";
    }

    public void OnClickStartButton()
    {
       
        
            sound.PlayOneShotSound(clip);  //決定音 
            StartCoroutine(CountdownCoroutine());
            button.transform.position = new Vector2(0, 1500);
            Time.timeScale = 1f;
            
        
    }

    private void Update()
    {
        if(Playercontroller.Instance.start==true)
        {
            if (one == true)
            {
                StartCoroutine(CountdownCoroutine());
                button.transform.position = new Vector2(0, 1500);
                one = false;
            }
        }
    }

    IEnumerator CountdownCoroutine()
    {
        _Countdown.gameObject.SetActive(true);
        button.gameObject.SetActive(true);

        _Countdown.text = "3";
        yield return new WaitForSeconds(1.0f);

        _Countdown.text = "2";
        yield return new WaitForSeconds(1.0f);

        _Countdown.text = "1";
        yield return new WaitForSeconds(1.0f);

        _Countdown.text = "GO!";
        yield return new WaitForSeconds(1.0f);

        _Countdown.text = "";
        _Countdown.gameObject.SetActive(false);
        button.gameObject.SetActive(false);

    }
}
