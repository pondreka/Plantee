using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private Movement selectedRobot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    if (selectedRobot != null)
                    {
                        selectedRobot = null;
                    }
                    else
                    {
                        selectedRobot = hitInfo.collider.GetComponent<Movement>();
                    }
                }

                if (hitInfo.collider.CompareTag("Hex"))
                {
                    if (selectedRobot != null)
                    {
                        selectedRobot.SetDestination(hitInfo.transform.position);
                    }
                }
                
            }
        }
    }
}
