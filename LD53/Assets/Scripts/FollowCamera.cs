using UnityEngine;

public class FollowCamera : MonoBehaviour
{


    [SerializeField]
    private bool followX = false;
    [SerializeField]
    private bool followY = false;

    [SerializeField]
    private bool followZ = false;

    [SerializeField]
    private Transform target;

    private Vector3 newPosition;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target != null)
        {
            newPosition = transform.position;
            if (followX)
            {
                newPosition.x = target.position.x;
            }
            if (followY)
            {
                newPosition.y = target.position.y;
            }
            if (followZ)
            {
                newPosition.z = target.position.z;
            }

            transform.position = newPosition;
        }
    }

}