using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject robot;

    private bool robotSelected;
    private Outline robotOutline;

    private int range = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        robotSelected = false;
        robotOutline = GameObject.FindGameObjectWithTag("Player").GetComponent<Outline>();
        
        range = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.GetCurrentHex() != null)
        {
            LevelManager.Instance.SelectHexInRange(range);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Click on the robot
                    if (hitInfo.collider.CompareTag("Player") && LevelManager.Instance.GetAction() > 0)
                    {
                        //robot deselected
                        if (robotSelected)
                        {
                            robotSelected = false;
                            robotOutline.enabled = false;
                            range = -1;


                        }
                        //robot selection
                        else
                        {
                            robotSelected = true;
                            robotOutline.enabled = true;
                            range = LevelManager.Instance.GetAction();
                        }
                    }

                    //Click on a hex
                    if (hitInfo.collider.CompareTag("Hex"))
                    {
                        //Moves only if the robot is selected, the clicked hex is a neighbor and there are actions left
                        if (robotSelected && hitInfo.collider.GetComponent<HexInteractions>().IsClickable())
                        {
                            LevelManager.Instance.MoveToLocation(hitInfo.transform.position);
                            robotSelected = false;
                            robotOutline.enabled = false;
                            range = -1;
                            
                        }
                    }

                }
            }
        }
        
        
    }
    
    
    //Is used for correct calculation of hex in range in case of movement
    public bool RobotSelected()
    {
        return robotSelected;
    }
    
    
    //Setter for hex range
    //-1 for no hex selection
    public void SetRange(int r)
    {
        range = r;
    }
    
}
