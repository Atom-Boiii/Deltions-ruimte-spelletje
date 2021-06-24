using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public ParticleSystem[] shootEffects;
    public ParticleSystem[] shootEffects2;

    public float damage = 20;
    public float miningDamage = 20;

   public float shootDistance = 50;
    public Transform shootPoint;

    public Transform spherePoint;
    public Transform spherePoint2;

    public GameObject secondTurret;

    public LayerMask enemyLayer;

    public LineRenderer lr;
    public LineRenderer lr2;

    public LayerMask mineralLayer;
    public float mineDistance = 10f;
   

   
    public float secondsBetweenShots = 0.5f;
    public float secondsBetweenMine = 0.5f;


    private float _Timer;
    private float nextTimeToFire;

    private void Start()
    {
        if (PlayerPrefs.GetString("HasSecondMining") == "true")
        {
            spherePoint2.gameObject.SetActive(true);
            lr2.enabled = true;
        }
        else
        {
            spherePoint2.gameObject.SetActive(false);
            lr2.enabled = false;
        }

        if (PlayerPrefs.GetString("HasSecondShooting") == "true")
        {
            secondTurret.gameObject.SetActive(true);
            for (int i = 0; i < shootEffects2.Length; i++)
            {
                shootEffects2[i].gameObject.SetActive(true);
            }
        }
        else
        {
            secondTurret.gameObject.SetActive(false);
            for (int i = 0; i < shootEffects2.Length; i++)
            {
                shootEffects2[i].gameObject.SetActive(false);
            }
        }

        secondsBetweenMine = PlayerPrefs.GetFloat("MiningRate");
        miningDamage = PlayerPrefs.GetFloat("MiningDamage");

        damage = PlayerPrefs.GetFloat("ShootDamage");
        secondsBetweenShots = PlayerPrefs.GetFloat("ShootRate");
        shootDistance = PlayerPrefs.GetFloat("ShootDistance");
    }

    void Update()
    {
        lr.SetPosition(0, spherePoint.position);
        lr2.SetPosition(0, spherePoint2.position);

        if (Input.GetMouseButton(0))
        {
             if (Time.time >= nextTimeToFire)
             {
                nextTimeToFire = Time.time + 1f / secondsBetweenShots;
                Shoot();
             }
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit _hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out _hit, mineDistance * 1000, mineralLayer))
            {
                lr.SetPosition(1, _hit.point);
                lr2.SetPosition(1, _hit.point);
            }
            else
            {
                lr.SetPosition(1, spherePoint.position);
                lr2.SetPosition(1, spherePoint2.position);
            }

            if (Time.time >= _Timer)
            {
                _Timer = Time.time + 1f/ secondsBetweenMine;
                Mine();
            }
            
        }
        else
        {
            lr.SetPosition(1, spherePoint.position);
            lr2.SetPosition(1, spherePoint2.position);
        }
    }
    void Shoot()
    {
        AudioHandler.AUDIO.PlayTrack("LaserShoot");

        for (int i = 0; i < shootEffects.Length; i++)
        {
            shootEffects[i].Play();
            
        }

        for (int i = 0; i < shootEffects2.Length; i++)
        {
            shootEffects2[i].Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, shootPoint.forward, out hit, shootDistance * 10, enemyLayer))
        {
            Debug.Log("I hit " + hit.transform.name);
            hit.transform.GetComponent<EnemyShip>().TakeDamage(damage);
        }
                
    }
    void Mine()
    {
        RaycastHit hit;
        bool checkhit = false;
        if (Physics.Raycast(transform.position, shootPoint.forward, out hit, mineDistance * 1000, mineralLayer))
        {
            if (hit.transform.GetComponent<Astroid>() != null)
            {
                hit.transform.GetComponent<Astroid>().TakeDamage(miningDamage);
                AudioHandler.AUDIO.PlayTrack("MiningSound");
            }
            lr.enabled = true;
            checkhit = true;
            lr.SetPosition(1, hit.point);
        }
        
        if(!checkhit)
        {
            lr.enabled = false;
        }
    }
}