using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FoeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabFoe;
    [SerializeField] private FoeSpawnPosition _position;
    [SerializeField] private FoeRotation _direction;

    private int _currentSize = 1;
    private int _maxCapacity = 5;
    private float _minY = 0f;
    private float _maxY = 360f;

    private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefabFoe),
            actionOnGet: PrepareGetObject,
            actionOnRelease: PrepareReleaseObject,
            actionOnDestroy: PrepareDestroyObject,
            collectionCheck: true,
            defaultCapacity: _currentSize,
            maxSize: _maxCapacity
            );
    }

    private void Start()
    {
        StartCoroutine(SpawnFoe());
    }

    private void PrepareGetObject(GameObject _object)
    {
        if (_object.TryGetComponent(out Foe foe))
        {
            foe.Died += OnFoeDied;
        }

        if(_object.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.linearVelocity = Vector3.zero;
        }

        Vector3 spawnPosition = _position.GetArea();
        _object.transform.position = spawnPosition;

        float randomY = Random.Range(_minY, _maxY);
        _object.transform.rotation = Quaternion.Euler(0, randomY, 0);

        _object.SetActive(true);
    }

    private void PrepareReleaseObject(GameObject _object)
    {
        _object.SetActive(false);
    }

    private void PrepareDestroyObject(GameObject _object)
    {
        Destroy(_object);
    }

    private void OnFoeDied(Foe foe)
    {
        foe.Died -= OnFoeDied;
        _pool.Release(foe.gameObject);
    }

    private IEnumerator SpawnFoe()
    {
        while (enabled)
        {
            if (_pool.CountActive < _maxCapacity)
            {
                _pool.Get();
            }

            yield return _spawnDelay;
        }
    }

}
