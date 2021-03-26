using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationManager : Singleton<ConfigurationManager>
{

    /// <summary>
    /// The seed for world generation.
    /// </summary>
    public int Seed = 1337;

    /// <summary>
    /// The number of chunks in the x and z dimension of the world.
    /// </summary>
    public int World_Diameter = 16;

    /// <summary>
    /// The number of chunks in the y dimension of the world.
    /// </summary>
    public int World_Height = 4;

    /// <summary>
    /// The number of blocks in any dimension of a chunk.
    /// </summary>
    public int Chunk_Diameter = 8;

    /// <summary>
    /// How much of the tile map does each tile take up.
    /// </summary>
    public float Tile_Map_Unit_Ratio = 0.25f;

    /// <summary>
    /// The number of acidic regions on the map.
    /// </summary>
    public int Number_Of_Acidic_Regions = 10;

    /// <summary>
    /// The radius of each acidic region
    /// </summary>
    public int Acidic_Region_Radius = 5;

    /// <summary>
    /// The number of container regions on the map.
    /// </summary>
    public int Number_Of_Container_Spheres = 5;

    /// <summary>
    /// The radius of each container region
    /// </summary>
    public int Container_Sphere_Radius = 10;
    
    /// <summary>
    /// The number of ants
    /// </summary>
    public int Number_Of_Ants = 100;
    
    /// <summary>
    /// Initial health of ant
    /// </summary>
    public int StartingHealth = 200;
    
    /// <summary>
    /// Health gained from eating mulch
    /// </summary>
    public int MulchHp = 20;
    
    /// <summary>
    /// Health loss per time step
    /// </summary>
    public int HpCost = 1;
}
