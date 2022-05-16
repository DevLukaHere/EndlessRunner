using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAtPlayer;
    private Vector3 startSet;
    private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationSet = new Vector3(0, 5, 5);

    // Start is called before the first frame update
    void Start()
    {
        transition = 0.0f;
        lookAtPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        startSet = transform.position - lookAtPlayer.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = lookAtPlayer.position + startSet;

        //X
        moveVector.x = 0;
        //Y
        moveVector.y = Mathf.Clamp(moveVector.y, 5, 5);

        if(transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            //Start animation
            transform.position = Vector3.Lerp(moveVector + animationSet, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAtPlayer.position + Vector3.up);
        }
    }

}
