using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dump : MonoBehaviour
{
    private int trashCount = 0;

    [SerializeField] private GameObject trashPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Adding a trash cube onto the dump in the right position
    public void AddTrash(GameObject card)
    {
        if (trashCount < 10)
        {
            GameObject trash = Instantiate(trashPrefab, this.transform, true);
            trash.transform.localPosition =
                new Vector3(-0.5f + 0.1f * trashCount, 0.3f, 0.15f);
            trash.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            trashCount++;
        }
        else if (trashCount < 20)
        {
            GameObject trash = Instantiate(trashPrefab, this.transform, true);
            trash.transform.localPosition =
                new Vector3(-0.5f + 0.1f * (trashCount - 10), -0.3f, 0.15f);
            trash.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            trashCount++;
        }
        else if (trashCount < 30)
        {
            GameObject trash = Instantiate(trashPrefab, this.transform, true);
            trash.transform.localPosition =
                new Vector3(-0.5f + 0.1f * (trashCount - 20), 0.3f, 0.25f);
            trash.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            trashCount++;
        }
        else if (trashCount < 40)
        {
            GameObject trash = Instantiate(trashPrefab, this.transform, true);
            trash.transform.localPosition =
                new Vector3(-0.5f + 0.1f * (trashCount - 30), -0.3f, 0.25f);
            trash.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            
            trashCount++;
        }
        else
        {
            card.GetComponent<CardBasic>().SetCardRange(-1);
        }
    }

    //Getter for the trash on the dump
    public int GetTrashLevel()
    {
        return trashCount;
    }
}
