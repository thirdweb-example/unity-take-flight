using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private GameObject _wallPrefab;

    [SerializeField]
    private float _padding = 15f;

    [SerializeField]
    private int _totalWalls;

    [SerializeField]
    private Color _activeColor;

    [SerializeField]
    private Color _inactiveColor;

    private List<GameObject> _walls;

    private void Awake()
    {
        _walls = new List<GameObject>();
        for (int i = 0; i < _totalWalls; i++)
        {
            GameObject wall = Instantiate(
                _wallPrefab,
                new Vector3(0, 0, i * _padding),
                Quaternion.identity,
                transform
            );
            _walls.Add(wall);
        }
    }

    private void Update()
    {
        foreach (var wall in _walls)
        {
            if (_player.position.z - wall.transform.position.z > _padding * _totalWalls / 2)
            {
                wall.transform.position = new Vector3(
                    0,
                    0,
                    _walls[_walls.Count - 1].transform.position.z + _padding
                );
                _walls.Remove(wall);
                _walls.Add(wall);
                break;
            }
        }

        Colorize();
    }

    private void Colorize()
    {
        int currentLaneIndex = CharacterManager.Instance.GetLaneIndex();

        for (int i = 0; i < _walls.Count; i++)
        {
            var renderers = _walls[i].GetComponentsInChildren<Renderer>();
            for (int j = 0; j < renderers.Length; j++)
            {
                if (j != currentLaneIndex)
                    renderers[j].material.color = _inactiveColor;
                else
                    renderers[currentLaneIndex].material.color = _activeColor;
            }
        }
    }
}
