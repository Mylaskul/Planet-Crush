using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    GameObject audioInstance;
    AudioSource audio1;
    AudioSource audio2;
    bool isBound = false;
    int speed = 150;
    int dir;

    // Use this for initialization
    void Start()
    {
        audio1 = WorldScript.explAudio;
        audio2 = GetComponent<AudioSource>();
        dir = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (dir == 0)
            transform.Rotate(Vector3.forward, 1);
        else
            transform.Rotate(Vector3.forward, -1);
        if (WorldScript.score > 20000)
            speed = 300;
        else if (WorldScript.score > 10000)
            speed = 200;
        if (!isBound)
            GetComponent<Rigidbody2D>().velocity = speed * GetComponent<Rigidbody2D>().velocity.normalized;
        else
        {
            if (Input.GetKey("left"))
            {
                transform.RotateAround(Vector3.zero, Vector3.back, -120 * Time.deltaTime);
            }
            if (Input.GetKey("right"))
            {
                transform.RotateAround(Vector3.zero, Vector3.back, 120 * Time.deltaTime);
            }
            if (Input.GetKeyDown("up"))
            {
                isBound = false;
                GetComponent<Rigidbody2D>().AddForce(transform.position.normalized);
            }
        }
        if (transform.position.x > WorldScript.horzExtent + 300 || transform.position.x < -WorldScript.horzExtent - 300
            || transform.position.y > WorldScript.vertExtent + 300 || transform.position.y < -WorldScript.vertExtent - 300)
        {
            if (isBound)
            {
                WorldScript.weapon.SendMessage("SetIsLocked", false);
                WorldScript.weapon.SendMessage("ToggleLine");
                WorldScript.weapon.SendMessage("ResetBeamLength");
            }
            Destroy(gameObject);
        }
    }

    void SetIsBound(bool status)
    {
        isBound = status;
    }

    void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Planet")
        {
            if (isBound)
            {
                WorldScript.weapon.SendMessage("SetIsLocked", false);
                WorldScript.weapon.SendMessage("ToggleLine");
                WorldScript.weapon.SendMessage("ResetBeamLength");
            }
            audio1.Play();
            WorldScript.score += 500 * WorldScript.mult;
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Player")
        {
            if (isBound)
            {
                WorldScript.weapon.SendMessage("SetIsLocked", false);
                WorldScript.weapon.SendMessage("ToggleLine");
                WorldScript.weapon.SendMessage("ResetBeamLength");
            }
            audio1.Play();
            Destroy(WorldScript.weapon);
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (GetComponent<Renderer>().material.color == Color.cyan)
        {
            if (coll.tag == "Mult")
            {
                if (WorldScript.mult == 1)
                    WorldScript.mult = 2;
                else
                    WorldScript.mult += 2;
                audio2.Play();
                Destroy(coll.gameObject);
            }
            else if (coll.tag == "Shield")
            {
                float randomX = 0f;
                float randomY = 0f;
                while (randomX < 150 && randomX > -150 && randomY < 150 && randomY > -150)
                {
                    randomX = Random.Range(-WorldScript.horzExtent + 50, WorldScript.horzExtent - 50);
                    randomY = Random.Range(-WorldScript.vertExtent + 50, WorldScript.vertExtent - 50);
                }
                GameObject moon = (GameObject)Instantiate(gameObject, new Vector3(randomX, randomY, 0), Quaternion.Euler(0, 0, 0));
                moon.GetComponent<Renderer>().material.color = Color.white;
                audio2.Play();
                Destroy(coll.gameObject);
            }
        }
    }

}
