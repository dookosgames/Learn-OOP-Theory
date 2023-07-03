using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    start,playing,pause,gameover
}
public class GameManager : MonoBehaviour
{
    //Game State Broadcast
    public static event Action<GameState> A_GameState;
   

    public GameState currentGameState { get; private set; }


    private void Start()
    {
        currentGameState = GameState.start;
    }

    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.start:
                //Game just started. Need the Start UI panel
                currentGameState = GameState.start;
                A_GameState.Invoke(GameState.start);
                break;
            case GameState.playing:
                //Sub is diving, hide UI panel
                currentGameState = GameState.playing;
                A_GameState.Invoke(GameState.playing);
                break;
            case GameState.pause:
                currentGameState = GameState.pause;
                A_GameState.Invoke(GameState.pause);
                break;
            case GameState.gameover:
                //Sub exploded. GameOver Panel
                currentGameState = GameState.gameover;
                A_GameState.Invoke(GameState.gameover);
                break;
            default:
                break;
        }
    }




    //DIVE BUTTON
    public void DiveButtonPress()
    {
        //button is pressed and game state is changed to playing
        if (currentGameState==GameState.start) { SetGameState(GameState.playing); }
    }
}
