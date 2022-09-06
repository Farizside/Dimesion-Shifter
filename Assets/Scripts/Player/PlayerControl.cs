using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] bool isGrounded;
    [SerializeField] float jumpHeight;
    [SerializeField] float velocity;
    [SerializeField] Transform foot;
    [SerializeField] float footRadius;
    [SerializeField] LayerMask mask;
    [SerializeField] SpriteRenderer eye;

    [SerializeField] AudioSource runAudio;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource inAirAudio;
    [SerializeField] AudioSource bgmAudio;

    public GameObject panel;

    private float _runCount = 0.3f;

    private bool _isRun;

    private bool _isPaused;
    
    private Animator gameAnimator;
    // Start is called before the first frame update
    void Start()
    {
        gameAnimator = gameObject.GetComponent<Animator>();
        _isPaused = false;
    }

    void AnimationControl()
    {
        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            gameAnimator.SetBool("isMoving", false);
            runAudio.Pause();

            _isRun = false;
            
            _runCount = 0.3f;
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!_isRun)
            {
                StartCoroutine(OnPlayerMove());

                _isRun = true;
                
                if (_runCount > 0)
                {
                    _runCount -= Time.deltaTime;
                }
                else
                {
                    _isRun = false;

                    _runCount = 0.3f;
                }
            }

            gameAnimator.SetBool("isMoving", true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        AnimationControl();
        isGrounded = Physics2D.OverlapCircle(foot.position, footRadius, mask) != null ? true : false;
        
        if(Input.GetKey(KeyCode.LeftArrow) && _isPaused == false)
        {
            transform.localPosition += new Vector3(-velocity,0,0) * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            eye.flipX = true;
        }
        else if(Input.GetKey(KeyCode.RightArrow) && _isPaused == false)
        {
            transform.localPosition += new Vector3(velocity,0,0) * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            eye.flipX = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && _isPaused == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpHeight), ForceMode2D.Force);
            gameAnimator.SetTrigger("Jump");
            jumpAudio.Play();
            
            runAudio.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.GetComponent<PanelController>().ToggleActive();
            _isPaused = !_isPaused;
        }

        if (!isGrounded && _isRun)
        {
            runAudio.Play();
        }
        
        
        gameAnimator.SetBool("isOnAir", !isGrounded);
    }

    IEnumerator OnPlayerMove()
    {
        runAudio.Play();

        Debug.Log("Running!!");
        
        yield return new WaitForSeconds(0.5f);
    }
}
