using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public Image Health, Health_Dep, Stamina, Stamina_Dep;

    public float depDelay = 0.3f, depSpeed = 5f;

    float health_p = 1, stam_p = 1;

    CharacterController player;

    Coroutine h_dep, s_dep, s_reg;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<CharacterController>();


        CharacterController.OnDamageTaken += CharacterController_OnDamageTaken;
        CharacterController.OnStaminaUsed += CharacterController_OnStaminaUsed;
    }

    private void CharacterController_OnStaminaUsed(float newAmount)
    {
        if(Stamina_Dep.fillAmount < Stamina.fillAmount)
        {
            Stamina_Dep.fillAmount = Stamina.fillAmount;
        }
        stam_p = newAmount / 100f;
        Stamina.fillAmount = stam_p;

        if (s_dep != null)
            StopCoroutine(s_dep);
        s_dep = StartCoroutine(StaminaDepletion());


    }

    private void CharacterController_OnDamageTaken(float newHealth)
    {
        if(Health_Dep.fillAmount < Health.fillAmount)
        {
            Health_Dep.fillAmount = Health.fillAmount;
        }
        health_p = player.health / 100f;
        Health.fillAmount = health_p;

        if(h_dep != null)
            StopCoroutine(h_dep);
        h_dep = StartCoroutine(HealthDepletion());

        
    }

    

    IEnumerator HealthDepletion()
    {
        yield return new WaitForSeconds(depDelay);

        while(true)
        {
            Health_Dep.fillAmount = Mathf.Lerp(Health_Dep.fillAmount, health_p, Time.deltaTime * depSpeed);
            yield return null;
        }
    }

    IEnumerator StaminaDepletion()
    {
        yield return new WaitForSeconds(depDelay);

        while (true)
        {
            Stamina_Dep.fillAmount = Mathf.Lerp(Stamina_Dep.fillAmount, stam_p, Time.deltaTime * depSpeed);
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        Stamina.fillAmount = player.stamina/100f;
    }
}
