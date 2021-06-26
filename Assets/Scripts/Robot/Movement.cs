using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private Vector3 destination;
    private Vector3 direction;
    
    
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var transform1 = transform;
        direction = destination - transform1.position;
        direction = Vector3.ClampMagnitude(direction, Vector3.Distance(transform1.position, destination));
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    public void SetDestination(Vector3 dest)
    {
        destination = new Vector3(dest.x, transform.position.y, dest.z);
    }
}
