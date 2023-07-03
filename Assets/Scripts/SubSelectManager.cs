using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSelectManager : MonoBehaviour
{


    //Action called when Sub is chosen to be used (active sub)
    public static event Action<GameObject> a_ActiveSub;

    [SerializeField] GameObject[] SubsToChoose;
    [SerializeField] GameObject _SubSelected;
 
    //index num of sub selected in array
    private int currentIndex = 0;


    private void Start()
    {
        //Start game wiht default sub selected
        _SubSelected = SubsToChoose[0];
        _SubSelected.SetActive(true);
        a_ActiveSub.Invoke(_SubSelected);
        
    }
    //Selector Buttons
    public void SubSelectionButton(int direction)
    {
        currentIndex += direction;

        if (currentIndex >= SubsToChoose.Length - 1) { currentIndex = 0; }
        else if (currentIndex < 0) { currentIndex = SubsToChoose.Length - 1; }

        if (SubsToChoose[currentIndex] != _SubSelected)
        {
            _SubSelected.SetActive(false);
            _SubSelected = SubsToChoose[currentIndex];
            _SubSelected.SetActive(true);

            //Broadcast active sub
            a_ActiveSub.Invoke(_SubSelected);
        }
        
    }
}
