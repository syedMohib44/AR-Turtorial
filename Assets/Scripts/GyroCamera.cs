using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class GyroCamera : MonoBehaviour
{
    private Gyroscope gyroScope;
    private bool gyroSuppoted = false;

    [SerializeField]
    private float startY;
    [SerializeField]
    private Quaternion correctionQuaternion;


    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR

        gyroSuppoted = SystemInfo.supportsGyroscope;

        correctionQuaternion = Quaternion.Euler(90f, 0f, 0f);


        gyroScope = Input.gyro;
        if (gyroSuppoted)
        {
            gyroScope = Input.gyro;
            gyroScope.enabled = true;
        }
# endif
    }

    void Update()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        GyroModifyCamera();
#endif
    }

    void GyroModifyCamera()
    {
        Quaternion gyroQuaternion = GyroToUnity(Input.gyro.attitude);
        // rotate coordinate system 90 degrees. Correction Quaternion has to come first
        Quaternion calculatedRotation = correctionQuaternion * gyroQuaternion;
        transform.rotation = calculatedRotation;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    void ResetGyroRotation()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500))
        {
            Vector3 hitPoint = hit.point;
            float z = Vector3.Distance(Vector3.zero, hitPoint);
        }

        startY = transform.eulerAngles.y;
    }
    private float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }
}