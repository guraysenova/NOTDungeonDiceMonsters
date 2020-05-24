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
    bool isPlacing;


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
            PlaceBox();
            CheckPlaceability();
        }
        else
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(isPlacing);
            transform.GetChild(0).GetChild(1).gameObject.SetActive(isPlacing);
            transform.GetChild(1).GetChild(0).gameObject.SetActive(isPlacing);
            transform.GetChild(1).GetChild(1).gameObject.SetActive(isPlacing);
        }
    }
    void CheckMirrored()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            Flip();
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

    void CheckPlaceability()
    {
        if (!isMirrored)
        {
            canPlace = BoardManager.instance.CanPlace(diceUnfoldData[selectedIndex], gameObject);
        }
        else
        {
            canPlace = BoardManager.instance.CanPlace(mirroredDiceUnfoldData[selectedIndex], gameObject);
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
    }

    void CheckRotate()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            gameObject.transform.eulerAngles += (Vector3.up * 90f);
        }
    }

    void PlaceBox()
    {
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            if (!isMirrored)
            {
                Instantiate(foldedBoxesPrefabs[selectedIndex], gameObject.transform.position, Quaternion.Euler(gameObject.transform.rotation.eulerAngles));
                BoardManager.instance.PlaceBox(diceUnfoldData[selectedIndex], gameObject);
            }
            else
            {
                Instantiate(mirroredFoldedBoxesPrefabs[selectedIndex], gameObject.transform.position, Quaternion.Euler(gameObject.transform.rotation.eulerAngles));
                BoardManager.instance.PlaceBox(mirroredDiceUnfoldData[selectedIndex], gameObject);
            }
            //gameObject.GetComponent<Player>().SummonUnit("RedPawn");
        }
    }

}
