using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    TwoDCoordinate boardSize = null;
    [SerializeField]
    GameObject tilePrefab = null;
    [SerializeField]
    GameObject playerTilePrefab = null;

    Board board;

    public static BoardManager instance;

    public Board Board
    {
        get
        {
            return board;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        transform.GetChild(0).localScale = new Vector3(boardSize.y / 5f , 1f, boardSize.x / 5f);
        transform.GetChild(0).localPosition = new Vector3(boardSize.x - 1, 0, boardSize.y - 1);
    }

    private void Start()
    {
        board = new Board(boardSize);
        SetBoard();
    }

    void SetBoard()
    {
        for (int i = 0; i < boardSize.y; i++)
        {
            for (int j = 0; j < boardSize.x; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3((j * 2), -0.05f, (i * 2)) , Quaternion.Euler(0f,0f,0f));

                if (i == 0 || i == boardSize.y - 1)
                {
                    if (boardSize.x % 2 == 1)
                    {
                        if (j == boardSize.x / 2)
                        {
                            if (i == 0)
                            {
                                GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(((j) * 2), -0.05f, ((i - 1) * 2)), Quaternion.Euler(0f, 0f, 0f));
                                playerTile.transform.SetParent(gameObject.transform);
                            }
                            if (i == boardSize.y - 1)
                            {
                                GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(((j) * 2), -0.05f, ((i + 1)* 2)), Quaternion.Euler(0f, 0f, 0f));
                                playerTile.transform.SetParent(gameObject.transform);
                            }
                        }
                    }
                }
                tile.transform.SetParent(gameObject.transform);
            }
        }
    }
}