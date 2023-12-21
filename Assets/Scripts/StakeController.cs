using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StakeController : MonoBehaviour
{
    /* This is Stacking Controller. Manages trail stacking of coins */ 

    public CinemachineVirtualCamera vCam;
    public static StakeController instance;
    public Transform meeple;
    public GameObject grandChild;
    float movementDelay = 1f;
    
    public List<GameObject> coins = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    
        }
    }
 

public void StackCoin(GameObject other, int index)
    {
        
        Debug.Log(coins[index]);
        Debug.Log(coins.Count);
        Debug.Log("Stacking2");
        //other.transform.parent = transform;
        Vector3 newPos = coins[index].transform.localPosition;
        Debug.Log("Stacking1");
        newPos.z -= 1.75f;
        other.transform.localPosition = newPos;

        coins.Add(other);

        if (coins.Count > 6)
            vCam.m_Lens.FieldOfView = 50 + (index);
        Debug.Log("Stacked");

    }

    /* ---- these comment lines are alternative movement methods ---- */


    /* private void MoveListCoins()
     {
         float randomNumber = Random.Range(-1f* InputManager.Instance.GetDirection().x, 1f* -InputManager.Instance.GetDirection().x);

         for (int i = 1; i <coins.Count; i++)
         {
             Vector3 pos = coins[i].transform.localPosition; 
             pos.x = randomNumber;
             coins[i].transform.DOLocalMove(pos, movementDelay);
         }
     }*/

    /*
    private void MoveOrigins()
    {
        for (int i = 1; i < coins.Count; i++)
        {
            Vector3 pos = coins[i].transform.localPosition;
            pos.x = coins[0].transform.localPosition.x;
            coins[i].transform.DOLocalMove(pos, 0.70f);
        }
    }*/

    /* -------------------- */
}
