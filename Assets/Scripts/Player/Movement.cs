using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    private GameObject curHex;

    [SerializeField] private float movingTime = 1.2f;
    
    private NavMeshAgent robot;
    
    
    private void Awake()
    {
        robot = GetComponent<NavMeshAgent>();
    }
    

    //Starting the movement coroutine
    public void Move(Vector3 targetPoint)
    {
        StartCoroutine(MoveToLocation(targetPoint));
    }
    //Moves robot to a hex position along the shortest path 
    private IEnumerator MoveToLocation(Vector3 targetPoint)
    {
        while (TempTargetPosition(targetPoint) != targetPoint)
        {
            Vector3 tempPosition = TempTargetPosition(targetPoint);
            robot.destination = tempPosition;
            robot.isStopped = false;
            
            yield return new WaitForSeconds(movingTime);
        }

        robot.destination = targetPoint;
        robot.isStopped = false;
    }

    //Getter for the current hex
    public GameObject GetCurrentHex()
    {
        return curHex;
    }
    
    //Detection of the current hex and 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hex"))
        {
            curHex = other.gameObject; 
        }
    }

    //Calculates the position of the hex closest to the target position
    private Vector3 TempTargetPosition(Vector3 targetPosition)
    {
        List<GameObject> neighbors = LevelManager.Instance.GetHexNeighbors(curHex);
        GameObject minDistHex = curHex;
        float minDist = 10000f;

        for (int n = 0; n < neighbors.Count; n++)
        {
            float dist = Mathf.Sqrt(Mathf.Pow((targetPosition.x - neighbors[n].transform.position.x), 2f)
                                  + Mathf.Pow((targetPosition.z - neighbors[n].transform.position.z), 2));

            if (dist < minDist)
            {
                minDist = dist;
                minDistHex = neighbors[n];
            }
        }
        return minDistHex.transform.position;
    }
}
