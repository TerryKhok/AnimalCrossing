using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3 _spawnLocation;
    [SerializeField] private Quaternion _spawnRotation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Sparrow")
            for (int i = 0; i < 8; i++)
                Instantiate(_prefab, _spawnLocation + new Vector3(i * 3f, 0, 0), _spawnRotation);
    }
}
