using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public GameObject P1Score;
    public GameObject P2Score;
    public GameObject TextWindow;
    public string PlayerReflected;

    
    // Start is called before the first frame update
    void Start()
    {
        P1Score.GetComponent<TextMeshProUGUI>().color = Color.red;
        P2Score.GetComponent<TextMeshProUGUI>().color = Color.blue;
        rb2D = GetComponent<Rigidbody2D>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        ResetAndSendBallInRandomDirection();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (P1ScoreValue == 5) 
        {
            ingame = false;
            TextWindow.GetComponent<TextMeshProUGUI>().text = "Red Win";
            if (Input.GetKeyDown(KeyCode.Space) && ingame == false) ResetGame();
        }
        if (P2ScoreValue == 5)
        {
            ingame = false;
            TextWindow.GetComponent<TextMeshProUGUI>().text = "Blue Win";
            if (Input.GetKeyDown(KeyCode.Space) && ingame == false) ResetGame();
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

    private void ResetBall()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
        rb2D.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        ingame = true;
    }
    private void ResetAndSendBallInRandomDirection()
    {
        ResetBall();
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
        if (transform.position.x > 0)
        {
            P1ScoreValue++;
            P1Score.GetComponent<TextMeshProUGUI>().text = P1ScoreValue.ToString();
            TextWindow.GetComponent<TextMeshProUGUI>().color = Color.red;
            TextWindow.GetComponent<TextMeshProUGUI>().text = "Red Score";
        }

        if (transform.position.x <0)
        {
            P2ScoreValue++;
            P2Score.GetComponent<TextMeshProUGUI>().text = P2ScoreValue.ToString();
            TextWindow.GetComponent <TextMeshProUGUI>().color = Color.blue;
            TextWindow.GetComponent<TextMeshProUGUI>().text = "Blue Score";
        }

        ResetBall();
        ingame = false;
    }
}
