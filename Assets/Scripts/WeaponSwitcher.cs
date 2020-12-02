using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetWeaponActive();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon; //set a var first that hold val of current

        ProcessKeyInput(); 
        ProcessScrollWheel();

        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1 on keyboard 
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
    }

    private void ProcessScrollWheel()
    { 
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // up on scroll wheel
        {
            if (currentWeapon >= transform.childCount - 1) // if at maximum go back to 0
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // up on scroll wheel
        {
            if (currentWeapon <= 0) // if at maximum go back to 0
            {
                currentWeapon = transform.childCount - 1; 
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;
        //iterate through weapons and set currentWeapon to active
        foreach(Transform weapon in transform) //type of Transform called weapon in a transform
        {
            if(weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true); //set checkbox active in inspector
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++; //step index before forEach loop goes to next
        }
    }
}
