using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] CameraFollow _Cam;
    [SerializeField] BackgroundScroller _Bg;
    [SerializeField] TextMeshProUGUI _DepthDisplay;

    //ENCAPSULATION
    public float currentDepth { get; private set; }
    
   

    // Update is called once per frame
    void Update()
    {
        currentDepth= (_Cam.transform.position.y + (_Bg._bgCounter * (_Bg.yLength - _Bg._startPos.y))) * _Bg._feetPerUnit; //gets the current depth of the sub by checking for the bg tile currently displayed and the Y value

        if (currentDepth>= 0) { _DepthDisplay.text = "Depth: " + currentDepth.ToString("00"); }
        
    }
}
