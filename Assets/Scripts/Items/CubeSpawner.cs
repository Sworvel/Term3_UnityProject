using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    ObjectPooler op;

    void Start()
    {
        op = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        op.SpawnFromPool("Cube", transform.position, Quaternion.identity);
    }
}
