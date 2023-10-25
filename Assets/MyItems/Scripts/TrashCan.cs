using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(90 * Time.deltaTime, Vector3.up);
        if (transform.position.x > 10)
        {
            transform.position -= Vector3.right * 18f;
        }
    }
}
