using UnityEngine;

public class VisibilityMaskBehavior : MonoBehaviour
{
    #region Fields
    private float m_ShrinkDelay = 2.5f;
    private float m_ShrinkSpeed = 1f;
    private float m_TimeElapsed = 0f;
    #endregion

    #region Methods
    private void Update()
    {
        if (m_TimeElapsed < m_ShrinkDelay)
        {
            m_TimeElapsed += Time.deltaTime;
            return;
        }

        if (transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x - (m_ShrinkSpeed * Time.deltaTime),
                transform.localScale.y - (m_ShrinkSpeed * Time.deltaTime), 1f);
        else
            Destroy(gameObject);
    }
    #endregion
}
