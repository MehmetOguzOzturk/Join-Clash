using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponManager : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("member"))
        {
            other.gameObject.GetComponent<StickmanManager>().StickmanTakeDamage();
        }
    }
}
