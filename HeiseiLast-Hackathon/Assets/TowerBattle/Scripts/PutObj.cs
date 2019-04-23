using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutObj : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Put()
    {
        rb.useGravity = true;
    }
}
