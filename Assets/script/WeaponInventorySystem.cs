using System.Collections.Generic;
using UnityEngine;

public class WeaponInventorySystem : MonoBehaviour
{
    [SerializeField]
    private WeaponBase PrimariWeapon;
    [SerializeField]
    private WeaponBase SecondaryWeapon;
    [SerializeField]
    private WeaponBase MeleeWeapon;
    private WeaponBase CurrentWeapon;
    private ItemBase CurrentItem;
    [SerializeField]
    private Transform PosHand;
    [SerializeField]
    private ItemBase Item1;
    [SerializeField]
    private ItemBase Item2;
    [SerializeField]
    private ItemBase Item3;
    [SerializeField]
    private ItemBase Item4;
    private float raycastDistance = 5f;

    private void Start() {
        PrimariWeapon = null;
        SecondaryWeapon = null;
        CurrentWeapon = null;
        MeleeWeapon = null;
        if(PrimariWeapon != null) 
        {
            PrimariWeapon.gameObject.SetActive(false);
        }
        if(SecondaryWeapon != null) 
        {
            SecondaryWeapon.gameObject.SetActive(false);
        }
        if(MeleeWeapon != null) 
        {
            MeleeWeapon.gameObject.SetActive(false);
        }
        
        
    }
    private void Update() {
       if (Input.GetKeyDown(KeyCode.Alpha1))
       {
            SwitchToPrimaryWeapon();
       } else if (Input.GetKeyDown(KeyCode.Alpha2)) 
       {
            SwitchToSecondaryWeapon();
       } else if (Input.GetKeyDown(KeyCode.Alpha3)) 
       {
            SwitchToMeleeWeapon();
       } else if (Input.GetKeyDown(KeyCode.Alpha4)) 
       {
            SwitchToItem1();
       } else if (Input.GetKeyDown(KeyCode.Alpha5)) 
       {
            SwitchToItem2();
       } else if (Input.GetKeyDown(KeyCode.Alpha6)) 
       {
            SwitchToItem3();
       } else if (Input.GetKeyDown(KeyCode.Alpha7)) 
       {
            SwitchToItem4();
       }

       if (CurrentWeapon != null && Input.GetMouseButtonDown(0)) 
       {
            if(CurrentWeapon == MeleeWeapon) 
            {
                CurrentWeapon.Attack();
            } else
            {
                CurrentWeapon.Shoot();
            }
       }
       if (CurrentItem != null && Input.GetMouseButtonDown(0)) 
       {
            CurrentItem.Use();
       }

       if(Input.GetKeyDown(KeyCode.E)) 
       {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, raycastDistance)) 
            {
                WeaponBase CompWeapon = hit.collider.GetComponent<WeaponBase>();
                ItemBase CompItem = hit.collider.GetComponent<ItemBase>();
                if(CompWeapon != null) 
                {
                    AddInInventoryWeapon(hit.collider.gameObject, CompWeapon);
                }
                if(CompItem != null) 
                {
                    AddInInventoryItem(hit.collider.gameObject, CompItem);
                }
            }
       }
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            if(CurrentWeapon != null) 
            {
                DropFromInventoryWeapon();
            } else if (CurrentItem != null) 
            {
                DropFromInventoryItem();  
            }
            
        }
    }

    private void DropFromInventoryWeapon() 
    {
        if (CurrentWeapon == null) {Debug.Log("Imposible for Drop Weapon. Weapon is Null"); return;}

        if(CurrentWeapon == PrimariWeapon) 
        {
            PrimariWeapon.gameObject.transform.SetParent(null);
            PrimariWeapon = null;
        }else if(CurrentWeapon == SecondaryWeapon) 
        {
            SecondaryWeapon.gameObject.transform.SetParent(null);
            SecondaryWeapon = null;
        }else if (CurrentWeapon == MeleeWeapon) 
        {
            MeleeWeapon.gameObject.transform.SetParent(null);
            MeleeWeapon = null;
        }

        CurrentWeapon = null; // Сброс текущего оружия после его броска.
    }
    private void DropFromInventoryItem() 
    {
        if (CurrentItem == Item1) 
        {
            Item1.gameObject.transform.SetParent(null);
            Item1 = null;
        } else if (CurrentItem == Item2) 
        {
            Item2.gameObject.transform.SetParent(null);
            Item2 = null;
        } else if (CurrentItem == Item3) 
        {
            Item3.gameObject.transform.SetParent(null);
            Item3 = null;
        } else if (CurrentItem == Item4) 
        {
            Item4.gameObject.transform.SetParent(null);
            Item4 = null;
        }
    }

    private void AddInInventoryItem(GameObject ItemObject, ItemBase CompItem) 
    {
        ItemObject.transform.SetParent(PosHand);
        ItemObject.transform.localPosition = Vector3.zero;
        ItemObject.transform.localRotation = Quaternion.identity;
        if(Item1 == null) 
        {
            Item1 = CompItem;
            Item1.gameObject.SetActive(false);
        } else if (Item2 == null) 
        {
            Item2 = CompItem;
            Item2.gameObject.SetActive(false);
        } else if (Item3 == null) 
        {
            Item3 = CompItem;
            Item3.gameObject.SetActive(false);
        } else if (Item4 == null) 
        {
            Item4 = CompItem;
            Item4.gameObject.SetActive(false);
        }
    }
    private void AddInInventoryWeapon(GameObject WeaponObject, WeaponBase CompWeapon) 
    {
        WeaponObject.transform.SetParent(PosHand);
        WeaponObject.transform.localPosition = Vector3.zero;
        WeaponObject.transform.localRotation = Quaternion.identity;
        switch (CompWeapon.stats.type) 
        {
            case WeaponType.Melee:
                if (MeleeWeapon == null) 
                {
                    MeleeWeapon = CompWeapon;
                    MeleeWeapon.gameObject.SetActive(false);
                }
                break;
            default:
                if(PrimariWeapon == null) 
                {
                    PrimariWeapon = CompWeapon;
                    PrimariWeapon.gameObject.SetActive(false);
                } else if (SecondaryWeapon == null) 
                {
                    SecondaryWeapon = CompWeapon;
                    SecondaryWeapon.gameObject.SetActive(false);
                }
                break;
        }
    }

    private void SwitchToPrimaryWeapon() 
    {
        if (CurrentWeapon != PrimariWeapon) 
        {
            if (PrimariWeapon != null) { PrimariWeapon.gameObject.SetActive(true); }
            if (SecondaryWeapon != null) { SecondaryWeapon.gameObject.SetActive(false); }
            if (MeleeWeapon != null) { MeleeWeapon.gameObject.SetActive(false); }
            if (Item1 != null) { Item1.gameObject.SetActive(false); }
            if (Item2 != null) { Item2.gameObject.SetActive(false); }
            if (Item3 != null) { Item3.gameObject.SetActive(false); }
            if (Item4 != null) { Item4.gameObject.SetActive(false); }
            
            CurrentItem = null;

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
            if (MeleeWeapon != null) { MeleeWeapon.gameObject.SetActive(false); }
            if (Item1 != null) { Item1.gameObject.SetActive(false); }
            if (Item2 != null) { Item2.gameObject.SetActive(false); }
            if (Item3 != null) { Item3.gameObject.SetActive(false); }
            if (Item4 != null) { Item4.gameObject.SetActive(false); }
            
            CurrentItem = null;

            CurrentWeapon = SecondaryWeapon;
            Debug.Log("Secon");
        }
    }
    private void SwitchToMeleeWeapon() 
    {
        if (CurrentWeapon != MeleeWeapon) 
        {
            if (PrimariWeapon != null) { PrimariWeapon.gameObject.SetActive(false); }
            if (SecondaryWeapon != null) { SecondaryWeapon.gameObject.SetActive(false); }
            if (MeleeWeapon != null) { MeleeWeapon.gameObject.SetActive(true); }
            if (Item1 != null) { Item1.gameObject.SetActive(false); }
            if (Item2 != null) { Item2.gameObject.SetActive(false); }
            if (Item3 != null) { Item3.gameObject.SetActive(false); }
            if (Item4 != null) { Item4.gameObject.SetActive(false); }

            CurrentItem = null;

            CurrentWeapon = MeleeWeapon;
            Debug.Log("Melee");
        }
    }
    private void SwitchToItem1() 
    {
        if (CurrentItem != Item1) 
        {
            if (PrimariWeapon != null) { PrimariWeapon.gameObject.SetActive(false); }
            if (SecondaryWeapon != null) { SecondaryWeapon.gameObject.SetActive(false); }
            if (MeleeWeapon != null) { MeleeWeapon.gameObject.SetActive(false); }
            if (Item1 != null) { Item1.gameObject.SetActive(true); }
            if (Item2 != null) { Item2.gameObject.SetActive(false); }
            if (Item3 != null) { Item3.gameObject.SetActive(false); }
            if (Item4 != null) { Item4.gameObject.SetActive(false); }

            CurrentWeapon = null;
            
            CurrentItem = Item1;
            Debug.Log("Item1");
        }
    }
    private void SwitchToItem2() 
    {
        if (CurrentItem != Item2) 
        {
            if (PrimariWeapon != null) { PrimariWeapon.gameObject.SetActive(false); }
            if (SecondaryWeapon != null) { SecondaryWeapon.gameObject.SetActive(false); }
            if (MeleeWeapon != null) { MeleeWeapon.gameObject.SetActive(false); }
            if (Item1 != null) { Item1.gameObject.SetActive(false); }
            if (Item2 != null) { Item2.gameObject.SetActive(true); }
            if (Item3 != null) { Item3.gameObject.SetActive(false); }
            if (Item4 != null) { Item4.gameObject.SetActive(false); }

            CurrentWeapon = null;
            
            CurrentItem = Item2;
            Debug.Log("Item2");
        }
    }
    private void SwitchToItem3() 
    {
        if (CurrentItem != Item3) 
        {
            if (PrimariWeapon != null) { PrimariWeapon.gameObject.SetActive(false); }
            if (SecondaryWeapon != null) { SecondaryWeapon.gameObject.SetActive(false); }
            if (MeleeWeapon != null) { MeleeWeapon.gameObject.SetActive(false); }
            if (Item1 != null) { Item1.gameObject.SetActive(false); }
            if (Item2 != null) { Item2.gameObject.SetActive(false); }
            if (Item3 != null) { Item3.gameObject.SetActive(true); }
            if (Item4 != null) { Item4.gameObject.SetActive(false); }

            CurrentWeapon = null;
            
            CurrentItem = Item3;
            Debug.Log("Item3");
        }
    }
    private void SwitchToItem4() 
    {
        if (CurrentItem != Item4) 
        {
            if (PrimariWeapon != null) { PrimariWeapon.gameObject.SetActive(false); }
            if (SecondaryWeapon != null) { SecondaryWeapon.gameObject.SetActive(false); }
            if (MeleeWeapon != null) { MeleeWeapon.gameObject.SetActive(false); }
            if (Item1 != null) { Item1.gameObject.SetActive(false); }
            if (Item2 != null) { Item2.gameObject.SetActive(false); }
            if (Item3 != null) { Item3.gameObject.SetActive(false); }
            if (Item4 != null) { Item4.gameObject.SetActive(true); }

            CurrentWeapon = null;
            
            CurrentItem = Item4;
            Debug.Log("Item4");
        }
    }
}