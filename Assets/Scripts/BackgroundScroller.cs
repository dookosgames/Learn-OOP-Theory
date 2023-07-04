using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class BackgroundScroller : MonoBehaviour
{


    [Header("References")]
    [SerializeField] SpriteRenderer _bgRenderer;
    [SerializeField] SpriteShapeRenderer _topWater;
    [SerializeField] SpriteRenderer _bottomWater;
    [SerializeField] GameObject _player;


    [Header("Positions")]
    [SerializeField] Vector3 _endPosY;
    public Vector3 _startPos { get; private set; }    
    public float yLength { get; private set; } //y length of the water background sprite



    [Header("Background Colors")]
    [SerializeField] Color[] _bgColors;


    [Header("Depth")]
    [SerializeField] TextMeshProUGUI _depthDisplay;
    [SerializeField] int[] _depths;
    public float _feetPerUnit { get; private set; }
    [SerializeField] int _depthOfWaterLevel;
    public int GetDepthOfWater { get { return _depthOfWaterLevel; } }
    


    public int _bgCounter { get; private set; }


    private void OnEnable()
    {
        SubSelectManager.a_ActiveSub += ActivePlayer;
    }
    private void OnDisable()
    {
        SubSelectManager.a_ActiveSub -= ActivePlayer;
    }

    void ActivePlayer(GameObject sub)
    {
        _player = sub;
    }

    private void Start()
    {
        //get y size of water tile
        yLength=_bgRenderer.bounds.min.y;

        //set feet per unit
        _feetPerUnit = _depthOfWaterLevel/yLength;

        //set startPos
        _startPos = new Vector3(0,_bgRenderer.bounds.max.y,0);
        //set EndPos
        _endPosY = _bgRenderer.bounds.min;
    }

    private void Update()
    {
        //Loops background
        ResetBackground();
    }


    //Resets the background
    private void ResetBackground()
    {
        if (_player.GetComponent<SpriteRenderer>().bounds.center.y <= _endPosY.y)
        {
            _player.transform.position = _startPos;
            ChangeDepth();
            ChangeBackground();
        }

        //selects background to change to once player reachs loops spot
        void ChangeBackground()
        {
            //sets new background color
            _bgRenderer.color = _bgColors[_bgCounter];
            _topWater.color = _bgColors[_bgCounter];
            _bottomWater.color = _bgColors[_bgCounter];

            //Moves to next index
            _bgCounter++;

            //check index to make sure its valid
            if (_bgCounter > _bgColors.Length-1)
            {
                _bgCounter = 0;
            }
        }

        //selects depth that matches BG
        void ChangeDepth()
        {
            //Sets depth for this bg
            _depthDisplay.text = _depths[_bgCounter].ToString();
            
        }
    }




}
