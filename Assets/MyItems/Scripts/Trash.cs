using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] float gravity = 1;
    [SerializeField] float jumpStart = 1;
    [SerializeField] float spread = 1;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        velocity.z = Mathf.Sqrt(2 * (jumpStart + Random.Range(-spread, spread) / 4) * gravity);
        velocity.x = Random.Range(-spread, spread);
        transform.position = new Vector3(transform.position.x, transform.position.y, - 4.8f);
    }

    // Update is called once per frame
    void Update()
    {
        velocity.z -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
        if (transform.position.y < 0 )
        {
            Destroy(gameObject);
        }
    }
}
