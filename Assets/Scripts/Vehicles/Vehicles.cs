using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour
{
    [Header("Basic Specs")]
    [SerializeField] int _health;
    [SerializeField] int _mass;
    [SerializeField] Sprite _designSprite;

    public int GetHealth { get=> _health; }
    public int GetMass { get=>_mass;}
    public Sprite GetDesignSprite { get => _designSprite; }
}
