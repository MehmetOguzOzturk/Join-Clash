using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossManager : MonoBehaviour
{
    int bossHealt;

    public Slider slider;

    private void Start()
    {
        bossHealt = 75;
        slider.maxValue = bossHealt;
    }
    public void TakeDamageFromPlayers()
    {
        bossHealt -= 1;
        slider.value = bossHealt;

        if (bossHealt==0)
        {
            gameObject.SetActive(false);
        }
    }

    
   
        
    
}
