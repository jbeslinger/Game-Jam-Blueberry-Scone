using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnimationSFX : MonoBehaviour
{
    #region Fields
    private AudioSource m_MyAudioSource;
    #endregion

    #region Methods
    private void Awake()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    public void PlaySFXClip(AudioClip sfxClipToPlay)
    {
#if UNITY_EDITOR
        if (sfxClipToPlay == null)
        {
            Debug.LogError("No clip, idiot.");
            return;
        }
#endif
        m_MyAudioSource.PlayOneShot(sfxClipToPlay);
    }
    #endregion
}
