using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public GameObject object1, object2;
    public GameObject ground;
    private Vector3 initialPosition;
    public float speed = 5.0f;
    private float planeWidthX = 1.0f, planeWidthZ = 1.0f;
    private int minUnit = 5;
    void Start()
    {
        initialPosition = object1.transform.localPosition;
        planeWidthX = ground.transform.localScale.x * minUnit;
        planeWidthZ = ground.transform.localScale.z * minUnit;
    }
    void Update()
    {
        object1.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        object1.transform.rotation = Quaternion.Euler(0, 90, 360 * Time.deltaTime);

        object2.transform.Rotate(0, 90 * Time.deltaTime, 0, Space.World);

        float distanceZ = planeWidthZ - Mathf.Abs(object1.transform.position.z);
        float distanceX = planeWidthX - Mathf.Abs(object1.transform.position.x);

        if (distanceZ < 1 || distanceX < 1)
            object1.transform.localPosition = initialPosition;

    }
}
