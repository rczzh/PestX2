using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttack : MonoBehaviour
{
    private GameObject player;
    private Vector2 targetPos;
    public float chargeSpeed = 10;
    private bool isAttacking = false;
    private bool onCoolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, chargeSpeed * Time.deltaTime);
            if (Vector2.Distance(player.transform.position, transform.position) <= 1)
            {
                if (!onCoolDown)
                {
                    onCoolDown = true;
                    GameController.DamagePlayer(1);
                }
            }
        }
        else
        {
            isAttacking = false;
        }
        
    }

    public void Attack()
    {
        isAttacking = true;
        onCoolDown = false;
        targetPos = player.transform.position;
    }
}
