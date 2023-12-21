using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    /* Controls for obstacle objects */

    public float rotatePlus = 10;
    public Vector3 rotOfAx;
    public enum ObstacleType
    {
        Ax,
        Plus,
    }

    public ObstacleType currentType;

    private void Start()
    {
        rotOfAx = new Vector3(-40,-90 , 0);
    }

    private void Update()
    {
        switch(currentType){
            case ObstacleType.Ax:
                if(transform.rotation == Quaternion.Euler(0, -90, 0))
                {
                    transform.DORotate(new Vector3(-40, -90, 0), 1f).OnComplete(() => transform.DORotate(new Vector3(0, -90, 0), 1f));
                }
                break;
            case ObstacleType.Plus:
                transform.Rotate(new Vector3(0f, rotatePlus, 0f), Space.World);
                break;
        }
    }


}
