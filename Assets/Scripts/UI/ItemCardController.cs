﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardController : MonoBehaviour
{
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            Destroy(gameObject);
        }
    }
}
