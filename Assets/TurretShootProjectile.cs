using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    // bIsShooting stands for if machine is allowed to shoot
    private bool bIsShooting = false;
    // bShotHasFired - Check if turret has shot the shot
    private bool bShotHasFired = true;
    // projectileInstance - projectile which is spawned on the shot.
    private Rigidbody projectileInstance;
    // projectileToLaunch - Projectile which is set from array, and deployed to be ready to be spawned.
    private Rigidbody projectileToLaunch;

    // platesShot - increase the number for shot plates.
    private TextMeshProUGUI  platesShot;
    private int numOfPlatesShot = 0;

    [SerializeField] 
    public float shotPower = 50;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        platesShot = GameObject.FindGameObjectWithTag("PlatesShot").GetComponent<TextMeshProUGUI>();
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
            projectileInstance.AddForce(tipOfTheTurret.up *  shotPower, ForceMode.Impulse);
            audioSource.PlayOneShot(fireSound);
            numOfPlatesShot++;
            platesShot.text = "Launched plates: " + numOfPlatesShot;
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

    public void clearNumOfShotPlates()
    {
        numOfPlatesShot = 0;
        platesShot.text = "Launched plates: " + numOfPlatesShot;
    }

    public bool getbIsShooting(){
        return bIsShooting;
    }

    public void setbIsShooting(bool newbIsShooting){
        bIsShooting = newbIsShooting;
    }

    public Rigidbody getbProjectileToLaunch(){
        return projectileToLaunch;
    }
}
