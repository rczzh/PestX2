using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    AudioController audioController;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            audioController.PlaySFX(audioController.bulletHit);
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(GameController.Damage);
            Destroy(gameObject);
        }

        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        } 

        if (collision.tag == "Boss")
        {
            audioController.PlaySFX(audioController.bulletHit);
            collision.gameObject.GetComponent<Boss>().TakeDamage(GameController.Damage);
            Destroy(gameObject);
        }
    }
}
