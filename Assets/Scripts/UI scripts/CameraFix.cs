using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    public SpriteRenderer backGround;
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = backGround.bounds.size.x / backGround.bounds.size.y;

        if (screenRatio >= targetRatio)

            Camera.main.orthographicSize = backGround.bounds.size.y / 2;
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = backGround.bounds.size.y / 2 * differenceInSize;
        }

    }


}
