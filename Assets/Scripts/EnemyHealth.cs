using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitpoints = 100f;

    //create a public method reducing hitpoints by amt of dmg

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken"); //calls method on 
        hitpoints -= damage;
        if (hitpoints <= 0)
        {
            Destroy(gameObject); //script is placed on enemy gameObject
        }
    }
}
