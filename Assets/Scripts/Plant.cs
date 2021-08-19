using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private int stage = 0;
    private bool seeds = false;
    private Color color;
    private int water = -1;
    private int nutrition = -1;
    private int toxicity = -1;
    private int spreading = -1;

    void Awake()
    {
        color = gameObject.GetComponent<Renderer>().material.color;
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttributes(int w, int n, int t, int s)
    {
        if (water == -1)
        {
            water = w;
            nutrition = n;
            toxicity = t;
            spreading = s;
        }
    }

    private void Evolve()
    {
        switch (stage)
        {
            case 0:
                transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                break;
            case 1:
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                break;
            case 2:
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 3:
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                seeds = true;
                break;
        }

        if (stage < 3)
        {
            stage++;
        }
    }

    private void Spoil()
    {
        color = new Color(0.5f, 0.5f, 0f);
    }
}
