using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpSO powerupSO;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.GetComponent<PlayerController>().Heal(
            powerupSO.increment, 
            powerupSO.boost
        );
    }
}
