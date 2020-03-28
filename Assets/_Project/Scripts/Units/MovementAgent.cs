using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementAgent : MonoBehaviour
{
    Tween movement;

    public void GoPath(List<Vector3> path)
    {
        gameObject.transform.DOPath(path.ToArray(), 10f);
    }
}