﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitpoints = 100f;

    //create a public method reducing hitpoints by amt of dmg

    public void TakeDamage(float damage)
    {
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            Debug.Log("p1 is dead");
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
