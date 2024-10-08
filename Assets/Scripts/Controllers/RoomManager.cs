﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private int currentLevel = 1;

    [SerializeField] GameObject[] bossPrefabs;
    [SerializeField] GameObject[] startRoomPrefabs;
    [SerializeField] GameObject[] bossRoomPrefabs;
    [SerializeField] GameObject[] itemRoomPrefabs;

    [SerializeField] GameObject[] sewersPrefabList;
    [SerializeField] GameObject[] backAlleyPrefabList;
    [SerializeField] GameObject[] chemPlantPrefabList;

    //[SerializeField] List<GameObject> roomPrefabList;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 15;

    int roomWidth = 17;
    int roomHeight = 11;

    int gridSizeX = 10;
    int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();
    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;
    private int roomCount;
    private float offSetX = 0.5f, offSetY = 0.5f;

    private bool generationComplete = false;
    private bool bossSpawned = false;
    private bool itemSpawned = false;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();
        // starts generation from centre of grid
        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            // generate rooms in different directions
            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }
        else if (roomCount < minRooms && (!bossSpawned || !itemSpawned))
        {
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            generationComplete = true;
            currentLevel++;
        }
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(startRoomPrefabs[currentLevel - 1], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        GameObject newRoom;

        if (roomGrid[x, y] != 0)
        {
            return false;
        }

        if (roomCount >= maxRooms)
        {
            return false;
        }

        // magic numbers
        if (Random.value < 0.5 && roomIndex != Vector2Int.zero)
        {
            return false;
        }

        // prevents room clustering
        if (CountAdjRooms(roomIndex) > 1)
        {
            return false;
        }

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        //boss room
        if(roomCount == maxRooms)
        {
            newRoom = Instantiate(bossRoomPrefabs[currentLevel - 1], GetPositionFromGridIndex(roomIndex), Quaternion.identity);

            // create boss
            var bossPos = GetPositionFromGridIndex(roomIndex);
            Instantiate(bossPrefabs[currentLevel - 1], bossPos + new Vector3(offSetX, offSetY), Quaternion.identity);
            bossSpawned = true;
        }
        //item room
        else if(roomCount == (int)(Mathf.Floor(minRooms / 2)))
        {
            newRoom = Instantiate(itemRoomPrefabs[currentLevel - 1], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
            itemSpawned = true;
        }
        //normal rooms
        else
        {
            var index = Random.Range(0, sewersPrefabList.Length);
            //sewers
            if (currentLevel == 1)
            {
                newRoom = Instantiate(sewersPrefabList[index], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
            }
            else if (currentLevel == 2)
            {
                newRoom = Instantiate(backAlleyPrefabList[index], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
            }
            else if (currentLevel == 3)
            {
                newRoom = Instantiate(chemPlantPrefabList[index], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
            }
            else
            {
                // fail safe room
                newRoom = Instantiate(startRoomPrefabs[0], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
            }
        }

        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);

        OpenDoors(newRoom, x, y);
        return true;
    }

    public void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeX];
        roomQueue.Clear();
        roomCount = 0;
        bossSpawned = false;
        generationComplete = false;

        itemSpawned = false;
        ClearAllItems();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        // neighbours
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            // neighbouring room to left
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            // neighbouring room to right
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            // neighbouring room to top
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            // neighbouring room to bottom
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
        {
            return roomObject.GetComponent<Room>();
        }
        return null;
    }

    private int CountAdjRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++; // left neighbour
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++; // right neighbour
        if (y > 0 && roomGrid[x, y - 1] != 0) count++; // bottom neighbour
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++; // top neighbour

        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        // returns position of room based on grid index (x, y);
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                
                Gizmos.DrawWireCube((position + new Vector3(offSetX, offSetY)), new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }

    private void ClearAllItems()
    {
        GameObject[] extraItems;
        extraItems = GameObject.FindGameObjectsWithTag("Item");

        foreach(GameObject item in extraItems)
        {
            Destroy(item.gameObject);
        }
    }
}
