using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField] private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField][Range(0, 5)] private int corridorSize = 2;
    [SerializeField][Range(0, 10)] private int roomOffset = 1;
    [SerializeField] private bool randomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        BoundsInt bounds = new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0));
        var roomsList = DungeonGenerationAlgorithms.BinarySpacePartitioning(bounds, minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        floorPositions = CreateSimpleRooms(roomsList);

        // Get room center points
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floorPositions.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        // Iterate through the rooms and connect them until they are all connected
        while (roomCenters.Count > 0)
        {
            Vector2Int closestRoom = FindClosestPointTo(currentRoomCenter, roomCenters);
            List<Vector2Int> newCorridor = CreateCorridorToClosestRoom(currentRoomCenter, closestRoom);
            currentRoomCenter = closestRoom;

            // expand the corridor if necessary
            newCorridor = IncreaseCorridorSizeByX(corridorSize, newCorridor);
            corridors.UnionWith(newCorridor);

            roomCenters.Remove(closestRoom);
        }

        return corridors;
    }

    private List<Vector2Int> CreateCorridorToClosestRoom(Vector2Int currentRoomCenter, Vector2Int closestRoom)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);

        // check if we have to adjust y value
        while (position.y != closestRoom.y)
        {
            if (position.y < closestRoom.y)
            {
                position += Vector2Int.up;
            }
            else
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }

        // check if we have to adjust x value
        while (position.x != closestRoom.x)
        {
            if (position.x < closestRoom.x)
            {
                position += Vector2Int.right;
            }
            else
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float minDist = float.MaxValue;

        foreach (var position in roomCenters)
        {
            float currentDist = Vector2Int.Distance(currentRoomCenter, position);
            if (currentDist < minDist)
            {
                minDist = currentDist;
                closest = position;
            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        foreach (var room in roomsList)
        {
            for (int col = roomOffset; col < room.size.x - roomOffset; col++)
            {
                for (int row = roomOffset; row < room.size.y - roomOffset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floorPositions.Add(position);
                }
            }
        }

        return floorPositions;
    }

    private List<Vector2Int> IncreaseCorridorSizeByX(int size, List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();

        for (int i = 1; i < corridor.Count; i++)
        {
            // loop through surrounding tiles.
            // size division is to deal with even number increases
            for (int x = -(size / 2); x < (size - size / 2); x++)
            {
                for (int y = -(size / 2); y < (size - size / 2); y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }

        return newCorridor;
    }
}