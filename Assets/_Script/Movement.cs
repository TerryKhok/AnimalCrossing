using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;
    private Vector3 _moveForward;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _footstepCD = .4f;
    [SerializeField] private bool _footstep = true;

    private Vector3 _jumpForward;
    private Vector3 _wallJumpForward;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private float _jumpForce = 6.5f;

    private bool _touchGnd;
    //private bool _jumped;
    private bool _wallJump_L;
    private bool _wallJump_R;
    private bool _doubleJump;
    private float _startAngle = 0.0f;
    private float _targetAngle = 90.0f;

    private ParticleSystem _particalSys;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        // this.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        _particalSys = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        _moveForward = new Vector3(0f, 0f, 0f);
        _jumpForward = new Vector3(0f, 0f, 0f);
        _wallJumpForward = new Vector3(0f, 0f, 0f);

        if (Input.GetKey(_leftKey))
        {
            _moveForward.x = -1.0f;
            _startAngle = this.transform.eulerAngles.y;
            _targetAngle = -90.0f;
            if (_footstep == true&&_touchGnd)
            {
                Footsteps();
                _footstep=false;
            }
        }
        if (Input.GetKey(_rightKey))
        {
            _moveForward.x = 1.0f;
            _startAngle = this.transform.eulerAngles.y;
            _targetAngle = 90.0f;
            if (_footstep == true&&_touchGnd)
            {
                Footsteps();
                _footstep=false;
            }
        }

        float angle = Mathf.LerpAngle(_startAngle, _targetAngle, 0.1f);
        this.transform.eulerAngles = new Vector3(0.0f, angle, 0.0f);

        Ray ray = new Ray(transform.position + new Vector3(0.0f, 0.05f, 0.0f), new Vector3(0.0f, -1.0f, 0.0f));
        Ray ray_L = new Ray(transform.position + new Vector3(0.0f, 0.05f, 0.0f), new Vector3(-1.0f, 0.0f, 0.0f));
        Ray ray_R = new Ray(transform.position + new Vector3(0.0f, 0.05f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f));
        RaycastHit hit;
        RaycastHit hit_L;
        RaycastHit hit_R;
        _touchGnd = false;
        _wallJump_L = false;
        _wallJump_R = false;


        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.distance < 0.2f)
            {
                _touchGnd = true;
                // _jumped = false;
            }
        }
        if (Physics.Raycast(ray_L, out hit_L, 10.0f))
        {
            if (hit_L.distance < 0.6f)
            {
                _wallJump_L = true;
                Debug.Log("wall Jump Left True");
            }
        }
        if (Physics.Raycast(ray_R, out hit_R, 10.0f))
        {
            if (hit_R.distance < 0.6f)
            {
                _wallJump_R = true;
                Debug.Log("wall Jump Right True");

            }
        }

        if ((Input.GetKeyDown(_jumpKey) || Input.GetKeyDown(KeyCode.UpArrow)) && _touchGnd == true)
        {
            _jumpForward.y = 1f;
            _doubleJump = true;
            FindObjectOfType<AudioManager>().Play("Jump");
            Debug.Log("Double Jump:" + _doubleJump);

            _rb.AddForce(_jumpForward * _jumpForce, ForceMode.Impulse);
            _anim.SetBool("jumping", true);
        }
        else if ((Input.GetKeyDown(_jumpKey) || Input.GetKeyDown(KeyCode.UpArrow)) && _doubleJump == true && _touchGnd == false && _wallJump_L == false && _wallJump_R == false)
        {
            Debug.Log("double jumped");
            _jumpForward.y = 1f;
            _doubleJump = false;
            FindObjectOfType<AudioManager>().Play("Jump");

            _rb.AddForce(_jumpForward * _jumpForce, ForceMode.Impulse);
            _anim.SetBool("jumping", true);

        }
        else if ((Input.GetKeyDown(_jumpKey) || Input.GetKeyDown(KeyCode.UpArrow)) && Input.GetKey(_leftKey) && _wallJump_L == true && _touchGnd == false)
        {
            _wallJumpForward.x = 1f;
            _wallJumpForward.y = 1f;
            FindObjectOfType<AudioManager>().Play("Jump");

            _rb.AddForce(_wallJumpForward * _jumpForce, ForceMode.Impulse);
            Debug.Log("Jump Left");
            _anim.SetBool("jumping", true);
        }
        else if ((Input.GetKeyDown(_jumpKey) || Input.GetKeyDown(KeyCode.UpArrow)) && Input.GetKey(_rightKey) && _wallJump_R == true && _touchGnd == false)
        {
            _wallJumpForward.x = -1f;
            _wallJumpForward.y = 1f;
            FindObjectOfType<AudioManager>().Play("Jump");

            _rb.AddForce(_wallJumpForward * _jumpForce, ForceMode.Impulse);
            Debug.Log("Jump Right");
            _anim.SetBool("jumping", true);
        }
        else
        {
            _anim.SetBool("jumping", false);
        }

        if (_moveForward != Vector3.zero)
            _anim.SetBool("running", true);
        else
            _anim.SetBool("running", false);

        if (!_touchGnd)
            _anim.SetBool("flying", true);
        else
            _anim.SetBool("flying", false);
        // Debug.Log(_rb.velocity);

        if(_rb.velocity.x > 1f && _touchGnd){
            if(!_particalSys.isEmitting)
                _particalSys.Play();
        }else{
            if(_particalSys.isEmitting)
                _particalSys.Stop();
        }


        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Result")
            _anim.SetBool("dancing", true);
        else
            _anim.SetBool("dancing", false);
    }



    private void FixedUpdate()
    {
        _rb.AddForce(_moveForward * _speed, ForceMode.Force);
    }

    private void Footsteps()
    {
        int rng = Random.Range(1, 5);
        switch (rng)
        {
            case 1:
                FindObjectOfType<AudioManager>().Play("Walk1");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Play("Walk2");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Play("Walk3");
                break;
            case 4:
                FindObjectOfType<AudioManager>().Play("Walk4");
                break;
            default: return;
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_footstepCD);
        _footstep = true;
    }
}
