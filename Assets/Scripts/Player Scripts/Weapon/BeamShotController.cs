using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShotController : ShotController
{
    private float maxSize;
    private float currSize;

    private float cuurTrailSize;
    private TrailRenderer trailRenderer;

    protected override void Start()
    {
        base.Start();
        //transform_.localScale = new Vector3(shotSize, transform_.localScale.y, transform_.localScale.z);
        maxSize = shotSize+.7f*shotSize;
        currSize = shotSize;
        trailRenderer = GetComponent<TrailRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        IncreaseSize();

    }
    private void IncreaseSize()
    {
        currSize = Mathf.Lerp(currSize, maxSize, Time.deltaTime);
        transform_.localScale = new Vector3(currSize, currSize, currSize);
        trailRenderer.widthMultiplier = currSize;
    }
}
