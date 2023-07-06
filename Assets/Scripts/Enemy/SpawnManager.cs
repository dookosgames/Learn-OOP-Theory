
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawner Positions")]
    [SerializeField] Transform _BottomSpawnerPos;
    

    [Header("Obstacles to Spawn")]
    [SerializeField] List<Enemy> _Obstacles;

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
    }


   

    IEnumerator SpawnNow()
    {

        while (currentState==GameState.playing)
        {
            yield return new WaitForSeconds(_repeatWaitTime);
            Spawn(_BottomSpawnerPos);
        }

    }

    //ABSTRACTION
    private Enemy Spawn(Transform pos)
    {
        //choose random obstacle to spawn
        int index = Random.Range(0, _Obstacles.Count);

        //choose random X to spawn it
        float x = Random.Range(camFollow.cameraWidthL,camFollow.cameraWidthR);

        Vector2 newPos = new Vector2(x, pos.position.y);

        Enemy enemy=Instantiate(_Obstacles[index], newPos, Quaternion.identity);

        return enemy;
    }





}
