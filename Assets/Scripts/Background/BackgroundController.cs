using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float speed_BG;
    private float topY = 0;
    private GameObject topPlain;
    [SerializeField]
    private GameObject BG_prefab;
    private Transform BG_endPoint;

    private List<BackgroundMove> list = new List<BackgroundMove>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            list.Add(child.GetComponent<BackgroundMove>());
        }
        for(int i = 0; i < list.Count; i++)
        {
            if(i!=0)
            {
                list[i].setFollowDot(list[i - 1].endOfPlain);
            }
        }
    }
    public void checkTopX(float newY, GameObject plain)
    {
        if (newY >= topY)
        {
            topY = newY;
            topPlain = plain;
            BG_endPoint = topPlain.GetComponent<BackgroundMove>().endOfPlain;
        }
    }
    private void checkSpawnBG()
    {
        if (topPlain.transform.position.y <= 20)
        {
            list.Add(Instantiate(BG_prefab, new Vector3(0, BG_endPoint.position.y + 5.4f, 0), Quaternion.identity, transform).GetComponent<BackgroundMove>());
            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (i != 0)
            //    {
            //        list[i].setFollowDot(list[i - 1].endOfPlain);
            //    }
            //}
        }
        else
            topY = topPlain.transform.localPosition.y;
    }
    void FixedUpdate()
    {
        checkSpawnBG();
    }
}
