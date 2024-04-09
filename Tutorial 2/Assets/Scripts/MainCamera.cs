using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject target;

    public float cameraDistanceY;
    public float cameraDistanceZ;

    private void Update()
    {
        Vector3 vec = target.transform.position;
        transform.position = new Vector3(vec.x, vec.y + cameraDistanceY, vec.z - cameraDistanceZ);
    }
}
