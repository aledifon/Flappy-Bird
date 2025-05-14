using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField] private float animationSpeed;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
 
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed*Time.deltaTime, 0);
    }
}
