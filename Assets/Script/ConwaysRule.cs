
/*Rule:-
Any live cell with fewer than two live neighbors dies, as if by underpopulation.
Any live cell with two or three live neighbors lives on to the next generation.
Any live cell with more than three live neighbors dies, as if by overpopulation.
Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConwaysRule {

    static ConwaysRule _instance;

    public static ConwaysRule Instance ()
    {
        if (_instance == null)
            _instance = new ConwaysRule();

        return _instance;
    }

    public CellState GetUpdateCellState (CellScript cell)
    {
        CellState state = cell.state;
        int liveNeighbour = GetLiveNeighbourCount(cell);
        int deathNeighbour = GetDeathNighbour(cell);
        Debug.Log("---"+state+"---"+liveNeighbour);
        if (state.Equals(CellState.LIFE))
        {
            if (liveNeighbour < 2)
                state = CellState.DEATH;//Underpopulation
            else if (liveNeighbour > 3)
                state = CellState.DEATH;//Overpopulation
            else
                state = CellState.LIFE;//pass
        }
        else if(state.Equals(CellState.DEATH))
        {
            if (liveNeighbour == 3)
                state = CellState.LIFE;//reproduction
        }
        Debug.Log("----"+state);
        return state;
    }

    int GetLiveNeighbourCount (CellScript cell)
    {
        int liveNeighbour = 0;
        if (cell.N != null && cell.N.GetComponent<CellScript>().state.Equals(CellState.LIFE))
            liveNeighbour++;
        if (cell.NW != null && cell.NW.GetComponent<CellScript>().state.Equals(CellState.LIFE))
            liveNeighbour++;
        if (cell.NE != null && cell.NE.GetComponent<CellScript>().state.Equals(CellState.LIFE))
            liveNeighbour++;
        if (cell.E != null && cell.E.GetComponent<CellScript>().state.Equals(CellState.LIFE))
            liveNeighbour++;
        if (cell.W != null && cell.W.GetComponent<CellScript>().state.Equals(CellState.LIFE))
            liveNeighbour++;
        if (cell.S != null && cell.S.GetComponent<CellScript>().state.Equals(CellState.LIFE))
            liveNeighbour++;
        if (cell.SW != null && cell.SW.GetComponent<CellScript>().state.Equals(CellState.LIFE))
            liveNeighbour++;
        if (cell.SE != null && cell.SE.GetComponent<CellScript>().state.Equals(CellState.LIFE))
            liveNeighbour++;
        return liveNeighbour;
    }

    int GetDeathNighbour (CellScript cell)
    {
        int deathNeighbour = 0;
        if (cell.N != null && cell.N.GetComponent<CellScript>().state.Equals(CellState.DEATH))
            deathNeighbour++;
        if (cell.NW != null && cell.NW.GetComponent<CellScript>().state.Equals(CellState.DEATH))
            deathNeighbour++;
        if (cell.NE != null && cell.NE.GetComponent<CellScript>().state.Equals(CellState.DEATH))
            deathNeighbour++;
        if (cell.E != null && cell.E.GetComponent<CellScript>().state.Equals(CellState.DEATH))
            deathNeighbour++;
        if (cell.W != null && cell.W.GetComponent<CellScript>().state.Equals(CellState.DEATH))
            deathNeighbour++;
        if (cell.S != null && cell.S.GetComponent<CellScript>().state.Equals(CellState.DEATH))
            deathNeighbour++;
        if (cell.SW != null && cell.SW.GetComponent<CellScript>().state.Equals(CellState.DEATH))
            deathNeighbour++;
        if (cell.SE != null && cell.SE.GetComponent<CellScript>().state.Equals(CellState.DEATH))
            deathNeighbour++;
        return deathNeighbour;
    }

}
