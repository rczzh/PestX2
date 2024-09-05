using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle, Follow, Die
};

public class EnemyController : MonoBehaviour
{
    GameObject player;

    public EnemyState currState = EnemyState.Idle;

    public float range;
    public float speed;
    public float health;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Follow: Follow(); break;
            case EnemyState.Die: break;
        }

        if (IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Idle;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    void Idle()
    {

    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
