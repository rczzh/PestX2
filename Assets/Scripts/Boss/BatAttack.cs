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
        GameObject bullet = Instantiate(enemyBulletPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<EnemyBullet>().bulletSpeed = bulletSpeed;

        //targets player
        Vector3 vectorToPlayer = gameObject.transform.position - player.transform.position;
        Vector3 rotateVectorToPlayer = Quaternion.Euler(0, 0, 90) * vectorToPlayer;
        bullet.transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotateVectorToPlayer);
    }
}
