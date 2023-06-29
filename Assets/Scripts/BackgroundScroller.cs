using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [Header("Positions")]
    private GameObject _player;
    [SerializeField] Vector3 _startPos;
    [SerializeField] float _endPosY;
    [SerializeField] SpriteRenderer _bgRenderer;



    [Header("Background Colors")]
    [SerializeField] Color[] _bgColors;



    private int _bgCounter = 0;

    private void Start()
    {
        _player = gameObject;
    }

    private void Update()
    {
        //Loops background
        ResetBackground();
    }


    //Resets the background
    private void ResetBackground()
    {
        if (_player.transform.position.y <= _endPosY)
        {
            _player.transform.position = _startPos;
            ChangeBackground();
        }

        //selects background to change to once player reachs loops spot
        void ChangeBackground()
        {
            //sets new background color
            _bgRenderer.color = _bgColors[_bgCounter];

            //Moves to next index
            _bgCounter++;

            //check index to make sure its valid
            if (_bgCounter > _bgColors.Length-1)
            {
                _bgCounter = 0;
            }
        }
    }




}
