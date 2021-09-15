using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private int range = 0;
    private int water = -1;
    private int nutrition = -1;
    private int toxicity = -1;
    private int spreading = -1;
    
    [SerializeField] private GameObject plantPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (water == -1)
        {
            water = Random.Range(0, 11);
            nutrition = Random.Range(0, 11);
            toxicity = Random.Range(0, 11);
            spreading = Random.Range(0, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //TODO: Implement plant objects and script
    //TODO: Implement Seeds creating plant objects depending on hex attributes

    public int GetRange()
    {
        return range;
    }
    
    public bool IsPlayable(GameObject hex)
    {
        if (hex.gameObject.GetComponent<HexInteractions>().HasPlant())
        {
            return false;
        }
        return true;
    }
    
    public void CardAction(GameObject hex)
    {
        if (!hex.gameObject.GetComponent<HexInteractions>().HasPlant())
        {
            GameObject plant = Instantiate(plantPrefab, hex.transform, false);
            plant.gameObject.transform.localPosition = new Vector3(-0.25f,0.1f,0.2f);
            plant.gameObject.GetComponent<Plant>().SetAttributes(water, nutrition, toxicity, spreading);
            hex.gameObject.GetComponent<HexInteractions>().Plant();
        }
        
    }
}
