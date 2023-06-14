using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    [SerializeField] private string next;
    

    public void OnClickStartButton()
    {
        
        SceneManager.LoadScene(next);
    }
}
