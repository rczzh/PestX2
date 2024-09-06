using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 1;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        print(gameObject.transform.position);
        GameObject bullet = Instantiate(enemyBulletPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<EnemyBullet>().bulletSpeed = bulletSpeed;
        bullet.transform.rotation = transform.rotation;
    }
}
