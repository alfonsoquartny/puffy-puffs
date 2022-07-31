using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{

    public int width;
    public int height;

    public GameObject tilePrefab;

    tile[,] m_allTiles;

    void Start()
    {
        m_allTiles = new tile[width, height];
        SetupTiles();
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
            }

        }
    }

}
