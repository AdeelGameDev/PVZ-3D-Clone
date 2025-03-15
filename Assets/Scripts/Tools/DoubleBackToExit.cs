using System.Collections;
using UnityEngine;

public class DoubleBackToExit : MonoBehaviour
{
    private bool backButtonPressedOnce = false;
    private float timeSinceLastBackButtonPress;
    public float doublePressTime = 0.5f; // Time window for double press in seconds

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (backButtonPressedOnce)
            {
                if (Time.time - timeSinceLastBackButtonPress < doublePressTime)
                {
                    Application.Quit();
                }
                else
                {
                    backButtonPressedOnce = false;
                }
            }
            else
            {
                backButtonPressedOnce = true;
                timeSinceLastBackButtonPress = Time.time;

                // Show the toast message using Android native functions
                AndroidNativeFunctions.ShowToast("Tap again to exit", true);
                StartCoroutine(ResetBackButtonPress());
            }
        }
    }

    private IEnumerator ResetBackButtonPress()
    {
        yield return new WaitForSeconds(doublePressTime);
        backButtonPressedOnce = false;
    }
}
