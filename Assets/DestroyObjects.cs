using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public ParticleSystem DestructionEffect;
    [SerializeField]
    public float destroyTime = 7f;
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
    public void Explode()
     {
        //Instantiate our one-off particle system
        ParticleSystem explosionEffect = Instantiate(DestructionEffect);
        explosionEffect.transform.position = transform.position;
        //play it
        explosionEffect.Play();
        //destroy the particle system when its duration is up, right
        //it would play a second time.
        Destroy(explosionEffect.gameObject, explosionEffect.main.duration);
        //destroy our game object
        Destroy(gameObject);
     
     }
}
