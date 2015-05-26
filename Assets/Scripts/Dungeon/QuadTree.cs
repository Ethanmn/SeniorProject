﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Credit to David Leon (http://xdavidleon.tumblr.com/)

public class QuadTree
{
    // The Random Seed
    public int seed;

    // QuadTree boundary
    public AABB boundary;

    // Children
    public QuadTree northWest;
    public QuadTree northEast;
    public QuadTree southWest;
    public QuadTree southEast;

    // Constructor
    public QuadTree(AABB _aabb)
    {
        boundary = _aabb;
    }

    /*
     * METHODS
     */

    // Create a random slice inside the QuadTree boundary
    public bool RandomSlice(int iteration, int max_iterations, ref XY output)
    {
        if (iteration > max_iterations) return false;

        float sliceX = Mathf.Round(Random.Range(boundary.Left(), boundary.Right()));
        float sliceY = Mathf.Round(Random.Range(boundary.Bottom(), boundary.Top()));

        if (Mathf.Abs(sliceX - boundary.Left()) < DungeonGenerator.ROOM_MIN_SIZE) return RandomSlice(iteration + 1, max_iterations, ref output);
        if (Mathf.Abs(boundary.Right() - sliceX) < DungeonGenerator.ROOM_MIN_SIZE) return RandomSlice(iteration + 1, max_iterations, ref output);
        if (Mathf.Abs(boundary.Top() - sliceY) < DungeonGenerator.ROOM_MIN_SIZE) return RandomSlice(iteration + 1, max_iterations, ref output);
        if (Mathf.Abs(sliceY - boundary.Bottom()) < DungeonGenerator.ROOM_MIN_SIZE) return RandomSlice(iteration + 1, max_iterations, ref output);

        output = new XY(sliceX, sliceY);
        return true;
    }

    // Procedurally generate QuadTrees
    // Make a maximum of "max_depth" levels of depth
    public void GenerateZones(int depth, int max_depth)
    {
        // Reached max depth
        if (depth > max_depth) return;

        // If there's no place for 4 rooms, exit
        if (boundary.half.x * 2.0f < DungeonGenerator.ROOM_MIN_SIZE * 2) return;
        if (boundary.half.y * 2.0f < DungeonGenerator.ROOM_MIN_SIZE * 2) return;

        // Try to make a slice capable of generating 4 more rooms
        // If we can't manage to make it in "10" tries, exit
        XY slice = new XY();
        bool new_slice = RandomSlice(1, 10, ref slice);
        if (new_slice == false) return;

        // Place QuadTree children
        AABB aabb1 = new AABB();
        XY size = new XY(slice.x - boundary.Left(), boundary.Top() - slice.y);
        aabb1.center = new XY(slice.x - size.x / 2.0f, slice.y + size.y / 2.0f);
        aabb1.half = new XY(size.x / 2.0f, size.y / 2.0f);
        if (aabb1.half.x * 2.0f >= DungeonGenerator.ROOM_MIN_SIZE && aabb1.half.y * 2.0f >= DungeonGenerator.ROOM_MIN_SIZE)
        {
            northWest = new QuadTree(aabb1);
            northWest.GenerateZones(depth + 1, max_depth);
        }

        AABB aabb2 = new AABB();
        size = new XY(boundary.Right() - slice.x, boundary.Top() - slice.y);
        aabb2.center = new XY(slice.x + size.x / 2.0f, slice.y + size.y / 2.0f);
        aabb2.half = new XY(size.x / 2.0f, size.y / 2.0f);
        if (aabb2.half.x * 2.0f >= DungeonGenerator.ROOM_MIN_SIZE && aabb2.half.y * 2.0f >= DungeonGenerator.ROOM_MIN_SIZE)
        {
            northEast = new QuadTree(aabb2);
            northEast.GenerateZones(depth + 1, max_depth);
        }

        AABB aabb3 = new AABB();
        size = new XY(slice.x - boundary.Left(), slice.y - boundary.Bottom());
        aabb3.center = new XY(slice.x - size.x / 2.0f, slice.y - size.y / 2.0f);
        aabb3.half = new XY(size.x / 2.0f, size.y / 2.0f);
        if (aabb3.half.x * 2 >= DungeonGenerator.ROOM_MIN_SIZE && aabb3.half.y * 2 >= DungeonGenerator.ROOM_MIN_SIZE)
        {
            southWest = new QuadTree(aabb3);
            southWest.GenerateZones(depth + 1, max_depth);
        }

        AABB aabb4 = new AABB();
        size = new XY(boundary.Right() - slice.x, slice.y - boundary.Bottom());
        aabb4.center = new XY(slice.x + size.x / 2.0f, slice.y - size.y / 2.0f);
        aabb4.half = new XY(size.x / 2.0f, size.y / 2.0f);
        if (aabb4.half.x * 2 >= DungeonGenerator.ROOM_MIN_SIZE && aabb4.half.y * 2 >= DungeonGenerator.ROOM_MIN_SIZE)
        {
            southEast = new QuadTree(aabb4);
            southEast.GenerateZones(depth + 1, max_depth);
        }
    }

    public void Subdivide(int level, int maxLevels)
    {
        if (level > maxLevels) return;
        northWest = new QuadTree(new AABB(new XY((boundary.center.x - boundary.half.x / 2), (boundary.center.y + boundary.half.y / 2)), boundary.half / 2));
        northEast = new QuadTree(new AABB(new XY((boundary.center.x + boundary.half.x / 2), (boundary.center.y + boundary.half.y / 2)), boundary.half / 2));
        southWest = new QuadTree(new AABB(new XY((boundary.center.x - boundary.half.x / 2), (boundary.center.y - boundary.half.y / 2)), boundary.half / 2));
        southEast = new QuadTree(new AABB(new XY((boundary.center.x + boundary.half.x / 2), (boundary.center.y - boundary.half.y / 2)), boundary.half / 2));
        northWest.Subdivide(level + 1, maxLevels);
        northEast.Subdivide(level + 1, maxLevels);
        southWest.Subdivide(level + 1, maxLevels);
        southEast.Subdivide(level + 1, maxLevels);
    }

    public void PrintQuadTree(ref Texture2D output, Color c)
    {
        Color color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        for (int x = (int)boundary.Bottom(); x < (int)boundary.Top(); x++) // From bottom to top
            for (int y = (int)boundary.Left(); y < (int)boundary.Right(); y++) // From left to right
                output.SetPixel(y, x, color);


        if (northWest != null) northWest.PrintQuadTree(ref output, Color.red);
        if (northEast != null) northEast.PrintQuadTree(ref output, Color.green);
        if (southWest != null) southWest.PrintQuadTree(ref output, Color.blue);
        if (southEast != null) southEast.PrintQuadTree(ref output, Color.yellow);
    }

    public void GenerateDungeon(int s)
    {
        seed = s;
        Random.seed = seed;
        Debug.Log("Generation with seed " + seed);
        GenerateZones(1, DungeonGenerator.MAX_DEPTH);
    }

    public Texture2D DungeonToTexture()
    {
        Texture2D texOutput = new Texture2D((int)(DungeonGenerator.MAP_WIDTH), (int)(DungeonGenerator.MAP_HEIGHT), TextureFormat.ARGB32, false);
        PrintQuadTree(ref texOutput, Color.white);
        texOutput.filterMode = FilterMode.Point;
        texOutput.wrapMode = TextureWrapMode.Clamp;
        texOutput.Apply();
        return texOutput;
    }

    public void TextureToFile(Texture2D t)
    {
        byte[] bytes = t.EncodeToPNG();
        FileStream myFile = new FileStream(Application.dataPath + "/Resources/Generated/" + seed + ".png", FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
        myFile.Write(bytes, 0, bytes.Length);
        myFile.Close();
    }
}