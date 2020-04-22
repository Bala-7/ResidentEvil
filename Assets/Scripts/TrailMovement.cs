using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMovement : MonoBehaviour
{

    public Transform rotateCenter;

    [Range(0, 10)]
    public int verticalSpeed;
    private float realVSpeed;

    public int circleSpeed;
    private float realCircleSpeed;

    private float currentTime = 0;

    public float localYLow = 0.2f;
    public float localYHigh = 0.5f;

    private Vector3 startLocalPos;

    [Range(0.0f, 2 * Mathf.PI)]
    public float startVerticalPhase;

    // Start is called before the first frame update
    void Start()
    {
        realVSpeed = verticalSpeed / 1000.0f;
        realCircleSpeed = circleSpeed / 1000.0f;

        startLocalPos = transform.localPosition;
        currentTime = startVerticalPhase;
    }

    // Update is called once per frame
    void Update()
    {
        float s = Mathf.Sin(currentTime);
        float v = (s >= 0) ? s : -s;
        //transform.localPosition = Vector3.Lerp(new Vector3(0,localYLow,0), new Vector3(0,localYHigh,0), v);
        Vector3 yLerp = Vector3.Lerp(new Vector3(0, localYLow, 0), new Vector3(0, localYHigh, 0), v);
        currentTime += realVSpeed;

        if (currentTime >= (2 * Mathf.PI))
            currentTime = 0;

        transform.RotateAround(rotateCenter.position, Vector3.up, 240 * Time.deltaTime);
//        Vector3 desiredPosition = (transform.position - rotateCenter.position).normalized * 0.5f + rotateCenter.position;
        Vector3 desiredPosition = new Vector3(transform.position.x - rotateCenter.position.x, 0, transform.position.z - rotateCenter.position.z).normalized * 0.4f + rotateCenter.position;

        desiredPosition = new Vector3(desiredPosition.x, yLerp.y, desiredPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 240);
    }
}
