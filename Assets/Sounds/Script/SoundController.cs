
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundController : MonoBehaviour
{
    [SerializeField, Header("BGMの再生を管理する")] private AudioSource bgmManager;
    [SerializeField, Header("SEの再生を管理する")] private AudioSource seManager;

    /// <summary>
    /// BGMの再生許可　True:再生可能　False:再生不可
    /// </summary>
    private bool bgmFlag;
    /// <summary>
    /// 歩行音の再生許可　True:再生可能　False:再生不可
    /// </summary>

    private void Awake()
    {
        //開始時に使うAudioSourceがない場合は、取得する
        if (bgmManager == null)
            bgmManager = transform.Find("BgmManager").gameObject.GetComponent<AudioSource>();
        if (seManager == null)
            seManager = transform.Find("SeManager").gameObject.GetComponent<AudioSource>();

    }

    /// <summary>
    /// BGMの再生を開始する
    /// </summary>
    public void BGM_Play()
    {
        if (bgmFlag)
        {
            bgmManager.Play();
            bgmFlag = false;
        }
    }

    /// <summary>
    /// BGMの再生を一時停止する
    /// </summary>
    public void BGM_Pause()
    {
        if (!bgmFlag)
        {
            bgmManager.Pause();
            bgmFlag = true;
        }
    }

    /// <summary>
    /// BGMの再生を終了する
    /// </summary>
    public void BGM_Stop()
    {
        if(!bgmFlag)
        {
            bgmManager.Stop();
            bgmFlag = true;
        }
    }

    /// <summary>
    /// 一回だけ音を鳴らす処理
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayOneShotSound(AudioClip audioClip)
    {
        seManager.PlayOneShot(audioClip);
       
    }



    public float BgmManagerVolume { set { bgmManager.volume = value; } }
    public float SeManagerVolume { set { seManager.volume = value; } }


}
