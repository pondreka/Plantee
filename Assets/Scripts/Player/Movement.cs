using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    private GameObject curHex;
    private List<GameObject> neighborList = new List<GameObject>();
    private List<GameObject> oldNeighborList = new List<GameObject>();
    
    private NavMeshAgent robot;
    private void Awake()
    {
        robot = GetComponent<NavMeshAgent>();
    }

    public void MoveToLocation(Vector3 targetPoint)
    {
        robot.destination = targetPoint;
        robot.isStopped = false;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hex"))
        {
            curHex = other.gameObject;
            neighborList = other.gameObject.GetComponentInParent<HexMap>().HexNeighbors(curHex);

            for (int i = 0; i < oldNeighborList.Count; i++)
            {
                oldNeighborList[i].gameObject.GetComponent<HexInteractions>().OutlineOff();
                oldNeighborList[i].gameObject.GetComponent<HexInteractions>().NotClickable();
            }

            for (int i = 0; i < neighborList.Count; i++)
            {
                neighborList[i].gameObject.GetComponent<HexInteractions>().OutlineOn();
                neighborList[i].gameObject.GetComponent<HexInteractions>().Clickable();
            }

            oldNeighborList = neighborList;
        }
    }
}
