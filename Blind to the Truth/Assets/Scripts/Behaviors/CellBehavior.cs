using UnityEngine;
using UnityEngine.Tilemaps;

public class CellBehavior : MonoBehaviour
{
    #region Fields
    public Transform keySpawnPoint;
    #endregion

    #region Methods
    private void Awake()
    {
        foreach (TilemapRenderer r in GetComponentsInChildren<TilemapRenderer>())
        {
            r.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }
    #endregion
}
