using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankManager : MonoBehaviour
{
    //0 is red tank, 1 is blue tank
    public int playerNum;

    public GameObject shooter;
    public GameObject projectile;
    public GameObject shootArea;
    public GameObject uiText;

    private GameObject bulletBall;
    private bool hasJumped;
    private static bool hasWon;

    private void Start()
    {
        //modify gravity
        Physics.gravity = new Vector3(0f, -20f, 0f);
        hasJumped = false;
        hasWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        //check if dead
        if (this.transform.position.y < -15f)
        {
            Text ui = uiText.GetComponent<Text>();

            if (!hasWon && playerNum == 0) {
                ui.color = Color.blue;
                ui.text = "Blue tank wins! Press space to restart.";
                hasWon = true;
            }
            else if (!hasWon && playerNum == 1)
            {
                ui.color = Color.red;
                ui.text = "Red tank wins! Press space to restart.";
                hasWon = true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                //reload the scene
                SceneManager.LoadScene(0);
            }
        }

        //player input
        //red player
        if (playerNum == 0)
        {
            //move left
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            //move right
            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
            //jump
            if (Input.GetKey(KeyCode.W))
            {
                Jump();
            }
            //shoot
            if (Input.GetKeyDown(KeyCode.S))
            {
                Shoot();
            }
            //move barrel up
            if (Input.GetKey(KeyCode.Alpha1)){
                RotateBarrelUp();
            }
            //move barrel down
            if (Input.GetKey(KeyCode.Alpha2))
            {
                RotateBarrelDown();
            }
        }
        //blue player
        else if (playerNum == 1)
        {
            //move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            //move right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            //jump
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Jump();
            }
            //shoot
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Shoot();
            }
            //move barrel up
            if (Input.GetKey(KeyCode.Period))
            {
                RotateBarrelUp();
            }
            //move barrel down
            if (Input.GetKey(KeyCode.Slash))
            {
                RotateBarrelDown();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //assuming collider is a platform
        if (collision.collider.CompareTag("Platform") && collision.transform.position.y < this.transform.position.y)
        {
            hasJumped = false;
        }
    }

    void MoveLeft()
    {
        Debug.Log("Movement is reversed, you might want to fix me.");
        this.transform.position += new Vector3(10f, 0f, 0f) * Time.deltaTime;
    }

    void MoveRight()
    {
        Debug.Log("Movement is reversed, you might want to fix me.");
        this.transform.position += new Vector3(-10f, 0f, 0f) * Time.deltaTime;
    }

    void Jump()
    {
        hasJumped = true;
        this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 15f, 0f);
    }

    void RotateBarrelUp()
    {
        shooter.transform.Rotate(new Vector3(0f, 0f, 120f) * Time.deltaTime);
    }

    void RotateBarrelDown()
    {
        shooter.transform.Rotate(new Vector3(0f, 0f, -120f) * Time.deltaTime);
    }

    void Shoot()
    {
        //shoot out a bullet with the barrel
        GameObject bullet = (GameObject)Instantiate(projectile, shootArea.transform.position, shooter.transform.rotation);
        bulletBall.GetComponent<Rigidbody>().velocity = shooter.transform.up * 25f;
    }
}
