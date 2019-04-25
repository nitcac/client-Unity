using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TowerBattleState
{
    wait, preparation, put
}
public class GameManeger : MonoBehaviour
{
    TowerBattleState state;
    Vector3 mousePos, zDistance = new Vector3(0, 0, 1);
    Vector3 putPos;
    GameObject putObj;
    Stack<GameObject> stuckObj = new Stack<GameObject>();
    Rigidbody rb;
    [SerializeField]
    GameObject[] putObjList;
    Vector3 createObjPos = new Vector3(0, 0.6f, 0.82f);
    float nowHeight;
    [SerializeField]
    ScoreManeger scoreManeger;
    [SerializeField]
    UIManeger uiManeger;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(TowerBattleState.wait);
        nowHeight = Camera.main.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeState(TowerBattleState changeState)
    {
        state = changeState;
        if (state == TowerBattleState.wait)
        {
            SetNextObj();
            ChangeState(TowerBattleState.preparation);
        }
    }

    public void OnScreenDrug()//画面全体ボタンから呼び出し
    {
        if (state != TowerBattleState.preparation) return;
        ChangeState(TowerBattleState.preparation);
        mousePos = Input.mousePosition;
        putPos = Camera.main.ScreenToWorldPoint((mousePos + zDistance));
        putObj.transform.position = new Vector3(putPos.x, nowHeight + 0.6f, putPos.z);
        Debug.Log("");
    }

    public void PutObjSpin()//ボタンからの呼び出し
    {
        if (state != TowerBattleState.preparation) return;
        putObj.GetComponent<Animator>().SetTrigger("Spin");
    }


    public void OnScreenUp()
    {
        if (state != TowerBattleState.preparation) return;
        if (putObj == null) { Debug.Log("Objnull"); return; }
        putObj.GetComponent<PutObj>().Put();
        putObj.GetComponent<Animator>().enabled = false;
        stuckObj.Push(putObj);
        ChangeState(TowerBattleState.put);
    }

    void SetNextObj()
    {
        if (putObj == null)
        {
            createObjPos = new Vector3(0, nowHeight + 0.5f, 0.82f);
            putObj = Instantiate(putObjList[Random.Range(0, putObjList.Length)], createObjPos, Quaternion.Euler(new Vector3(0, 0, 0)));
            uiManeger.SetObjNameText(putObj.name);
            scoreManeger.AddScore();
            uiManeger.SetScoreText(scoreManeger.PutScore.ToString());
        }
    }

    public void CheckHeight()
    {
        if (nowHeight <= putObj.transform.position.y)
        {
            nowHeight = putObj.transform.position.y;
            StartCoroutine(UpCameraPos(nowHeight));
        }
        putObj = null;
        ChangeState(TowerBattleState.wait);
    }

    private IEnumerator UpCameraPos(float upPos)
    {
        float firstPos = Camera.main.transform.position.y;
        while (Camera.main.transform.position.y + (upPos - firstPos) * Time.deltaTime * 0.001f <= upPos)
        {
            Camera.main.transform.position += new Vector3(0, (upPos - firstPos) * Time.deltaTime * 0.001f, 0);
        }
        Camera.main.transform.position = new Vector3(0, upPos, 0);
        yield return null;
    }

    private void ResetGame()
    {
        //Reset処理 stuckObjの削除
    }
}