using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletCollider : MonoBehaviour
{
    private SimpleShoot weaponScore;
    // Start is called before the first frame update

    private void Start() {
        weaponScore = GameObject.FindObjectOfType<SimpleShoot>();
    }
    void OnCollisionEnter(Collision other) {
        var objectsToShoot = other.gameObject.GetComponent<DestroyObjects>();

        if (objectsToShoot)
        {
            weaponScore.IncreaseShotObjects();
            objectsToShoot.Explode();
        }
    }
}
