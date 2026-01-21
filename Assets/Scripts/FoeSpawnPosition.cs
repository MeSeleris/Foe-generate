using System.Collections.Generic;
using UnityEngine;

public class FoeSpawnPosition : MonoBehaviour
{
    [SerializeField] private Transform _spawnAreaFirst;
    [SerializeField] private Transform _spawnAreaSecond;
    [SerializeField] private Transform _spawnAreaThird;

    private Transform _currentSpawnArea;
    private List<Transform> _spawnAreas;

    private int _minNumber = 0;
    private int _maxNumber = 3;

    private void Awake()
    {
        _spawnAreas = new List<Transform>
        {
            _spawnAreaFirst, 
            _spawnAreaSecond,
            _spawnAreaThird,
            };
    }

    public Vector3 GetArea()
    {
        int areaIndex = UnityEngine.Random.Range(_minNumber, _maxNumber);

        _currentSpawnArea = _spawnAreas[areaIndex];

        return _currentSpawnArea.position;
    }
}
