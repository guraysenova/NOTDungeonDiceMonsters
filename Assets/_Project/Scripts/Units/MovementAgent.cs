using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementAgent : MonoBehaviour
{
    Tween movement;

    public void GoPath(List<Vector3> path)
    {
        if (movement.IsPlaying())
        {
            return;
        }
        movement = gameObject.transform.DOPath(path.ToArray(), (float)path.Count * 0.5f, PathType.Linear , PathMode.TopDown2D , 10 , Color.red);
    }
}