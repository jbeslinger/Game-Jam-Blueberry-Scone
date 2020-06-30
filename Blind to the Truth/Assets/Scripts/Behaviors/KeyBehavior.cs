using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(AudioSource))]
public class KeyBehavior : MonoBehaviour
{
    #region Enums
    private enum ColorChannel { NONE, RED, GREEN, BLUE };
    private enum Mode { ADD, SUBTRACT };
    #endregion

    #region Constants
    private const float CELL_HORIZ_UNIT = 60f, CELL_VERT_UNIT = 34f;
    #endregion

    #region Hardcoded Values
    private Vector2[] m_RaycastPositions = { (Vector2.up * CELL_VERT_UNIT), (Vector2.up * CELL_VERT_UNIT) + (Vector2.right * CELL_HORIZ_UNIT), (Vector2.right * CELL_HORIZ_UNIT), (Vector2.right * CELL_HORIZ_UNIT) + (Vector2.down * CELL_VERT_UNIT), (Vector2.down * CELL_VERT_UNIT), (Vector2.down * CELL_VERT_UNIT) + (Vector2.left * CELL_HORIZ_UNIT), (Vector2.left * CELL_HORIZ_UNIT), (Vector2.left * CELL_HORIZ_UNIT) + (Vector2.up * CELL_VERT_UNIT) };
    #endregion

    #region Fields
    private ColorChannel m_ColorChannel = ColorChannel.NONE;
    #endregion

    #region Methods
    private void Awake()
    {
        Color myColor = GetComponentInChildren<SpriteRenderer>().color;

        if (myColor.r == 0)
            m_ColorChannel = ColorChannel.RED;
        else if (myColor.g == 0)
            m_ColorChannel = ColorChannel.GREEN;
        else if (myColor.b == 0)
            m_ColorChannel = ColorChannel.BLUE;
    }

    private void Start()
    {
        ChangeNeighborCellColors(Mode.SUBTRACT);
    }

    private void OnDestroy()
    {
        try
        {
            ChangeNeighborCellColors(Mode.ADD);
        }
        catch { }
    }

    /// <summary>
    /// This method casts out to all of the neighboring cells & updates their tilemap colors to the key color.
    /// </summary>
    private void ChangeNeighborCellColors(Mode mode)
    {
        Vector2 origin = transform.position;
        RaycastHit2D hit;



        // CURRENT CELL
        hit = Physics2D.Raycast(origin, origin);
        foreach (Tilemap t in hit.transform.GetComponentsInChildren<Tilemap>())
        {
            Color calculatedColor = t.color;

            switch (m_ColorChannel)
            {
                case ColorChannel.NONE:
                    break;
                case ColorChannel.RED:
                    if (mode == Mode.SUBTRACT)
                        calculatedColor.r -= 1f;
                    else if (mode == Mode.ADD)
                        calculatedColor.r += 1f;
                    break;
                case ColorChannel.GREEN:
                    if (mode == Mode.SUBTRACT)
                        calculatedColor.g -= 1f;
                    else if (mode == Mode.ADD)
                        calculatedColor.g += 1f;
                    break;
                case ColorChannel.BLUE:
                    if (mode == Mode.SUBTRACT)
                        calculatedColor.b -= 1f;
                    else if (mode == Mode.ADD)
                        calculatedColor.b += 1f;
                    break;
            }

            t.color = calculatedColor;
        }



        // OUTER RING OF CELLS
        foreach (Vector2 v in m_RaycastPositions)
        {
            hit = Physics2D.Raycast(origin + v, origin + v);
            if (hit.collider != null)
            {
                foreach (Tilemap t in hit.transform.GetComponentsInChildren<Tilemap>())
                {
                    Color calculatedColor = t.color;

                    switch (m_ColorChannel)
                    {
                        case ColorChannel.NONE:
                            break;
                        case ColorChannel.RED:
                            if (mode == Mode.SUBTRACT)
                                calculatedColor.r -= 0.6f;
                            else if (mode == Mode.ADD)
                                calculatedColor.r += 0.6f;
                            break;
                        case ColorChannel.GREEN:
                            if (mode == Mode.SUBTRACT)
                                calculatedColor.g -= 0.6f;
                            else if (mode == Mode.ADD)
                                calculatedColor.g += 0.6f;
                            break;
                        case ColorChannel.BLUE:
                            if (mode == Mode.SUBTRACT)
                                calculatedColor.b -= 0.6f;
                            else if (mode == Mode.ADD)
                                calculatedColor.b += 0.6f;
                            break;
                    }

                    t.color = calculatedColor;
                }
            }
        }


    }
    #endregion
}
