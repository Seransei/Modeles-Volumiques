using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float yaw = 0,
        pitch = 0;

    [Range(1,10)]
    public float speed;

    void Update()
    {
        yaw += 5 * -Input.GetAxis("Mouse Y");
        pitch += 5 * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(yaw, pitch, 0);

        Vector3 mvt = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Z))
            mvt += transform.forward;
        if (Input.GetKey(KeyCode.S))
            mvt -= transform.forward;
        if (Input.GetKey(KeyCode.D))
            mvt += transform.right;
        if (Input.GetKey(KeyCode.Q))
            mvt -= transform.right;

        transform.position += mvt;
    }
}
