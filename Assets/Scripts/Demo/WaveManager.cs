using System.Collections;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;


public class WaveManager : MonoBehaviour {

    private WaveManager _instace;
    public WaveManager Instance { get => _instace; }
    private void Awake()
    {
        if (_instace == null)
        {
            _instace = this;
        }
        else { Destroy(gameObject); }
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
