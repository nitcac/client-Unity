using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class AR_gameManager : MonoBehaviour {
  string[] gengo = new string[]{"令和","平成","昭和","大正","明治","草","高専","安久","万字"};
  bool isFinding=false;

  CameraDevice cameraDevice;

  [SerializeField]
  Text GengoText;

  [SerializeField]
  Text debugText;

  [SerializeField]
  GameObject Frame;

  [SerializeField]
  GameObject canvas;
  GameObject frashImg;
  Text textBK;
  Text textRD;

  // Start is called before the first frame update
  void Start() {
    frashImg = canvas.transform.Find("flash").gameObject;
    frashImg.SetActive(false);
    textBK = canvas.transform.Find("textFrame/textBK").gameObject.GetComponent<Text>();
    textRD = canvas.transform.Find("textFrame/textRD").gameObject.GetComponent<Text>();
    textBK.text = "新元号ついに発表";
    textRD.text = "";
  }

  // Update is called once per frame
  void Update() {

    //左右判定
    if (Frame.transform.position.x > 0) debugText.text = "デバッグ：右側";
    else debugText.text = "デバッグ：左側";

  }
  public void markerFind() {
    string newGengo = gengo[Random.Range(0, gengo.Length)];
    GengoText.text = newGengo;
    textBK.text = "新元号は「　　　」に決定";
    textRD.text=newGengo;
    isFinding = true;
    FlashON();
  }

  public void markerLost() {
    textBK.text = "新元号ついに発表";
    textRD.text = "";
    isFinding = false;
  }

  void FlashOFF() {
    //cameraDevice.SetFlashTorchMode(false);  //できん
    frashImg.SetActive(false);
    if(isFinding)Invoke("FlashON", Random.Range(0.1f,0.5f));  //フラッシュ演出を続ける
  }

  void FlashON() {
    //cameraDevice.SetFlashTorchMode(true);
    frashImg.SetActive(true);
    Invoke("FlashOFF", 0.3f);
  }

}
