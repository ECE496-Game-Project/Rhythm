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
        private void Start() { m_BeatPeriod = 60 / m_BPM; }
        private void OnInspectorGUI() {}
    }

    public class Timer
    {
	    
    }
}