using UnityEngine;

public class SpawnDeer : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3 _spawnLocation;
    [SerializeField] private Quaternion _spawnRotation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Sparrow")
            Instantiate(_prefab, _spawnLocation, _spawnRotation);
    }
}
