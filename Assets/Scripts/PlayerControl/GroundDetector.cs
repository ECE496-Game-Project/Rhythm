using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{

    [Header("Reference variable")]
    [SerializeField]
    private Transform _feet;



    [Header("Parameter")]
    [Tooltip("The distance between player feet and the ground must be less" +
        "than the tolerance so that it is consider to be on the ground")]
    [SerializeField]
    private float _tolerance;


    [Header("State")]
    public bool _onGround;

    public bool OnGround
    {
        get { return _onGround; }
    }

    private void Start()
    {
        _onGround= true;
    }

    private void Update()
    {
        Vector3 up = transform.up;
        _onGround = Physics.Raycast(_feet.position + 0.1f * up, - up, 0.1f + _tolerance);
    }
}
