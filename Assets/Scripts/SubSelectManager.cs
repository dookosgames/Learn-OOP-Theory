using System;
using UnityEngine;
using TMPro;

public class SubSelectManager : MonoBehaviour
{


    [SerializeField] GameObject[] SubsToChoose;

    [Header("Sub Selected Stuff")]
    [SerializeField] GameObject _SubSelected;
    [SerializeField] TextMeshProUGUI _SubName;
    [SerializeField] TextMeshProUGUI _Health;
    [SerializeField] TextMeshProUGUI _Hull;
    
    
 
    //index num of sub selected in array
    private int currentIndex = 0;


    //Action called when Sub is chosen to be used (active sub)
    public static event Action<GameObject> a_ActiveSub;

    private void Start()
    {
        //Start game wiht default sub selected
        _SubSelected = SubsToChoose[0];
        _SubSelected.SetActive(true);
        DisplaySubInfo();
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
            DisplaySubInfo();
            _SubSelected.SetActive(true);


            

            //Broadcast active sub
            a_ActiveSub.Invoke(_SubSelected);
        }
        
    }

    private void DisplaySubInfo()
    {
        _SubName.text = _SubSelected.name;
        _Health.text = "Health: "+_SubSelected.GetComponent<Vehicles>().GetHealth.ToString();
        _Hull.text = "Hull: "+_SubSelected.GetComponent<Vehicles>().GetHull.ToString();
    }
}
