using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenetransition : MonoBehaviour
{
    [SerializeField] private string next;
    [SerializeField] private string next2;
    [SerializeField] private string next3;


    public void OnClickStartButton()
    {
        if (Goal.stage==false)
        {
            SceneManager.LoadScene(next);
        }
        else
        {
            SceneManager.LoadScene(next2);
        }
        if(Goal.tutorialstage==true)
        {
            SceneManager.LoadScene(next3);
        }
    }
}
