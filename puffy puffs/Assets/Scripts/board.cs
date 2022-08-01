using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{

    public int width;
    public int height;
    public int borderSize;


    public GameObject tilePrefab;
    public GameObject[] gamePiecePrefabs;

    tile[,] m_allTiles;
    gamePiece[,] m_allGamePieces;



    void Start()
    {
        m_allGamePieces = new gamePiece[width, height];

        m_allTiles = new tile[width, height];
        SetupTiles();
        SetupCamera();
        fillRandom();
    }

    void SetupTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 9), Quaternion.identity) as GameObject;
                tile.name = "Tile (" + i + "," + j + ")";

                m_allTiles[i, j] = tile.GetComponent<tile>();
                tile.transform.parent = transform;
                m_allTiles[i, j].Init(i, j, this);
            }

        }
    }

    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width-1) / 2f, (float)(height-1) / 2f, -10f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;

        float verticalSize = height / 2f + (float)borderSize;

        float horizontalSize = ((float)width / 2f + (float)borderSize / aspectRatio);

        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;

    }

    GameObject GetRandomGamePiece()
    {
        int randomIdx = Random.Range(0, gamePiecePrefabs.Length);

        if (gamePiecePrefabs[randomIdx] == null)
        {
            Debug.LogWarning("BOARD:  " + randomIdx + "does not contain a valid GamePiece prefab!");
        }

        return gamePiecePrefabs[randomIdx];
    }

    void PlaceGamePiece(gamePiece gamePiece, int x, int y)
    {
        if (gamePiece == null)
        {
            Debug.LogWarning("board: Invalid GamePiece!");
            return;

        }
        gamePiece.transform.position = new Vector3(x, y, 0);
        gamePiece.transform.rotation = Quaternion.identity;
        gamePiece.SetCoord(x, y);

    }

    void fillRandom()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject randomPiece = Instantiate(GetRandomGamePiece(),Vector3.zero,Quaternion.identity)as GameObject;
                if (randomPiece != null)
                {
                    PlaceGamePiece(randomPiece.GetComponent<gamePiece>(), i, j);
                }
            }
        }
    }
}
