using UnityEngine;
using System.Collections;

public class BowlingBall : MonoBehaviour
{

    private Rigidbody rb;
    private AudioSource audioSource;
    private Vector3 startPosition;

    public Vector3 velocity;
    public bool isInPlay;

	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	    audioSource = GetComponent<AudioSource>();
	    rb.useGravity = false;
	    startPosition = transform.position;
	}

    public void Launch(Vector3 velocity)
    {
        rb.useGravity = true;
        audioSource.Play();
        rb.velocity = velocity;
    }

    public void Reset()
    {
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        isInPlay = false;
        GetComponent<DragLaunch>().arrowsPanel.SetActive(true);
    }
    
}
