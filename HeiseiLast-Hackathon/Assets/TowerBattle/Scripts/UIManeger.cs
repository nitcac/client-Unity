using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour
{
    [SerializeField]
    Text scoreText, objNameText, resultScoreText, rankingText;
    [SerializeField]
    GameManeger gameManeger;
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

    public void OnRestartButtonClick()
    {
        gameManeger.ChangeState(TowerBattleState.reset);
    }

    public void SetResultScore(string score)
    {
        resultScoreText.text = "Score:" + score;
    }

    public void SetRankingText(string ranking)
    {
        rankingText.text = ranking;
    }
}
