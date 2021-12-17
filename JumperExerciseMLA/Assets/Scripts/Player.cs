using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class Player : Agent
{
    public float force = 8.0f;
    public Transform reset = null;
    private Rigidbody rb = null;
    private bool canJump = true;

    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    // public override void OnActionReceived(float[] vectorAction)
    // {
    //     if (vectorAction[0] == 1)
    //     {
    //         Thrust();
    //     }
    // }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionsDiscrete = actions.DiscreteActions;
        if (actionsDiscrete[0] == 1)
        {
            Thrust();
        }
    }
    
    // public override void Heuristic(float[] actionsOut)
    // {
    //     actionsOut[0] = 0;
    //     if (Input.GetKey(KeyCode.UpArrow) == true)
    //     {
    //         actionsOut[0] = 1;
    //     }
    // }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actionsOutDiscrete = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.UpArrow))
            actionsOutDiscrete[0] = 1;
    }
    
    
    public override void OnEpisodeBegin()
    {
        ResetPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle") == true)
        {
            AddReward(-1.0f);
            Destroy(collision.gameObject);
            EndEpisode();
        }
        // if (collision.gameObject.CompareTag("walltop") == true)
        // {
        //     AddReward(-1.0f);
        //     EndEpisode();
        // }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wallreward") == true)
        {
            AddReward(0.1f);
        }
    }

    private void Thrust()
    {
        //rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
        if (canJump)
        {
            canJump = false; // Om dit te laten werken moet je het landen van de player zien door een trigger of collision op te vangen
            // Dat gaat problemen geven omdat ge constant een collision hebt, dus gaat ge ook een andere rigidbody moeten vastmaken aan de player die apart triggered
            rb.AddForce(Vector2.up * force, ForceMode.Impulse);
        }
    }

    private void ResetPlayer()
    {
        this.transform.position = new Vector3(reset.position.x, reset.position.y, reset.position.z);
    }
}
