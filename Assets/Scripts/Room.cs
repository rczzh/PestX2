using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    public Vector2Int RoomIndex { get; set; }

    private void Start()
    {
    }

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
        }
        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
        }
        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
        }
        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // zeroing on camera
            CameraController.instance.MoveCamera(new Vector3 ((RoomIndex.x - 5) * 17, (RoomIndex.y - 5) * 11, -10));
        }
    }
}
