
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] Transform _BottomSpawnerPos;
    [SerializeField] Transform _LeftSpawnerPos;
    [SerializeField] Transform _RightSpawnerPos;

    [SerializeField] List<Enemy> _Obstacles;

    // Start is called before the first frame update
    void Start()
    {
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(_BottomSpawnerPos);
        }
    }

    private Enemy Spawn(Transform pos)
    {
        int index = Random.Range(0, _Obstacles.Count);

        Enemy enemy=Instantiate(_Obstacles[index], pos.position, Quaternion.identity);

        return enemy;
    }
}
