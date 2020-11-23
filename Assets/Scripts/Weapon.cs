using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        // from, direction, distance
        Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range); //returns bool, did you hit yes/no
        Debug.Log("i hit this thing: " + hit.transform.name);
    }
}
