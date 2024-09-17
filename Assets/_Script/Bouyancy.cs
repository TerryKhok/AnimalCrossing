using UnityEngine;

public class Bouyancy : MonoBehaviour
{
    public float _underWaterDrag = 3;
    public float _underWaterAngularDrag = 1;
    public float _airDrag = 0;
    public float _airAngularDrag = .05f;
    public float _floatingPower = 15f;
    public float _waterHeight;
    private Animator _anim;

    Rigidbody _rb;

    bool _underwater;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
         _anim = GetComponent<Animator>();
    }

    // private void FixedUpdate()
    // {
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Water")
        {
            float difference = transform.position.y - _waterHeight;
            if (difference < 0)
            {
                _rb.AddForceAtPosition(Vector3.up * _floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
                if (!_underwater)
                {
                    _underwater = true;
                    SwitchState(true);
                }
            }
            else if (_underwater)
            {
                _underwater = false;
                SwitchState(false);
            }
        }
    }
    // }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            _rb.drag = _underWaterDrag;
            _rb.angularDrag = _underWaterAngularDrag;
            FindObjectOfType<AudioManager>().Play("Splash");
            _anim.SetBool("swimming", true);
        }
        else
        {
            _rb.drag = _airDrag;
            _rb.angularDrag = _airAngularDrag;
            FindObjectOfType<AudioManager>().Stop("Splash");
            _anim.SetBool("swimming", false);
        }
    }
}
