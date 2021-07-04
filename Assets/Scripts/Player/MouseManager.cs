using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private bool robotSelected;
    private Outline robotOutline;

    private bool cardSelected;
    private GameObject card;

    private bool hexSelected;

    private int range = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        robotSelected = false;
        robotOutline = GameObject.FindGameObjectWithTag("Player").GetComponent<Outline>();

        cardSelected = false;

        hexSelected = false;
        
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
                        //robot deselection
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

                            //card deselection
                            if (cardSelected)
                            {
                                cardSelected = false;
                                card.GetComponentInChildren<Outline>().enabled = false;
                            }

                            hexSelected = false;
                        }
                    }

                    //Click on a card
                    if (hitInfo.collider.CompareTag("Card") && hitInfo.collider.GetComponentInParent<Card>().IsOnHand())
                    {
                        
                        Outline cardOutline = hitInfo.collider.GetComponent<Outline>();
                        
                        //card deselection
                        if (cardSelected)
                        {
                            //if this card was selected
                            if (card == hitInfo.collider.transform.parent.gameObject)
                            {
                                cardSelected = false;
                                cardOutline.enabled = false;
                                range = -1;
                            }
                            
                            //if another card was selected
                            else
                            {
                                card.GetComponentInChildren<Outline>().enabled = false;
                                card = hitInfo.collider.transform.parent.gameObject;
                                cardOutline.enabled = true;
                                range = card.GetComponent<Card>().GetRange();
                            }
                            
                        }
                        
                        //card selection
                        else
                        {
                            card = hitInfo.collider.transform.parent.gameObject;
                            cardSelected = true;
                            cardOutline.enabled = true;
                            
                            //TODO: Implement the different types of range depending on the card
                            range = card.GetComponent<Card>().GetRange();

                            //robot deselection
                            if (robotSelected)
                            {
                                robotSelected = false;
                                robotOutline.enabled = false;
                            }

                            hexSelected = false;

                        }
                    }

                    //Click on trash
                    //TODO: Implement getting a random action card or tool out of the trash
                    if (hitInfo.collider.CompareTag("Trash") && hexSelected)
                    {
                        hitInfo.collider.GetComponentInParent<HexAttributes>().SetTrash(-1);
                        CardManager.Instance.NewCard(5);
                        
                        if (hitInfo.collider.GetComponentInParent<HexAttributes>().GetTrash() == 0)
                        {
                            hexSelected = false;
                            range = -1;
                        }
                    }
                    
                    //Click on a hex
                    if (hitInfo.collider.CompareTag("Hex"))
                    {
                        //Moves only if the robot is selected and the clicked hex is in action range
                        if (robotSelected && hitInfo.collider.GetComponent<HexInteractions>().IsClickable())
                        {
                            LevelManager.Instance.MoveToLocation(hitInfo.transform.position);
                            robotSelected = false;
                            robotOutline.enabled = false;
                            range = -1;
                            
                        }

                        //Action is fulfilled only if the hex is in range and a card is selected
                        //TODO: Implement the different actions depending on the card
                        else if (cardSelected && hitInfo.collider.GetComponent<HexInteractions>().IsClickable())
                        {
                            CardManager.Instance.Discard(card);
                            cardSelected = false;
                            card.GetComponentInChildren<Outline>().enabled = false;
                            LevelManager.Instance.SetAction(-1);
                            range = -1;
                        }
                        
                        //If nothing else is selected, the hex can be selected and objects on the hex collected
                        //can also be used to select the trash field
                        //TODO: Create a trash field using a boolean and enable the player to put trash there
                        else if (!robotSelected && !cardSelected)
                        {
                            hexSelected = true;
                            range = 0;
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
