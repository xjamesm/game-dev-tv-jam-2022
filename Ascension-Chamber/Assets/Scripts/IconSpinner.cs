using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSpinner : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 360f;
    [SerializeField] float scalePercent = 10f;
    [SerializeField] float scaleSpeed = 0.5f;

    bool scaleUp = false;
    Vector3 originalScale;
    Vector3 scaleTarget;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        transform.localScale = Vector3.MoveTowards(transform.localScale, scaleTarget, scaleSpeed * Time.deltaTime);

        if (transform.localScale == scaleTarget)
        {
            if (scaleUp)
            {
                scaleTarget = originalScale * (1 - (scalePercent / 100));
                scaleUp = false;
            }
            else
            {
                scaleTarget = originalScale * (1 + (scalePercent / 100));
                scaleUp = true;
            }
        }
    }
}
