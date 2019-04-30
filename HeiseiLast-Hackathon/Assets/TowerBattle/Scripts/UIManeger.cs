using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManeger : MonoBehaviour
{
    [SerializeField]
    Text scoreText, objNameText, resultScoreText, rankingText;
    [SerializeField]
    GameManeger gameManeger;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip audioClip;
    [SerializeField]
    GameObject rankingPanel, resultPanel;
    [SerializeField]
    APIManeger apiManager;
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
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void OnBackTitleButtonClick()
    {
    Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    public void OnRankingViewClick()
    {
        rankingPanel.SetActive(true);
        resultPanel.SetActive(false);
        apiManager.Request();
    }

    public void OnSpinButton()
    {
        gameManeger.PutObjSpin();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void SetResultScore(string score)
    {
        resultScoreText.text = "Score:" + score;
        resultPanel.SetActive(true);
        rankingPanel.SetActive(false);
    }

    public void SetRankingText(string ranking)
    {
        rankingText.text = ranking;
    }
}
