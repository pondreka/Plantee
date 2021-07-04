using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private int range = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Range can only be changed when entering a trash field
    //Otherwise card must stay on hand because the hex will not be clickable (range -1)
    public void SetRange()
    {
        range = 0;
    }
    
    public int GetRange()
    {
        return range;
    }
}
