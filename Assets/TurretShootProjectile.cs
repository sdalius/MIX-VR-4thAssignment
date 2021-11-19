using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootProjectile : MonoBehaviour
{
    [SerializeField]
    public Rigidbody projectile;

    [SerializeField]
    public Transform barrelEnd;
    [SerializeField]
    public float fireRate = 10f;
    [SerializeField]
    public AudioClip fireSound;

    private AudioSource audioSource;
    private bool bIsShooting = false;
    private bool bShotHasFired = true;

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
        bShotHasFired = false;
        Rigidbody projectileInstance;

        projectileInstance = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation); 
        projectileInstance.AddForce(barrelEnd.up *  50f, ForceMode.Impulse);
        audioSource.PlayOneShot(fireSound);
        yield return new WaitForSeconds(fireRate);
        bShotHasFired = true;
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
}
