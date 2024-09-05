using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject player;
    private float widthOffset = 2.25f;
    private float heightOffset = 2.25f;

    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (doorType == DoorType.left)
            {
                //print("left");
                player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y);
            }
            if (doorType == DoorType.right)
            {
                //print("right");
                player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
            }
            if (doorType == DoorType.top)
            {
                //print("top");
                player.transform.position = new Vector2(transform.position.x, transform.position.y + heightOffset);
            }
            if (doorType == DoorType.bottom)
            {
                //print("down");
                player.transform.position = new Vector2(transform.position.x, transform.position.y - heightOffset);
            }
        }
    }
}
