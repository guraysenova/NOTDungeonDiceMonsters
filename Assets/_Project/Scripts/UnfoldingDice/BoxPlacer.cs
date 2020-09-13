using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlacer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> foldedBoxesPrefabs = new List<GameObject>();

    [SerializeField]
    List<GameObject> mirroredFoldedBoxesPrefabs = new List<GameObject>();

    [SerializeField]
    List<DiceUnfoldData> diceUnfoldData = new List<DiceUnfoldData>();

    [SerializeField]
    List<DiceUnfoldData> mirroredDiceUnfoldData = new List<DiceUnfoldData>();

    [SerializeField]
    int selectedIndex = 0;

    bool canPlace;

    bool isMirrored;

    [SerializeField]
    bool isPlacing , isPortal;

    int count = 0;

    TwoDCoordinate startPos, endPos;

    [SerializeField]
    GameObject portalPrefab;

    int rotation = 0;


    private void Start()
    {
        Rotate();
        Rotate();
        Rotate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            isPlacing = !isPlacing;
        }
        if (isPlacing)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(!isMirrored);
            transform.GetChild(0).GetChild(1).gameObject.SetActive(isMirrored);
            transform.GetChild(1).GetChild(0).gameObject.SetActive(!isMirrored);
            transform.GetChild(1).GetChild(1).gameObject.SetActive(isMirrored);

            CheckMirrored();
            CycleSelectedBoxes();
            CheckRotate();
            SetPositionOnCursor();

            CheckPortal();

            PlaceBox(transform.GetComponent<Player>().TeamVal);
            CheckPlaceability(transform.GetComponent<Player>().TeamVal);
        }
        else
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(isPlacing);
            transform.GetChild(0).GetChild(1).gameObject.SetActive(isPlacing);
            transform.GetChild(1).GetChild(0).gameObject.SetActive(isPlacing);
            transform.GetChild(1).GetChild(1).gameObject.SetActive(isPlacing);
        }
        transform.GetChild(2).gameObject.SetActive(isPortal && count < 2);
        if (isPortal)
        {
            PlacePortal();
        }
    }
    void CheckMirrored()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            Flip();
        }
    }

    void CheckPortal()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            isPortal = !isPortal;
        }
    }

    void PlacePortal()
    {
        if (Input.GetMouseButtonDown(0) && count < 2)
        {
            if(count == 0)
            {
                startPos = new TwoDCoordinate((int)transform.position.x / 2, (int)transform.position.z / 2);
            }
            else
            {
                endPos = new TwoDCoordinate((int)transform.position.x / 2, (int)transform.position.z / 2);
                BoardManager.instance.Board.PlacePortal(startPos, endPos);
            }
            Instantiate(portalPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            count++;
        }
    }
    void Mirror()
    {
        isMirrored = !isMirrored;
        transform.GetChild(0).GetChild(0).gameObject.SetActive(!isMirrored);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(isMirrored);
        transform.GetChild(1).GetChild(0).gameObject.SetActive(!isMirrored);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(isMirrored);
    }
    void Flip()
    {
        Mirror();
        if((gameObject.transform.eulerAngles.y >= 89f && gameObject.transform.eulerAngles.y <= 91f ) || (gameObject.transform.eulerAngles.y >= 269f && gameObject.transform.eulerAngles.y <= 271f))
        {
            Rotate();
            Rotate();
        }
    }

    void CheckPlaceability(TeamEnum team)
    {
        if (!isMirrored)
        {
            canPlace = BoardManager.instance.Board.CanPlace(diceUnfoldData[selectedIndex], new TwoDCoordinate((int)transform.position.x, (int)transform.position.z), (Rotation)rotation, team);
        }
        else
        {
            canPlace = BoardManager.instance.Board.CanPlace(mirroredDiceUnfoldData[selectedIndex], new TwoDCoordinate((int)transform.position.x, (int)transform.position.z), (Rotation)rotation, team);
        }
        transform.GetChild(0).gameObject.SetActive(canPlace);
        transform.GetChild(1).gameObject.SetActive(!canPlace);
    }

    void CycleSelectedBoxes()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (selectedIndex == 0)
            {
                selectedIndex = foldedBoxesPrefabs.Count - 1;
            }
            else
            {
                selectedIndex--;
            }
            SetSelectedBoxPreview();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (selectedIndex == foldedBoxesPrefabs.Count - 1)
            {
                selectedIndex = 0;
            }
            else
            {
                selectedIndex++;
            }
            SetSelectedBoxPreview();
        }
    }

    void SetSelectedBoxPreview()
    {
        for (int i = 0; i < 2; i++)
        {
            foreach (Transform box in transform.GetChild(0).GetChild(i))
            {
                box.gameObject.SetActive(false);
            }
            foreach (Transform box in transform.GetChild(1).GetChild(i))
            {
                box.gameObject.SetActive(false);
            }
            transform.GetChild(0).GetChild(i).GetChild(selectedIndex).gameObject.SetActive(true);
            transform.GetChild(1).GetChild(i).GetChild(selectedIndex).gameObject.SetActive(true);
        }
    }

    void SetPositionOnCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Board"))
            {
                Vector3 position = new Vector3(0,0,0);
                int x = 0;
                int z = 0;
                if((int)hit.point.x % 2 == 0)
                {
                    x = (int)hit.point.x;
                }
                else
                {
                    x = (int)hit.point.x + 1;
                }
                if((int)hit.point.z % 2 == 0)
                {
                    z = (int)hit.point.z;
                }
                else
                {
                    z = (int)hit.point.z + 1;
                }
                position.x = x;
                position.z = z;
                gameObject.transform.position = position;
            }
        }
    }

    void Rotate()
    {
        gameObject.transform.eulerAngles += (Vector3.up * 90f);
        if(gameObject.transform.eulerAngles.y <= 0.1f && gameObject.transform.eulerAngles.y > 359.9f)
        {
            rotation = 0;
        }
        if (gameObject.transform.eulerAngles.y <= 90.1f && gameObject.transform.eulerAngles.y > 89.9f)
        {
            rotation = 1;
        }
        if (gameObject.transform.eulerAngles.y <= 180.1f && gameObject.transform.eulerAngles.y > 179.9f)
        {
            rotation = 2;
        }
        if (gameObject.transform.eulerAngles.y <= 270.1f && gameObject.transform.eulerAngles.y > 269.9f)
        {
            rotation = 3;
        }
    }

    void CheckRotate()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            gameObject.transform.eulerAngles += (Vector3.up * 90f);
        }
    }

    void PlaceBox(TeamEnum team)
    {
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            if (!isMirrored)
            {
                GameObject box = Instantiate(foldedBoxesPrefabs[selectedIndex], gameObject.transform.position, Quaternion.Euler(gameObject.transform.rotation.eulerAngles));
                box.GetComponent<Box>().SetTeam(team);
                BoardManager.instance.Board.PlaceBox(diceUnfoldData[selectedIndex], new TwoDCoordinate((int)transform.position.x , (int)transform.position.z), (Rotation)rotation , team);
            }
            else
            {
                GameObject box = Instantiate(mirroredFoldedBoxesPrefabs[selectedIndex], gameObject.transform.position, Quaternion.Euler(gameObject.transform.rotation.eulerAngles));
                box.GetComponent<Box>().SetTeam(team);
                BoardManager.instance.Board.PlaceBox(mirroredDiceUnfoldData[selectedIndex], new TwoDCoordinate((int)transform.position.x, (int)transform.position.z), (Rotation)rotation , team);
            }
            gameObject.GetComponent<Player>().SummonUnit("RedPawn");
        }
    }

}
