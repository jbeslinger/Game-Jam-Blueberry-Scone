using UnityEngine;

public class ResolutionSwapTest : MonoBehaviour
{
    private int m_ScreenWidth = 480, mScreenHeight = 270;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Screen.SetResolution(m_ScreenWidth, mScreenHeight, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Screen.SetResolution(m_ScreenWidth * 2, mScreenHeight * 2, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Screen.SetResolution(m_ScreenWidth * 3, mScreenHeight * 3, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Screen.SetResolution(m_ScreenWidth * 4, mScreenHeight * 4, false);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (Screen.fullScreen)
                Screen.SetResolution(m_ScreenWidth, mScreenHeight, false);
            else
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
    }
}
