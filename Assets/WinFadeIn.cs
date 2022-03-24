using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinFadeIn : MonoBehaviour
{

    public Image winone, wintwo;

    public Color fadeToColor;
    public float fadeSpeed = 5f;
    public float fadeDelay = 0f;


    // Start is called before the first frame update
    void Start()
    {
        winone.color = Color.clear;
        wintwo.color = Color.clear;
        CharacterController.OnWin += CharacterController_OnWin;
    }

    private void CharacterController_OnWin(float newAmount)
    {
        StartCoroutine(FadeInWinScreen());
    }

   

    IEnumerator FadeInWinScreen()
    {

        yield return new WaitForSeconds(fadeDelay);
        winone.color = Color.white;
        wintwo.color = Color.white;
        while (winone.color.a > 0.01)
        {
            winone.color = Color.Lerp(winone.color, fadeToColor, Time.deltaTime * fadeSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(4);
        while(true)
        {
            wintwo.color = Color.Lerp(wintwo.color, fadeToColor, Time.deltaTime * (fadeSpeed / 2));
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
