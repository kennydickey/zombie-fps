using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    //general ammo for every weapon
    //[SerializeField] int ammoAmount = 10;

    //array of type AmmoSlot called ammoSlots that show in inspector
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable] //show content that belongs to this class in inspector
    //class within a class!
    private class AmmoSlot //only accesible to ammo
    {
        public AmmoType ammoType; //public and therefore accessible but only by ammo
        public int ammoAmount;
    }

    // method says: whenever you call me, I need to know what ammo type you're talking about
    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }


    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        //if nothing is returned..
        return null;
    }
}
