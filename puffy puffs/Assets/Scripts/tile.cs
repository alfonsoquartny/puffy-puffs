using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    board m_board;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

   public void Init(int x, int y, board board)
    {
        xIndex = x;
        yIndex = y;
        m_board = board;
    }
}