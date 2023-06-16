using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public GameObject Ball;
    public GameObject RedPlayer;
    public GameObject BluePlayer;

    public List<GameObject> PowerUp;
    private int Side;

    private Vector2 randomPosition1;
    private Vector2 randomPosition2;
    private Vector2 randomPosition3;


    private void BoostSelector1()
    {
        PowerUp[0].transform.position = randomPosition1;
        PowerUp[0].SetActive(true);
    }
    private void BoostSelector2()
    {
        PowerUp[1].transform.position = randomPosition2;
        PowerUp[1].SetActive(true);
    }
    private void BoostSelector3()
    {
        PowerUp[2].transform.position = randomPosition3;
        PowerUp[2].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BoostSelector1", 0f, 8f);
        InvokeRepeating("BoostSelector2", 0f, 8f);
        InvokeRepeating("BoostSelector3", 0f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
                randomPosition1 = new Vector2(Random.Range(-7f, 7f), Random.Range(4f, -4f));
                randomPosition2 = new Vector2(Random.Range(-7f, 7f), Random.Range(4f, -4f));
                randomPosition3 = new Vector2(Random.Range(-7f, 7f), Random.Range(4f, -4f));
        
    }
}
