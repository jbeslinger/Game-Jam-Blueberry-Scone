using UnityEngine;

public class WhiteNoisePlayerBehavior : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
