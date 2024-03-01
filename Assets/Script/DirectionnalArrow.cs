using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionnalArrow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.LookAt(target);
        Vector3 relativepos = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativepos, Vector3.up) * Quaternion.Euler(0, 92, 0);
    }
}
