using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallGenerating : MonoBehaviour
{
    [SerializeField] private Tilemap _myTileMap;

    [Header("TileBase that marks the tile to draw")]
    [SerializeField] private TileBase _wallTile;
    [SerializeField] private TileBase _blankTile;
    [SerializeField] private TileBase _superPelletTile;

    [Header("The gameObject to instantiate")]
    [SerializeField] private GameObject _wallPreference;
    [SerializeField] private GameObject _smallPellet;
    [SerializeField] private GameObject _superPellet;

    [Header("check floating height of pellet")]
    [SerializeField] private float _up;
    [SerializeField] private int _numberOfPellet = 0;

    
    
    

    private void Start()
    {

    // With every "POSITION" within my Tile Map:
    // If the tile at that position is painted, Create the wallpreference at that space.
        foreach(Vector3Int position in _myTileMap.cellBounds.allPositionsWithin)
        {
           
            if(_myTileMap.GetTile(position) == _blankTile)
            {
             
            }
            else if (_myTileMap.GetTile(position) == _wallTile)
            {

                // Convert tile position at that cell to world position 
                Vector3 cellPosition = _myTileMap.GetCellCenterWorld(position);

                //Instantiate the wallpreference at that world position
                Instantiate(_wallPreference, cellPosition + new Vector3(0,0.5f,0), Quaternion.identity);
            }else if (_myTileMap.GetTile(position) == _superPelletTile)
            {
                Vector3 cellPosition = _myTileMap.GetCellCenterWorld(position);
                Instantiate(_superPellet, cellPosition + new Vector3(0, _up, 0), Quaternion.identity);
            }
            else
            {
                Vector3 cellPosition = _myTileMap.GetCellCenterWorld(position);
                Instantiate(_smallPellet, cellPosition + new Vector3(0, _up, 0), Quaternion.identity);
                _numberOfPellet += 1;
            }
            
        }
        
    }


}
