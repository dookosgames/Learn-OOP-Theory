using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject[] Panels;

    [SerializeField] GameObject _StartPanel;
    [SerializeField] GameObject _PlayingPanel;
    [SerializeField] GameObject _Gameover;

    private void OnEnable()
    {
        GameManager.A_GameState += ChangeUI;
    }
    private void OnDisable()
    {
        GameManager.A_GameState -= ChangeUI;
    }

    //ensures that when game starts, Start panel is on
    private void Start()
    {
        ChangeUI(GameState.start);
    }

    private void ChangeUI(GameState state)
    {
        switch (state)
        {
            case GameState.start:
                TogglePanels(_StartPanel);
                break;
            case GameState.playing:
                TogglePanels(_PlayingPanel);
                break;
            case GameState.pause:
                break;
            case GameState.gameover:
                TogglePanels(_Gameover);
                break;
            default:
                break;
        }
    }


    //Restart Button Press
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    //Toggle Panels
    private void TogglePanels(GameObject panelToShow)
    {
        foreach(GameObject panel in Panels)
        {
            if (panel == panelToShow) { panel.SetActive(true); }
            else { panel.SetActive(false); }
        }
    }
    private void TogglePanels()
    {
        foreach(GameObject panel in Panels)
        {
           panel.SetActive(false);
        }
    }


}
