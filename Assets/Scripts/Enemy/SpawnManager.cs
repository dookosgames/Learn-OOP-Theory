
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawner Positions")]
    [SerializeField] Transform _BottomSpawnerPos;
    [SerializeField] Transform _leftSpawnMax;
    [SerializeField] Transform _leftSpawnMin;


    [Header("Obstacles to Spawn")]
    [SerializeField] List<Enemy> _HorizontalObs;
    [SerializeField] List<Enemy> _BottomObs;

    //Spawnable area defined by Camera Viewport in the CameraFollow script
    [SerializeField] CameraFollow camFollow;

    [Header("Spawn Timeing")]
    [SerializeField] float _repeatWaitTime;

    private GameState currentState; 

    private void OnEnable()
    {
        GameManager.A_GameState += CheckState;
    }
    private void OnDisable()
    {
        GameManager.A_GameState -= CheckState;
    }
    void CheckState(GameState state)
    {
        currentState = state;

        if (state == GameState.playing) { StartCoroutine(SpawnNow()); }
        else if (state == GameState.gameover) { StopCoroutine(SpawnNow()); }
    }


   

    IEnumerator SpawnNow()
    {

        while (currentState==GameState.playing)
        {
            yield return new WaitForSeconds(_repeatWaitTime);
            SpawnLeft();
            SpawnBottom();
        }

    }

    //ABSTRACTION
    private void SpawnBottom()
    {
        //choose random obstacle to spawn
        int index = Random.Range(0, _BottomObs.Count);

        //choose random X to spawn it
        float x = Random.Range(camFollow.cameraWidthL,camFollow.cameraWidthR);

        Vector2 newPos = new Vector2(x, _BottomSpawnerPos.position.y);

        Enemy enemy=Instantiate(_BottomObs[index], newPos, Quaternion.identity);

    }

    private void SpawnLeft()
    {
        //choose random obstacle to spawn
        int index = Random.Range(0, _HorizontalObs.Count);
        float yRand = Random.Range(_leftSpawnMin.position.y, _leftSpawnMax.position.y);

        Vector2 newPos = new Vector2(_leftSpawnMax.position.x, yRand);

        Enemy enemy = Instantiate(_HorizontalObs[index], newPos, Quaternion.identity);

    }





}
