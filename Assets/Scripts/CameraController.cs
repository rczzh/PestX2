using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float cameraMoveSpeed;

    public Vector3 targetPos;
    public Vector3 currentPos;

    private float cameraXOffset = .5f;
    private float cameraYOffset = .5f;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPos();
        
    }

    private void UpdateCameraPos()
    {
        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * cameraMoveSpeed);
        }
    }

    public void MoveCamera(Vector3 pos)
    {
        targetPos = new Vector3(pos.x + cameraXOffset, pos.y + cameraYOffset, pos.z);
    }

    public void ResetCamera()
    {
        transform.position = new Vector3(0.5f, 0.5f, -10);
    }
}
