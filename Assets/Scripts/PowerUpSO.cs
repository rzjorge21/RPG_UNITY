using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/Powerups")]
public class PowerUpSO : ScriptableObject 
{
    public string powerUpName;
    public float increment;
    public bool boost;
}
