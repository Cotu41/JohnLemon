using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathFadeIn : MonoBehaviour
{

    Image youdied;

    public Color fadeToColor;
    public float fadeSpeed = 5f;
    public float fadeDelay = 0f;


    // Start is called before the first frame update
    void Start()
    {
        youdied = GetComponent<Image>();
        CharacterController.OnDeath += CharacterController_OnDeath;
    }

    private void CharacterController_OnDeath(float newAmount)
    {
        StartCoroutine(FadeInDeathScreen());
    }
    
    IEnumerator FadeInDeathScreen()
    {
        yield return new WaitForSeconds(fadeDelay);
        while(true)
        {
            youdied.color = Color.Lerp(youdied.color, fadeToColor, Time.deltaTime*fadeSpeed);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
