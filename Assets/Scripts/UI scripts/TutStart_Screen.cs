using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutStart_Screen : MonoBehaviour
{
    private CanvasGroup thisCanvas;
    private float currTime =0;
    [SerializeField]
    private float fadeTime;
    void Start()
    {
        thisCanvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currTime< fadeTime)
        {
            currTime += Time.deltaTime;
            thisCanvas.alpha = Mathf.Lerp(1, 0, currTime/ fadeTime);
        }
        else
            GetComponent<Canvas>().enabled = false;
    }
}
