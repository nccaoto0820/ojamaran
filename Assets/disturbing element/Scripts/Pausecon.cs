using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pausecon : MonoBehaviour
{
    [SerializeField] private GameObject pause;

    

    
    void Update()
    {
        
        if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            //ステージごと変える
            SceneManager.LoadScene("Stage2");
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        pause.SetActive(!pause.activeSelf);

        if (pause.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void OnClickStartButton()
    {
        pause.SetActive(!pause.activeSelf);
        Time.timeScale = 1f;
    }
}
