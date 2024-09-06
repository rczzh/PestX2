using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle, Follow, Attack, Die
};

public class EnemyController : MonoBehaviour
{
    GameObject player;

    public EnemyState currState = EnemyState.Idle;

    public float range;
    public float speed;
    public float health;
    public float attackingRange;
    public float coolDown;

    private bool dead = false;
    private bool coolDownAttack = false;


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
            case EnemyState.Attack: Attack(); break;
            case EnemyState.Die: Die(); break;
        }

        if (IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Idle;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackingRange)
        {
            currState = EnemyState.Attack;
        }

        if (health <= 0)
        {
            currState = EnemyState.Die;
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

    void Attack()
    {
        if (!coolDownAttack)
        {
            GameController.DamagePlayer(1);
            StartCoroutine(CoolDown());
        }
        
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= attackingRange)
            {
                Debug.Log("Collision");
                currState = EnemyState.Attack;
            }
        }
    }
}
