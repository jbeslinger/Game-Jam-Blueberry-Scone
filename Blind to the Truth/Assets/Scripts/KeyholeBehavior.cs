using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KeyholeBehavior : MonoBehaviour
{
    public void DepositKeys()
    {
        GetComponent<Animator>().SetTrigger("All Keys Collected");
    }
}
