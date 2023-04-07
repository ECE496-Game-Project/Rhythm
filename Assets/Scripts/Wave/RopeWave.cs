using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeWave : MonoBehaviour
{
    [Header("ViewOnly")]
    [SerializeField] private Transform[] ropeTransforms;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;

    [Header("BallGenerate")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float ballSize = 1.0f;
    [SerializeField] private float ballSpacing = 1.0f;
    [SerializeField] private int numBalls = 10;



    private void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    private void CreateRope() {
        ropeTransforms = new Transform[numBalls];

        Vector3 direction = (endPoint - startPoint).normalized;
        float distance = Vector3.Distance(startPoint, endPoint);
        float totalSpacing = (numBalls - 1) * ballSpacing;
        float spacing = (distance - totalSpacing) / (numBalls - 1);

        for (int i = 0; i < numBalls; i++) {
            Vector3 position = startPoint + direction * (i * spacing + i * ballSpacing);
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity, this.transform);
            ball.transform.localScale = new Vector3(ballSize, ballSize, ballSize);
            ropeTransforms[i] = ball.transform;
        }
    }

    private void DestoryRope() {

    }
}
