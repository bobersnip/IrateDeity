using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] private SimpleRandomWalkObject simpleRandomWalkObject;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
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