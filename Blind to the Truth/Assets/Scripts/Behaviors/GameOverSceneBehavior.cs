using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameOverSceneBehavior : MonoBehaviour
{
    public void PlayGameOverAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
