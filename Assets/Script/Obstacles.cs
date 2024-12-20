using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    [SerializeField] private GameObject obstacles1;
    [SerializeField] private GameObject obstacles2;
    [SerializeField] private GameObject obstacles3;
    [SerializeField] private GameObject obstacles4;
    [SerializeField] private float LevelWidth;
    [SerializeField] private float LevelHeight;
    [SerializeField] private float minDistance;
    [SerializeField] private int obstacleCount;
    [SerializeField] private int testBeforeNext;
    [SerializeField] private  GameObject material;

    List<Vector3> points = new List<Vector3>();
    public Vector3 CentreRegion;

    public void Generate()
    {
        Vector3 regionSize = new Vector3(LevelWidth , 5.75f, LevelHeight);
        CentreRegion = transform.position;

        points.Clear();
        List<Vector3> spawnPoints = PoissonDiscSampling.GeneratePoints(minDistance, regionSize, testBeforeNext, CentreRegion);

        while (spawnPoints.Count == 0)
        {
           spawnPoints = PoissonDiscSampling.GeneratePoints(minDistance, regionSize, testBeforeNext, CentreRegion);
        }

        foreach (Vector3 point in spawnPoints)
        {
            int obstacleRandom = Random.Range(1,5);
            if (points.Count < obstacleCount)
            {
                if (obstacleRandom == 1)
                {
                    GameObject obstacle = Instantiate(obstacles1, point, (Quaternion.identity * Quaternion.Euler(-90, 0, 0)));
                    obstacle.transform.SetParent(transform);
                    obstacle.GetComponent<MeshRenderer>().material = material.GetComponent<MeshRenderer>().material;
                    points.Add(point);
                }
                else if (obstacleRandom == 2)
                {
                    GameObject obstacle = Instantiate(obstacles2, point, (Quaternion.identity * Quaternion.Euler(-90, 0, 0)));
                    obstacle.GetComponent<MeshRenderer>().material = material.GetComponent<MeshRenderer>().material;
                    obstacle.transform.SetParent(transform);
                    points.Add(point);
                }
                else if (obstacleRandom == 3)
                {
                    GameObject obstacle = Instantiate(obstacles3, point, (Quaternion.identity * Quaternion.Euler(-90, 0, 0)));
                    obstacle.GetComponent<MeshRenderer>().material = material.GetComponent<MeshRenderer>().material;
                    obstacle.transform.SetParent(transform);
                    points.Add(point);
                }
                else if (obstacleRandom == 4)
                {
                    GameObject obstacle = Instantiate(obstacles4, point, (Quaternion.identity * Quaternion.Euler(-90, 0, 0)));
                    obstacle.GetComponent<MeshRenderer>().material = material.GetComponent<MeshRenderer>().material;
                    obstacle.transform.SetParent(transform);
                    points.Add(point);
                }
            }
            else
            {
                break;
            }
        }
    }

    void Start()
    {
        Generate();
    }

    void Update()
    {
        
    }
}
