using System.Collections;
using System.Collections.Generic;

public class Board
{
    TwoDCoordinate boardSize = null;

    List<TileData> tileData = new List<TileData>();

    PathFinder pathFinder = new PathFinder();

    public static BoardManager instance;

    public Board(TwoDCoordinate _boardSize)
    {
        boardSize = _boardSize;
        BoardInit();
        pathFinder.SetGrid(tileData, boardSize);
    }

    public PathFinder PathFinder
    {
        get
        {
            return pathFinder;
        }
    }

    public List<TileData> GetTileData()
    {
        return tileData;
    }

    void BoardInit()
    {
        for (int i = 0; i < boardSize.y; i++)
        {
            for (int j = 0; j < boardSize.x; j++)
            {
                TileData myTile = new TileData();
                TwoDCoordinate coordinate = new TwoDCoordinate((j * 2), (i * 2));
                myTile.coordinates = coordinate;
                //GameObject tile = Instantiate(tilePrefab, new Vector3((j * 2), -0.05f, (i * 2)), Quaternion.Euler(0f, 0f, 0f));

                if (i == 0 || i == boardSize.y - 1)
                {
                    if (boardSize.x % 2 == 1)
                    {
                        if (j == boardSize.x / 2)
                        {
                            myTile.isConnected = true;

                            if (i == 0)
                            {
                                //GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(((j) * 2), -0.05f, ((i - 1) * 2)), Quaternion.Euler(0f, 0f, 0f));
                                //playerTile.transform.SetParent(gameObject.transform);
                                myTile.AddTeam(TeamEnum.Team1);
                            }
                            if (i == boardSize.y - 1)
                            {
                                //GameObject playerTile = Instantiate(playerTilePrefab, new Vector3(((j) * 2), -0.05f, ((i + 1) * 2)), Quaternion.Euler(0f, 0f, 0f));
                                //playerTile.transform.SetParent(gameObject.transform);
                                myTile.AddTeam(TeamEnum.Team2);
                            }
                        }
                    }
                }

                tileData.Add(myTile);
                //tile.transform.SetParent(gameObject.transform);
            }
        }
    }

    public bool CanPlace(DiceUnfoldData diceUnfoldData, TwoDCoordinate objPos, Rotation rotation, TeamEnum team)
    {
        bool canPlace = false;

        List<TileData> myTiles = GetMyTiles(diceUnfoldData, objPos , rotation);

        List<TileData> tilesToCheck = new List<TileData>();

        foreach (TileData myTile in myTiles)
        {
            foreach (TileData tile in tileData)
            {
                if (tile.coordinates.x == myTile.coordinates.x && tile.coordinates.y == myTile.coordinates.y && !tile.isFilled)
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
            foreach (TileData tile in tilesToCheck)
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

    public void PlaceBox(DiceUnfoldData diceUnfoldData, TwoDCoordinate objPos, Rotation rotation, TeamEnum team)
    {
        if (CanPlace(diceUnfoldData, objPos , rotation , team))
        {
            List<TileData> myTiles = GetMyTiles(diceUnfoldData, objPos, rotation);

            List<TileData> connectedTiles = new List<TileData>();

            foreach (TileData myTile in myTiles)
            {
                TileData tile1 = new TileData();
                tile1.coordinates = new TwoDCoordinate(myTile.coordinates.x + 2, myTile.coordinates.y);
                connectedTiles.Add(tile1);

                TileData tile2 = new TileData();
                tile2.coordinates = new TwoDCoordinate(myTile.coordinates.x - 2, myTile.coordinates.y);
                connectedTiles.Add(tile2);

                TileData tile3 = new TileData();
                tile3.coordinates = new TwoDCoordinate(myTile.coordinates.x, myTile.coordinates.y - 2);
                connectedTiles.Add(tile3);

                TileData tile4 = new TileData();
                tile4.coordinates = new TwoDCoordinate(myTile.coordinates.x, myTile.coordinates.y + 2);
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

            pathFinder.UpdateGrid(tileData, boardSize);
        }
    }

    public void PlacePortal(TwoDCoordinate startPos, TwoDCoordinate endPos)
    {
        foreach (TileData tile in tileData)
        {
            if (tile.coordinates.x == startPos.x * 2 && tile.coordinates.y == startPos.y * 2)
            {
                tile.hasPortal = true;
                tile.portalX = endPos.x;
                tile.portalY = endPos.y;
            }
            if (tile.coordinates.x == endPos.x * 2 && tile.coordinates.y == endPos.y * 2)
            {
                tile.hasPortal = true;
                tile.portalX = startPos.x;
                tile.portalY = startPos.y;
            }
        }
        pathFinder.UpdateGrid(tileData, boardSize);
    }

    List<TileData> GetMyTiles(DiceUnfoldData diceUnfoldData, TwoDCoordinate objPos, Rotation rotation)
    {
        List<TileData> myTiles = new List<TileData>();

        foreach (TwoDCoordinate item in diceUnfoldData.myCoordinates)
        {
            TileData myTile = new TileData();
            myTile.coordinates = new TwoDCoordinate(-1, -1);

            TwoDCoordinate pos = new TwoDCoordinate(2 * item.x, -2 * item.y);

            if (rotation == Rotation.Zero)
            {
                myTile.coordinates.x = objPos.x + pos.x;
                myTile.coordinates.y = objPos.y + pos.y;
            }
            else if (rotation == Rotation.Ninety)
            {
                myTile.coordinates.x = objPos.x + pos.y;
                myTile.coordinates.y = objPos.y - pos.x;
            }
            else if (rotation == Rotation.OneEighty)
            {
                myTile.coordinates.x = objPos.x - pos.x;
                myTile.coordinates.y = objPos.y - pos.y;
            }
            else if (rotation == Rotation.TwoSeventy)
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
            if (tile.coordinates.x == pos.x && tile.coordinates.y == pos.y)
            {
                return tile;
            }
        }
        return null;
    }

}

public enum Rotation
{
    Zero = 0,
    Ninety = 1,
    OneEighty = 2 ,
    TwoSeventy = 3
}
