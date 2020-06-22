using UnityEngine;

public class MazeParentBehavior : MonoBehaviour
{
    #region Constants
    private const float CELL_X_UNIT = 60f, CELL_Y_UNIT = 34f;
    #endregion

    #region Fields
    public GameObject[] cellPrefabs = new GameObject[30];
    public GameObject startCellPrefab, goalCellPrefab;

    private Cell[,] m_Cells;
    private Vector2[,] m_CellSpawnPoints;
    #endregion

    #region Methods
    private void Awake()
    {
        //TODO: Pull from the PlayerPrefs to get the size of the maze
        m_Cells = new Cell[7, 7];
        m_CellSpawnPoints = new Vector2[7, 7];

        GenerateCellSpawnPoints();
        GenerateNewMaze();
        FillMazeWithPrefabs();
        SpawnKeys();
    }

    // Populates the m_CellSpawnPoints array with vector2 world positions to spawn the cells in
    // Lots of very specific math
    private void GenerateCellSpawnPoints()
    {
        int midOffset = (int)((m_CellSpawnPoints.GetLength(0) / 2f) - 0.5f);

        for (int row = 0; row < m_CellSpawnPoints.GetLength(0); row++)
        {
            for (int col = 0; col < m_CellSpawnPoints.GetLength(1); col++)
            {
                float spawnPointX = CELL_X_UNIT * (row - midOffset);
                float spawnPointY = -1 * (CELL_Y_UNIT * (col - midOffset));

                m_CellSpawnPoints[row, col] = new Vector2(spawnPointX, spawnPointY);
            }
        }
    }

    private void GenerateNewMaze()
    {
        // Fill the array with empty Cell objects
        for (int row = 0; row < m_CellSpawnPoints.GetLength(0); row++) { for (int col = 0; col < m_CellSpawnPoints.GetLength(1); col++) { m_Cells[row, col] = new Cell(); } }

        // Step through the whole maze and generate random cells
        for (int row = 0; row < m_CellSpawnPoints.GetLength(0); row++)
        {
            for (int col = 0; col < m_CellSpawnPoints.GetLength(1); col++)
            {
                if (m_Cells[row, col].isStart || m_Cells[row, col].isGoal)
                    continue;

                int chanceToAddExit = 0; // 0%-100%
                switch (m_Cells[row, col].NumberOfExits)
                {
                    case 0:
                        chanceToAddExit = 100;
                        break;
                    case 1:
                        chanceToAddExit = 70;
                        break;
                    case 2:
                        chanceToAddExit = 40;
                        break;
                    case 3:
                        chanceToAddExit = 25;
                        break;
                    case 4:
                        chanceToAddExit = 0;
                        break;
                }

                // Run the odds to add a random exit to the cell
                if (Random.Range(1, 101) <= chanceToAddExit)
                {
                    Cell.Exits[] possibleExits = { Cell.Exits.NORTH, Cell.Exits.EAST, Cell.Exits.SOUTH, Cell.Exits.WEST };
                    Cell.Exits randomExit = Cell.Exits.NONE;

                    // Check the neighbors for the cell to check for nulls
                    bool looking = true;
                    while (looking)
                    {
                        randomExit = possibleExits[Random.Range(0, 4)];

                        if (m_Cells[row, col].HasTheExit(randomExit))
                            continue;

                        switch (randomExit)
                        {
                            case Cell.Exits.NORTH:
                                if (GetCellAbove(row, col) == null)
                                    continue;
                                else
                                {
                                    m_Cells[row, col].AddExit(randomExit);
                                    m_Cells[row - 1, col].AddExit(Cell.Exits.SOUTH);
                                    looking = false;
                                }
                                break;
                            case Cell.Exits.EAST:
                                if (GetCellRight(row, col) == null)
                                    continue;
                                else
                                {
                                    m_Cells[row, col].AddExit(randomExit);
                                    m_Cells[row, col + 1].AddExit(Cell.Exits.WEST);
                                    looking = false;
                                }
                                break;
                            case Cell.Exits.SOUTH:
                                if (GetCellBeneath(row, col) == null)
                                    continue;
                                else
                                {
                                    m_Cells[row, col].AddExit(randomExit);
                                    m_Cells[row + 1, col].AddExit(Cell.Exits.NORTH);
                                    looking = false;
                                }
                                break;
                            case Cell.Exits.WEST:
                                if (GetCellLeft(row, col) == null)
                                    continue;
                                else
                                {
                                    m_Cells[row, col].AddExit(randomExit);
                                    m_Cells[row, col - 1].AddExit(Cell.Exits.EAST);
                                    looking = false;
                                }
                                break;
                        }
                    }
                }
            }
        }

        // Generate the starter cell in the center
        int midpointIndex = (int)((m_CellSpawnPoints.GetLength(0) / 2f) - 0.5f);
        Cell startCell = new Cell(); startCell.isStart = true;
        startCell.AddExit(Cell.Exits.NORTH); startCell.AddExit(Cell.Exits.EAST); startCell.AddExit(Cell.Exits.SOUTH); startCell.AddExit(Cell.Exits.WEST);
        m_Cells[midpointIndex, midpointIndex] = startCell;
        m_Cells[midpointIndex - 1, midpointIndex].AddExit(Cell.Exits.SOUTH); m_Cells[midpointIndex, midpointIndex + 1].AddExit(Cell.Exits.WEST); m_Cells[midpointIndex + 1, midpointIndex].AddExit(Cell.Exits.NORTH); m_Cells[midpointIndex, midpointIndex - 1].AddExit(Cell.Exits.EAST);

        // Generate the goal cell near the top
        Cell goalCell = new Cell(); goalCell.isGoal = true;
        goalCell.AddExit(Cell.Exits.NORTH); startCell.AddExit(Cell.Exits.EAST); startCell.AddExit(Cell.Exits.WEST);
        int randomCol = Random.Range(1, m_CellSpawnPoints.GetLength(1) - 1);
        m_Cells[0, randomCol] = goalCell;
        m_Cells[0, randomCol + 1].AddExit(Cell.Exits.WEST); m_Cells[0, randomCol - 1].AddExit(Cell.Exits.EAST);
    }

