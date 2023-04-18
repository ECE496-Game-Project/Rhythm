using System.Collections;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using SupportiveLib;

public class WaveManager : MonoBehaviour {

    Timer m_Timer;
    private static WaveManager _instance;
    public static WaveManager Instance { get => _instance; }
    
    public BeatList _InputBeatCache;
    
    int _successBeat = 0;

    private BeatList _answer;


    public RectTransform _rectTransform;
    
    public Vector3 _originalPosition;

    public Vector3 _step;
    
    public void BeatValidation(){
        var cache = InputController.Instance.GetICCCache();
        int beatValid = 0;
        foreach(var ip in cache){
            Debug.Log(ip._Value);
        }
        // restart if more than 2 input actions happen
        if (cache.Count > 2){
            _successBeat = 0;
            goto validate;
        }

        
        // 短按 beat: (IMPULSE, HIGH) / (IMPULSE, LOW) [inputCache有且仅有value X + value 0]
        // 空 beat: (CONTINUOUS, HIGH) / (CONTINUOUS, LOW) / (CONTINUOUS, MID) [inputCache无输入]

        // 长按 beat:
        //长按 HIGH :(CONTIPRESS + HIGH), CONTINOUS(>=0), (CONTIRELEASE + MID): 
        // CONTIPRESS + HIGH： input cache 有一个 1 的input pack
        // CONTINOUS: inputCache无输入
        // CONTIRELEASE + MID input cache 有一个 0 的input pack

        
        var curBeatAnswer = _answer._List[_successBeat];
        var noteType = curBeatAnswer._NoteType;
        var waveType = curBeatAnswer._WaveType;
        if (waveType == WaveType.CONTINUOUS){
            
            if (cache.Count != 0){
                beatValid = 0;
                goto validate;
            }else{
                beatValid = 1;
                goto validate;
            }
            
        }
        else if(waveType == WaveType.IMPULSE){
            float value = (float) noteType;
            
            if (cache.Count != 2){
                beatValid = 0;
                goto validate;
            }

            var first = cache.Dequeue();
            var second = cache.Dequeue();
            
            if (first._Value == value && second._Value == 0f){
                beatValid = 1;
                goto validate;
            }else{
                beatValid = 0;
                goto validate;
            }
        }else if (waveType == WaveType.CONTIPRESS){
            float value = (float) noteType;
            if (cache.Count != 1){
                beatValid = 0;
                goto validate;
            }

            var first = cache.Dequeue();
            if (first._Value == value){
                beatValid = 1;
                goto validate;
            }else{
                beatValid = 0;
                goto validate;
            }
            
        }else if(waveType == WaveType.CONTIRELEASE){
            if (cache.Count != 1){
                beatValid = 0;
                goto validate;
            }

            var first = cache.Dequeue(); 
            if (first._Value == 0f){
                beatValid = 1;
                goto validate;
            }else{
                beatValid = 0;
                goto validate;
            }
        }


        validate:
        switch(beatValid){
            case 0:
                _successBeat = 0;
                ResetPointer();
                break;
            case 1:
                _successBeat++;
                MovePointerToNext();
                break;
        }

        InputController.Instance.ClearCache();

        // // InputPack longestIP = null;
        // // float previousDuration = float.PositiveInfinity;
        // // foreach (var input in cache){

        // //     Debug.Log(input);

        // //     // if the input is keep pressing from last beat to the current beat
        // //     // do something
        // //     //choose the input pack if it is still holding
        // //     if (input._EndTime == -1){
        // //         longestIP = input;
        // //         break;
        // //     }

        // //     float duration = input._EndTime - input._StartTime;
        // //     if (duration < previousDuration){
        // //         previousDuration = duration;
        // //         longestIP = input;
        // //     }
        // // }

        // //longestIP into BeatType
        // BeatType beat = new BeatType();

        // if (longestIP != null){
        //     beat._NoteType = (NoteType) longestIP._Value;
        //     beat._WaveType = (longestIP._EndTime == -1)? WaveType.CONTINOUS: WaveType.IMPULSE;
        // }else{
        //     beat._NoteType = (NoteType) 0;
        //     beat._WaveType = WaveType.CONTINOUS;
        // }
            
        // _InputBeatCache._List.Add(beat);
        // InputController.Instance.ClearCache();

        // //var beatAnswer = RhythmManager.Instance.m_CurrRhythm.m_BeatAnswer;
        // if (beatAnswer._List[_successBeat] == beat){
        //     _successBeat++;

        //     if(_successBeat == beatAnswer._List.Count){
        //         RhythmManager.Instance.PlayRhythm();
        //         return true;
        //     }
        // }else{
        //     _successBeat = 0;
        // }
    }

    private void MovePointerToNext(){
        _rectTransform.position += _step;
    }

    private void ResetPointer(){
        _rectTransform.position = _originalPosition;
    }

    public void DrawWave(){
        //draw wave
    }

    public void Beat(){
        Debug.Log("beat");
        BeatValidation();
        DrawWave();
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else { Destroy(gameObject); }   
    }

    private void Start(){
        var period = 1.0f;
        m_Timer = new Timer(period, Beat);
        _originalPosition = _rectTransform.position;

        _answer= new BeatList();
        _answer._List.Add(new BeatType(NoteType.HIGH, WaveType.IMPULSE));
        _answer._List.Add(new BeatType(NoteType.LOW, WaveType.CONTIPRESS));
        _answer._List.Add(new BeatType(NoteType.LOW, WaveType.CONTIRELEASE));
        _answer._List.Add(new BeatType(NoteType.HIGH, WaveType.IMPULSE));
        _answer._List.Add(new BeatType(NoteType.MID, WaveType.CONTINUOUS));
        _answer._List.Add(new BeatType(NoteType.LOW, WaveType.IMPULSE));
    }

    private void Update(){
        m_Timer?.Update();
    }
}
