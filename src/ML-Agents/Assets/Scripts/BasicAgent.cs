using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicAgent : Agent
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform environment;
    [SerializeField] private SpriteRenderer result;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-4f, -2f), 0, Random.Range(-4f, 4f));
        target.localPosition = new Vector3(Random.Range(2f, 4f), 0, Random.Range(-4f, 4f));
        environment.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * movementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            AddReward(1f);
            result.color = Color.green;
            EndEpisode();
        }
        else if (other.CompareTag("Wall"))
        {
            AddReward(-0.2f);
            result.color = Color.red;
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}