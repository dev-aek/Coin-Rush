using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    /* only for some rotate effects*/

    public Transform resetRotation;
    float speed = .05f;
    float timeCount = 0.0f;
    float rotateSpeed = 50;
    public bool isStackeble;

    // This can to the coin rotate effects
    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Normal)
        {
            transform.Rotate(new Vector3(0f, -InputManager.Instance.GetDirection().x * rotateSpeed, 0f), Space.World);

            if (InputManager.Instance.GetDirection().x == 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, resetRotation.rotation, timeCount * speed);
                timeCount = timeCount + Time.deltaTime;
            }

            // Alternative code lines
            if (isStackeble)
            {
                //transform.Translate(new Vector3(Mathf.Lerp(transform.position.x, transform.position.x, speed*Time.deltaTime), 0f, 0f),Space.World);

            }

        }



    }

}
