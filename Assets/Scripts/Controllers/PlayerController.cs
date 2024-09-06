using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public float speed;
    new Rigidbody2D rigidbody;
    private Vector2 moveDirection;

    public GameObject bulletPrefab;
    public float shotSpeed;
    private float lastFire;
    public float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
    }

    // Update is called once per frame
    void Update()
    {
        fireDelay = GameController.FireRate;
        speed = GameController.MovementSpeed;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical); ;
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        float shootHor = Input.GetAxisRaw("ShootHorizontal");
        float shootVert = Input.GetAxisRaw("ShootVertical");

        //animator.SetFloat("Horizontal", shootHor);
        //animator.SetFloat("Vertical", shootVert);

        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rigidbody.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * shotSpeed : Mathf.Ceil(x) * shotSpeed,
            (y < 0) ? Mathf.Floor(y) * shotSpeed : Mathf.Ceil(y) * shotSpeed,
            0
        );
    }
}
