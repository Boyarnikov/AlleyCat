using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class CatMovement : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text t;
    Vector3 velocity;
    [SerializeField] BoxCollider c;
    [SerializeField] float speed = 1;
    [SerializeField] float walkAcceleration = 1;
    [SerializeField] float groundDeceleration = 1;
    [SerializeField] float jumpHeight = 1;
    [SerializeField] float jumpSpeedBoost = 1;
    [SerializeField] float gravity = 1;

    [SerializeField] GameObject cat;

    private bool grounded;
    private bool hang;
    private bool knocked;


    // Update is called once per frame
    void Update()
    {
        t.text = "";
        if (knocked)
        {
            cat.transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward);
            t.text = "ÌÀÑËÈÍÓ ÏÎÉÌÀË";
        }
        else if (hang)
        {
            cat.transform.rotation = Quaternion.AngleAxis(-45, Vector3.right);
        } else
        {
            cat.transform.rotation = Quaternion.AngleAxis(-Mathf.Max(0, Mathf.Min(180, velocity.x * 180 + 90f)) - 90, Vector3.up);
        }
        cat.transform.position = transform.position;


        grounded = false;
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        
        if (!hang & !knocked) {
            if (moveInput != 0)
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
            }
            
        }
        transform.Translate(velocity * Time.deltaTime);

        hang = false;

        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale / 2f, Quaternion.identity);

        foreach (Collider hit in hits)
        {
            if (hit == c)
                continue;

            Vector3 closestPoint = hit.transform.position;


            if (hit.tag == "knock")
            {
                knocked = true;
                hang = false;
            }

            if (hit.tag == "win" && !knocked)
            {
                Debug.Log("Win!");
                t.text = "Íèøòÿê, îêíî!";
            }

            if (hit.tag == "wall") {
                float dist_x = hit.transform.position.x - transform.position.x;
                float scales_x = (hit.transform.localScale.x + transform.localScale.x) / 2;
                if (dist_x > 0)
                {
                    scales_x *= -1;
                }

                velocity.x = 0;
                Vector3 push = new Vector3(dist_x + scales_x, 0, 0);

                transform.Translate(push);
            }

            if (hit.tag == "wall") {
                float dist_x = hit.transform.position.x - transform.position.x;
                float scales_x = (hit.transform.localScale.x + transform.localScale.x) / 2;
                if (dist_x > 0)
                {
                    scales_x *= -1;
                }

                velocity.x = 0;
                Vector3 push = new Vector3(dist_x + scales_x, 0, 0);

                transform.Translate(push);
            }

            if (hit.tag == "platform" && vInput >= 0 && !knocked || hit.tag == "floor")
            {
                if (Vector3.Angle(-closestPoint + transform.position, Vector3.up) < 90 && velocity.z <= 0.01)
                {
                    grounded = true;
                    if (hit.tag == "floor")
                        knocked = false;
                }
            }

            if (hit.tag == "hang" && vInput >= 0 && !knocked)
            {
                if (Vector3.Angle(-closestPoint + transform.position, Vector3.up) < 90 && velocity.z <= 0.01)
                {
                    hang = true;
                    velocity.x = 0;
                }
            }
        }

        if (grounded || hang)
        {
            velocity.z = 0;
            if (Input.GetButton("Jump") && !knocked)
            {
                if (hang)
                {
                    velocity.z = Mathf.Sqrt(4 * (jumpHeight + jumpSpeedBoost) * gravity);
                } else
                {
                    velocity.z = Mathf.Sqrt(2 * (jumpHeight + jumpSpeedBoost * velocity.magnitude / 2) * gravity);
                }
            if (grounded)
                {
                    knocked = false;
                }
            }
        } 
        else
        {
            velocity.z -= gravity * Time.deltaTime;
        }

        


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
