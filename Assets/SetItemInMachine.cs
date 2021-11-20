using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItemInMachine : MonoBehaviour
{
    private TurretShootProjectile fireMachine;

    // Start is called before the first frame update
    void Start()
    {
        fireMachine = FindObjectOfType<TurretShootProjectile>();
    }
    void OnCollisionEnter(Collision collision)
    {
        var placeObjectInMachine = collision.gameObject.GetComponent<Rigidbody>();

        if (placeObjectInMachine)
            Debug.Log("Got an item!" + placeObjectInMachine);
            fireMachine.setProjectile(placeObjectInMachine.name);
    }
    
    void OnCollisionExit(Collision collision)
    {
        var placeObjectInMachine = collision.gameObject.GetComponent<Rigidbody>();

        if (placeObjectInMachine)
            fireMachine.setProjectile("none");
    }
}
