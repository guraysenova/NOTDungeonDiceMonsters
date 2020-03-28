using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerEnum player = PlayerEnum.Player1;

    [SerializeField]
    bool wannaSummon;

    [SerializeField]
    Agent selectedAgent;

    [SerializeField]
    int movePoints = 100;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Unit"))
                {
                    selectedAgent = hit.transform.GetComponent<Agent>();
                }
                else
                {
                    selectedAgent = null;
                }
            }
            else
            {
                selectedAgent = null;
            }

            if (wannaSummon)
            {

                RaycastHit hit2;
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out hit2))
                {
                    if (hit2.transform.gameObject.CompareTag("Board"))
                    {
                        TwoDCoordinate endPos = new TwoDCoordinate();

                        endPos.x = Mathf.RoundToInt(gameObject.transform.position.x);
                        endPos.y = Mathf.RoundToInt(gameObject.transform.position.z);


                        if (!BoardManager.instance.GetTile(endPos).isFilled)
                        {
                            selectedAgent = null;
                            return;
                        }
                        SummonUnit("Man_Eater_Bug");
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Board"))
                {
                    if(selectedAgent != null && movePoints > 0)
                    {
                        TwoDCoordinate startPos = new TwoDCoordinate();

                        startPos.x = Mathf.RoundToInt(selectedAgent.gameObject.transform.position.x);
                        startPos.y = Mathf.RoundToInt(selectedAgent.gameObject.transform.position.z);

                        TwoDCoordinate endPos = new TwoDCoordinate();

                        endPos.x = Mathf.RoundToInt(gameObject.transform.position.x);
                        endPos.y = Mathf.RoundToInt(gameObject.transform.position.z);


                        if (!BoardManager.instance.GetTile(endPos).isFilled)
                        {
                            selectedAgent = null;
                            return;
                        }

                        List<GridNode> path = PathFinder.instance.GetPath(startPos, endPos);

                        // move
                        selectedAgent.GoPath(path, movePoints);

                        if (path.Count >= movePoints)
                        {
                            movePoints = 0;
                        }
                        else
                        {
                            movePoints -= path.Count;
                        }

                        selectedAgent = null;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            wannaSummon = !wannaSummon;
        }
    }

    void SummonUnit(string id)
    {
        GameObject unit = Instantiate(Units.instance.UnitFromId(id), gameObject.transform.position , Quaternion.Euler(0,0,0));
        
        unit.GetComponent<Agent>().Player = player;
    }
}