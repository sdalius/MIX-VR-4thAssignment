using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    [SerializeField]
    public float destroyTime = 7f;
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
