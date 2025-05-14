using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour
{
    [SerializeField] private float speed;

    private float leftEdge;

    // Start is called before the first frame update
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPillars();
        PillarMovement();
    }

    private void DestroyPillars()
    {
        if (transform.position.x < leftEdge)        
            Destroy(gameObject);        
    }
    private void PillarMovement()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
