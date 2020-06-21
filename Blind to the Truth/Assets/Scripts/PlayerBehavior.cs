using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour
{
    #region Fields
    public GameObject visibilityCirclePrefab;
    public Transform circleVisualizer;

    private Rigidbody2D m_MyRigidbody2D;
    private float m_MovementSpeed = 12f;
    private float m_MaxCircleSize = 1.5f;
    #endregion

    #region Methods
    private void Awake()
    {
        m_MyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private float m_CircleScale = 0f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (m_CircleScale < m_MaxCircleSize)
                m_CircleScale += Time.deltaTime * 3f;
            else
                m_CircleScale = m_MaxCircleSize;
            
            //TODO: Play Echolocation fillup sound on loop
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            SpawnEcholocationCircle(m_CircleScale);
            m_CircleScale = 0f;
        }

        circleVisualizer.localScale = new Vector3(m_CircleScale, m_CircleScale, 1);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float x = 0.0f, y = 0.0f;

        if (Input.GetKey(KeyCode.UpArrow))
            y = 1.0f * m_MovementSpeed;
        else if (Input.GetKey(KeyCode.DownArrow))
            y = -1.0f * m_MovementSpeed;
        if (Input.GetKey(KeyCode.LeftArrow))
            x = -1.0f * m_MovementSpeed;
        else if (Input.GetKey(KeyCode.RightArrow))
            x = 1.0f * m_MovementSpeed;

        m_MyRigidbody2D.velocity = new Vector2(x, y);
    }

    private void SpawnEcholocationCircle(float scale)
    {
        //TODO: Play the echolocation noise
        GameObject echo = Instantiate(visibilityCirclePrefab, transform.position, Quaternion.identity);
        echo.transform.localScale = new Vector3(scale, scale, 1f);
    }
    #endregion
}
