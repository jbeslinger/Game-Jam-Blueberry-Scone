using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    #region Fields
    public static UIBehavior instance;

    public Image cyanKeyImage, magKeyImage, yelKeyImage;
    public Image[] decoyImages = new Image[3];
    #endregion

    #region Methods
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        UpdateUIKeys(false, false, false);
    }

    /// <summary>
    /// Simply call this method to decrement the number of decoy icons onscreen.
    /// </summary>
    public void RemoveOneDecoyIcon()
    {
        if (decoyImages[0].gameObject.activeSelf)
            decoyImages[0].gameObject.SetActive(false);
        else if (decoyImages[1].gameObject.activeSelf)
            decoyImages[1].gameObject.SetActive(false);
        else if (decoyImages[2].gameObject.activeSelf)
            decoyImages[2].gameObject.SetActive(false);
    }

    /// <summary>
    /// Pass your key flags directly to update the keys on the Canvas.
    /// </summary>
    public void UpdateUIKeys(bool cyanKeyFlag, bool magKeyFlag, bool yelKeyFlag)
    {
        cyanKeyImage.gameObject.SetActive(cyanKeyFlag);
        magKeyImage.gameObject.SetActive(magKeyFlag);
        yelKeyImage.gameObject.SetActive(yelKeyFlag);
    }
    #endregion
}
