using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject deathScreen;
    public MovementController controller;

    public Slider shieldSlider;
    public Slider healthSlider;

    public float maxHealth = 100;
    public float maxShield = 100;

    public GameObject Camera;

    private float currentHealth;
    private float currenShield;
    public float shieldCooldownTimer = 20;
    public float shieldRechargeRate = 1;
    private float shieldTimer;

    private void Start()
    {
        maxShield = PlayerPrefs.GetFloat("Shield");
        maxHealth = PlayerPrefs.GetFloat("Hull");

        shieldSlider.maxValue = maxShield;
        healthSlider.maxValue = maxHealth;

        currenShield = maxShield;
        currentHealth = maxHealth;

        shieldSlider.value = currenShield;
        healthSlider.value = currentHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            DoDamage(999f);
        }

        shieldSlider.value = currenShield;
        healthSlider.value = currentHealth;

        shieldTimer += 1 * Time.deltaTime;
        if (shieldTimer >= shieldCooldownTimer)
        {
            if (currenShield < maxShield)
                currenShield += shieldRechargeRate * Time.deltaTime;
            if (currenShield > maxShield)
                currenShield = maxShield;
        }

    }

    public void DoDamage(float damageamount)
    {
        if(currenShield <= 0f)
        {
            currentHealth -= damageamount;
            shieldTimer = 0;
        }
        else if(currenShield >= 1f)
        {
            currenShield -= damageamount / 2f;
            shieldTimer = 0;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            StartCoroutine(Death());
        }
    }

    public IEnumerator Death()
    {
        Camera.transform.parent = null;
        controller.enabled = false;
        Cursor.visible = true;

        yield return new WaitForSeconds(5);

        deathScreen.SetActive(true);
        PlayerPrefs.SetInt("GreenCrystals", 0);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return GetMaxHealth();
    }
}