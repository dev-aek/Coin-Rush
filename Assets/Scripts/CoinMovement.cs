using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    /* controls the processes of the coins */
    
    public Transform connectedNode;
    public int indexCoin;
    void Update()
    {
        transform.position = new Vector3(
            connectedNode.position.x,
            transform.position.y, 
            connectedNode.position.z - (indexCoin+1f)
        );

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Vector3 explosionPos = transform.position;
            StakeController.instance.coins.Remove(gameObject);

            Destroy(gameObject);    


        }
    }
}
