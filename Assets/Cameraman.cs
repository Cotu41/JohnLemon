using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraman : MonoBehaviour
{

    public float rotationSpeed = 100f;
    public float cameraDistance = 1f;

    Transform lookTarget;



    // Start is called before the first frame update
    void Start()
    {
        ResetLook();
    }

    public void ResetLook()
    {
        lookTarget = transform.parent;
    }

    public void SetLook(Transform lookTarget)
    {
        this.lookTarget = lookTarget;
    }

    // Update is called once per frame
    void FixedUpdate()
    {



        float x_rotation = Input.GetAxis("LookHorizontal");
        

        float y_rotation = Input.GetAxis("LookVertical");
        Vector3 roto =  Vector3.Normalize((transform.up * y_rotation) + (transform.right * x_rotation * -1)) * rotationSpeed;
        transform.localPosition += roto * Time.deltaTime;
        transform.localPosition = Vector3.Normalize(transform.localPosition) * cameraDistance;
        

    }


    private void Update()
    {
        transform.LookAt(lookTarget);
    }
}
