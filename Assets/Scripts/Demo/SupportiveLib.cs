using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace SupportiveLib{
    public class BeatType{
        public NoteType _NoteType;
        public WaveType _WaveType;

        public BeatType(NoteType noteType, WaveType waveType) {
            _NoteType = noteType;
            _WaveType = waveType;
        }
    }

    public class BeatList {
        public List<BeatType> _List;

        public BeatList(){
            _List = new List<BeatType>();
        }
        
    }



    public enum NoteType {HIGH = 1, MID = 0, LOW = -1, INVALID = -2}
    public enum WaveType { IMPULSE = 0, CONTIPRESS = 1, CONTIRELEASE = 2, CONTINUOUS = 3, INVALID = -1}
    
    public class InputPack
    {
        public InputPack(float value=0f/* , float startTime=-1f, float endTime=-1f */)
        {
            _Value = value;
            // _StartTime = startTime;
            // _EndTime = endTime;
        }

        public override string ToString()
        {
            return $"Value: {_Value}";
        }
        public float _Value = 0f;
        // public float _StartTime = -1f;
        // public float _EndTime = -1f;
    }

    public class Rhythm
    {
        public int m_BPM;
        public float m_Meter;
        public float m_BeatPeriod;

        AudioClip m_RhythmSource;
        public AudioClip RhythmSource
        {
            get { return m_RhythmSource; }
        }
        public BeatList m_BeatAnswer;
        
        private void Awake() 
        { 
            m_BeatPeriod = 60 / m_BPM; 
        }

        private void OnInspectorGUI() 
        {

        }
    }

     public class Timer
     {
         float _currTime;
         public float m_TimeLimit;
         private Action m_TimerOperation;
         
         public Timer(float timeLimit, Action timerOperation)
         {
             _currTime = 0f;
             m_TimeLimit = timeLimit;
             m_TimerOperation = timerOperation;
         }
         
         public void ResetTimer()
         {
             _currTime = 0f;
         }
         
         public void Update()
         {
             _currTime += Time.deltaTime;
             if(_currTime >= m_TimeLimit)
             {
                 m_TimerOperation?.Invoke();
                 ResetTimer();
             }
         }
     }

    

}