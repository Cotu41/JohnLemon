using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float health = 100f;
    public float stamina = 100f;

    public float rollCost = 12f;

    public float turnSpeed = 10f;
    public float moveSpeed = 3f;

    public float stamina_regen_speed = 3f;
    public float stamina_regen_delay = 2f;

    bool freezeInput = false;
    bool dead = false;

    float rollDuration = 1f;
    float rollProgress = 0f;
    float rollSpeed = 4f;

    bool rolling = false;

    Vector3 aimForward, aimRight;
    Vector3 debugVector;

    Animator animator;
    Transform body;

    public delegate void PlayerStatChanged(float newAmount);
    public static event PlayerStatChanged OnDamageTaken, OnStaminaUsed, OnDeath;

    Coroutine stamina_regen;

    // Start is called before the first frame update
    void Start()
    {


        animator = GetComponentInChildren<Animator>();
        body = transform.GetChild(0);

        OnStaminaUsed += CharacterController_OnStaminaUsed;
        OnDeath += CharacterController_OnDeath;
    }

    private void CharacterController_OnDeath(float newAmount)
    {
        dead = true;
    }

    private void CharacterController_OnStaminaUsed(float newAmount)
    {
        if (stamina_regen != null)
            StopCoroutine(stamina_regen);

        stamina_regen = StartCoroutine(StaminaRegen());
    }

    // Update is called once per frame
    void Update()
    {

        if (!dead)
        {
            aimForward = Camera.main.transform.forward; // Move relative to camera direction
            aimRight = Camera.main.transform.right;

            Vector3 movement = new Vector3();
            float horizontal = 0, vertical = 0;

            if (!freezeInput)
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
            }


            if (Input.GetButtonDown("Jump") && stamina >= rollCost)
            {
                //print("ROLL");
                animator.SetTrigger("roll");
                rollProgress = rollDuration;
                stamina -= rollCost;
                OnStaminaUsed(stamina);
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                TakeDamage(10);
            }

            movement = Vector3.ProjectOnPlane(aimForward, Vector3.down) * vertical;
            movement += aimRight * horizontal;


            if (!freezeInput)
                transform.position += movement * Time.deltaTime * moveSpeed;



            //movement = transform.localToWorldMatrix.MultiplyPoint(movement);




            if (horizontal + vertical != 0)
            {
                body.rotation = Quaternion.Lerp(body.rotation, Quaternion.LookRotation(movement), Time.deltaTime * turnSpeed);
                animator.SetBool("moving", true);
            }
            else animator.SetBool("moving", false);

            if (rollProgress > 0)
            {
                freezeInput = true;
                rollProgress -= Time.deltaTime;
                transform.position += body.transform.forward * rollSpeed * Time.deltaTime;
                rolling = true;
            }
            else
            {
                rolling = false;
                freezeInput = false;
            }


            //movement -= body.forward;
            //movement = Vector3.Normalize(movement);

            animator.SetFloat("x_move", movement.x);
            animator.SetFloat("z_move", movement.z);

        }
        

        //debugVector = movement;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        OnDamageTaken(health);
        if(health <= 0)
        {
            animator.SetTrigger("death");
            OnDeath(0);
        }
    }

    IEnumerator StaminaRegen()
    {
        yield return new WaitForSeconds(stamina_regen_delay);
        while(true)
        {
            stamina += stamina_regen_speed * Time.deltaTime;
            if (stamina >= 100)
            {
                stamina = 100;
                break;
            }
            yield return null;
        }
    }

    IEnumerator Roll()
    {
        
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, aimForward);
        Gizmos.color = Color.red;
        Debug.DrawRay(transform.position, debugVector);
    }
}
