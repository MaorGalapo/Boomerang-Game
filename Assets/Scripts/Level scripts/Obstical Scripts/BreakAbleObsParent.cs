using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAbleObsParent : MonoBehaviour
{
    private Transform transform_;
    private GeneralMove child;
    private bool isChild;
    // Start is called before the first frame update
    void Start()
    {
        int x = getNewRotation();
        transform_ = GetComponent<Transform>();
        transform_.transform.Rotate(x, 90, -90);
        isChild = (transform_.childCount > 0);
        if (isChild)
            child = GetComponentInChildren<GeneralMove>();
    }
    private void Update()
    {
        if (isChild&child == null)
            Destroy(gameObject);
    }
    int getNewRotation()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                return 90;
            case 1:
                return 135;
            case 2:
                return -135;
            case 3:
                return 0;
        }
        return 90;
    }
}
