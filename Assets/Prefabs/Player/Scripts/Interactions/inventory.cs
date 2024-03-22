using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Champs
    [Header("Lists")]
    [SerializeField] List<GameObject> _inventory = new List<GameObject>(); // Liste de GameObject Keys
    //[SerializeField] List<Weapon> _weapons = new List<Weapon>(); // Liste de GameObject Weapons
    [SerializeField] List<GameObject> _tools = new List<GameObject>(); // Liste de GameObject Tools

    public List<GameObject> itemInventory { get => _inventory; set => _inventory = value; }
    //public List<Weapon> Weapons { get => _weapons; set => _weapons = value; }

    //private int selectedWeaponIndex = 0; // Index de l'arme actuellement sélectionnée
    //public int SelectedWeaponIndex
    //{
    //    get => selectedWeaponIndex;
    //    set => selectedWeaponIndex = Mathf.Clamp(value, 0, Weapons.Count - 1);
    //}
    #endregion
    #region Unity LifeCycle
    #endregion
    #region Methods ITEMS
    public void AddItem(GameObject item)
    {
        //Testing max item in list
        var count = itemInventory.Count;
        if (count > 3)
        {
            Debug.Log($"Votre inventaire est plein !");
            string error = $"Vous avez atteinds le nombre maximal de clés - nb keys{count}";
        }
        else
        {
            itemInventory.Add(item);
        }   
    }
    public void RemoveItem(GameObject item)
    {
        itemInventory.Remove(item);
    }
    public bool HasItem(GameObject item)
    {
        return itemInventory.Contains(item);
    }
    #endregion
    #region Methods Weapons
    //public void AddWeapons(Weapon weapon)
    //{
    //    //throw new NotImplementedException();
    //    //Testing max item in list
    //    var count = Weapons.Count;
    //    if (count > 5)
    //    {
    //        Debug.Log($"Votre inventaire est plein !");
    //        string error = $"Vous avez atteinds le nombre maximal d'armes - nb Weapons{count}";
    //    }
    //    else
    //    {    
    //        int weaponIndex = weapon.GetWeaponIndex();
    //        // Vérifier si l'arme avec le même index existe déjà dans l'inventaire
    //        if (!HasWeaponWithIndex(weaponIndex))
    //        {
    //            Weapons.Add(weapon);
    //        }
    //        else
    //        {
    //            weapon.ReloadAmmo(weapon.MaxAmmo);
    //        }
    //    }
    //}
    //public void RemoveWeapons(Weapon weapon)
    //{
    //    //throw new NotImplementedException();
    //    Weapons.Remove(weapon);
    //}
    //public bool HasWeapons(Weapon weapon)
    //{
    //    //throw new NotImplementedException();
    //    return Weapons.Contains(weapon);
    //}
    //public bool HasWeaponWithIndex(int weaponIndex)
    //{
    //    foreach (var weapon in Weapons)
    //    {
            
    //        if (weapon.GetWeaponIndex() == weaponIndex)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    #endregion
    #region Methods Tools
    public void AddTools(GameObject Tool)
    {
        throw new NotImplementedException();
    }
    public void RemoveTools(GameObject Tool)
    {
        throw new NotImplementedException();
    }
    public bool HasTools(GameObject Tool)
    {
        throw new NotImplementedException();

    }
    #endregion
    #region Coroutines
    #endregion
}