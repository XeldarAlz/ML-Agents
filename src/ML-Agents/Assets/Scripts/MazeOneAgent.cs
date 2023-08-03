using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MazeOneAgent : Agent
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform environment;
    [SerializeField] private Transform wall;
    [SerializeField] private SpriteRenderer result;

    private Vector3 m_AgentStartPosition;
    public override void Initialize()
    {
        m_AgentStartPosition = transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-4f, -2f), 0, UnityEngine.Random.Range(-4f, 4f));
        target.localPosition = new Vector3(UnityEngine.Random.Range(2f, 4f), 0, UnityEngine.Random.Range(-4f, 4f));
        wall.localPosition = new Vector3(0, 0, UnityEngine.Random.Range(-3.5f, 3.5f));
        environment.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(wall.localPosition);
        sensor.AddObservation(wall.localScale);
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

        continuousActions[0] = Input.GetAxisRaw("Horizontal") * Time.deltaTime * movementSpeed;
        continuousActions[1] = Input.GetAxisRaw("Vertical") * Time.deltaTime * movementSpeed;
    }
}
