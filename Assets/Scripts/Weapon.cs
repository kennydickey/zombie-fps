using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot; //places ammo.cs in a serializeFiled in weapon insp
                                    //furthermore, Ammo.cs is on player so we can monitor ammo amount in player context
                                    //ammoSlot is Ammo pretty much
    [SerializeField] float timeBetweenShots = 0.5f;

    bool canShoot = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false; //after coroutine has started
        if (ammoSlot.GetCurrentAmmo() > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true; //after time specified, firing may continue
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        // from, direction, info output I think, distance
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) //returns bool, did you hit yes/no
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return; //if obj does not have EnemyHealth, do nothing

            target.TakeDamage(damage); //tells EnemyHealth script to take damage when hit
        }
        else
        {
            return; //to protect from nullref errors
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        //obj, location, direction(purpendicular to surface)
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1); //destroy the fx
    }
}
