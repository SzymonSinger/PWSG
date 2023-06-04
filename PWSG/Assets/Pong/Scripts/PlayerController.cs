using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float PlayerSpeed;
    public KeyCode UpKey;
    public KeyCode DownKey;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Controller()
    {
        if (Input.GetKey(UpKey) && transform.position.y < 3.5f)
        {
            transform.position += Vector3.up * Time.deltaTime * PlayerSpeed;
        }
        if (Input.GetKey(DownKey) && transform.position.y > -3.5f)
        {
            transform.position += Vector3.down * Time.deltaTime * PlayerSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Controller();
    }
}
