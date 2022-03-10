using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float turnSpeed = 20f;





    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    AudioSource m_AudioSource;
    Quaternion m_Rotation = Quaternion.identity;



    //DEBUG

    Vector3 db_forward;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0, vertical);
        m_Movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        db_forward = desiredForward;
        m_Rotation = Quaternion.LookRotation(desiredForward);

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        if(!m_AudioSource.isPlaying && isWalking)
        {
            m_AudioSource.Play();
        }
        else
        {
            m_AudioSource.Stop();
        }
    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, db_forward);
    }
}
