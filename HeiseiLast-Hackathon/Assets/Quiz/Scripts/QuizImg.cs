using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuizImg : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
  Rigidbody2D rg;

  void Start() {
    rg=GetComponent<Rigidbody2D>();
    Debug.Log("でばっぐ");
  }
  void Update() {

  }
  
  // ドラックが開始したとき呼ばれる.
  public void OnBeginDrag(PointerEventData eventData) {
    rg.gravityScale = 0;
  }

  // ドラック中に呼ばれる.
  public void OnDrag(PointerEventData eventData) {
    transform.position = eventData.position;
  }

  // ドラックが終了したとき呼ばれる.
  public void OnEndDrag(PointerEventData eventData) {
    rg.gravityScale = 10 ;
  }

}