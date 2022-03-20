using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;
using System.Linq;

public class Jumper : Agent
{
    public float maxsteps = 1100f;
    public float setmaxsteps;
    private Vector3 startingPosition;
    public float vel = 2;
    public static float actualscore;
    public Vector3 movimento = new Vector3(0, 0, 0);
    private List<float> listScore = new List<float>();
    public static float avg;
    Queue<float> myQ = new Queue<float>();
    private int queueN = 0;
    public override void Initialize()
    {

        startingPosition = transform.position;
    }

    public void Reset()
    {
        transform.position = startingPosition;
        setmaxsteps = maxsteps;
        actualscore = GetCumulativeReward();
        if (queueN < 20)
        {
            queueN++;
            myQ.Enqueue(actualscore);
        }
        else
        {
            myQ.Enqueue(actualscore);
            myQ.Dequeue();
        }
        avg = Queryable.Average(myQ.AsQueryable());
        //tenere solo ultimi valori
        print(avg);
        EndEpisode();

    }

    public void Update()
    {
        setmaxsteps--;

        AddReward(-0.0001f);
        if (setmaxsteps <= 0)
        {
            Reset();
        }
        if (transform.position.y < -1)
        {
            Reset();
            AddReward(-0.5f);
        }

    }

    public override void OnActionReceived(float[] vectorAction)
    {

        movimento = Vector3.zero;
        if (Mathf.FloorToInt(vectorAction[0]) == 0) { movimento += Vector3.forward; }
        if (Mathf.FloorToInt(vectorAction[0]) == 1) { movimento += -Vector3.forward; }
        if (Mathf.FloorToInt(vectorAction[1]) == 0) { movimento += Vector3.left; }
        if (Mathf.FloorToInt(vectorAction[1]) == 1) { movimento += -Vector3.left; }

    }
    private void FixedUpdate()
    {
        transform.position += movimento * Time.fixedDeltaTime * vel;

    }

    private void OnTriggerEnter(Collider collidedObj)
    {
        AddReward(1f); //New  
        Reset();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall") == true)
        {
            //AddReward(-0.1f);
        }
    }


}