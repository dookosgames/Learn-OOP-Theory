using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    [SerializeField] GameObject _StartPanel;

    private void OnEnable()
    {
        GameManager.A_GameState += ChangeUI;
    }
    private void OnDisable()
    {
        GameManager.A_GameState -= ChangeUI;
    }

    private void ChangeUI(GameState state)
    {
        switch (state)
        {
            case GameState.start:
                _StartPanel.SetActive(true);
                break;
            case GameState.playing:
                _StartPanel.SetActive(false);
                break;
            case GameState.pause:
                break;
            case GameState.gameover:
                break;
            default:
                break;
        }
    }




    //DIVE Button
    public void DiveButtonPress()
    {

    }
}
