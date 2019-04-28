using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TowerBattleState
{
    wait, preparation, put, end, reset
}
public class GameManeger : MonoBehaviour
{
    TowerBattleState state;
    Vector3 mousePos, zDistance = new Vector3(0, 0, 1);
    Vector3 putPos;
    GameObject putObj;
    [SerializeField]
    GameObject endUIPanel;
    List<GameObject> stuckObj = new List<GameObject>();
    Rigidbody rb;
    [SerializeField]
    GameObject[] putObjList;
    Vector3 createObjPos = new Vector3(0, 4.0f, 0f), firstCreateObjPos;
    float nowHeight;
    [SerializeField]
    ScoreManeger scoreManeger;
    [SerializeField]
    UIManeger uiManeger;
    bool isDrug;
    Vector3 firstCameraPos;
    // Start is called before the first frame update

    private void Awake()
    {
        firstCreateObjPos = createObjPos;
        firstCameraPos = Camera.main.transform.position;
    }
    void Start()
    {
        Camera.main.transform.position = firstCameraPos;
        createObjPos = firstCreateObjPos;
        ChangeState(TowerBattleState.wait);
        nowHeight = Camera.main.transform.position.y;
        endUIPanel.SetActive(false);
        isDrug = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrug)
        {
            OnScreenDrug();
        }
    }

    public void ChangeState(TowerBattleState changeState)
    {
        state = changeState;
        if (state == TowerBattleState.end)
        {
            endUIPanel.SetActive(true);
            StopCoroutine("UpCameraPos");
            Time.timeScale = 0;
        }
        if (state == TowerBattleState.reset)
        {
            if (stuckObj.Count > 0)
            {
                foreach (var item in stuckObj)
                {
                    Destroy(item.gameObject);
                    scoreManeger.ResetScore();
                }
            }
            stuckObj = new List<GameObject>();
            Destroy(putObj.gameObject);
            putObj = null;
            Start();
        }
        if (state == TowerBattleState.wait)
        {
            SetNextObj();
            ChangeState(TowerBattleState.preparation);
        }
    }

    public void OnScreenPut()//画面全体ボタンから呼び出し
    {
        isDrug = true;
        Debug.Log("ositayo");
    }

    private void OnScreenDrug()//isDrugがTrueのときに呼び出し
    {
        if (state != TowerBattleState.preparation) return;
        ChangeState(TowerBattleState.preparation);
        mousePos = Input.mousePosition;
        putPos = Camera.main.ScreenToWorldPoint((mousePos + zDistance));
        putObj.transform.position = new Vector3(putPos.x, nowHeight + 4.0f, putPos.z);
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
        putObj.GetComponent<PutObj2d>().Put();
        putObj.GetComponent<Animator>().enabled = false;
        stuckObj.Add(putObj);
        ChangeState(TowerBattleState.put);
        isDrug = false;
    }

    void SetNextObj()
    {
        if (putObj == null)
        {
            createObjPos = new Vector3(0, nowHeight + 4.0f, 0f);
            putObj = Instantiate(putObjList[Random.Range(0, putObjList.Length)], createObjPos, Quaternion.Euler(new Vector3(0, 0, 0)));
            uiManeger.SetObjNameText(putObj.name);
            uiManeger.SetScoreText(scoreManeger.PutScore.ToString());
        }
    }

    public void CheckHeight()
    {
        scoreManeger.AddScore();
        for (int i = 0; i < stuckObj.Count; i++)
        {
            if (nowHeight <= stuckObj[i].transform.position.y)
            {
                nowHeight = stuckObj[i].transform.position.y;
            }
        }
        if (nowHeight <= putObj.transform.position.y)
        {
            nowHeight = putObj.transform.position.y;
        }
        StartCoroutine(UpCameraPos(nowHeight));
        putObj = null;
        ChangeState(TowerBattleState.wait);
    }

    private IEnumerator UpCameraPos(float upPos)
    {
        float beforePos = Camera.main.transform.position.y;
        while (Camera.main.transform.position.y + (upPos - beforePos) * Time.deltaTime <= upPos)
        {
            Camera.main.transform.position += new Vector3(0, (upPos - beforePos) * Time.deltaTime, 0);
            yield return null;
        }
        Camera.main.transform.position = new Vector3(0, upPos, -10);
        yield break;
    }
}