using UnityEngine;
using System.Collections;

public class CanonScript : MonoBehaviour
{

    LineRenderer line;
    bool isFiring = false;
    bool isLocked = false;
    int beamLength = 0;
    public AudioClip laserSound;
    public AudioClip hitSound;
    AudioSource audioFile;
    int maxBeamLength = 500;
    RaycastHit2D hit;

    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 15f;
        line.endWidth = 15f;
        line.startColor = Color.white;
        line.endColor = Color.cyan;
        audioFile = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left") && !isFiring)
        {
            transform.RotateAround(Vector3.zero, Vector3.back, -120 * Time.deltaTime);
        }
        if (Input.GetKey("right") && !isFiring)
        {
            transform.RotateAround(Vector3.zero, Vector3.back, 120 * Time.deltaTime);
        }
        if (Input.GetKeyDown("up") && !isFiring)
        {
            if (isLocked)
            {
                line.enabled = false;
                isLocked = false;
                hit.transform.gameObject.SendMessage("SetSpeed", 300);
                beamLength = 0;
            }
            else
            {
                isFiring = true;
                audioFile.clip = laserSound;
                audioFile.Play();
            }
        }
        if (isFiring)
        {
            if (beamLength == maxBeamLength)
            {
                line.enabled = false;
                isFiring = false;
                beamLength = 0;
            }
            else
            {
                line.enabled = true;
                hit = Physics2D.Raycast(transform.position, transform.up, beamLength - 40);
                if (hit.collider != null && hit.transform.gameObject.tag == "Planet")
                {
                    hit.transform.position = transform.up * beamLength;
                    line.SetPosition(0, transform.position + transform.up * 30);
                    line.SetPosition(1, hit.transform.position);
                    hit.rigidbody.velocity = Vector3.zero;
                    hit.transform.gameObject.SendMessage("SetIsBound", true);
                    hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                    isFiring = false;
                    isLocked = true;
                    audioFile.clip = hitSound;
                    audioFile.Play();
                    WorldScript.score += 100 * WorldScript.mult;
                }
                else
                {
                    line.SetPosition(0, transform.position + transform.up * 30);
                    line.SetPosition(1, transform.up * 100 + transform.up * beamLength);
                    beamLength += 20;
                }
            }
        }
        if (isLocked)
        {
            line.SetPosition(0, transform.position + transform.up * 30);
            line.SetPosition(1, hit.transform.position);
        }
    }

    void ToggleLine()
    {
        line.enabled = false;
    }

    void SetIsLocked(bool status)
    {
        isLocked = status;
    }

    void ResetBeamLength()
    {
        beamLength = 0;
    }

}
