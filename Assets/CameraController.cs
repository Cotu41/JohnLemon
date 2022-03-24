using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x_rotation = Input.GetAxis("LookHorizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, x_rotation, 0);

        float y_rotation = Input.GetAxis("LookVertical") * rotationSpeed * Time.deltaTime;
        Vector3 rotation_vector = Vector3.Cross(transform.position, Camera.main.transform.position);
        transform.Rotate(y_rotation, 0, 0);

        /*
        Vector3 fixedEulers = Vector3.Cross(transform.rotation.eulerAngles, new Vector3(1, 1, 0));
        transform.rotation = Quaternion.Euler(fixedEulers);
        */
    }
}
