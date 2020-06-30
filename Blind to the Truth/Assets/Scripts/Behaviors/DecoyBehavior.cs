using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DecoyBehavior : MonoBehaviour
{
    #region Fields
    private AudioSource m_MyAudioSource;
    
    private float m_DecoyLifetime = 8f;
    private float m_TimeElapsed = 0f;

    private Transform m_Player;
    private float m_DistanceToPlayer = 0f;
    #endregion

    #region Methods
    private void Awake()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (m_TimeElapsed < m_DecoyLifetime)
            m_TimeElapsed += Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void LateUpdate()
    {
        m_DistanceToPlayer = Vector2.Distance(transform.position, m_Player.position);
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float maxVolume = 0.5f;
        m_MyAudioSource.volume = 0.5f / (Mathf.Log(Mathf.Clamp(m_DistanceToPlayer, 1f, 30f)) + 1);
    }
    #endregion
}
