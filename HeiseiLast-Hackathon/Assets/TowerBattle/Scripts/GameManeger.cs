using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TowerBattleState
{
    start, wait, put
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
        state = TowerBattleState.start;
        nowHeight = Camera.main.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDrag()
    {
        SetNextObj();
        mousePos = Input.mousePosition;
        putPos = Camera.main.ScreenToWorldPoint((mousePos + zDistance));
        putObj.transform.position = new Vector3(putPos.x, nowHeight + 0.6f, putPos.z);
    }

    private void OnMouseUp()
    {
        if (putObj == null) { Debug.Log("Objnull"); return; }
        putObj.GetComponent<PutObj>().Put();
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
    }

    private IEnumerator UpCameraPos(float upPos)
    {
        float firstPos = Camera.main.transform.position.y;

        while (Camera.main.transform.position.y + upPos * Time.deltaTime <= upPos)
        {
            Camera.main.transform.position += new Vector3(0, (upPos - firstPos) * Time.deltaTime, 0);
        }
        Camera.main.transform.position = new Vector3(0, upPos, 0);
        yield return null;
    }
}