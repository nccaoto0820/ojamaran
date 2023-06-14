
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoundController : MonoBehaviour
{
    [SerializeField, Header("BGM�̍Đ����Ǘ�����")] private AudioSource bgmManager;
    [SerializeField, Header("SE�̍Đ����Ǘ�����")] private AudioSource seManager;

    /// <summary>
    /// BGM�̍Đ����@True:�Đ��\�@False:�Đ��s��
    /// </summary>
    private bool bgmFlag;
    /// <summary>
    /// ���s���̍Đ����@True:�Đ��\�@False:�Đ��s��
    /// </summary>

    private void Awake()
    {
        //�J�n���Ɏg��AudioSource���Ȃ��ꍇ�́A�擾����
        if (bgmManager == null)
            bgmManager = transform.Find("BgmManager").gameObject.GetComponent<AudioSource>();
        if (seManager == null)
            seManager = transform.Find("SeManager").gameObject.GetComponent<AudioSource>();

    }

    /// <summary>
    /// BGM�̍Đ����J�n����
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
    /// BGM�̍Đ����ꎞ��~����
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
    /// BGM�̍Đ����I������
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
    /// ��񂾂�����炷����
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayOneShotSound(AudioClip audioClip)
    {
        seManager.PlayOneShot(audioClip);
       
    }



    public float BgmManagerVolume { set { bgmManager.volume = value; } }
    public float SeManagerVolume { set { seManager.volume = value; } }


}
