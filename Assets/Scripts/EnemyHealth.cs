using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitpoints = 100f;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    //create a public method reducing hitpoints by amt of dmg

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken"); //calls method on 
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            //Destroy(gameObject); //script is placed on enemy gameObject
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; //condensed line, no need for brackets
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
