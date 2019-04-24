using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutObj : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    GameManeger gameManeger;
    bool isPut, heightCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
        isPut = false;
        heightCheck = false;
        gameManeger = GameObject.Find("GameManeger").GetComponent<GameManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x <= 0.0001f && rb.velocity.y <= 0.0001f && isPut && !heightCheck)
        {
            gameManeger.CheckHeight();
            heightCheck = true;
        }
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
        }
        if (other.transform.tag == "fall")
        {
            //おちたあ
        }
    }
}
