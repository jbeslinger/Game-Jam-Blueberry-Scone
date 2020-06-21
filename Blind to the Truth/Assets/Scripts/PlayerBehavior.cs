using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour
{
    #region Fields
    private Rigidbody2D m_MyRigidbody2D;
    private float m_Speed = 12f;
    #endregion

    #region Methods
    private void Awake()
    {
        m_MyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float x = 0.0f, y = 0.0f;

        if (Input.GetKey(KeyCode.UpArrow))
            y = 1.0f * m_Speed;
        else if (Input.GetKey(KeyCode.DownArrow))
            y = -1.0f * m_Speed;
        if (Input.GetKey(KeyCode.LeftArrow))
            x = -1.0f * m_Speed;
        else if (Input.GetKey(KeyCode.RightArrow))
            x = 1.0f * m_Speed;

        m_MyRigidbody2D.velocity = new Vector2(x, y);
    }
    #endregion
}
