using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private string SelectScene;
    
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(SelectScene);
        
    }
}
