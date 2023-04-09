using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace SupportiveLib{
    using BeatType = Tuple<NoteType, WaveType>;
    using BeatList = List<Tuple<NoteType, WaveType>>;

    public enum NoteType {HIGH = 1, MID = 0, LOW = -1, INVALID = -2}
    public enum WaveType { IMPULSE = 0, CONTINOUS = 1, INVALID = -1}
    
    public class InputPack
    {
        public InputPack(float value=0f, float startTime=-1f, float endTime=-1f)
        {
            _Value = value;
            _StartTime = startTime;
            _EndTime = endTime;
        }

        public override string ToString()
        {
            return $"Value: {_Value} StartTime: {_StartTime}, EndTime: {_EndTime}";
        }
        public float _Value = 0f;
        public float _StartTime = -1f;
        public float _EndTime = -1f;
    }

    public class Rhythm
    {
        public int m_BPM;
        float m_Meter;
        float m_BeatPeriod;

        AudioClip m_RhythmSource;
        BeatList m_BeatAnswer;
        
        private void Start() 
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
             }
         }
     }
}