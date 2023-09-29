using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftAICircleMovement : MonoBehaviour
{
    public GameObject centerObject;
    public int radious;

    private float randomSpeed;
    private float randomHeight;
    private float angleSpeed = 0.3f;

    private void Awake()
    {
        randomSpeed = Random.Range(0.3f, 0.8f);
        randomHeight = Random.Range(5, 10);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        angleSpeed += randomSpeed * Time.deltaTime;

        float x = Mathf.Cos(angleSpeed) * radious;
        float y = Mathf.Sin(angleSpeed) * radious;

        Vector3 newPosition = new Vector3(-x, randomHeight, -y);

        Vector3 v1 = -1 * Vector3.forward;
        Vector3 v2 = transform.position - newPosition;

        float angle = Vector3.Angle(v1, v2);
        //Vector3 normal = Vector3.Cross(v1, v2);
        //angle *= Mathf.Sign(Vector3.Dot(normal, Vector3.up));

        transform.Rotate(Vector3.up, angleSpeed);

        transform.position = newPosition;
    }
}
