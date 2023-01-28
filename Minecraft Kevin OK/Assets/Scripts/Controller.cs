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

        Camera.main.transform.eulerAngles = new Vector3(Pitch, Yaw, 0);

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Fly");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 dirForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        Vector3 dirSide = Camera.main.transform.right;
        Vector3 dirUp = Vector3.up;
        Vector3 moveDir = (inputX * dirSide) + (inputY * dirUp) + (inputZ * dirForward);

        Camera.main.transform.position += moveDir * MoveSpeed * Time.deltaTime;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if(Physics.Raycast(ray, out Hit))
        {
            Vector3 pos = Hit.point;
            pos += Hit.normal * 0.1f;
            pos = new Vector3(Mathf.Floor(pos.x), Mathf.Floor(pos.y), Mathf.Floor(pos.z));
            pos += Vector3.one * 0.7f;
            Outline.transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(BlockPrefab, pos, Quaternion.identity);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (Hit.collider.name == "Block(Clone)")
                {
                    Destroy(Hit.collider.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 9999);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Hit.point, 0.05f);
        Gizmos.DrawRay(Hit.point, Hit.normal);
    }
}
