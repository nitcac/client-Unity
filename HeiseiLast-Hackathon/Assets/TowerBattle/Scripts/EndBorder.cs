using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBorder : MonoBehaviour
{
    [SerializeField]
    GameManeger gameManger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "")
        {
            gameManger.ChangeState(TowerBattleState.end);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PutObj")
        {
            gameManger.ChangeState(TowerBattleState.end);
        }
    }
}
