using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    [Header("これを踏んだ時の高さ")] public float enemyjump;

    [HideInInspector] public bool playerStepOn;
}
