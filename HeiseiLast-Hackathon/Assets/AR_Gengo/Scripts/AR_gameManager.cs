using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AR_gameManager : MonoBehaviour {
  string[] gengo = new string[]{"令和","平成","昭和","大正","明治","草","高専","安久","万字"};

  [SerializeField]
  Text GengoText;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }
  public void markerFind() {
    GengoText.text = gengo[Random.Range(0, gengo.Length)];
  }

}
