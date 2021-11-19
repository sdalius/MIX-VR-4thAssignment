using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public GameObject thingToDestroy;
    void Update()
    {
        Destroy(thingToDestroy, 3.5f);
    }

}
