using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionnalArrow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;

    private void Update()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.x = transform.position.x;
        transform.LookAt(targetPosition);
    }
}
