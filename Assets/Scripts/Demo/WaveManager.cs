using System.Collections;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using SupportiveLib;

public class WaveManager : MonoBehaviour {

    private static WaveManager _instance;
    public static WaveManager Instance { get => _instance; }
    
    public BeatList _InputBeatCache;
    
    int _successBeat = 0;
    

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else { Destroy(gameObject); }   
    }

    public bool Wave(){
        
        // Change Input into Beat Wave
        var cache = InputController.Instance.GetICCCache();

        InputPack longestIP = null;
        float previousDuration = float.PositiveInfinity;
        foreach (var input in cache){
            // if the input is keep pressing from last beat to the current beat
            // do something
            //choose the input pack if it is still holding
            if (input._EndTime == -1){
                longestIP = input;
                break;
            }

            float duration = input._EndTime - input._StartTime;
            if (duration < previousDuration){
                previousDuration = duration;
                longestIP = input;
            }
        }

        //longestIP into BeatType
        BeatType beat = new BeatType();

        if (longestIP != null){
            beat._NoteType = (NoteType) longestIP._Value;
            beat._WaveType = (longestIP._EndTime == -1)? WaveType.CONTINOUS: WaveType.IMPULSE;
        }else{
            beat._NoteType = (NoteType) 0;
            beat._WaveType = WaveType.CONTINOUS;
        }
            
        _InputBeatCache._List.Add(beat);
        InputController.Instance.ClearCache();



        // TODO: Draw Wave UI


        // TODO: Verify Beat Wave
        var beatAnswer = RhythmManager.Instance.m_CurrRhythm.m_BeatAnswer;
        if (beatAnswer._List[_successBeat] == beat){
            _successBeat++;

            if(_successBeat == beatAnswer._List.Count){
                RhythmManager.Instance.PlayRhythm();
                return true;
            }
        }else{
            _successBeat = 0;
        }

        return false;

    }


    public void WaveStart() {
        if(_InputBeatCache != null) _InputBeatCache._List.Clear();
        else _InputBeatCache = new BeatList();

        // TODO: Inisialize Wave UI
    }
}
