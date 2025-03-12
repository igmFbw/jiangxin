using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
public class mirror : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.Rotate(0, 0, 45);
    }
    public Vector2 countReflectVe(Vector2 dir)
    {
        if (Mathf.Abs(transform.localEulerAngles.z) <= .1f || Mathf.Abs(transform.localEulerAngles.z - 90) <= .1f || Mathf.Abs(transform.localEulerAngles.z - 180) <= .1f || Mathf.Abs(transform.localEulerAngles.z - 270) <= .1f)
        {
            if (dir == Vector2.left)
                return Vector2.left;
            else if (dir == Vector2.right)
                return Vector2.right;
            else if (dir == Vector2.down)
                return Vector2.down;
            else
                return Vector2.up;
        }
        else if (Mathf.Abs(transform.localEulerAngles.z - 45) <= .1f || Mathf.Abs(transform.localEulerAngles.z - 225) <= .1f)
        {
            if (dir == Vector2.left)
                return Vector2.down;
            else if (dir == Vector2.down)
                return Vector2.left;
            else if (dir == Vector2.right)
                return Vector2.up;
            else
                return Vector2.right;
        }
        else
        {
            if (dir == Vector2.left)
            {
                return Vector2.up;
            }
            else if (dir == Vector2.up)
                return Vector2.left;
            else if (dir == Vector2.right)
                return Vector2.down;
            else
                return Vector2.right;
        }
    }
}
