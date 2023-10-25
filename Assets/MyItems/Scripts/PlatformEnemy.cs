using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnemy : MonoBehaviour
{
    [SerializeField] GameObject prop;
    [SerializeField] float timeDelta;
    [SerializeField] float chance;
    private float timeUpdated = 0f;
    private float timeCurrent = 0f;
    private Vector3 propScale;

    private void Start()
    {
        propScale = prop.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timeCurrent += Time.deltaTime;
        if (timeCurrent > timeDelta + timeUpdated)
        {
            timeUpdated += timeDelta;
            if (Random.Range(0f, 1f) < chance)
            {
                tag = "Untagged";
                prop.transform.localScale = new Vector3(propScale.x, propScale.y * 0.9f, propScale.z);
            }
            else
            {
                tag = "platform";
                prop.transform.localScale = propScale;
            }
        }
    }
}
