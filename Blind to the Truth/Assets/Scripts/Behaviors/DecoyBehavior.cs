using UnityEngine;

public class DecoyBehavior : MonoBehaviour
{
    private float m_DecoyLifetime = 8f;
    private float m_TimeElapsed = 0f;

    private void Update()
    {
        if (m_TimeElapsed < m_DecoyLifetime)
            m_TimeElapsed += Time.deltaTime;
        else
            Destroy(gameObject);
    }
}
