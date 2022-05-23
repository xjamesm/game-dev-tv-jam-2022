using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        //if (boxCollider == null)
        //    boxCollider = GetComponent<BoxCollider>();

        //Gizmos.DrawCube(transform.position, boxCollider.size);
    }
}
