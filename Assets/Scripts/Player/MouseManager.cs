using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Movement robot;

    private bool robotSelected;
    
    // Start is called before the first frame update
    void Start()
    {
        robotSelected = false;
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
                //Click on the robot
                if (hitInfo.collider.CompareTag("Player"))
                {
                    var outline = hitInfo.collider.GetComponent<Outline>();
                    //robot deselected
                    if (robotSelected)
                    {
                        robotSelected = false;
                        outline.enabled = false;

                    }
                    //robot selection
                    else
                    {
                        robotSelected = true;
                        robot = hitInfo.collider.GetComponent<Movement>();
                        outline.enabled = true;
                    }
                }

                //Click on a hex
                if (hitInfo.collider.CompareTag("Hex"))
                {
                    //Moves only if the robot is selected, the clicked hex is a neighbor and there are actions left
                    if (robotSelected && hitInfo.collider.GetComponent<Hex>().IsClickable() && GameManager.Instance.GetAction() > 0)
                    {
                        robot.MoveToLocation(hitInfo.transform.position);
                        GameManager.Instance.SetAction(-1);
                    }
                }
                
            }
        }
    }
}
