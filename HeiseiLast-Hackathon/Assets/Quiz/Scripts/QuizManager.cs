using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour {
  private TextAsset csvFile; // CSVファイル
  private List<string[]> quizDataz = new List<string[]>(); // CSVの中身を入れるリスト

  List<int> randomIndex = new List<int>();
  int quizNum=0;  //クイズ出現順番
  bool playing=true;

  [SerializeField]
  GameObject quizSpawner;

  [SerializeField]
  GameObject QuizPrefab;

  [SerializeField]
  Text scoreText;
  int rightNum=0;
  int wrongNum=0;

  [SerializeField]
  Text timerText;
  float totalTime=30;
  int seconds=0;

  //マルバツの画像，テキスト
  [SerializeField]
  GameObject maru;
  Text maruInfo;
  Animator maruAnm;
  [SerializeField]
  GameObject batsu;
  Text batsuInfo;
  Animator batsuAnm;

  [SerializeField]
  GameObject EndImages;

  //効果音
  AudioSource audio_right;
  AudioSource audio_wrong;
  AudioSource audio_BGM;
  AudioSource audio_whistle;

  void Start() {
    loadQuizCSV();
    LoadSounds();
    GetMaruBatsu();

    Invoke("QuizSpawn", 1f);
    Invoke("GameEnd", totalTime);
  }

  void Update() {
    if(totalTime>0)timer();
  }

  void loadQuizCSV() {
    int quizLen = 0; // CSVの行数
    csvFile = Resources.Load("HeiseiQ") as TextAsset; /* ResoucesのCSV読み込み */
    StringReader reader = new StringReader(csvFile.text);
    while (reader.Peek() > -1) {
      string line = reader.ReadLine();
      quizDataz.Add(line.Split(',')); // リストに入れる
      Debug.Log(quizDataz[quizLen][0]);
      Debug.Log(quizDataz[quizLen][1]);
      quizLen++; // 行数加算
    }

    setRandomIndex(quizLen);
  }

  void QuizSpawn() {
    if (quizNum >= randomIndex.Count || !playing) return; //クイズ数超えたら

    //クイズのスポーン
    GameObject quizObj =
      Instantiate(
        QuizPrefab,
        quizSpawner.transform.localPosition,
        quizSpawner.transform.localRotation);
    quizObj.transform.SetParent(quizSpawner.transform, false);
    QuizImg quizImg = quizObj.GetComponent<QuizImg>();
    //ランダムにクイズの内容をセッティングする
    quizImg.setQuiz(
      quizDataz[randomIndex[quizNum]][0], 
      int.Parse(quizDataz[randomIndex[quizNum]][1])
    );
    quizNum++;
    Invoke("QuizSpawn", 3.5f);
  }

  //ランダム番号のリストセット
  void setRandomIndex(int Len) {
    for (int i = 0; i < Len; i++) {
      randomIndex.Add(i);
    }
    for (int i = 0; i < Len; i++) {
      int n = Random.Range(0, Len), tmp;
      tmp = randomIndex[i];
      randomIndex[i] = randomIndex[n];
      randomIndex[n] = tmp;
    }
  }

  public void quizRight(int year) {
    if (!playing) return;

    audio_right.Play();
    rightNum++;
    scoreText.text = " 正解 ：" + rightNum + "\n不正解："+wrongNum;

    string gengo;
    if (year > 0) gengo = "平成";
    else {
      gengo = "昭和";
      year *= -1; //昭和データは負の数なので
    }
    maruInfo.text = gengo + year.ToString()+"年";
    maruAnm.SetTrigger("view");
  }

  public void quizWrong(int year) {
    if (!playing) return;

    audio_wrong.Play();
    wrongNum++;
    scoreText.text = " 正解 ：" + rightNum + "\n不正解：" + wrongNum;

    string gengo;
    if (year > 0) gengo = "平成";
    else {
      gengo = "昭和";
      year *= -1; //昭和データは負の数なので
    }
    batsuInfo.text = gengo + year.ToString() + "年";
    batsuAnm.SetTrigger("view");
  }

  void GameEnd() {
    Debug.Log("終了");
    audio_BGM.Stop();
    playing = false;
    audio_whistle.Play();
    EndImages.SetActive(true);
    EndImages.transform.Find("result/Text").
      GetComponent<Text>().text = "正解の数・・・・"+rightNum+"\n不正解の数・・・"+wrongNum;
  }

  void GetMaruBatsu() {
    maruInfo = maru.transform.Find("info").GetComponent<Text>();
    maruAnm = maru.GetComponent<Animator>();
    batsuInfo=batsu.transform.Find("info").GetComponent<Text>();
    batsuAnm = batsu.GetComponent<Animator>();
  }

  void LoadSounds() {
    AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
    audio_right = audioSources[0];
    audio_wrong = audioSources[1];
    audio_BGM = audioSources[2];
    audio_whistle = audioSources[3];
  }

  void timer() {
    totalTime -= Time.deltaTime;
    seconds = (int)totalTime;
    timerText.text = "TIME:"+seconds.ToString();
  }

  public void backTitleScene() {
    Debug.Log("タイトル");
    SceneManager.LoadScene("Title");
  }
}
