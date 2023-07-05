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
        _Obstacles = new List<Enemy>();
    }

    
}
