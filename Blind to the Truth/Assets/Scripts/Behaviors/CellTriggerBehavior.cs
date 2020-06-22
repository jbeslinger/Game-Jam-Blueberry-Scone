using UnityEngine;

public class CellTriggerBehavior : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        }
    }
}
