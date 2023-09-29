using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGunPlayerControllerScript : MonoBehaviour
{
    public static int Score = 0;

    private Vector2 mousePositionLast;
    private Vector2 mousePositionNew;
    private Vector2 mouseMoveDirection;

    private GameObject AAGunRotatableObject;
    private GameObject AAGunDizuo;

    private int theScreenWidth;
    private int theScreenHeight;
    private int Boundary = 50;

    private float xAngle = 0;
    private float yAngle = 0;

    private const float AAGUN_LIMIT_ANGLE_MAX = 90;
    private const float AAGUN_LIMIT_ANGLE_MIN = 0;

    private float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        AAGunDizuo = gameObject.transform.Find("naval_gun_armour").gameObject;
        AAGunRotatableObject = AAGunDizuo.transform.Find("naval_gun").gameObject;

        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        mousePositionLast = mousePositionNew;
        mousePositionNew = Input.mousePosition;
        mouseMoveDirection = mousePositionNew - mousePositionLast;

        if (Input.mousePosition.x > theScreenWidth - Boundary)
        {
            xAngle += speed * Time.deltaTime / 10;
        }
        else if (Input.mousePosition.x < 0 + Boundary)
        {
            xAngle -= speed * Time.deltaTime / 10;
        }
        else if (Input.mousePosition.y > theScreenHeight - Boundary)
        {
            xAngle += speed * Time.deltaTime / 10;
        }
        else if (Input.mousePosition.y < 0 + Boundary)
        {
            xAngle -= speed * Time.deltaTime / 10;
        }
        else
        {
            xAngle = mouseMoveDirection.x * 0.1f;
            yAngle = mouseMoveDirection.y * 0.1f;
        }

        AAGunDizuo.transform.Rotate(Vector3.forward, xAngle);

        Vector3 vect = ConvertToInpectorEulers(AAGunRotatableObject);
        float yRotate = vect.y;
        if (yRotate >= AAGUN_LIMIT_ANGLE_MIN && yRotate <= AAGUN_LIMIT_ANGLE_MAX)
        {
            AAGunRotatableObject.transform.Rotate(Vector3.left, yAngle);
        }

        vect = ConvertToInpectorEulers(AAGunRotatableObject);
        yRotate = vect.y;


        if (yRotate < AAGUN_LIMIT_ANGLE_MIN)
        {
            AAGunRotatableObject.transform.Rotate(Vector3.left, (yRotate - AAGUN_LIMIT_ANGLE_MIN) * -1);
        }
        if (yRotate > AAGUN_LIMIT_ANGLE_MAX)
        {
            AAGunRotatableObject.transform.Rotate(Vector3.left, (yRotate - AAGUN_LIMIT_ANGLE_MAX) * -1);
        }
    }

    private Vector3 ConvertToInpectorEulers(GameObject gameObject)
    {
        Vector3 angle = gameObject.transform.localEulerAngles;
        float x = angle.x;
        float y = angle.y;
        float z = angle.z;

        if (Vector3.Dot(transform.up, Vector3.up) >= 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = angle.x - 360f;
            }
        }
        if (Vector3.Dot(transform.up, Vector3.up) < 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = 180 - angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = 180 - angle.x;
            }
        }

        if (angle.y > 180)
        {
            y = angle.y - 360f;
        }

        if (angle.z > 180)
        {
            z = angle.z - 360f;
        }

        return new Vector3(Mathf.Floor(x), Mathf.Ceil(y), Mathf.Floor(z));
    }
}
