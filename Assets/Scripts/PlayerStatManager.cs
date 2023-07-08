
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{

    [SerializeField] SubSelectManager _SubSelection;

    [SerializeField] Slider _PlayerHealth;
    [SerializeField] Slider _PlayerHull;

    private bool startTracking;


    private float subHealthTotal;
    private float subHullTotal;
    private Vehicles sub;

    private void OnEnable()
    {
        GameManager.A_GameState += TrackSub;
    }
    private void OnDisable()
    {
        GameManager.A_GameState -= TrackSub;
    }


    //turns tracking on and off depending on game state
    private void TrackSub(GameState state)
    {
        if (state == GameState.playing)
        {
            startTracking = true;
            sub = _SubSelection._SubSelected.GetComponent<Vehicles>();
            subHealthTotal = sub.GetHealth;
            subHullTotal = sub.GetHull;
        
        }
        else { startTracking = false; }
    }

    //tracks sub stats
    private void Update()
    {
        if (startTracking)
        {
            ChangeSlider();   
        }
    }

    private void ChangeSlider()
    {
        _PlayerHealth.value = sub.GetHealth / subHealthTotal;
        _PlayerHull.value = sub.GetHull / subHullTotal;
    }
}
