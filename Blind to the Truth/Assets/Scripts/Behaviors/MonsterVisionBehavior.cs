using UnityEngine;

public class MonsterVisionBehavior : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            transform.parent.GetComponent<MonsterBehavior>().myTarget = collision.transform;
            transform.parent.GetComponent<MonsterBehavior>().Attack();
        }
    }
}
