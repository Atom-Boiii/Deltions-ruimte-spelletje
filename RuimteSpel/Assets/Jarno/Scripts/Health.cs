using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;

    public GameObject Camera;

    private float currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(float damageamount)
    {
        currentHealth -= damageamount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            StartCoroutine(Death());
        }
    }

    public IEnumerator Death()
    {
        Camera.transform.parent = null;
        yield return new WaitForSeconds(5);
        //  DeathScreen.SetActive(true);
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