    private void FillMazeWithPrefabs()
    {
        for (int row = 0; row < m_Cells.GetLength(0); row++)
        {
            for (int col = 0; col < m_Cells.GetLength(1); col++)
            {
                if (m_Cells[row, col].isStart)
                {
                    Instantiate(startCellPrefab, m_CellSpawnPoints[row, col], Quaternion.identity, transform);
                }
                else if (m_Cells[row, col].isGoal)
                {
                    Instantiate(goalCellPrefab, m_CellSpawnPoints[row, col], Quaternion.identity, transform);
                }
                else
                {
                    int cellType = (int)m_Cells[row, col].exits;
                    int prefabIndex = (cellType * 2) - Random.Range(1, 3);
                    Instantiate(cellPrefabs[cellType], m_CellSpawnPoints[row, col], Quaternion.identity, transform);
                }
            }
        }
    }

    private void SpawnKeys()
    {

    }

    private Cell GetCellAbove(int currentRow, int currentCol)
    {
        try { return m_Cells[currentRow - 1, currentCol]; }
        catch { return null; }
    }
    private Cell GetCellBeneath(int currentRow, int currentCol)
    {
        try { return m_Cells[currentRow + 1, currentCol]; }
        catch { return null; }
    }
    private Cell GetCellLeft(int currentRow, int currentCol)
    {
        try { return m_Cells[currentRow, currentCol - 1]; }
        catch { return null; }
    }
    private Cell GetCellRight(int currentRow, int currentCol)
    {
        try { return m_Cells[currentRow, currentCol + 1]; }
        catch { return null; }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (m_CellSpawnPoints == null)
            return;

        Gizmos.color = Color.cyan;
        foreach (Vector2 v in m_CellSpawnPoints)
            Gizmos.DrawSphere(v, 2f);
    }
#endif
    #endregion
}
