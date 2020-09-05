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
        transform.GetChild(0).localPosition = new Vector3(boardSize.x - 1, 0, boardSize.y - 1);
    }

    void Start()
    {
        Board();
        PathFinder.instance.SetGrid(tileData, boardSize);
    }

    public List<TileData> GetTileData()
    {
        return tileData;
    }

    void Board()
    {
        for (int i = 0; i < boardSize.y; i++)
        {
            for (int j = 0; j < boardSize.x; j++)
            {
                TileData myTile = new TileData();
                TwoDCoordinate coordinate = new TwoDCoordinate();
                coordinate.x =  (j * 2);
                coordinate.y =  (i * 2);
                myTile.coordinates = coordinate;
                GameObject tile = Instantiate(tilePrefab, new Vector3((j * 2), -0.05f, (i * 2)) , Quaternion.Euler(0f,0f,0f));

                if (i == 0 || i == boardSize.y - 1)
                {
                    if (boardSize.x % 2 == 1)
                    {
                        if (j == boardSize.x / 2)
                        {
                            myTile.isConnected = true;

                            if (i == 0)
                            {
                                GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(((j) * 2), -0.05f, ((i - 1) * 2)), Quaternion.Euler(0f, 0f, 0f));
                                playerTile.transform.SetParent(gameObject.transform);
                                myTile.AddTeam(TeamEnum.Team1);
                            }
                            if (i == boardSize.y - 1)
                            {
                                GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(((j) * 2), -0.05f, ((i + 1)* 2)), Quaternion.Euler(0f, 0f, 0f));
                                playerTile.transform.SetParent(gameObject.transform);
                                myTile.AddTeam(TeamEnum.Team2);
                            }
                        }
                    }
                }

                tileData.Add(myTile);
                tile.transform.SetParent(gameObject.transform);
            }
        }
    }

    public bool CanPlace(DiceUnfoldData diceUnfoldData , GameObject obj , TeamEnum team)
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
        if (canPlace)
        {
            foreach (TileData tile in tilesToCheck)
            {
                if (tile.DoesTeamExist(team))
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

    public void PlaceBox(DiceUnfoldData diceUnfoldData, GameObject obj , TeamEnum team)
    {
        if(CanPlace(diceUnfoldData, obj , team))
        {
            List<TileData> myTiles = GetMyTiles(diceUnfoldData, obj);

            List<TileData> connectedTiles = new List<TileData>();

            foreach (TileData myTile in myTiles)
            {
                TileData tile1 = new TileData();
                tile1.coordinates = new TwoDCoordinate();
                tile1.coordinates.x = myTile.coordinates.x + 2;
                tile1.coordinates.y = myTile.coordinates.y;
                connectedTiles.Add(tile1);

                TileData tile2 = new TileData();
                tile2.coordinates = new TwoDCoordinate();
                tile2.coordinates.x = myTile.coordinates.x - 2;
                tile2.coordinates.y = myTile.coordinates.y;
                connectedTiles.Add(tile2);

                TileData tile3 = new TileData();
                tile3.coordinates = new TwoDCoordinate();
                tile3.coordinates.x = myTile.coordinates.x;
                tile3.coordinates.y = myTile.coordinates.y - 2;
                connectedTiles.Add(tile3);

                TileData tile4 = new TileData();
                tile4.coordinates = new TwoDCoordinate();
                tile4.coordinates.x = myTile.coordinates.x;
                tile4.coordinates.y = myTile.coordinates.y + 2;
                connectedTiles.Add(tile4);
            }

            foreach (TileData myTile in myTiles)
            {
                foreach (TileData tile in tileData)
                {
                    if (tile.coordinates.x == myTile.coordinates.x && tile.coordinates.y == myTile.coordinates.y && !tile.isFilled)
                    {
                        tile.isFilled = true;
                        tile.isConnected = true;
                        tile.AddTeam(team);
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
                        tile.AddTeam(team);
                        break;
                    }
                }
            }

            PathFinder.instance.UpdateGrid(tileData, boardSize);
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

    public TileData GetTile(TwoDCoordinate pos)
    {
        foreach (TileData tile in tileData)
        {
            if(tile.coordinates.x == pos.x && tile.coordinates.y == pos.y)
            {
                return tile;
            }
        }
        return null;
    }
}