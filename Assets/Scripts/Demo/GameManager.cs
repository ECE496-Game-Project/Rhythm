using System.Collections;
using UnityEngine;
using SupportiveLib;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    InputController _InputController;
    WaveManager _WaveManager;
    Timer m_Timer;

    void Awake()
    {
        if (Instance == null) { _instance = this; }
        else { Destroy(gameObject); }

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
