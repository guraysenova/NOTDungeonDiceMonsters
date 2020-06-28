using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sides = new List<GameObject>();

    [SerializeField]
    GameObject currentSide;

    [SerializeField]
    Color highlightColor;

    bool rolled;

    private void Start()
    {
        currentSide = sides[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
            //gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.up * 5;
        }
        CheckForSide();
        if(!rolled && gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 0.02f && gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude <= 0.02f)
        {
            rolled = true;
            //Debug.Log(currentSide.name);
        }
    }

    void CheckForSide()
    {
        for (int i = 0; i < sides.Count; i++)
        {
            if (sides[i].transform.position.y > currentSide.transform.position.y)
            {
                currentSide = sides[i];
            }
        }
        for (int i = 0; i < sides.Count; i++)
        {
            if(sides[i] != currentSide)
            {
                sides[i].GetComponent<DiceSide>().UnHighLight();
            }
        }
        currentSide.GetComponent<DiceSide>().Highlight(highlightColor);
    }
}