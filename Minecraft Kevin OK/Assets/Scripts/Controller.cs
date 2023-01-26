using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector2 CameraSpeed = new Vector2(180, 180);
    public float MoveSpeed = 10;
    public GameObject BlockPrefab;
    public GameObject Outline;

    private float Pitch = 0;
    private float Yaw = 0;
    private RaycastHit Hit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Pitch += Input.GetAxis("Mouse Y") * CameraSpeed.x * Time.deltaTime;
        Yaw += Input.GetAxis("Mouse X") * CameraSpeed.y * Time.deltaTime;
    }
}
