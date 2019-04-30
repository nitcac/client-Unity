using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutObj2d : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    GameManeger gameManeger;
    bool isPut;

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0;
        isPut = false;
        gameManeger = GameObject.Find("Maneger").GetComponent<GameManeger>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Put()
    {
        rb.gravityScale = 1f;
    }
    private void OnCollisionEnter2D(Collision2D other)
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
