using System.Collections;
using UnityEngine;
using SupportiveLib;
using System.Collections.Generic;

public class RhythmManager : MonoBehaviour
{
    private static RhythmManager _instance;
    public static RhythmManager Instance { get => _instance; }
    
    AudioSource _audioSource;
    [SerializeField] List<Rhythm> RhythmList; // The first element is a defualt empty rhythm
    int m_RhythmIdx;
    public int RhythmIdx
    {
        get { return  m_RhythmIdx; }
        set
        {
            if (value >= 0 && value < RhythmList.Count)
            {
                m_RhythmIdx = value;
            }
            else
            {
                Debug.LogWarning("Trying to select rhythm that does not exist.");
            }
        }
    }

    public Rhythm m_CurrRhythm
    {
        get => RhythmList[m_RhythmIdx];

    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else { Destroy(gameObject); }
    }

    // Use this for initialization
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
        
    // Update is called once per frame
    void Update()
    {

    }
    public void PlayRhythm()
    {
        _audioSource.Play();
    }
    public void PauseRhythm()
    {
        _audioSource.Pause();
    }
    public void StopRhythm()
    {
        _audioSource.Stop();
    }
    public void SelectRhythm(int idx)
    {
        RhythmIdx = idx;
        _audioSource.clip = m_CurrRhythm.RhythmSource;
    }
    void ResetManager()
    {
        RhythmIdx = 0;
    }
}