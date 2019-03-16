using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tile Meshes_SO",menuName = "Tiles")]
public class TileMeshes : ScriptableObject
{
    [SerializeField] private int tileSize = 5;
    [SerializeField] private Mesh[] meshes;

    public Mesh[] Meshes { get => meshes; }
    public int TileSize { get => tileSize; }
}
