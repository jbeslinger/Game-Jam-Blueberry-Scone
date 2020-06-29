using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameOverSceneBehavior : MonoBehaviour
{
    #region Constants
    private const float MOVEMENT_UNIT = 0.5f + (1f / 8f);
    #endregion

    #region Fields
    public Transform glitchParentObject;
    public Sprite[] glitchSprites;
    #endregion

    #region Methods
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

    public void WriteGlitchTextToScreen()
    {
        StartCoroutine(WriteGlitchSprite());
    }

    private IEnumerator WriteGlitchSprite()
    {
        float waitTime = 0.001f;
        int columns = 50, rows = 26;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                yield return new WaitForSeconds(waitTime);

                GameObject glitch = new GameObject("Glitch");
                glitch.AddComponent<SpriteRenderer>().sprite = glitchSprites[(rows * x) + y];
                glitch.transform.parent = glitchParentObject;
                glitch.transform.localPosition = new Vector2(x * MOVEMENT_UNIT, -y * MOVEMENT_UNIT);
            }
        }
    }
    #endregion
}
