using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoundAnim : MonoBehaviour
{
    private Animator animator;
    private Image image;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }
    public void SetBound(bool bound)
    {
        animator.enabled = bound;
        image.enabled = bound;
    }
    public void setColor(Color color)
    {
        image.color = color;
    }
    public bool getBoundActive()
    {
        return animator.enabled;
    }
}
