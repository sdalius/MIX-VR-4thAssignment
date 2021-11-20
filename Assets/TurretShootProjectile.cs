using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootProjectile : MonoBehaviour
{
    [SerializeField]
    public Rigidbody[] availableProjectiles;

    [SerializeField]
    public Transform tipOfTheTurret;
    [SerializeField]
    public float fireRate = 1f;
    [SerializeField]
    public AudioClip fireSound;

    private AudioSource audioSource;
    private bool bIsShooting = false;
    private bool bShotHasFired = true;
    private Rigidbody projectileInstance;
    private Rigidbody projectileToLaunch;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(bIsShooting && bShotHasFired)
            StartCoroutine(startShooting());
    }

    IEnumerator startShooting()
    {
        if (projectileToLaunch)
        {
            bShotHasFired = false;
            projectileInstance = Instantiate(projectileToLaunch, tipOfTheTurret.position, tipOfTheTurret.rotation); 
            projectileInstance.AddForce(tipOfTheTurret.up *  50f, ForceMode.Impulse);
            audioSource.PlayOneShot(fireSound);
            yield return new WaitForSeconds(fireRate);
            bShotHasFired = true;
        }
        else{
            bIsShooting = false;
        }
    }

    public void increaseFireRate()
    {
        if (fireRate >= 2.0f)
            return;
        else fireRate += 0.25f;
    }

    public void decreaseFireRate()
    {
        if (fireRate <= 0.25f)
            return;
        else fireRate -= 0.25f;
    }

    public void startFiring()
    {
        if (bIsShooting)
            bIsShooting = false;
        else bIsShooting = true;
    }

    public void setProjectile(string name){
        if (name == "bowl01")
            projectileToLaunch = availableProjectiles[0];
        else if (name == "plate01")
            projectileToLaunch = availableProjectiles[1];
        else if (name == "none")
            projectileToLaunch = null;
    }
}
