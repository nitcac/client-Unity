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
    [SerializeField]
    GameObject putObj;
    Rigidbody rb;
    [SerializeField]
    GameObject[] putObjList;
    Vector3 objFirstPos = new Vector3(0, 0.6f, 0.82f);
    float nowHeight;
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
    }

    private void OnMouseDrag()
    {
        if (state != TowerBattleState.wait && state != TowerBattleState.preparation) return;
        ChangeState(TowerBattleState.preparation);
        SetNextObj();
        mousePos = Input.mousePosition;
        putPos = Camera.main.ScreenToWorldPoint((mousePos + zDistance));
        putObj.transform.position = new Vector3(putPos.x, nowHeight + 0.6f, putPos.z);
    }

    private void OnMouseUp()
    {
        if (state != TowerBattleState.preparation) return;
        if (putObj == null) { Debug.Log("Objnull"); return; }
        putObj.GetComponent<PutObj>().Put();
        ChangeState(TowerBattleState.put);
    }

    private void PutObjSpin()
    {
        if (state == TowerBattleState.preparation)
        {
            //回転
        }
    }

    void SetNextObj()
    {
        if (putObj == null)
        {
            putObj = Instantiate(putObjList[Random.Range(0, putObjList.Length)], objFirstPos, Quaternion.Euler(new Vector3(0, 0, 0)));
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
        while (Camera.main.transform.position.y + (upPos - firstPos) * Time.deltaTime <= upPos)
        {
            Camera.main.transform.position += new Vector3(0, (upPos - firstPos) * Time.deltaTime, 0);
        }
        Camera.main.transform.position = new Vector3(0, upPos, 0);
        yield return null;
    }
}