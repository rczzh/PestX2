using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    private static int currentLevel = 1;

    private static int health = 6;

    private static int maxHealth = 6;

    private static float movementSpeed = 5;

    private static float fireRate = 0.5f;

    private static float bulletSize = 0.5f;

    private static float damage = 1f;

    public List<String> itemsCollected = new List<String>();

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }
    public static float Damage { get => damage; set => bulletSize = damage; }

    private void Awake()
    {
        instance = this;
        // reset player

        health = 6;

        maxHealth = 6;

        movementSpeed = 5;

        fireRate = 0.5f;

        bulletSize = 0.5f;

        damage = 1f;

        currentLevel = 1;

        itemsCollected = new List<String>();
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

    public static void MovementSpeedChange(float amount)
    {
        movementSpeed += amount;
    }

    public static void FireRateChange(float amount)
    {
        fireRate -= amount;
    }

    public static void BulletSizeChange(float amount)
    {
        BulletSize += amount;
    }

    public static void DamageChange(float amount)
    {
        Damage += amount;
    }

    public void UpdateItemsCollected(CollectionController item)
    {
        itemsCollected.Add(item.item.name);

        // item synergies
        if (itemsCollected.Contains("Monkeypox") && itemsCollected.Contains("Vaccine") && itemsCollected.Contains("Steroids"))
        {
            FireRateChange(0.1f);
        }
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel > 3)
        {
            GameController.instance.transform.GetComponent<UIMenues>().victory = true;
        } 
        else
        {
            StartCoroutine(Reset());
        }
        
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);

        // restart level
        GameObject roomManager = GameObject.FindGameObjectWithTag("RoomManager");
        roomManager.GetComponent<RoomManager>().RegenerateRooms();
        // respawn player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector2(0 + 0.5f, 0 + 0.5f);
        CameraController.instance.ResetCamera();
    }

    public static void KillPlayer()
    {
        GameController.instance.transform.GetComponent<UIMenues>().playerIsDead = true;
    }
}
