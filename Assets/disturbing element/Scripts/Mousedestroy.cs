using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousedestroy : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
