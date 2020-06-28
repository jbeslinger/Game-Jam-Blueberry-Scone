using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class KeyholeBehavior : MonoBehaviour
{
    #region Fields
    public Text myTextBlurb;
    #endregion

    #region Methods
    public void DepositKeys()
    {
        GetComponent<Animator>().SetTrigger("All Keys Collected");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            myTextBlurb.color = Color.white;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            myTextBlurb.color = new Color(1, 1, 1, 0);
    }
    #endregion
}
