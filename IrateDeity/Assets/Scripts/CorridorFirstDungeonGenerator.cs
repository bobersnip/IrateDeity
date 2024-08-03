using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 10, corridorCount = 5;
    [SerializeField][Range(0, 5)] private int corridorSize = 2;
    [SerializeField][Range(0.1f, 1f)] private float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        // create the corridors
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        // create the rooms
        HashSet<Vector2Int> roomFloors = CreateRooms(potentialRoomPositions);
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnds(deadEnds, ref roomFloors);

        // combine the hashsets to create all the floor
        floorPositions.UnionWith(roomFloors);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorSizeByX(corridorSize, corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        // paint the floors and walls
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
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

    // Creates rooms on all dead ends so that we don't have any empty hallways
    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, ref HashSet<Vector2Int> roomFloors)
    {
        foreach (var endPosition in deadEnds)
        {
            if (roomFloors.Contains(endPosition) == false)
            {
                var room = RunRandomWalk(endPosition);
                roomFloors.UnionWith(room);
            }
        }
    }

    // Finds all unattached corridor ends
    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var position in floorPositions)
        {
            int neighborsCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighborsCount++;
                }
            }
            if (neighborsCount == 1)
            {
                deadEnds.Add(position);
            }
        }

        return deadEnds;
    }

    // Creates the rooms in the form of a HashSet to prevent overlap on the same tile
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        // basically this is ordering the rooms by Guid which is an arbitrary id
        // hence, random room selection when generating
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(
            x => Guid.NewGuid()
        ).Take(roomCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    // Creates corridors which we will spawn rooms on the ends of.
    // This returns a List of Lists so that we can widen the corridors later
    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = DungeonGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }

        return corridors;
    }
}