using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class comPass : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform bell;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float detectDistance;
    private Quaternion targetRotation;
    private void Update()
    {
        if (detectPlayer())
        {
            Vector2 direction = bell.position - player.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, -90);
            //transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1.5f * Time.deltaTime);
    }
    public bool detectPlayer()
    {
        if (Physics2D.OverlapCircle(bell.position, detectDistance, playerMask)!=null)
            return true;
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(bell.position, detectDistance);
    }
}
