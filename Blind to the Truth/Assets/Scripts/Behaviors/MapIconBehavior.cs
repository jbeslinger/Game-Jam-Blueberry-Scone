using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MapIconBehavior : MonoBehaviour
{
    #region Constants
    private const float PPU = 8f;
    private const float CELL_HORIZ_UNIT = 60f, CELL_VERT_UNIT = 34f;
    #endregion

    #region Fields
    RectTransform m_RectTransform;
    #endregion

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        m_RectTransform.localPosition = CalculateMapPosition(Camera.main.transform.position);
    }

    private Vector2 CalculateMapPosition(Vector2 cellPosition)
    {
        float x = cellPosition.x, y = cellPosition.y;
        return new Vector2(
            (x / CELL_HORIZ_UNIT) * PPU,
            (y / CELL_VERT_UNIT) * PPU);
    }
}
