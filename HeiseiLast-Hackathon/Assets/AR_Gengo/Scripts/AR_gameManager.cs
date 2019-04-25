using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class AR_gameManager : MonoBehaviour {
  string[] gengo = new string[]{"令和","安明","安永","西米","元モ","草","高専","安久","万字"};
  bool isFinding=false;
  bool isGameStart = false;

  bool isMissionR;  //指示が右側か（falseで左）

  CameraDevice cameraDevice;

  [SerializeField]
  Text GengoText;
  Text missionText;

  [SerializeField]
  Text debugText;

  [SerializeField]
  GameObject Frame;

  [SerializeField]
  GameObject canvas;
  GameObject frashImg;
  Text textBK;
  Text textRD;
  GameObject shuchu;

  [SerializeField]
  Animator missionAnm;

  //音楽関連
  AudioSource audio_BGM;
  AudioSource audio_cymbal;
  int BPM = 126;
  float totalTime;
  float startTime;
  int BPMNum=0;
  float ratio_hasu=0;  //ratio再計算端数

  // Start is called before the first frame update
  void Start() {
    frashImg = canvas.transform.Find("flash").gameObject;
    frashImg.SetActive(false);
    textBK = canvas.transform.Find("textFrame/textBK").gameObject.GetComponent<Text>();
    textRD = canvas.transform.Find("textFrame/textRD").gameObject.GetComponent<Text>();
    textBK.text = "新元号ついに発表";
    textRD.text = "";
    missionText=canvas.transform.Find("mission/Text").gameObject.GetComponent<Text>();
    shuchu = canvas.transform.Find("mission/shuchu").gameObject;

    AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
    audio_BGM = audioSources[0];
    audio_cymbal = audioSources[1];
    Invoke("GameStart", 3f);

    startTime = Time.timeSinceLevelLoad+3f;
    //一泊の秒数=60/テンポ
    totalTime = 60f / (float)BPM;
  }

  void FixedUpdate() {
    float diff = Time.timeSinceLevelLoad - startTime;
    float ratio = diff / totalTime +ratio_hasu;
    ratio_hasu = 0; //端数(ループの一度だけ適用すればいい)
    if(ratio>=1f) {
      ratio_hasu = ratio - 1f;
      startTime = Time.timeSinceLevelLoad;
      BPMNum++;
      Debug.Log("BPM:"+(BPMNum%8+1));
      //一時的に左側の条件を無効にしてます
      if (BPMNum>16 && (BPMNum % 8 == 1 && Random.Range(0, 5) == 7) ||BPMNum%16==10 ) {
        //左右どちらかに指示
        if (Random.Range(0, 3) == 0) {
          isMissionR=true;
          missionText.text = "右に見せろ！";
          shuchu.transform.localPosition = new Vector3(950,0,0);
        }
        else {
          isMissionR = false;
          missionText.text = "左に見せろ！";
          shuchu.transform.localPosition = new Vector3(-950, 0, 0);
        }

        missionAnm.SetTrigger("mission");
      }
    }
  }

  // Update is called once per frame
  void Update() {

    //左右判定
    if (Frame.transform.position.x > 0) debugText.text = "デバッグ：右側";
    else debugText.text = "デバッグ：左側";

  }

  //マーカー認識時
  public void markerFind() {
    string newGengo = gengo[Random.Range(0, gengo.Length)];
    audio_cymbal.Play();
    GengoText.text = newGengo;
    textBK.text = "新元号は「　　　」に決定";
    textRD.text=newGengo;
    isFinding = true;
    FlashON();
  }
  //マーカーロスト時
  public void markerLost() {
    textBK.text = "新元号ついに発表";
    textRD.text = "";
    isFinding = false;
  }

  void FlashOFF() {
    //cameraDevice.SetFlashTorchMode(false);  //できん cameraDeviceの使い方が違う？
    frashImg.SetActive(false);
    if(isFinding)Invoke("FlashON", Random.Range(0.1f,0.5f));  //フラッシュ演出を続ける
  }

  void FlashON() {
    //cameraDevice.SetFlashTorchMode(true);
    frashImg.SetActive(true);
    Invoke("FlashOFF", 0.3f);
  }

  void GameStart() {
    Debug.Log("Play BGM");
    isGameStart = true;
    audio_BGM.Play();
  }
}
