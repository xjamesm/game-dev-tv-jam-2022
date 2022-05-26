using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool inputEnabled = false;

    private float v;
    private float h;

    public float H { get => h; }
    public float V { get => v; }

    public void GetKeyInput()
    {
        if (inputEnabled)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
        else
        {
            h = 0;
            v = 0;
        }
    }
}
