using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;




public class WaveController : MonoBehaviour
{
    [Header("Water Attributes")]
    private int cornerCount = 2;
    [SerializeField] SpriteShapeController ss_Controller;
    [SerializeField] int WaveCount = 6;
    [SerializeField] GameObject wavePointsHolder;
    [SerializeField] GameObject wavePref;


    [Header("Wave Attributes")]
    [SerializeField] float springStiffness = 0.1f;
    [SerializeField] float dampening = .1f;
    [SerializeField] float spread = .006f;
    [SerializeField] List<WavePoint> springs = new List<WavePoint>();

    [Header("Wave Maker")]
    [SerializeField] float _waveMakerDelay;
    [SerializeField] float _waveMakeMaxRepeatTime;
    [SerializeField] float _waveMinForce;
    [SerializeField] float _waveMaxForce;
    

    private void Start()
    {
        SetWaves();
        StartCoroutine(WaveMaker());
    }

    private void FixedUpdate()
    {
        
        foreach(WavePoint w in springs)
        {
            if (w == null) { return; }


            w.WaveSpringUpdate(springStiffness, dampening);
            w.WavePointUpdate();

        }

        UpdateSprings();

       
    }

    private void SetWaves()
    {
        Spline waterSpline = ss_Controller.spline;
        int waterPointCount = waterSpline.GetPointCount();

        //remove any points between the 2 corner points (just in case)
        for (int i = cornerCount; i < waterPointCount-cornerCount; i++)
        {
            waterSpline.RemovePointAt(cornerCount);
        }

        //Get width between the corners
        Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
        Vector3 waterTopRightConer = waterSpline.GetPosition(2);
        float waterWidth = waterTopRightConer.x - waterTopLeftCorner.x;

        //Get even spacing between waves
        float spacingPerWave = waterWidth / (WaveCount + 1);



        //Insert the waves we wamt
        for (int i = WaveCount; i > 0 ; i--)
        {
            int index = cornerCount;

            float xPosition = waterTopLeftCorner.x + (spacingPerWave * i);
            Vector3 wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
            waterSpline.InsertPointAt(index, wavePoint);
            waterSpline.SetHeight(index, 1f);
            waterSpline.SetCorner(index, false);
            waterSpline.SetTangentMode(index, ShapeTangentMode.Continuous);
        }

        CreateSprings(waterSpline);
    }


    private void UpdateSprings()
    {
        int count = springs.Count;

        float[] left_Deltas = new float[count];
        float[] right_Deltas = new float[count];

        for (int i = 0; i < count; i++)
        {
            if (i > 0)
            {
                left_Deltas[i] = spread * (springs[i].height - springs[i - 1].height);
                springs[i - 1].velocity += left_Deltas[i];
            }

            if (i < springs.Count - 1)
            {
                right_Deltas[i] = spread * (springs[i].height - springs[i + 1].height);
                springs[i + 1].velocity += right_Deltas[i];
            }
        }
    }



    private void CreateSprings(Spline waterSpline)
    {
        springs = new();

        for (int i = 0; i <= WaveCount+1; i++)
        {
            int index = i + 1;
            Smoothen(ss_Controller, index);

            GameObject wavePoint = Instantiate(wavePref, wavePointsHolder.transform, false);
            wavePoint.transform.localPosition = waterSpline.GetPosition(index);
            WavePoint waterspring = wavePoint.GetComponent<WavePoint>();
            waterspring.Init(ss_Controller);
            springs.Add(waterspring);
        }

    }

    private IEnumerator WaveMaker()
    {
        while (true)
        {
            //choose random force strength for wave
            float force = Random.Range(_waveMinForce, _waveMaxForce);

            foreach (WavePoint s in springs)
            {
                s.velocity += force;
                yield return new WaitForSeconds(_waveMakerDelay);

            }
            float time = Random.Range(1, _waveMakeMaxRepeatTime);
            yield return new WaitForSeconds(time);
        }
        
    }

  
    private void Smoothen(SpriteShapeController sc, int pointIndex)
    {
        Vector3 position = sc.spline.GetPosition(pointIndex);
        Vector3 positionNext = sc.spline.GetPosition(SplineUtility.NextIndex(pointIndex, sc.spline.GetPointCount()));
        Vector3 positionPrev = sc.spline.GetPosition(SplineUtility.PreviousIndex(pointIndex, sc.spline.GetPointCount()));
        Vector3 forward = gameObject.transform.forward;

        float scale = Mathf.Min((positionNext - position).magnitude, (positionPrev - position).magnitude) * 0.33f;

        Vector3 leftTangent = (positionPrev - position).normalized * scale;
        Vector3 rightTangent = (positionNext - position).normalized * scale;

        sc.spline.SetTangentMode(pointIndex, ShapeTangentMode.Continuous);
        SplineUtility.CalculateTangents(position, positionPrev, positionNext, forward, scale, out rightTangent, out leftTangent);

        sc.spline.SetLeftTangent(pointIndex, leftTangent);
        sc.spline.SetRightTangent(pointIndex, rightTangent);
    }
}
