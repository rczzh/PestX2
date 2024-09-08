using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    GameObject player;
    public float health;
    public bool isFlipped = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Die()
    {
        GameController.instance.NextLevel();

        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        if (enemyBullets != null)
        {
            foreach (GameObject bullet in enemyBullets)
            {
                Destroy(bullet);
            }
        }
        
        Destroy(gameObject);
    }
}
