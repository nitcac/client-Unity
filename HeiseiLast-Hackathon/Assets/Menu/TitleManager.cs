using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
  [SerializeField]
  Animator titleAnm;

  [SerializeField]
  GameObject menu;

  public void gameStart() {
    titleAnm.SetTrigger("start");
    Invoke("moveMenu", 1.2f);
  }

  void moveMenu() {
    menu.SetActive(true);
  }

  public void PlayQuiz() {
    SceneManager.LoadScene("QuizMainScene");
  }

  public void PlayAR() {
    SceneManager.LoadScene("ARmainScene");
  }
}
