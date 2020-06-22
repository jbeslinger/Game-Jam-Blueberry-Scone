using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterBehavior : MonoBehaviour
{
    #region Fields
    public Transform myTarget = null;

    private float m_MovementSpeed = 5f / 1000;
    private int m_MonsterPhase = 0;
    private bool m_Alive = false;
    #endregion

    #region Methods
    private void LateUpdate()
    {
        if (!m_Alive)
            return;

        FollowTarget();
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

    private void FollowTarget()
    {
        if (myTarget == null)
            return;

        transform.position = Vector2.MoveTowards(transform.position, myTarget.position, m_MovementSpeed);
    }

    public void NextPhase()
    {
        if (m_MonsterPhase < 4)
        {
            m_Alive = true;
            m_MovementSpeed *= 1.5f;
            m_MonsterPhase++;
        }
    }
    #endregion
}
