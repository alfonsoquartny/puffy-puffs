using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
	public int xIndex;
	public int yIndex;

	board m_board;

	// Use this for initialization
	void Start()
	{

	}

	public void Init(int x, int y, board board)
	{
		xIndex = x;
		yIndex = y;
		m_board = board;

	}

	void OnMouseDown()
	{
		if (m_board != null)
		{
			m_board.ClickTile(this);
		}

	}

	void OnMouseEnter()
	{
		if (m_board != null)
		{
			m_board.DragToTile(this);
		}

	}

	void OnMouseUp()
	{
		if (m_board != null)
		{
			m_board.ReleaseTile();
		}

	}
}
