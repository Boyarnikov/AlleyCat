using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] List<Window> windows;
    [SerializeField] float timeDelta;
    private float timeUpdated = 0f;
    private float timeCurrent = 0f;

    void OpenRandomWindow()
    {
        int w = Random.Range(0, windows.Count);
        for (int i = 0; i < windows.Count; i++) {
            windows[i].SetOpen(i == w);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCurrent += Time.deltaTime;
        if (timeCurrent > timeDelta + timeUpdated) {
            timeUpdated += timeDelta;
            OpenRandomWindow();
        }
    }
}
