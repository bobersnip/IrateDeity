using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected SimpleRandomWalkObject simpleRandomWalkObject;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        // run the simple random walk for a given amount of iterations
        for (int i = 0; i < simpleRandomWalkObject.iterations; ++i)
        {
            var path = DungeonGenerationAlgorithms.SimpleRandomWalk(currentPosition, simpleRandomWalkObject.walkLength);
            // UnionWith allows us to add the current path to the floor positions with no duplicates
            floorPositions.UnionWith(path);

            // boolean flag for starting random positions
            if (simpleRandomWalkObject.startRandomlyEachIteration)
            {
                // set the next iterations position to a random discovered tile
                currentPosition = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }
}