using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class DungeonGenerationAlgorithms
{
    // HashSet is used so that we don't process the same tile twice
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        // add the start position to the hash set
        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }

    // We output a list from this function, since it is ordered.
    // This allows us to grab the most recently created tile
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToPartition, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();

        roomsQueue.Enqueue(spaceToPartition);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            // Skip the room if it doesn't meet the min size reqs
            if (room.size.y < minHeight || room.size.x < minWidth)
            {
                continue;
            }

            // Random is used to randomly try to split horizontally or vertically first
            if (Random.value > 0.5f)
            {
                if (room.size.y >= minHeight * 2)
                {
                    SplitHorizontally(minHeight, roomsQueue, room);
                }
                else if (room.size.x >= minWidth * 2)
                {
                    SplitVertically(minWidth, roomsQueue, room);
                }
                else
                {
                    roomsList.Add(room);
                }
            }
            else
            {
                if (room.size.x >= minWidth * 2)
                {
                    SplitVertically(minWidth, roomsQueue, room);
                }
                else if (room.size.y >= minHeight * 2)
                {
                    SplitHorizontally(minHeight, roomsQueue, room);
                }
                else
                {
                    roomsList.Add(room);
                }
            }
        }

        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        // get partition coord
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(
            room.min, // position
            new Vector3Int(xSplit, room.size.y, room.size.z) // size of room
        );
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), // position
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z) // size of room
        );

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        // get partition coord
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(
            room.min, // position
            new Vector3Int(room.size.x, ySplit, room.size.z) // size of room
        );
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), // position
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z) // size of room
        );

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}