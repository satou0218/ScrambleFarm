using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private WeaponData[] weapons;

    [Header("Current")]
    [SerializeField] private int currentWeaponIndex = 0;

    public WeaponData CurrentWeapon
    {
        get
        {
            if (weapons == null || weapons.Length == 0) return null;
            return weapons[currentWeaponIndex];
        }
    }

    public void ChangeWeapon(int index)
    {
        if (weapons == null || weapons.Length == 0) return;
        if (index < 0 || index >= weapons.Length) return;

        currentWeaponIndex = index;

        Debug.Log($"武器変更：{CurrentWeapon.weaponName}");
    }

    public void ChangeNextWeapon()
    {
        if (weapons == null || weapons.Length == 0) return;

        currentWeaponIndex++;

        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        Debug.Log($"武器変更：{CurrentWeapon.weaponName}");
    }

    public void ChangePreviousWeapon()
    {
        if (weapons == null || weapons.Length == 0) return;

        currentWeaponIndex--;

        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }

        Debug.Log($"武器変更：{CurrentWeapon.weaponName}");
    }
}