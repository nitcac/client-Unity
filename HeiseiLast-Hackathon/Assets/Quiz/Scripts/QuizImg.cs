using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuizImg : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
  Rigidbody2D rg;
  QuizManager quizManagerScript;

  int year;
  bool isDragging;



  void Start() {
    isDragging = false;
    rg=GetComponent<Rigidbody2D>();
    quizManagerScript = GameObject.Find("GameManager").GetComponent<QuizManager>();


  }
  void Update() {

  }
  
  // ドラックが開始したとき呼ばれる.
  public void OnBeginDrag(PointerEventData eventData) {
    rg.gravityScale = 0;
    isDragging = true;
  }

  // ドラック中に呼ばれる.
  public void OnDrag(PointerEventData eventData) {
    transform.position = eventData.position;
  }

  // ドラックが終了したとき呼ばれる.
  public void OnEndDrag(PointerEventData eventData) {
    rg.gravityScale = 15 ;
    isDragging = false;
  }

  public void setQuiz(string str,int n) {
    this.transform.Find("Text").gameObject.GetComponent<Text>().text = str;
    year = n;
  }

  private void OnTriggerStay2D(Collider2D collision) {
    if (!isDragging) {
      if((collision.gameObject.name=="Zenki" && year>=1 && year<=15) ||
         (collision.gameObject.name=="Kouki" && year>=16) ||
         (collision.gameObject.name == "pipe1" && year < 0 )
         ) {
        Debug.Log("正解！:" + year + "年");
        quizManagerScript.quizRight(year);
        Destroy(this.gameObject);
      }
      else {
        Debug.Log("不正解:" + year + "年");
        quizManagerScript.quizWrong(year);
        Destroy(this.gameObject);
      }
    }
  }


}