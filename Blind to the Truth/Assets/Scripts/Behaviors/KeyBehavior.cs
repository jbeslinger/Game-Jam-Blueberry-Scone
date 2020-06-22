using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    #region Fields
    private Color m_MyColor;
    #endregion

    #region
    private void Awake()
    {
        m_MyColor = GetComponentInChildren<SpriteRenderer>().color;
    }

    private void Start()
    {
        //TODO: Update the color of the surrounding maze cells
    }

    private void OnDestroy()
    {
        //TODO: Update the color of the surrounding maze cells
    }
    #endregion
}
