using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    private static int health = 6;

    private static int maxHealth = 6;

    private static float movementSpeed = 5;

    private static float fireRate = 0.5f;

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;
        OnPlayerDamaged?.Invoke();

        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
        OnPlayerDamaged?.Invoke();
    }

    public static void KillPlayer()
    {

    }
}
