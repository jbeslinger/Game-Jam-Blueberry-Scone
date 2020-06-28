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
        foreach (TilemapRenderer t in GetComponentsInChildren<TilemapRenderer>())
            t.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }
    #endregion
}
