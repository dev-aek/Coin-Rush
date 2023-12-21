using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    /*Script where the control and operations of the player are typen.*/

    public Transform resetRotation;

    float bonnusCount=0;
    public Transform meeple;
    public GameObject grandChild;

    void Start()
    {
        /*InputManager.Instance.onTouchStart += ProcessPlayerSwere;
        InputManager.Instance.onTouchMove += ProcessPlayerSwere;*/
        if (StakeController.instance.coins.Count == 0)
        {
            meeple = this.gameObject.transform.GetChild(0);
            grandChild = meeple.gameObject;
            Debug.Log("Stacking");
            StakeController.instance.coins.Add(grandChild);
        }

    }


    void Update()
    {
        ProcessPlayerForwardMovement();
    }

    private void ProcessPlayerForwardMovement() // Allows the player to move forward.
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Normal)
        {
 
            transform.Translate(new Vector3(
                0f,
                0f,
                GameManager.Instance.forwardSpeed), Space.Self);
        }

        if (GameManager.Instance.currentState == GameManager.GameState.Final)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            transform.Translate(new Vector3(
                0f,
                0f,
                GameManager.Instance.forwardSpeed*2), Space.Self);
        }
    }

    /* ---- these comment lines are alternative movement method ---- */


    /* private void ProcessPlayerSwere()
     {
         if (GameManager.Instance.currentState == GameManager.GameState.Normal)
         {

                 transform.Rotate(new Vector3(0f, -InputManager.Instance.GetDirection().x * GameManager.Instance.horizontalSpeed
                     , 0f), Space.World);        

         }
     }*/

    /* -------------------- */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            if (!StakeController.instance.coins.Contains(other.gameObject))
            {

                other.GetComponent<BoxCollider>().isTrigger = true;
                other.gameObject.tag = "Untagged";
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //other.gameObject.GetComponent<Rotate>().isStackeble = true;
                other.gameObject.transform.localScale = new Vector3(75f, 75f, 75f);
                other.gameObject.transform.Rotate(0f, -90f, 0f);
                other.gameObject.AddComponent<CoinMovement>();
                other.gameObject.GetComponent<CoinMovement>().connectedNode = transform;
                other.gameObject.GetComponent<CoinMovement>().indexCoin = StakeController.instance.coins.Count;
                //other.gameObject.GetComponent<Rigidbody>().useGravity = true;

                other.gameObject.transform.Rotate(-90f, 0f, 0f);


                StakeController.instance.StackCoin(other.gameObject, StakeController.instance.coins.Count -1);

            }
        }

        if (other.gameObject.tag == "Finish")
        {
            GameManager.Instance.currentState = GameManager.GameState.Final;
        }

        if (other.gameObject.tag == "Bonnus")
        {
            if(StakeController.instance.coins.Count > 1) {

                //method by which the final scene is provided

                GameObject body = StakeController.instance.coins[StakeController.instance.coins.Count - 1];
                StakeController.instance.coins.RemoveAt(StakeController.instance.coins.Count -1);
                Destroy(body);
                Quaternion stacakeble= Quaternion.identity;
                GameObject abc = Instantiate(GameManager.Instance.stackableCoin, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z), stacakeble);
                abc.gameObject.tag = "Untagged";
                abc.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                abc.transform.parent = gameObject.GetComponentInParent<Transform>().parent;

                bonnusCount++; //calculates the number of bonuses

            }
            else if(StakeController.instance.coins.Count == 1)
            {

                GameObject body = StakeController.instance.coins[StakeController.instance.coins.Count - 1];
                StakeController.instance.coins.RemoveAt(StakeController.instance.coins.Count - 1);
                body.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                body.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z-1);
                bonnusCount++;
                GameManager.Instance.currentState = GameManager.GameState.Victory; 
                GameManager.onWinEvent?.Invoke(); // Invoke Win
                StakeController.instance.coins.Clear();
                GameManager.Instance.bonnusText.text = "Bonnus: " + bonnusCount.ToString() + "x";

                bonnusCount = 0;





            }
            else if (StakeController.instance.coins.Count == 0)
            {
                GameManager.Instance.currentState = GameManager.GameState.Victory;
                GameManager.onWinEvent?.Invoke(); // Invoke Win
                StakeController.instance.coins.Clear();
                GameManager.Instance.bonnusText.text = "Bonnus: " + bonnusCount.ToString() + "x";

                bonnusCount = 0;

            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            //Provides crash and fail handling

            for (int i = 1; i < StakeController.instance.coins.Count; i++)
            {
                GameObject _gameObject = StakeController.instance.coins[i].gameObject;
                _gameObject.GetComponent<Rigidbody>().isKinematic = false;
                _gameObject.GetComponent<BoxCollider>().isTrigger = false;

                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, 10f);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                        rb.AddExplosionForce(GameManager.Instance.explosionForce, explosionPos, 10f, 3.0F);
                }

                /* ---- these comment lines are alternative codes ---- */

                // _gameObject.GetComponent<Rigidbody>().useGravity = true;

                //_gameObject.GetComponent<Rigidbody>().AddExplosionForce(5f, _gameObject.transform.position, 10f, 3f);

            }
            //gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 20f);

            GameManager.Instance.currentState = GameManager.GameState.Failed;
            GameManager.onLoseEvent?.Invoke(); //Invoke Lose
            StakeController.instance.coins.Clear();
            bonnusCount = 0;


        }
    }
}
