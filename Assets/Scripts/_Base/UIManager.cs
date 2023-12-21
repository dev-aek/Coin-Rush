using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    /* This script provides or mediates all UI operations of the game. */

    [Header("Menu")]
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject nextButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI levelText;


    [Header("Events")]
    [SerializeField] private UnityEvent onWinUI;
    [SerializeField] private UnityEvent onLoseUI;
    [SerializeField] private UnityEvent forceToCloseOnNewLevel;

    private float deltaTime;
    private int inputCounter = 0;
    private bool isUpdating = false;

    private void Awake()
    {
        //onGameStartUI?.Invoke();
        UpdateLevelText();

        if (GameManager.Instance.IsDebug && !menuObject.activeInHierarchy)
        {
            GameManager.Instance.currentState = GameManager.GameState.Normal;
        }
    }

    private void OnEnable()
    {
        GameManager.onWinEvent += ExecuteOnWin;
        GameManager.onLoseEvent += ExecuteOnLose;
        LevelManager.onNewLevelLoaded += ForceToClose;
    }

    private void OnDisable()
    {
        GameManager.onWinEvent -= ExecuteOnWin;
        GameManager.onLoseEvent -= ExecuteOnLose;
        LevelManager.onNewLevelLoaded -= ForceToClose;
    }
    private void Update()
    {
        UpdateLevelText();
    }

    private void ForceToClose()
    {
        inputCounter = 0;
        GameManager.Instance.GiveInputToUser = false;
        SetMenuOnNewLevelLoad();
        forceToCloseOnNewLevel?.Invoke();

    }

    private void SetMenuOnNewLevelLoad() // Allows the menu to open when a new level starts
    {

        menuObject.gameObject.SetActive(true);  

    }

    public void CloseTheMenu()
    {

        menuObject.gameObject.SetActive(false);
        GameManager.Instance.currentState = GameManager.GameState.Normal;

    }

    private void UpdateLevelText()
    {
        levelText.text = "Level" + " " + (1+PlayerPrefs.GetInt("LevelCount")).ToString();
    }
    private void ExecuteOnWin()
    {
        //GameManager.Instance.currentState = GameManager.GameState.Victory;
        onWinUI?.Invoke();
        nextButton.gameObject.active = true;

    }
    private void ExecuteOnLose()
    {
        //GameManager.Instance.currentState = GameManager.GameState.Failed;
        onLoseUI?.Invoke();
        restartButton.gameObject.active = true;
    }

    public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
        LevelManager.Instance.RestartLevel();
        //LevelManager.Instance.ui.UpdateLevelText();

    }

    public void RestartLevel()
    {
        LevelManager.Instance.RestartLevel();

    }



}
