using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// カメラ移動、ズーム(スマホ向けに調整済み)
[ExecuteInEditMode, DisallowMultipleComponent]
public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public GameObject target;
    public Vector3 offset;

    [SerializeField] private float distance = 4.0f;
    [SerializeField] private float polarAngle = 45.0f;
    [SerializeField] private float azimuthalAngle = -90.0f;

    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 15.0f;
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 150.0f;
    [SerializeField] private float mouseXSensitivity = 0.1f;
    [SerializeField] private float mouseYSensitivity = 0.1f;
    [SerializeField] private float scrollSensitivity = 0.001f;

    private float bdis;

    private void Awake()
    {
        instance = this;
    }

    void LateUpdate()
    {
        var current = EventSystem.current;
        var eventData = new PointerEventData(current)
        {
            position = Input.mousePosition
        };
        var result = new List<RaycastResult>();
        current.RaycastAll(eventData, result);
        var isExists = 0 < result.Count;
        if (!isExists)
        {
            if(Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Moved)
                {
                    updateAngle(touch.deltaPosition.x, touch.deltaPosition.y);
                }
            }
            if(Input.touchCount == 2)
            {
                Touch t1 = Input.GetTouch(0);
                Touch t2 = Input.GetTouch(1);
                if(t2.phase == TouchPhase.Began)
                {
                    bdis = Vector2.Distance(t1.position, t2.position);
                }
                if (t1.phase == TouchPhase.Moved && t2.phase == TouchPhase.Moved)
                {
                    var ndis = Vector2.Distance(t1.position, t2.position);
                    updateDistance((bdis - ndis) * -5);
                    bdis = ndis;
                }
            }
        }
        var lookAtPos = target.transform.position + offset;
        updatePosition(lookAtPos);
        transform.LookAt(lookAtPos);
    }

    void updateAngle(float x, float y)
    {
        x = azimuthalAngle - x * mouseXSensitivity;
        azimuthalAngle = Mathf.Repeat(x, 360);

        y = polarAngle + y * mouseYSensitivity;
        polarAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
    }

    void updateDistance(float scroll)
    {
        scroll = distance - scroll * scrollSensitivity;
        distance = Mathf.Clamp(scroll, minDistance, maxDistance);
    }

    void updatePosition(Vector3 lookAtPos)
    {
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }

    public void CameraReset()
    {
        var targetpos = target.transform.position;
        var respos = (target.transform.forward * -5) + (target.transform.up * 3);
        transform.position = targetpos + respos;
        transform.LookAt(targetpos);
        azimuthalAngle = -90 - transform.localEulerAngles.y;
    }
}
