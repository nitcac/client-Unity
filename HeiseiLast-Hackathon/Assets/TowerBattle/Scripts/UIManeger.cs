using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour
{
    [SerializeField]
    Text scoreText, objNameText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetScoreText(string score)
    {
        scoreText.text = score;
    }

    public void SetObjNameText(string name)
    {
        objNameText.text = name.Split('(')[0];
    }

}
