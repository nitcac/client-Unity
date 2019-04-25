using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManeger : MonoBehaviour
{
    public int PutScore { get; private set; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore()
    {
        PutScore++;
        Debug.Log(PutScore);
    }

    public void ResetScore()
    {
        PutScore = 0;
    }
}
