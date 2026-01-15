using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class RacingMovement : MonoBehaviour
{
    [Header("Movement Controls")]
    public float currentSpeed; // the player's current speed
    public float currentHorizontalSpeed; // the player's current horizontal speed
    [SerializeField] float baseSpeed; // starting movement speed along the track
    [SerializeField] float acceleration; // how fast the player speeds up
    [SerializeField] float maximumSpeed; // highest possible speed

    [SerializeField] float horizontalAcceleration; // acceleration when turning
    [SerializeField] float horizontalMaxSpeed; // maximum speed when turning
    [SerializeField] float horizontalDrag; // slow to a stop when turning

    public bool movingLeft;
    public bool movingRight;

    [Header("Powerup")]
    [SerializeField] float powerupDuration; // how long the lightning bonus lasts
    [SerializeField] float speedBoost; // speed multiplier for lightning bonus
    bool isPoweredUp; // checks whether or not the player is currently powered up

    [Header("Score")]
    public int currentScore; // player's current score
    float scoreFloat; //stores decimal score
    [SerializeField] float scorePerUnit; // how many points per unit traveled
    [SerializeField] float scorePerRock; // how many points per rock destroyed

    SpriteRenderer sr;
    Rigidbody rb;
    Coroutine powerupCoroutine;

    Animator animator; //animates the car
    public GameObject lightning;
    public GameObject cloud;

    public AudioSource crack;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        currentSpeed = baseSpeed;
        animator = this.GetComponent<Animator>();

        //for lighting cloud
        lightning = GameObject.Find("lightning_1");
        cloud = GameObject.Find("Cloud_9");
        lightning.GetComponent<SpriteRenderer>().enabled = false;
        cloud.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        // take in keyboard inputs
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            movingLeft = true;
            animator.SetBool("IsTurning", true);
            //sr.flipX = false;
            Debug.Log("Check");
        }
        else { movingLeft = false; animator.SetBool("IsTurning", false); }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movingRight = true;
            animator.SetBool("IsTurning", true);
            //sr.flipX = true;
        }
        else { movingRight = false; animator.SetBool("IsTurning", false); }
    }

    private void FixedUpdate()
    {
        // accelerate forward!
        currentSpeed += acceleration;
        if (currentSpeed > maximumSpeed) { currentSpeed = maximumSpeed; }

        //get horizontal speed
        if (movingLeft)
        {
            sr.flipX = false;
            currentHorizontalSpeed -= horizontalAcceleration;
            if (currentHorizontalSpeed < -horizontalMaxSpeed) { currentHorizontalSpeed = -horizontalMaxSpeed; }
        }
        else if (currentHorizontalSpeed < 0)
        {
            currentHorizontalSpeed += horizontalDrag;
        }

        if (movingRight)
        {
            sr.flipX = true;
            currentHorizontalSpeed += horizontalAcceleration;
            if (currentHorizontalSpeed > horizontalMaxSpeed) { currentHorizontalSpeed = horizontalMaxSpeed; }
        }
        else if (currentHorizontalSpeed > 0)
        {
            currentHorizontalSpeed -= horizontalDrag;
        }

        //clear small speed changes
        if (Mathf.Abs(currentHorizontalSpeed) < horizontalAcceleration)
        {
            currentHorizontalSpeed = 0;
            rb.velocity = Vector2.zero;
        }

        //powerup changes
        if (isPoweredUp)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow; // color change!!
            lightning.GetComponent<Animator>().SetBool("itemPickup", false);
            cloud.GetComponent<Animator>().SetBool("itemPickup", false);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white; // back to normal color...
        }

        // player moves here! negatives bc it's 2.5d...
        rb.velocity = new Vector3(currentHorizontalSpeed * -1, 0, currentSpeed * -1);

        //boost score based on distance traveled
        scoreFloat += (currentSpeed / 50) * scorePerUnit; // we divide by 50 because this check runs 50x per second
        currentScore = (int)scoreFloat;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            //destroy rocks
            if (isPoweredUp) { 
                Destroy(collision.gameObject);
                scoreFloat += scorePerRock;
            }
            else {
                //get destroyed by rocks, plus setting the high score
                if (currentScore > GameManager.instance.currentHighScore)
                {
                    GameManager.instance.currentHighScore = currentScore;
                }
                SceneManager.LoadScene("GameOver");
            }
        }

        if (collision.gameObject.tag == "Lightning")
        {
            // Enable cloud and lighting sprites/animations (maybe)
            lightning.GetComponent<Animator>().SetBool("itemPickup", true);
            cloud.GetComponent<Animator>().SetBool("itemPickup", true);

            crack.Play();

            // powerups!
            PowerUp();
            Destroy(collision.gameObject);

            //Aniamtion Transition 
            animator.SetBool("ThunderDrive", true);
        }
    }

    void PowerUp()
    {
        //lightning.GetComponent<SpriteRenderer>().enabled = true;

        if (isPoweredUp)
        {
            // reset the timer!
            StopCoroutine(powerupCoroutine);
            powerupCoroutine = StartCoroutine(EndPowerup(powerupDuration));
            return;
        }

        maximumSpeed *= speedBoost;
        horizontalMaxSpeed *= speedBoost;
        acceleration *= speedBoost;
        horizontalAcceleration *= speedBoost;

        isPoweredUp = true;
        powerupCoroutine = StartCoroutine(EndPowerup(powerupDuration));
    }

    IEnumerator EndPowerup(float time)
    {
        yield return new WaitForSeconds(time);

        //don't double count!
        if (!isPoweredUp)
        {
            yield break;
        }

        // return player to normal speed
        isPoweredUp = false;
        maximumSpeed /= speedBoost;
        horizontalMaxSpeed /= speedBoost;
        acceleration /= speedBoost;
        horizontalAcceleration /= speedBoost;

        if (currentSpeed > maximumSpeed) { currentSpeed = maximumSpeed; }
        if (currentHorizontalSpeed > horizontalMaxSpeed) { currentHorizontalSpeed = horizontalMaxSpeed; }
        if (currentHorizontalSpeed < -horizontalMaxSpeed) { currentHorizontalSpeed = -horizontalMaxSpeed; }

        //turn off the viduals
        animator.SetBool("ThunderDrive", false);
    }
}
