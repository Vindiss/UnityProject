using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    [SerializeField] private Transform obstacles1;
    [SerializeField] private Transform obstacles2;
    [SerializeField] private Transform obstacles3;
    [SerializeField] private Transform obstacles4;
    [SerializeField] private float LevelWidth;
    [SerializeField] private float LevelHeight;
    [SerializeField] private int obstacleCount;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float minVerticalScale;
    [SerializeField] private float maxVerticalScale;


    public void Generate()
    {
        Vector3 position = new Vector3 (Random.value * LevelWidth,0, Random.value * LevelHeight);

        RaycastHit hitInfo;

        if (Physics.Raycast(position, Vector3.down, out hitInfo, 100, LayerMask.GetMask("Ground")))
        {
            position = new Vector3(position.x, hitInfo.point.y, hitInfo.point.z);
        }

        Instantiate<Transform>(obstacles1, position, Quaternion.identity, transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
