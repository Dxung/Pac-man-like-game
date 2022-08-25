using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallGenerating : MonoBehaviour
{
    [SerializeField] private Tilemap _myTileMap;
    [SerializeField] private TileBase _ruleTile;
    [SerializeField] private GameObject _wallPreference;

    private void Start()
    {

    // With every "POSITION" within my Tile Map:
    // If the tile at that position is painted, Create the wallpreference at that space.
        foreach(Vector3Int position in _myTileMap.cellBounds.allPositionsWithin)
        {
            if (_myTileMap.GetTile(position) == _ruleTile)
            {

                // Convert tile position at that cell to world position 
                Vector3 cellPosition = _myTileMap.GetCellCenterWorld(position);

                //Instantiate the wallpreference at that world position
                Instantiate(_wallPreference, cellPosition + new Vector3(0,0.5f,0), Quaternion.identity);
            }
        }
    }

}
