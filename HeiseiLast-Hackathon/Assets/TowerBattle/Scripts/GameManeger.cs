using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    Vector3 mousePos, zDistance = new Vector3(0, 0, 1);
    Vector3 putPos;
    [SerializeField]
    GameObject putObj;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDrag()
    {
        mousePos = Input.mousePosition;
        putPos = Camera.main.ScreenToWorldPoint((mousePos + zDistance));
        putObj.transform.position = new Vector3(putPos.x, 0.6f, putPos.z);
    }

    private void OnMouseUp()//うまくよばれてなさそう
    {
        Debug.Log("put!");
        putObj.GetComponent<PutObj>().Put();
        putObj = null;
    }
}
