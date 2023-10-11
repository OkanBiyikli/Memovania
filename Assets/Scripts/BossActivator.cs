using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject bossToActive;

    public string bossRef;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            if(PlayerPrefs.HasKey(bossRef))
            {
                if (PlayerPrefs.GetInt(bossRef) != 1)
                {
                    bossToActive.SetActive(true);

                    gameObject.SetActive(false);
                }

            }else
            {
                bossToActive.SetActive(true);

                gameObject.SetActive(false);//bu scriptin bulunduğu
            }
        }    
    }
}
