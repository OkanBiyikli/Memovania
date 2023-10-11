using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth, maxHealth;

    public float invincibilityLength;
    private float invincCounter;

    public float flashLenght;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    public static PlayerHealthController instance;

     private void Awake() 
    {
        if(instance == null)
        {
            instance = this;    
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }
    

    void Start()
    {
        currentHealth = maxHealth;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                foreach(SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;//açıksa kapat kapalıysa aç gibi bir durum
                }
                flashCounter = flashLenght;
            }

            if(invincCounter <= 0)//eğer dokunulmazlığımız bittiyse
            {
                foreach(SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;//dokunulmazlığımız bittiğinde sprite açık olcak
                }
                flashCounter = 0f;
            }
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if(invincCounter <= 0)
        {

            currentHealth -= damageAmount;

            if(currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false);//for now

                RespawnController.instance.Respawn();

                AudioManager.instance.PlaySFX(8);

            }
            else
            {
                invincCounter = invincibilityLength;

                AudioManager.instance.PlaySFXAdjusted(11);
            }
            
            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
