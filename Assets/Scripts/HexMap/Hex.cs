using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Hex : MonoBehaviour
{
    //Hex attributes and position
    private int water = -1;
    private int nutrition = -1;
    private int toxicity = -1;
    private int trash = -1;
    private int columnPos = -1;
    private int rowPos = -1;
    private float maxAttributevalue = 10f;

    private bool isClickable = false;
    private Outline outline;

    //Trash
    [SerializeField] private GameObject trashPrefab;
    private List<GameObject> trashList = new List<GameObject>();
    private int trashCount = 0;
    
    //Toxicity
    [SerializeField] private Material hexMat;
    
    //Water
    [SerializeField] private Image waterBar;
    
    //Nutrition
    [SerializeField] private Image nutritionBar;
    
    // Start is called before the first frame update
    void Start()
    {
        if (trashPrefab == null)
        {
            Debug.LogError("No trash prefab assigned to Hex script!");
        }

        if (hexMat == null)
        {
            Debug.LogError("No hex material assigned to Hex script!");
        }
        
        if (waterBar == null)
        {
            Debug.LogError("No water bar assigned to Hex script!");
        }
        
        if (nutritionBar == null)
        {
            Debug.LogError("No nutrition bar assigned to Hex script!");
        }
        
        isClickable = false;
        outline = this.gameObject.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Getter water
    public int GetWater()
    {
        return water;
    }
    
    //Setter water
    //Adjustment of the attribute by the given value
    //value might be positive or negative
    public void SetWater(int value)
    {
        if (water == -1)
        {
            water = value;
        }
        else if (water + value < 0)
        {
            water = 0;
        }
        else if (water + value > 10)
        {
            water = 10;
        }
        else
        {
            water += value;
        }
        
        UpdateWater();
    }
    
    
    //Getter nutrition
    public int GetNutrition()
    {
        return nutrition;
    }
    
    //Setter nutrition
    //Adjustment of the attribute by the given value
    //value might be positive or negative
    public void SetNutrition(int value)
    {
        if (nutrition == -1)
        {
            nutrition = value;
        }
        else if (nutrition + value < 0)
        {
            nutrition = 0;
        }
        else if (nutrition + value > 10)
        {
            nutrition = 10;
        }
        else
        {
            nutrition += value;
        }

        UpdateNutrition();
    }
    
    
    //Getter toxicity
    public int GetToxicity()
    {
        return toxicity;
    }
    
    //Setter toxicity
    //Adjustment of the attribute by the given value
    //value might be positive or negative
    public void SetToxicity(int value)
    {
        if (toxicity == -1)
        {
            toxicity = value;
        }
        else if (toxicity + value < 0)
        {
            toxicity = 0;
        }
        else if (toxicity + value > 10)
        {
            toxicity = 10;
        }
        else
        {
            toxicity += value;
        }
        UpdateToxixity();
    }
    
    
    //Getter trash
    public int GetTrash()
    {
        return trash;
    }
    
    //Setter trash
    //Adjustment of the attribute by the given value
    //value might be positive or negative
    public void SetTrash(int value)
    {
        if (trash == -1)
        {
            trash = value;
        }
        else if (trash + value < 0)
        {
            trash = 0;
        }
        else if (trash + value > 10)
        {
            trash = 10;
        }
        else
        {
            trash += value;
        }
        
        TrashObjectGenerator();
    }

    
    //Setter for all Attributes at a time
    //For initialisation or updating
    public void SetAllAttributes(int wa, int nu, int to, int tr)
    {
        SetWater(wa);
        SetNutrition(nu);
        SetToxicity(to);
        SetTrash(tr);
    }
    
    //Getter column position
    public int GetColumn()
    {
        return columnPos;
    }
    
    //Setter column position
    private void SetColumn(int column)
    {
        if (columnPos == -1)
        {
            columnPos = column;
        }
    }
    
    //Getter row position
    public int GetRow()
    {
        return rowPos;
    }
    
    //Setter row position
    private void SetRow(int row)
    {
        if (rowPos == -1)
        {
            rowPos = row;
        }
    }
    
    //Setter for Hex position
    public void SetPosition(int column, int row)
    {
        SetColumn(column);
        SetRow(row);
    }

    //Create trash GameObjects and adjust visibility
    private void TrashObjectGenerator()
    {
        int max = 5;
        int trashNumber = trash / 2;
        
        if (trashList.Count == 0)
        {
            for (int t = 0; t < max; t++)
            {
                float position = 0.2f + 0.25f * t;
                GameObject trashObject = Instantiate(trashPrefab, this.transform, true);
                trashObject.transform.localPosition = new Vector3(0.4f, -0.4f, position);
                trashObject.name = "Trash_" + t;
                trashList.Add(trashObject);
                trashCount++;

                if (t > trashNumber)
                {
                    trashList[t].SetActive(false);
                    trashCount--;
                }
            }
        }
        else
        {
            UpdateTrash();
        }
       
        
    }

    //Updates the visible number of trash on a hex
    private void UpdateTrash()
    {
        int trashNumber = trash / 2;
        if (trashCount < trashNumber)
        {
            int addTrash = trashNumber - trashCount;
            for (int t = 1; t <= addTrash; t++)
            {
                trashList[t + trashCount].SetActive(true);
            }
            
        }
        else if (trashCount > trashNumber)
        {
            int removeTrash = trashCount - trashNumber;
            for (int t = 0; t < removeTrash; t++)
            {
                trashList[trashCount - t].SetActive(false);
            }
        }

        trashCount = trashNumber;
    }
    
    //Updates the color of the hex according to the toxicity
    private void UpdateToxixity()
    {

        switch (toxicity)
        {
            case 0:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.74f, 0.49f, 0);
                break;
            case 1:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.7f, 0.49f, 0);
                break;
            case 2:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.66f, 0.49f, 0);
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.625f, 0.49f, 0);
                break;
            case 4:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.58f, 0.49f, 0);
                break;
            case 5:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.54f, 0.40f, 0);
                break;
            case 6:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.51f, 0.49f, 0);
                break;
            case 7:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.47f, 0.49f, 0);
                break;
            case 8:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.42f, 0.49f, 0);
                break;
            case 9:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.39f, 0.49f, 0);
                break;
            case 10:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.35f, 0.49f, 0);
                break;
        }
    }

    //Updates water bar
    private void UpdateWater()
    {
        waterBar.fillAmount = water / maxAttributevalue;
    }
    
    //Updates nutrition bar
    private void UpdateNutrition()
    {
        nutritionBar.fillAmount = nutrition / maxAttributevalue;
    }

    //In movement range or not
    public bool IsClickable()
    {
        return isClickable;
    }

    //Not clickable for movement
    public void NotClickable()
    {
        isClickable = false;
    }

    //Clickable for movement
    public void Clickable()
    {
        isClickable = true;
    }


    //Outline On
    public void OutlineOn()
    {
        outline.enabled = true;
    }

    //Outline off
    public void OutlineOff()
    {
        outline.enabled = false;
    }
    

}
