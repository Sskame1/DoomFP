using System.Collections.Generic;
using UnityEngine;

public class WeaponInventorySystem : MonoBehaviour
{
    [SerializeField]
    private WeaponBase PrimariWeapon;
    [SerializeField]
    private WeaponBase SecondaryWeapon;
    private WeaponBase CurrentWeapon;
    [SerializeField]
    private Transform PosHand;
    private float raycastDistance = 5f;

    private void Start() {
        PrimariWeapon = null;
        SecondaryWeapon = null;
        CurrentWeapon = null;
        if(PrimariWeapon != null) 
        {
            PrimariWeapon.gameObject.SetActive(false);
        }
        if(SecondaryWeapon != null) 
        {
            SecondaryWeapon.gameObject.SetActive(false);
        }
        
    }
    private void Update() {
       if (Input.GetKeyDown(KeyCode.Alpha1))
       {
            SwitchToPrimaryWeapon();
       } else if (Input.GetKeyDown(KeyCode.Alpha2)) 
       {
            SwitchToSecondaryWeapon();
       }

       if (CurrentWeapon != null && Input.GetMouseButtonDown(0)) 
       {
            CurrentWeapon.Shoot();
       }

       if(Input.GetKeyDown(KeyCode.E)) 
       {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, raycastDistance)) 
            {
                WeaponBase CompWeapon = hit.collider.GetComponent<WeaponBase>();
                if(CompWeapon != null) 
                {
                    AddInInventory(hit.collider.gameObject, CompWeapon);
                }
            }
       }
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            DropFromInventory();
        }
    }

    private void DropFromInventory() {
        if(CurrentWeapon == PrimariWeapon) 
        {
            PrimariWeapon.gameObject.transform.SetParent(null);
            PrimariWeapon = null;
        }else if(CurrentWeapon == SecondaryWeapon) 
        {
            SecondaryWeapon.gameObject.transform.SetParent(null);
            SecondaryWeapon = null;
        }
    }

    private void AddInInventory(GameObject WeaponObject, WeaponBase CompWeapon) 
    {
        WeaponObject.transform.SetParent(PosHand);
        WeaponObject.transform.localPosition = Vector3.zero;
        WeaponObject.transform.localRotation = Quaternion.identity;
        if(PrimariWeapon == null) 
        {
            PrimariWeapon = CompWeapon;
        } else if (SecondaryWeapon == null) 
        {
            SecondaryWeapon = CompWeapon;
        }
    }

    private void SwitchToPrimaryWeapon() 
    {
        if (CurrentWeapon != PrimariWeapon) 
        {
            if (PrimariWeapon != null) { PrimariWeapon.gameObject.SetActive(true); }
            if (SecondaryWeapon != null) { SecondaryWeapon.gameObject.SetActive(false); }
            CurrentWeapon = PrimariWeapon;
            Debug.Log("Prim");
        }
    }
    private void SwitchToSecondaryWeapon() 
    {
        if (CurrentWeapon != SecondaryWeapon) 
        {
            if (PrimariWeapon != null) { PrimariWeapon.gameObject.SetActive(false); }
            if (SecondaryWeapon != null) { SecondaryWeapon.gameObject.SetActive(true); }
            CurrentWeapon = SecondaryWeapon;
            Debug.Log("Secon");
        }
    }
}