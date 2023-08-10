using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnPoint : MonoBehaviour
{
    public Vector3 GetSpawnPoint()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}
