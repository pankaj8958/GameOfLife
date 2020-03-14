using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public List<CellScript> cellList;
    public GameObject startPanel;
    public GameObject startButton, clearButton, StopButton;
    public static bool gameStarted = false;

    public static GameManager ShareInstance ()
    {
        if (_instance == null)
            _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
        return _instance;
    }

    public void StartGame ()
    {
        gameStarted = true;
        if (startPanel)
            startPanel.SetActive(false);
    }

    /// <summary>
    /// Start checking state by logic
    /// </summary>
    public void StartCheckingState ()
    {
        StartCoroutine( CheckfForStateChange());
        startButton.gameObject.SetActive(false);
        StopButton.gameObject.SetActive(true);
        clearButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Stop button action
    /// </summary>
    public void StopCheckingAnyMore ()
    {
        StopAllCoroutines();
       // StopCoroutine(CheckfForStateChange());
        startButton.gameObject.SetActive(true);
        StopButton.gameObject.SetActive(false);
        clearButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// clearButton action as it clear whole grid
    /// </summary>
    public void ClearAllGrid ()
    {
        ResetGrid();

        startButton.gameObject.SetActive(true);
        StopButton.gameObject.SetActive(false);
        clearButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// UpdatestateINDelay
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckfForStateChange ()
    {
        while(cellList != null)
        {
            for (int i = 0; i < cellList.Count; i++)
            {
                if (cellList[i] == null)
                    continue;
                CellState currentState = cellList[i].state;
                CellState newState = ConwaysRule.Instance().GetUpdateCellState(cellList[i]);

                if(!currentState.Equals(newState)) {
                    if(newState.Equals(CellState.LIFE))
                    {
                        cellList[i].ReplaceWithLife();
                    } else
                    {
                        cellList[i].ReplaceWithDeath();
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    /// <summary>
    /// adding the new to list and remove old
    /// </summary>
    /// <param name="listToReplace"></param>
    /// <param name="listReplacing"></param>
    public void UpdateCellList(CellScript listToReplace)
    {
        if (cellList.Contains(listToReplace))
            cellList.Remove(listToReplace);
    }

    /// <summary>
    /// add neighbour objects
    /// </summary>
    public void UpdateNeighbourOfCell()
    {
        if (cellList != null)
            cellList = cellList;
        StartCoroutine(UpdateNeighbourInDelay());
    }

    IEnumerator UpdateNeighbourInDelay ()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < cellList.Count; i++)
            cellList[i].UpdateNeighbour();
    }

    /// <summary>
    /// Reset all grid to death
    /// </summary>
    void ResetGrid ()
    {
        if(cellList != null && cellList.Count > 0)
        {
            for (int i = 0; i < cellList.Count; i++)
            {
                if(cellList[i].state == CellState.LIFE)
                    cellList[i].ReplaceWithDeath();
            }
            if (!CheckIfAllReset())
                ResetGrid();
        }
    }

    /// <summary>
    /// Check if all cell are reset.
    /// </summary>
    /// <returns></returns>
    bool CheckIfAllReset ()
    {
        bool allReset = true;
        for (int i = 0; i < cellList.Count; i++)
            if (cellList[i].state == CellState.LIFE)
                allReset = false;
        return allReset;
    }
}

public enum CellState
{
    LIFE,
    DEATH,
    NONE
}

