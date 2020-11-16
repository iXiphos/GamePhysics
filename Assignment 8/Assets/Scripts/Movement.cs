using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateCharacter();
    }


    void rotateCharacter()
    {
        if (transform.rotation.z > -90 || transform.rotation.z < 90)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                transform.Rotate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                transform.Rotate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
            }
        }
    }

}
