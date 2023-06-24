using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private TextMeshProUGUI textMeshPro;
    private SpriteRenderer spriteRenderer;

    bool ingame;
    int P1ScoreValue = 0;
    int P2ScoreValue = 0;

    public float speed;
    private Vector3 vel;

//    private List<GameObject> clonedBalls = new List<GameObject>();

    public GameObject RedPlayer;
    public GameObject BluePlayer;
    public GameObject P1Score;
    public GameObject P2Score;
    public GameObject TextWindow;
    public string PlayerReflected;

    private Vector3 RedOrginalScale;
    private Vector3 BlueOrginalScale;
    private Vector3 SizeUp;
    private Vector3 SizeDown;
    private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        RedOrginalScale = RedPlayer.transform.localScale;
        BlueOrginalScale = BluePlayer.transform.localScale;
        SizeUp = new Vector3(0f, .5f, 0f);
        SizeDown = new Vector3(0f, -.5f, 0f);
        P1Score.GetComponent<TextMeshProUGUI>().color = Color.red;
        P2Score.GetComponent<TextMeshProUGUI>().color = Color.blue;
        rb2D = GetComponent<Rigidbody2D>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        ScaleCorrector();
        if (P1ScoreValue == 5) 
        {
            ingame = false;
            TextWindow.GetComponent<TextMeshProUGUI>().text = "Red Win";
            if (Input.GetKeyDown(KeyCode.Space) && ingame == false)
            {
                ResetGame();
            }
        }
        if (P2ScoreValue == 5)
        {
            ingame = false;
            TextWindow.GetComponent<TextMeshProUGUI>().text = "Blue Win";
            if (Input.GetKeyDown(KeyCode.Space) && ingame == false)
            {
                ResetGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && ingame == false) ResetAndSendBallInRandomDirection();
    }

    private void ResetGame()
    {
        P1ScoreValue = 0;
        P2ScoreValue = 0;
        P1Score.GetComponent<TextMeshProUGUI>().text = "0";
        P2Score.GetComponent<TextMeshProUGUI>().text = "0";
        ResetAndSendBallInRandomDirection();
    }
   /* private void ResetClones()
    {
        foreach (GameObject clonedBall in clonedBalls)
        {
            Destroy(clonedBall);
        }
        clonedBalls.Clear();
    }
   */
    private void ScaleCorrector()
    {
        if (RedPlayer.transform.localScale.y > 3f) { RedPlayer.transform.localScale = new Vector3(1f, 3f, 1f); }
        if (BluePlayer.transform.localScale.y > 3f) { BluePlayer.transform.localScale = new Vector3(1f, 3f, 1f); }
        if (BluePlayer.transform.localScale.y < .5f) { BluePlayer.transform.localScale = new Vector3(1f, 0.5f, 1f); }
        if (RedPlayer.transform.localScale.y < .5f) { RedPlayer.transform.localScale = new Vector3(1f, 0.5f, 1f); }
    }
    private void ResetBall()
    {
        speed = 6;
        RedPlayer.transform.localScale = RedOrginalScale;
        BluePlayer.transform.localScale = BlueOrginalScale;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        rb2D.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        ingame = true;
    }
    private void ResetAndSendBallInRandomDirection()
    {
        ResetBall();
 //       ResetClones();
            TextWindow.GetComponent<TextMeshProUGUI>().text = "";
            rb2D.velocity = GenerateRandomVelocity(true) * speed;
            vel = rb2D.velocity;
    }
    private Vector3 GenerateRandomVelocity(bool shouldReturnNormalized)
    {
        Vector3 velocity = new Vector3();
        bool shouldGoRight = Random.Range(1, 100) > 50;
        bool shouldGoUp = Random.Range(1, 100) > 50;
        velocity.x = shouldGoRight ? Random.Range(-.8f, -.3f) : Random.Range(.8f, .3f);
        velocity.y = shouldGoUp ? Random.Range(-.8f, -.3f) : Random.Range(.8f, .3f);

        return shouldReturnNormalized ? velocity.normalized : velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audio.Play();
        if (collision.gameObject.CompareTag("Red"))
        {
            spriteRenderer.color = Color.red;
            PlayerReflected = "Red";
        }
        if(collision.gameObject.CompareTag("Blue"))
        {
            spriteRenderer.color = Color.blue;
            PlayerReflected = "Blue";
        }
        rb2D.velocity = Vector3.Reflect(vel, collision.contacts[0].normal);
        Vector3 newVelocityWithOffset = rb2D.velocity;
        newVelocityWithOffset += new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        rb2D.velocity = newVelocityWithOffset.normalized * speed;
        vel = rb2D.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Score_P1") || collision.CompareTag("Score_P2"))
        {
            if (collision.CompareTag("Score_P1"))
            {
                P1ScoreValue++;
                P1Score.GetComponent<TextMeshProUGUI>().text = P1ScoreValue.ToString();
                TextWindow.GetComponent<TextMeshProUGUI>().color = Color.red;
                TextWindow.GetComponent<TextMeshProUGUI>().text = "Red Score";
            }

            if (collision.CompareTag("Score_P2"))
            {
                P2ScoreValue++;
                P2Score.GetComponent<TextMeshProUGUI>().text = P2ScoreValue.ToString();
                TextWindow.GetComponent<TextMeshProUGUI>().color = Color.blue;
                TextWindow.GetComponent<TextMeshProUGUI>().text = "Blue Score";
            }

            ResetBall();
           // ResetClones();
            ingame = false;
        }

       /* if (collision.CompareTag("BallClone"))
        {
            if(PlayerReflected == "Red" || PlayerReflected == "Blue")
            {
            GameObject ballClone = Instantiate(this.gameObject, transform.position, transform.rotation);
            clonedBalls.Add(ballClone);
            Rigidbody2D cloneRigibody = ballClone.GetComponent<Rigidbody2D>();
            cloneRigibody.velocity = rb2D.velocity;
            }

        }*/
        if (collision.CompareTag("SizeUp"))
        {
            if(PlayerReflected == "Blue")
            {

                    BluePlayer.transform.localScale += SizeUp;

            }
            else if(PlayerReflected == "Red")
            {
                
                    RedPlayer.transform.localScale += SizeUp;

            }
        }
        else if (collision.CompareTag("SizeDown"))
        {
            if (PlayerReflected == "Blue")
            {

                    RedPlayer.transform.localScale += SizeDown;

            }
            else if(PlayerReflected == "Red")
            {

                    BluePlayer.transform.localScale += SizeDown;

            }

        }
        else if(collision.CompareTag("SpeedUp"))
        {
            speed += 2;
        }
    }
}
