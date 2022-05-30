using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    public Vector2 followOffset;
    public float speed = 3;
    private Vector2 threshold;
    private Rigidbody2D rb; 

    private void Start()
    {
        threshold = calcthreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);
        Vector3 newPos = transform.position;
        if (Mathf.Abs(xDifference) >= threshold.x) newPos.x = follow.x;
        if (Mathf.Abs(yDifference) >= threshold.y) newPos.y = follow.y;
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position,newPos,moveSpeed * Time.deltaTime);
    }

    private Vector3 calcthreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y; 
        return t;
    }
}
