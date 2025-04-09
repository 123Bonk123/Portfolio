using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField] private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    string attachedEntityTag = "Player";

    private void Start()
    {
        TargetNextWaypoint();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        float elapsedTimeRelative = _elapsedTime / _timeToWaypoint;

        elapsedTimeRelative = Mathf.SmoothStep(0f, 1f, elapsedTimeRelative);
        
        MovePlatform(elapsedTimeRelative);

        if (elapsedTimeRelative >= 1f)
        {
            TargetNextWaypoint();
        }
            
    }

    private void MovePlatform(float timeRelative)
    {
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, timeRelative);
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, timeRelative);
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0f;

        _timeToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position) / _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(attachedEntityTag))
            other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(attachedEntityTag))
            other.transform.SetParent(null);
    }
}
