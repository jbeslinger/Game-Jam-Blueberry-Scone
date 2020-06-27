using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour
{
    #region Fields
    public GameObject visibilityCirclePrefab, decoyPrefab;
    public Transform circleVisualizer;

    private Rigidbody2D m_MyRigidbody2D;
    private float m_MovementSpeed = 12f;
    private float m_MaxCircleSize = 1.5f;

    private int m_NumberOfDecoys = 3;

    private bool m_CyanKeyCollected = false, m_MagentaKeyCollected = false, m_YellowKeyCollected = false;
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
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            SpawnEcholocationCircle(m_CircleScale);
            m_CircleScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.X) &&
            GameObject.FindGameObjectWithTag("Decoy") == null && m_NumberOfDecoys > 0)
        {
            SpawnDecoy();
        }

        circleVisualizer.localScale = new Vector3(m_CircleScale, m_CircleScale, 1);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "C. Key":
                m_CyanKeyCollected = true;
                UIBehavior.instance.UpdateUIKeys(m_CyanKeyCollected, m_MagentaKeyCollected, m_YellowKeyCollected);
                GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterBehavior>().NextPhase();
                Destroy(collision.gameObject);
                break;
            case "M. Key":
                m_MagentaKeyCollected = true;
                UIBehavior.instance.UpdateUIKeys(m_CyanKeyCollected, m_MagentaKeyCollected, m_YellowKeyCollected);
                GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterBehavior>().NextPhase();
                Destroy(collision.gameObject);
                break;
            case "Y. Key":
                m_YellowKeyCollected = true;
                UIBehavior.instance.UpdateUIKeys(m_CyanKeyCollected, m_MagentaKeyCollected, m_YellowKeyCollected);
                GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterBehavior>().NextPhase();
                Destroy(collision.gameObject);
                break;
            case "Keyhole":
                OpenGoalDoor();
                break;
        }
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
        NotifyMonster(transform);
    }

    private void SpawnDecoy()
    {
        m_NumberOfDecoys--;
        GameObject decoy = Instantiate(decoyPrefab);
        decoy.transform.position = transform.position;
        NotifyMonster(decoy.transform);
        UIBehavior.instance.RemoveOneDecoyIcon();
    }

    private void OpenGoalDoor()
    {
        if (m_CyanKeyCollected && m_MagentaKeyCollected && m_YellowKeyCollected)
        {
            GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterBehavior>().NextPhase();
            //TODO: Turn on the guide arrow
            m_CyanKeyCollected = false; m_MagentaKeyCollected = false; m_YellowKeyCollected = false;
            UIBehavior.instance.UpdateUIKeys(m_CyanKeyCollected, m_MagentaKeyCollected, m_YellowKeyCollected);
            GameObject.FindGameObjectWithTag("Keyhole").GetComponent<KeyholeBehavior>().DepositKeys();
            GameObject.FindGameObjectWithTag("Goal Door").SetActive(false);
        }
    }

    private void NotifyMonster(Transform t)
    {
        GameObject.FindGameObjectWithTag("Monster").GetComponent<MonsterBehavior>().UpdateTarget(t);
    }
    #endregion
}
