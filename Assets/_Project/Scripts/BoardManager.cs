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

    [SerializeField]
    List<TileData> tileData = new List<TileData>();

    public static BoardManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        transform.GetChild(0).localScale = new Vector3(boardSize.y / 5f , 1f, boardSize.x / 5f);
        transform.GetChild(0).localPosition = new Vector3(-boardSize.y, 0, boardSize.x);
    }

    void Start()
    {
        Board();
    }

    void Board()
    {
        for (int j = 0; j < boardSize.y; j++)
        {
            for (int i = 0; i < boardSize.x; i++)
            {
                TileData myTile = new TileData();
                TwoDCoordinate coordinate = new TwoDCoordinate();
                coordinate.x = -1 - (j * 2);
                coordinate.y =  1 + (i * 2);
                myTile.coordinates = coordinate;
                GameObject tile = Instantiate(tilePrefab, new Vector3(-1 - (j * 2), -0.05f, 1 + (i * 2)) , Quaternion.Euler(0f,0f,0f));

                if (j == 0 || j == boardSize.y - 1)
                {
                    if (boardSize.x % 2 == 1)
                    {
                        if (i == boardSize.x / 2)
                        {
                            myTile.isConnected = true;

                            if (j == 0)
                            {
                                GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(-1 - ((j - 1) * 2), -0.05f, 1 + (i * 2)), Quaternion.Euler(0f, 0f, 0f));
                                playerTile.transform.SetParent(gameObject.transform);
                            }
                            if (j == boardSize.y - 1)
                            {
                                GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(-1 - ((j + 1) * 2), -0.05f, 1 + (i * 2)), Quaternion.Euler(0f, 0f, 0f));
                                playerTile.transform.SetParent(gameObject.transform);
                            }
                        }
                    }
                }

                tileData.Add(myTile);
                tile.transform.SetParent(gameObject.transform);
            }
        }
    }

    public bool CanPlace(DiceUnfoldData diceUnfoldData , GameObject obj)
    {
        bool canPlace = false;

        List<TileData> myTiles = GetMyTiles(diceUnfoldData, obj);

        List<TileData> tilesToCheck = new List<TileData>();

        foreach (TileData myTile in myTiles)
        {
            foreach (TileData tile in tileData)
            {
                if(tile.coordinates.x == myTile.coordinates.x && tile.coordinates.y == myTile.coordinates.y && !tile.isFilled)
                {
                    tilesToCheck.Add(tile);
                    canPlace = true;
                    break;
                }
                else
                {
                    canPlace = false;
                }
            }
            if (!canPlace)
            {
                break;
            }
        }
        if (canPlace)
        {
            foreach(TileData tile in tilesToCheck)
            {
                if (tile.isConnected == true)
                {
                    canPlace = true;
                    break;
                }
                else
                {
                    canPlace = false;
                }
            }
        }
        return canPlace;
    }

    public void PlaceBox(DiceUnfoldData diceUnfoldData, GameObject obj)
    {
        if(CanPlace(diceUnfoldData, obj))
        {
            List<TileData> myTiles = GetMyTiles(diceUnfoldData, obj);

            List<TileData> connectedTiles = new List<TileData>();

            foreach (TileData myTile in myTiles)
            {
                if(myTile.coordinates.x != -1)
                {
                    TileData tile = new TileData();
                    tile.coordinates = new TwoDCoordinate();

                    tile.coordinates.x = myTile.coordinates.x + 2;
                    tile.coordinates.y = myTile.coordinates.y;

                    connectedTiles.Add(tile);
                }
                if(myTile.coordinates.x != boardSize.x)
                {
                    TileData tile = new TileData();
                    tile.coordinates = new TwoDCoordinate();

                    tile.coordinates.x = myTile.coordinates.x - 2;
                    tile.coordinates.y = myTile.coordinates.y;

                    connectedTiles.Add(tile);
                }
                if (myTile.coordinates.y != 1)
                {
                    TileData tile = new TileData();
                    tile.coordinates = new TwoDCoordinate();

                    tile.coordinates.x = myTile.coordinates.x;
                    tile.coordinates.y = myTile.coordinates.y - 2;

                    connectedTiles.Add(tile);
                }
                if (myTile.coordinates.y != boardSize.y)
                {
                    TileData tile = new TileData();
                    tile.coordinates = new TwoDCoordinate();

                    tile.coordinates.x = myTile.coordinates.x;
                    tile.coordinates.y = myTile.coordinates.y + 2;

                    connectedTiles.Add(tile);
                }
            }

            foreach (TileData myTile in myTiles)
            {
                foreach (TileData tile in tileData)
                {
                    if (tile.coordinates.x == myTile.coordinates.x && tile.coordinates.y == myTile.coordinates.y && !tile.isFilled)
                    {
                        tile.isFilled = true;
                        tile.isConnected = true;
                        break;
                    }
                }
            }

            foreach (TileData myTile in connectedTiles)
            {
                foreach (TileData tile in tileData)
                {
                    if (tile.coordinates.x == myTile.coordinates.x && tile.coordinates.y == myTile.coordinates.y)
                    {
                        tile.isConnected = true;
                        break;
                    }
                }
            }
        }
    }

    List<TileData> GetMyTiles(DiceUnfoldData diceUnfoldData, GameObject obj)
    {
        List<TileData> myTiles = new List<TileData>();

        foreach (TwoDCoordinate item in diceUnfoldData.myCoordinates)
        {
            TileData myTile = new TileData();
            myTile.coordinates = new TwoDCoordinate();

            TwoDCoordinate objPos = new TwoDCoordinate();
            objPos.x = (int)obj.transform.position.x;
            objPos.y = (int)obj.transform.position.z;

            TwoDCoordinate pos = new TwoDCoordinate();
            pos.x = 2 * item.x;
            pos.y = -2 * item.y;

            if (obj.transform.eulerAngles.y < 1f && obj.transform.eulerAngles.y > -1f)
            {
                myTile.coordinates.x = objPos.x + pos.x;
                myTile.coordinates.y = objPos.y + pos.y;
            }
            else if (obj.transform.eulerAngles.y < 91f && obj.transform.eulerAngles.y > 89f)
            {
                myTile.coordinates.x = objPos.x + pos.y;
                myTile.coordinates.y = objPos.y - pos.x;
            }
            else if (obj.transform.eulerAngles.y < 181f && obj.transform.eulerAngles.y > 179f)
            {
                myTile.coordinates.x = objPos.x - pos.x;
                myTile.coordinates.y = objPos.y - pos.y;
            }
            else if (obj.transform.eulerAngles.y < 271f && obj.transform.eulerAngles.y > 269f)
            {
                myTile.coordinates.x = objPos.x - pos.y;
                myTile.coordinates.y = objPos.y + pos.x;
            }
            myTile.isFilled = false;
            myTiles.Add(myTile);
        }
        return myTiles;
    }
}