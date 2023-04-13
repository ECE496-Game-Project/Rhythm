using System.Collections;
using UnityEngine;
using SupportiveLib;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    Timer m_Timer;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else { Destroy(gameObject); }   
    }

    void Start()
    {
        
        
        
        // RhythmManager ChangeRhythm赋值给InputController的OnRythmChange
        InputController.Instance.OnRythmChange.AddListener(ChangeRhythm);

        

    }
    void ChangeRhythm(){
        // BeatTimer
        RhythmManager.Instance.SelectRhythm(1);
        var period = RhythmManager.Instance.m_CurrRhythm.m_BeatPeriod;
        m_Timer = new Timer(period, OnBeat);

        WaveManager.Instance.WaveStart();
    }

    
    // Update is called once per frame
    void Update() {
        // BeatTimer Update
        m_Timer?.Update();
    }

    void OnBeat()
    {
        if(WaveManager.Instance.Wave()){
            // WaveManager Wave BeatTimer Reset
            m_Timer = null;
            RhythmManager.Instance.PlayRhythm();
        }
    }

    
}
