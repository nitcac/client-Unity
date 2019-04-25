using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutObj : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    GameManeger gameManeger;
    bool isPut;

    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
        isPut = false;
        gameManeger = GameObject.Find("Maneger").GetComponent<GameManeger>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Put()
    {
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isPut)
        {
            isPut = true;
            Invoke("CallCheckHeight", 0.5f);
        }
        if (other.transform.tag == "fall")
        {
            //おちたあ
        }
    }

    private void CallCheckHeight()
    {
        gameManeger.CheckHeight();
    }
}
