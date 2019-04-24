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
        gameManeger = GameObject.Find("GameManeger").GetComponent<GameManeger>();
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
            gameManeger.CheckHeight();
            isPut = true;
        }
        if (other.transform.tag == "fall")
        {
            //おちたあ
        }
    }
}
