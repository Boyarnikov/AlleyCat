using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] GameObject trash;
    [SerializeField] bool open = false;
    Vector3 open_pos;

    void Start()
    {
        open_pos = transform.position;
    }

    public void SetOpen(bool state)
    {
        open = state;
        if (open)
        {
            Instantiate(trash).transform.position = transform.position;
            Instantiate(trash).transform.position = transform.position;
            Instantiate(trash).transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            transform.position = open_pos;
        }
        else
        {
            transform.position = open_pos + new Vector3(0, 0, 1);
        }
    }
}
