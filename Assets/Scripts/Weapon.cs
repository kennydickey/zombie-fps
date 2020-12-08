using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] AmmoType ammoType; //Ammotype is the public enum script we created
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;

    // imperfectr fix for switching bug
    private void OnEnable() //when this instance of this class is enabled
    {
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false; //after coroutine has started
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
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
