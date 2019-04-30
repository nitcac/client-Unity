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

  //効果音
  AudioSource audio_select;

  private void Start() {
    AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
    audio_select = audioSources[0];
  }

  public void gameStart() {
    audio_select.Play();
    titleAnm.SetTrigger("start");
    Invoke("moveMenu", 1.2f);
  }

  void moveMenu() {
    menu.SetActive(true);
  }

  public void PlayQuiz() {
    audio_select.Play();
    SceneManager.LoadScene("QuizMainScene");
  }

  public void PlayAR() {
    audio_select.Play();
    SceneManager.LoadScene("ARmainScene");
  }
}
