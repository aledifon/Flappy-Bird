using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float strength;

    [Header("Animation")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    [Header("Audio Fx")]    
    [SerializeField] AudioClip hitFx;
    [SerializeField] AudioClip jumpFx;

    AudioSource audioSource;

    private int spriteIndex;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    void Update()
    {
        BirdMovement();
        Touches();
        BirdGravity();        
    }
    private void OnEnable()
    {
        // Reset the initial Player's position
        //Vector3 position = transform.position;
        //position.y = 0;
        //transform.position = position;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        // Reset the direction vector
        direction = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
        else if (collision.CompareTag("Scoring"))
        {
            GameManager.Instance.IncreaseScore();
        }
    }

    private void BirdMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
            PlayJumpFx();
        }
    }
    private void Touches()
    {
        if (Input.touchCount > 0)
        {
            // Creating a var. to know the finger state
            Touch touch = Input.GetTouch(0);

            // Skip if there is a 2nd finger and just take into account the 1st time
            // the touchscreen has been touched.
            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
                PlayJumpFx();
            }
        }
    }
    private void BirdGravity()
    {
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }
    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
            spriteIndex = 0;

        spriteRenderer.sprite = sprites[spriteIndex];
    }
    private void PlayFx(AudioClip audioClip)
    {
        //if (audioSource.isPlaying)
        //    audioSource.Stop();

        audioSource.PlayOneShot(audioClip);
    }    
    public void PlayHitFx()
    {
        PlayFx(hitFx);
    }
    public void PlayJumpFx()
    {
        PlayFx(jumpFx);
    }
}
