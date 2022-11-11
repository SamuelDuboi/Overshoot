using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoScriptable", menuName = "ScriptableObjects/AmmoData", order = 1)]
public class AmmoScriptable : ScriptableObject
{
    public float damage;
    public GameObject ammoType;

}
