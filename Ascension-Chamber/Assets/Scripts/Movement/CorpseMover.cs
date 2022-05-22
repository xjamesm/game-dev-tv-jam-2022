using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseMover : Mover
{
    public IEnumerator MoveCorpse(Vector3 destination)
    {
        yield return MoveCo(destination, delay);
    }
}