using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuizManager : MonoBehaviour {
  private TextAsset csvFile; // CSVファイル
  private List<string[]> quizDataz = new List<string[]>(); // CSVの中身を入れるリスト

  List<int> randomIndex = new List<int>();
  int quizNum=0;  //クイズ出現順番

  [SerializeField]
  GameObject quizSpawner;

  [SerializeField]
  GameObject QuizPrefab;

  //効果音
  AudioSource audio_right;
  AudioSource audio_wrong;

  void Start() {
    loadQuizCSV();
    LoadSounds();

    Invoke("QuizSpawn", 2f);
  }

  void Update() {

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
    if (quizNum >= randomIndex.Count) return; //クイズ数超えたら

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
    Invoke("QuizSpawn", 4f);
  }

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
    audio_right.Play();
  }

  public void quizWrong(int year) {
    audio_wrong.Play();
  }

  void LoadSounds() {
    AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
    audio_right = audioSources[0];
    audio_wrong = audioSources[1];
  }
}
