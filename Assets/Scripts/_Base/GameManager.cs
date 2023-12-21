using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* The script that covers most of the game management of the game. */

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public enum GameState //Game States
    {
        BeforeStart,
        Normal,
        Failed,
        Final,
        Victory,
        Pause
    }
    [Header("Theme Settings")]
    [SerializeField] private bool showMenuOnNewSceneLoaded = false;
    [SerializeField] private bool giveInputOnFirstClick = false;
    [SerializeField] private bool takeInputCount = false;
    [SerializeField] private bool giveInputToUser = false;
    [SerializeField] private bool isDebug = true;


    public bool GiveInputToUser { get { return giveInputToUser; } set { giveInputToUser = value; } }
    public bool IsDebug { get { return isDebug; } }
    public bool GiveInputOnFirstClick { get { return giveInputOnFirstClick; } }
    public bool TakeInputCount { get { return takeInputCount; } }
    public bool ShowMenuOnNewSceneLoaded { get { return showMenuOnNewSceneLoaded; } }



    public static Action onWinEvent;
    public static Action onLoseEvent;

    [Header("Game Settings")]
    public GameState currentState = GameState.BeforeStart;
    public const float MAX_X = 90f;
    public const float MIN_X = -90f;
    public float playerSmooth = 8f;
    public float horizontalSpeed = 100f;
    public float forwardSpeed = 10f;
    public Transform resetRotation;
    public GameObject stackableCoin;
    public float explosionForce = 20f;
    public TextMeshProUGUI bonnusText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
    }

}
