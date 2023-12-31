using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerEnemey : MonoBehaviour
{
    //Animation variables

    Animator anim;
    public bool moving = false;
    public bool jumping = false;

    //Movement Variables
    Rigidbody2D rb; //create reference for rigidbody bc jump requires physics
    public float jumpForce; //the force that will be added to the vertical component of player's velocity
    public float speed;

    //Ground Check Variables
    public LayerMask groundLayer;
    public Transform groundCheck;
    public bool isGrounded;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .5f, groundLayer);

        Vector3 newPosition = transform.position;
        Vector3 newScale = transform.localScale;
        float currentScale = Mathf.Abs(transform.localScale.x);

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= speed;
            newScale.x = -currentScale;
            moving = true;
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += speed;
            newScale.x = currentScale;
            moving = true;
        }

        if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumping = true;

        }
        else 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            jumping = false;

        }


        if (!Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            moving = false;
        }
        anim.SetBool("isJumping", jumping);
        anim.SetBool("isMoving", moving);
        transform.position = newPosition;
        Debug.Log(transform.position);
        transform.localScale = newScale;
        Debug.Log(isGrounded);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("projectile"))
        {
            Destroy(gameObject);

        }

        if (collision.gameObject.tag.Equals("door"))
        {
            Debug.Log("hit");
            SceneManager.LoadScene(1);
        }

        if (collision.gameObject.tag.Equals("fight"))
        {
            Debug.Log("hit");
            SceneManager.LoadScene(2);
        }

        if(collision.gameObject.tag.Equals("Exit"))
        {
            Debug.Log("hit");
            SceneManager.LoadScene(3);
        }

    }
}

