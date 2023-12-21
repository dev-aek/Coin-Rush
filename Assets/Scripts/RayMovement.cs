using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMovement : MonoBehaviour
{
    /* This script controls horizontal movement and bounce effect.*/

    public float speed;
    public float velocity;
    private Camera mainCam;
    public float roadEndPoint;

    private Transform player;
    private Vector3 firstMousePos, firstPlayerPos;
    private bool moveTheBall;

    public int gap = 2;
    public float bodySpeed = 15f;

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> PositionHistory = new List<Vector3>();

    void Start()
    {
        mainCam =Camera.main;
        player = this.transform;

    }

    private void Update()
    {
        bodyParts = StakeController.instance.coins;

        if (Input.GetMouseButtonDown(0))
        {
            moveTheBall= true;
        }else if(Input.GetMouseButtonUp(0)) {
            moveTheBall= false; 
        }
        if (GameManager.Instance.currentState == GameManager.GameState.Normal)
        {
            if (moveTheBall)
            {
                Plane newPlane = new Plane(Vector3.up, 0.8f);
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

                if (newPlane.Raycast(ray, out var distance))
                {
                    Vector3 newMousePos = ray.GetPoint(distance) - firstMousePos;
                    Vector3 newPlayerPos = newMousePos + firstPlayerPos;
                    newPlayerPos.x = Mathf.Clamp(newPlayerPos.x, -roadEndPoint, roadEndPoint);
                    player.position = new Vector3(Mathf.SmoothDamp(player.position.x, newPlayerPos.x, ref velocity, speed * Time.deltaTime), player.position.y, player.position.z);
                }
            }
        }
    }

    private void FixedUpdate()
    {

        if (GameManager.Instance.currentState == GameManager.GameState.Normal)
        {
            PositionHistory.Insert(0, transform.position);
            int index = 0;
            if (bodyParts.Count > 0)
            {
                foreach (var body in bodyParts)
                {
                    if (body != null)
                    {
                        Vector3 point = PositionHistory[Mathf.Min(index * gap, PositionHistory.Count - 1)];
                        Vector3 moveDir = point - body.transform.position;
                        body.transform.position += moveDir * bodySpeed * Time.deltaTime;
                        body.transform.LookAt(point);
                        index++;

                    }
                }
            }
        }
    }

}
