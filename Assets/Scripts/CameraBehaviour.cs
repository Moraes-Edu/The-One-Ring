using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    List<GameObject> targets;

    public Vector3 offset;
    public float smoothTime = 0.5f; 

    public float zoomLimiter = 50f;
    public float maxPoint, minPoint;

    private Vector3 velocity;
    private Camera cam;

    public List<GameObject> Targets { get => targets; set => targets = value; }

    void Start()
    {
        cam = GetComponent<Camera>();
        Targets = GameController.Players;
    }
    void LateUpdate()
    {
        if(Targets.Count == 0)
            return;
        Move();
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;
            
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(newPosition.x,6f,newPosition.z), ref velocity, smoothTime);
        if(transform.position.x < minPoint)
        {
            transform.position = new Vector3(minPoint,6f, transform.position.y+offset.z);
        }
        else if(transform.position.x > maxPoint)
        {
            transform.position = new Vector3(maxPoint,6f, transform.position.z+offset.z);
        }
    }
    Vector3 GetCenterPoint()
    {
        if(Targets.Count == 1)
        {
            return Targets[0].transform.position;
        }

        var bounds = new Bounds(Targets[0].transform.position, Vector3.zero);
        for(int i = 0;i<Targets.Count; i++)
        {
            bounds.Encapsulate(Targets[i].transform.position);
        }

        return bounds.center;
    }
}
