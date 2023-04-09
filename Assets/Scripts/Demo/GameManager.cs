using System.Collections;
using UnityEngine;
using SupportiveLib;

public class GameManager : MonoBehaviour {

    InputController _InputController;
    WaveManager _WaveManager;
    Timer m_Timer;

    void Awake()
    {
        // m_Timer = new Timer(, );
    }


    // Use this for initialization
    void Start() {
        GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update() {
        // m_Timer.Update();
    }
}
