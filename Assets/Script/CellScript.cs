
/*
 Grid layout and neighbour max 8 neighbour 
  NW  N  NE
    \ | /
 W___\|/___E
     /|\
    / | \
  SW  S  SE
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour {
    public CellState state = CellState.NONE;
    public GameObject N,NW,W,SW,S,SE,E,NE;
    public bool isLife;
    int row, col;

    void Start ()
    {
        if(isLife)
            state = CellState.LIFE;
        else
            state = CellState.DEATH;

    }

    void OnMouseDown()
    {
        if(GameManager.gameStarted)
            ReplaceWithLife();
    }

    /// <summary>
    /// Replace thes cell with life cell
    /// </summary>
    public void ReplaceWithLife ()
    {
        GameManager.ShareInstance().UpdateCellList(this.GetComponent<CellScript>());
        GridAlignManager.Instance.CreateLifeCell(transform.position);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Replace thes cell with death cell
    /// </summary>
    public void ReplaceWithDeath ()
    {
        GameManager.ShareInstance().UpdateCellList(this.GetComponent<CellScript>());
        GridAlignManager.Instance.CreateDeathCell(transform.position);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Fetch neighbour cells
    /// </summary>
    public void UpdateNeighbour ()
    {
        string name = this.name;
        string[] nameSplit = name.Split(',');
        int.TryParse(nameSplit[0],out row);
        int.TryParse(nameSplit[1], out col);

        string nameN = (row + 1).ToString() + "," + (col).ToString();
        string nameNE = (row +1).ToString() + "," + (col + 1).ToString();
        string nameNW = (row + 1).ToString() + "," + (col - 1).ToString();
        string nameE = (row).ToString() + "," + (col + 1).ToString();
        string nameW = (row).ToString() + "," + (col - 1).ToString();
        string nameS = (row - 1).ToString() + "," + (col).ToString();
        string nameSW = (row - 1).ToString() + "," + (col - 1).ToString();
        string nameSE = (row - 1).ToString() + "," + (col + 1).ToString();
        
        N = GameObject.Find(nameN);
        NE = GameObject.Find(nameNE);
        NW = GameObject.Find(nameNW);
        E = GameObject.Find(nameE);
        W = GameObject.Find(nameW);
        S = GameObject.Find(nameS);
        SW = GameObject.Find(nameSW);
        SE = GameObject.Find(nameSE);
    }

}

