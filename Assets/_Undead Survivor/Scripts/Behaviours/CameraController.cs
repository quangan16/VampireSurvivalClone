using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothTime;
    public Vector3 velocity = Vector3.zero;

    public void Init()
    {
        
    }

    public void LateUpdate()
    {
        FollowTarget();
    }

    public void FollowTarget()
    {
        target = GameManager.Instance.player.transform.position + (Vector3)GameManager.Instance.player.AimDirection * 3;
        Vector3 targetPosition = target + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref velocity, smoothTime);
    }
}
