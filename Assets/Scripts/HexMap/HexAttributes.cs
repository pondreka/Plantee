using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HexAttributes : MonoBehaviour
{
    //Hex attributes and position
    private int water = -1;
    private int nutrition = -1;
    private int toxicity = -1;
    private int trash = -1;
    private float maxAttributevalue = 10f;

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
    
    
    //Descriptor list
    private HashSet<string> descriptor = new HashSet<string>();
    //private Collection<string> descriptor = new Collection<string>();
    
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

        //trashCount = 0;


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
        UpdateToxicity();
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
        else if (trash + value > 5)
        {
            trash = 5;
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

    //Create trash GameObjects and adjust visibility
    private void TrashObjectGenerator()
    {
        int max = 5;

        if (trashList.Count == 0)
        {
            for (int t = 0; t < max; t++)
            {
                float position = 0.2f + 0.25f * t;
                GameObject trashObject = Instantiate(trashPrefab, this.transform, true);
                trashObject.transform.localPosition = new Vector3(0.4f, -0.4f, position);
                trashObject.name = "Trash_" + t;
                trashList.Add(trashObject);

                if (t >= trash)
                {
                    trashList[t].SetActive(false);
                }
            }

            trashCount = trash;
        }
        else
        {
            UpdateTrash();
        }
       
        
    }

    //Updates the visible number of trash on a hex
    private void UpdateTrash()
    {
        if (trashCount < trash)
        {
            int addTrash = trash - trashCount;
            for (int t = 1; t <= addTrash; t++)
            {
                trashList[t + trashCount].SetActive(true);
            }
            
        }
        else if (trashCount > trash)
        {
            int removeTrash = trashCount - trash;
            for (int t = 0; t < removeTrash; t++)
            {
                trashList[trashCount - t-1].SetActive(false);
            }
        }

        trashCount = trash;
    }
    
    //Updates the color of the hex according to the toxicity
    private void UpdateToxicity()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0.4f, 0.1f + toxicity * 0.06f, 0f);
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
    
    //Add tag to descriptor
    public void AddTag(string tag)
    {
        descriptor.Add(tag);
    }
    
    //Getter for descriptor
    public HashSet<string> GetDescriptor()
    {
        return descriptor;
    }

}
