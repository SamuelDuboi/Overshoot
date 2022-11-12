using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Gauge GaugeTeam1;
    public Gauge GaugeTeam2;

    public List<MyController> Team1;
    public List<MyController> Team2;

    public WeaponType Ideal;
    public Ammo Ammo;
    public GameObject weeaponPrefab;
    public GameObject idealWeapon;
    
    public static GameManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void GiveIdealWeapon(Gauge gauge)
    {
        foreach (var player in gauge == GaugeTeam1 ? Team1 : Team2)
        {
            GameObject newWeapon = Instantiate(weeaponPrefab, transform.position + transform.right * 5.0f, Quaternion.identity);
        
            Weapon weaponScript = newWeapon.AddComponent<Weapon>();
            weaponScript.Init(Ammo, Ideal);
            Instantiate(idealWeapon, newWeapon.transform);
            player.OnTriggerStay(newWeapon.GetComponent<Collider>());
        }
    }
}
