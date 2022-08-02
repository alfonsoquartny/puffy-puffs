using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{

	public int width;
	public int height;

	public float borderSize;

	public GameObject tilePrefab;
	public GameObject[] gamePiecePrefabs;

	public float swapTime = 0.5f;

	tile[,] m_allTiles;
	gamePiece[,] m_allGamePieces;

	tile m_clickedTile;
	tile m_targetTile;


	void Start()
	{
		m_allTiles = new tile[width, height];
		m_allGamePieces = new gamePiece[width, height];

		SetupTiles();
		SetupCamera();
		FillRandom();
	}

	void SetupTiles()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;

				tile.name = "Tile (" + i + "," + j + ")";

				m_allTiles[i, j] = tile.GetComponent<tile>();

				tile.transform.parent = transform;

				m_allTiles[i, j].Init(i, j, this);

			}
		}
	}

	void SetupCamera()
	{
		Camera.main.transform.position = new Vector3((float)(width - 1) / 2f, (float)(height - 1) / 2f, -10f);

		float aspectRatio = (float)Screen.width / (float)Screen.height;

		float verticalSize = (float)height / 2f + (float)borderSize;

		float horizontalSize = ((float)width / 2f + (float)borderSize) / aspectRatio;

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

	public void PlaceGamePiece(gamePiece gamePiece, int x, int y)
	{
		if (gamePiece == null)
		{
			Debug.LogWarning("BOARD:  Invalid GamePiece!");
			return;
		}

		gamePiece.transform.position = new Vector3(x, y, 0);
		gamePiece.transform.rotation = Quaternion.identity;

		if (IsWithinBounds(x, y))
		{
			m_allGamePieces[x, y] = gamePiece;
		}

		gamePiece.SetCoord(x, y);
	}

	bool IsWithinBounds(int x, int y)
	{
		return (x >= 0 && x < width && y >= 0 && y < height);
	}

	void FillRandom()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				GameObject randomPiece = Instantiate(GetRandomGamePiece(), Vector3.zero, Quaternion.identity) as GameObject;

				if (randomPiece != null)
				{
					randomPiece.GetComponent<gamePiece>().Init(this);
					PlaceGamePiece(randomPiece.GetComponent<gamePiece>(), i, j);
					randomPiece.transform.parent = transform;

				}


			}
		}

	}

	public void ClickTile(tile tile)
	{
		if (m_clickedTile == null)
		{
			m_clickedTile = tile;
			//Debug.Log("clicked tile: " + tile.name);
		}
	}

	public void DragToTile(tile tile)
	{
		if (m_clickedTile != null && IsNextTo(tile, m_clickedTile))
		{
			m_targetTile = tile;
		}
	}

	public void ReleaseTile()
	{
		if (m_clickedTile != null && m_targetTile != null)
		{
			SwitchTiles(m_clickedTile, m_targetTile);
		}

		m_clickedTile = null;
		m_targetTile = null;

	}

	void SwitchTiles(tile clickedTile, tile targetTile)
	{

		gamePiece clickedPiece = m_allGamePieces[clickedTile.xIndex, clickedTile.yIndex];
		gamePiece targetPiece = m_allGamePieces[targetTile.xIndex, targetTile.yIndex];

		clickedPiece.Move(targetTile.xIndex, targetTile.yIndex, swapTime);
		targetPiece.Move(clickedTile.xIndex, clickedTile.yIndex, swapTime);


	}

	bool IsNextTo(tile start, tile end)
	{
		if (Mathf.Abs(start.xIndex - end.xIndex) == 1 && start.yIndex == end.yIndex)
		{
			return true;
		}

		if (Mathf.Abs(start.yIndex - end.yIndex) == 1 && start.xIndex == end.xIndex)
		{
			return true;
		}

		return false;
	}

}
