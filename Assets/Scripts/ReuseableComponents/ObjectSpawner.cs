
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private SpawnableObject _prefabToSpawn;
    [SerializeField] private float _spawnInterval = 0.5f;
    [SerializeField] private int _maxObjects = 100;

    private float _timeSinceLastSpawn;
    private List<SpawnableObject> _spawnedObjects = new();

    
    void Update()
    {
        if(_timeSinceLastSpawn <= 0.0f && _spawnedObjects.Count < _maxObjects)
        {
            var spawnpoint = PickPoint();
            var newObject = Instantiate(_prefabToSpawn, spawnpoint, Quaternion.identity);
            _spawnedObjects.Add(newObject);
            newObject.OnDespawnEvent += RemoveDestroyedObject;
            _timeSinceLastSpawn = _spawnInterval;
        }
        _timeSinceLastSpawn -= Time.deltaTime;
    }

    private void RemoveDestroyedObject(SpawnableObject objectToDestroy)
    {
        _spawnedObjects.Remove(objectToDestroy);
    }
    
    private Vector3 PickPoint()
    {
        Vector3 randomPoint = new(Random.Range(0f, 1f), Random.Range(0f, 1f));
        randomPoint.z = 10f; // set this to whatever you want the distance of the point from the camera to be. Default for a 2D game would be 10.
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(randomPoint);
        return worldPoint;
    }
}
