using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [Range(0, 10)]
    public int expandSpeed = 5;
    private float realExpandSpeed;

    private float currentTime;
    private float stepTime;
    private Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        realExpandSpeed = expandSpeed / 1000.0f;
        //expandSpeed = (1 - expandSpeed);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(Vector3.zero, startScale, currentTime);
        currentTime += realExpandSpeed;

        if (currentTime >= 1.0f)
            currentTime = 0;
    }
}
