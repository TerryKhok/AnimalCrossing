using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _cameraPos;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothTime = .3f;

    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _cameraPos = Vector3.zero;
        _offset = this.transform.position - _player.transform.position;
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 2f, -3f);
    }

    // private void FixedUpdate()
    // {
    // _cameraPos = new Vector3(_player.transform.position.x, _player.transform.position.y + 1f, -3f);

    //     this.transform.position = _cameraPos;
    //     this.transform.LookAt(_player.transform.position);
    // }

    private void LateUpdate()
    {
        Vector3 targetPos = _player.transform.position + _offset;
        this.transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, _smoothTime);

        //this.transform.LookAt(_player.transform.position);
        this.transform.LookAt(new Vector3(_player.transform.position.x + 1.2f, _player.transform.position.y + 1.0f, _player.transform.position.z));
    }
}
