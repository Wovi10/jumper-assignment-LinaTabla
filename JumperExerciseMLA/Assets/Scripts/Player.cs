using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class Player : Agent
{
    public float force = 15f;
    public Transform reset = null;
    private Rigidbody rb = null;

    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (vectorAction[0] == 1)
        {
            Thrust();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            actionsOut[0] = 1;
        }
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
        if (collision.gameObject.CompareTag("walltop") == true)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
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
        rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }

    private void ResetPlayer()
    {
        this.transform.position = new Vector3(reset.position.x, reset.position.y, reset.position.z);
    }
}
