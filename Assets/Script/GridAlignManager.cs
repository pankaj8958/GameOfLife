using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAlignManager : MonoBehaviour {

    public int MAX_ROW = 8;
    public int MAX_COL = 8;
    public GameObject lifeObject;
    public GameObject deathObject;

    public static GridAlignManager Instance;

    void Awake ()
    {
        if(Instance == null)
        {
            Instance = this.GetComponent<GridAlignManager>();
        }
    }

    // Use this for initialization
    void Start () {
        AdjustGrid(this.transform);
    }
	
    /// <summary>
    /// Adjust all grid considering grid size is 1 x 1
    /// </summary>
    /// <param name="parent"></param>
    void AdjustGrid (Transform parent)
    {
        GameManager.ShareInstance().cellList = new List<CellScript>();
            int rowCount = MAX_ROW;
            int columnCount = MAX_COL;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                int x = i;
                int y = j;
                if(x > 0 && x > rowCount/2)
                {
                    x = rowCount - i;
                    x = -x;
                }
                if(y > 0 && y > columnCount/2)
                {
                    y = columnCount - j;
                    y = -y;
                }
                CreateDeathCell(x, y);
            }

        }

       GameManager.ShareInstance().UpdateNeighbourOfCell();
    }

    /// <summary>
    /// create grid cell base on position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void CreateLifeCell (int x, int y)
    {
        if(lifeObject)
        {
            GameObject obj = Instantiate(lifeObject);
            obj.transform.position = new Vector2((float)x/2f,(float)y/2f);
            obj.name = GetCellName(x,y);
            GameManager.ShareInstance().cellList.Add(obj.GetComponent<CellScript>());
        }
    }

 
    /// <summary>
    /// create a life cell using position
    /// </summary>
    /// <param name="pos"></param>
    public void CreateLifeCell(Vector2 pos)
    {
        if (lifeObject)
        {
            GameObject obj = Instantiate(lifeObject);
            obj.transform.position = pos;
            obj.name = GetCellName(pos);
            obj.GetComponent<CellScript>().state = CellState.LIFE;
            GameManager.ShareInstance().cellList.Add(obj.GetComponent<CellScript>());
            GameManager.ShareInstance().UpdateNeighbourOfCell();
        }
    }

    /// <summary>
    /// Create death cell on grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void CreateDeathCell(int x, int y)
    {
        if (lifeObject)
        {
            GameObject obj = Instantiate(deathObject);
            obj.transform.position = new Vector2((float)x / 2f, (float)y / 2f);
            obj.name = GetCellName(x, y);
            GameManager.ShareInstance().cellList.Add(obj.GetComponent<CellScript>());
        }
    }

    /// <summary>
    /// Create a death cell on grid
    /// </summary>
    /// <param name="pos"></param>
    public void CreateDeathCell(Vector2 pos)
    {
        if (lifeObject)
        {
            GameObject obj = Instantiate(deathObject);
            obj.transform.position = pos;
            obj.name = GetCellName(pos);
            obj.GetComponent<CellScript>().state = CellState.DEATH;
            GameManager.ShareInstance().cellList.Add(obj.GetComponent<CellScript>());
            GameManager.ShareInstance().UpdateNeighbourOfCell();
        }
    }


    /// <summary>
    /// assign name of cell by x, y
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    string GetCellName(int x, int y)
    {
        string name = string.Empty;
        name = (y + MAX_COL / 2) + "," + (x + MAX_ROW / 2);
        return name;
    }

    /// <summary>
    /// assign name of cell using position
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    string GetCellName(Vector2 pos)
    {
        string name = string.Empty;
        name = ((pos.y * 2f) + MAX_COL / 2) + "," + ((pos.x * 2) + MAX_ROW / 2);
        return name;
    }

    

    
}
