using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    //Animation variables

    Animator anim;
    public bool moving = false;
    public bool jumping = false;
    public bool launched = false;
    public bool facingf = true;
    public bool direction = true;

    //Movement Variables
    Rigidbody2D rb; //create reference for rigidbody bc jump requires physics
    public float jumpForce; //the force that will be added to the vertical component of player's velocity
    public float speed;
    public float travel = 0.0f;

    //Ground Check Variables
    public LayerMask groundLayer;
    public Transform groundCheck;
    public bool isGrounded;
    //projectile
    public GameObject projectile;    
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
            facingf = false;
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += speed;
            newScale.x = currentScale;
            moving = true;
            facingf = true;
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

        if (Input.GetKey("space") && !launched)
        {
            Debug.Log("projectile");
            
            launched = true;

            direction = facingf;
        }

        if (launched) 
        {
            Vector3 newProjectilePosition = projectile.transform.position;
            if (direction)
            {
                newProjectilePosition.x += speed/3;
                travel += speed/3;
                
            }

            else 
            {
                newProjectilePosition.x -= speed/3; 
                travel -= speed/3;
            
            }

            if (travel >= 5.0 || travel <= -5.0) 
            {
                newProjectilePosition.x = newPosition.x;
                launched = false;
                travel = 0.0f;
            }
                        
            projectile.transform.position = newProjectilePosition;
        }
        else
        {
            Vector3 newProjectilePosition = projectile.transform.position;
            newProjectilePosition.x = newPosition.x; 
            newProjectilePosition.y = newPosition.y;
            projectile.transform.position = newProjectilePosition;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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

