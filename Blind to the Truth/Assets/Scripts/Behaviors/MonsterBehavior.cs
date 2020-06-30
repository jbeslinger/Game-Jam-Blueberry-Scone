using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MonsterBehavior : MonoBehaviour
{
    #region Fields
    public Transform myTarget = null;

    private AudioSource m_ScaryMusic;
    private float m_MovementSpeed = 0.007f;
    private float m_MovementSpeedMultiplier = 1f;
    private int m_MonsterPhase = 0;
    private bool m_Alive = false;
    private float m_DistanceToTarget;
    #endregion

    #region Properties
    private bool IsVisible { get {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
                return true;
            else
                return false;
        } }
    #endregion

    #region Methods
    private void Awake()
    {
        m_ScaryMusic = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        if (!m_Alive)
            return;

        m_DistanceToTarget = Vector2.Distance(transform.position, myTarget.position);

        UpdateSpeedMultiplier();
        FollowTarget();
        UpdateMusicVolume();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            SceneManager.LoadScene(3);
    }

    public void UpdateTarget(Transform t)
    {
        if (GameObject.FindGameObjectWithTag("Decoy") != null)
            myTarget = GameObject.FindGameObjectWithTag("Decoy").transform;
        else
            myTarget = t;

    }

    private void UpdateSpeedMultiplier()
    {
        m_MovementSpeedMultiplier = m_DistanceToTarget / 34;
        m_MovementSpeedMultiplier = Mathf.Clamp(m_MovementSpeedMultiplier, 1f, 20f);
    }

    private void FollowTarget()
    {
        if (myTarget == null)
            myTarget = GameObject.FindGameObjectWithTag("Player").transform;

        if (myTarget.tag == "Player")
            transform.position = Vector2.MoveTowards(transform.position, myTarget.position, m_MovementSpeed * Mathf.Pow(m_MovementSpeedMultiplier, 4));
        else if (myTarget.tag == "Decoy")
            // Move faster towards a decoy
            transform.position = Vector2.MoveTowards(transform.position, myTarget.position, (m_MovementSpeed * 8) * Mathf.Pow(m_MovementSpeedMultiplier, 2));
    }

    public void NextPhase()
    {
        if (m_MonsterPhase < 4)
        {
            m_Alive = true;
            //TeleportCloseToPlayer();
            m_MovementSpeed *= 1.4f;
            m_MonsterPhase++;
        }
    }

    public void Attack()
    {
        m_MovementSpeed *= 1.02f;
    }

    /*private void TeleportCloseToPlayer()
    {
        if (IsVisible)
            return;

        Vector2[] directions = { Vector2.up, Vector2.left, Vector2.right, Vector2.down };
        Vector2 fromDirection = directions[Random.Range(0, 4)];
        fromDirection.x *= 60f; fromDirection.y *= 34f;
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = playerPosition + fromDirection;
    }*/

    private void UpdateMusicVolume()
    {
        if (myTarget.tag == "Player")
            m_ScaryMusic.volume = 9f / m_DistanceToTarget;
    }
    #endregion
}
