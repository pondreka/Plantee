using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private int stage = 0;
    private bool seeds = false;
    private int water = -1;
    private int nutrition = -1;
    private int toxicity = -1;
    private int trash = -1;
    private int spreading = -1;
    private int roundCount = 0;
    private int evolutionAt = -1;
    private int actionValue = -1;
    private int actionIndex = -1;
    private GameObject card;

    private List<GameObject> neighbors = new List<GameObject>();
    private Outline plantOutline;


    private void Awake()
    {

        neighbors = LevelManager.Instance.GetHexNeighbors(transform.parent.gameObject);
        GameObject hex = null;
        foreach (var h in neighbors.Where(h => h.GetComponent<HexInteractions>().IsDump()))
        {
            hex = h;
        }
        neighbors.Remove(hex);
        
        plantOutline = GetComponent<Outline>();
        plantOutline.enabled = false;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInstance(int w, int n, int to, int tr, int s, int e, int v, int i, GameObject c)
    {
        if (water == -1)
        {
            water = w;
            nutrition = n;
            toxicity = to;
            trash = tr;
            spreading = s;
            evolutionAt = e;
            actionValue = v;
            actionIndex = i;
            card = c;
        }
    }

    //Defines the evolution stages of a plant
    private void Evolve()
    {
        if (stage < 3)
        {
            switch (stage)
            {
                case 0:
                    transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.3f);
                    break;
                case 1:
                    transform.localScale = new Vector3(0.25f, 0.2f, 0.25f);
                    break;
                case 2:
                    transform.localScale = new Vector3(0.3f, 0.2f, 0.3f);
                    Spreading();
                    PlantAction();
                    plantOutline.enabled = true;
                    seeds = true;
                    break;
            }

            stage++;
        }
        else
        {
            PlantAction();
        }
    }

    //Counts the rounds needed to evolve
    //Does not count if the hex attributes are not good enough for the plant
    public void RoundCount()
    {
        if (CompareAttributes(transform.parent.gameObject))
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0.6f, 0f);
            roundCount++;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0.3f, 0.3f, 0f);
        }

        if (roundCount == evolutionAt)
        {
            Evolve();
            roundCount = 0;
        }
    }

    //Spreading the plant over the surrounding hex fields 
    private void Spreading()
    {
        for (int i = 0; i < spreading; i++)
        {
            int rand = Random.Range(0, neighbors.Count);

            if (!neighbors[rand].GetComponent<HexInteractions>().HasPlant() && CompareAttributes(neighbors[rand]))
            {
                GameObject cardClone = Instantiate(card, card.transform.parent, false);
                cardClone.gameObject.SetActive(true);
                cardClone.transform.position = transform.position;
                cardClone.GetComponent<CardSeed>().CardAction(neighbors[rand]);
            }
        }


    }

    //Compare hex attributes with plant attributes
    private bool CompareAttributes(GameObject hex)
    {
        HexAttributes hexScript = hex.GetComponentInParent<HexAttributes>();

        return hexScript.GetWater() >= water && hexScript.GetNutrition() >= nutrition && hexScript.GetToxicity() >= 10 - toxicity && hexScript.GetTrash() <= trash;
    }
    
    //Getting a seed out of an evolved plant
    public void GetSeed()
    {
        plantOutline.enabled = false;
        seeds = false;
        card.gameObject.SetActive(true);
        CardManager.Instance.Discard(card);
    }

    //Seeds also gives information about the last evolution stage is reached
    public bool HasSeeds()
    {
        return seeds;
    }
    
    //Action of the plant after the last evolution stage
    //Index 0: trash reduction
    //Index 1: toxicity reduction
    //Index 2: nutrition increase
    private void PlantAction()
    {
        switch (actionIndex)
        {
            case 0:
                GetComponentInParent<HexAttributes>().SetTrash(-actionValue);
                break;
            case 1:
                foreach (var hex in neighbors)
                {
                    hex.GetComponent<HexAttributes>().SetToxicity(actionValue);
                }
                GetComponentInParent<HexAttributes>().SetToxicity(actionValue);
                break;
            case 2:
                foreach (var hex in neighbors)
                {
                    hex.GetComponent<HexAttributes>().SetNutrition(actionValue);
                }
                GetComponentInParent<HexAttributes>().SetNutrition(actionValue);
                break;
                
        }
    }
    
    //Stage setter
    public void SetStage(int value)
    {
        if (stage + value < 0)
        {
            stage = 0;
        }
        else if (stage + value > 3)
        {
            stage = 3;
        }
        else
        {
            stage += value;
        }
        Evolve();
    }
}
