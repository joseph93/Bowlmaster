using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BowlingBall))]
public class DragLaunch : MonoBehaviour
{
    private BowlingBall ball;
    private float startTime;
    private Vector3 startPosition;
    public GameObject arrowsPanel;

	// Use this for initialization
	void Start ()
	{
	    ball = GetComponent<BowlingBall>();
        Mathf.Clamp(ball.transform.position.x, -52.5f, 52.5f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DragStart()
    {
        if (!ball.isInPlay)
        {
            //Capture time and position of mouse click
            startTime = Time.time;
            startPosition = Input.mousePosition; 
        }
        
    }

    public void DragEnd()
    {
        if (!ball.isInPlay)
        {
            //launch the ball
            float deltaTime = Time.time - startTime;
            Vector3 endPosition = Input.mousePosition;
            Vector3 distance = endPosition - startPosition;

            float speedX = distance.x/deltaTime;
            float speedZ = distance.y/deltaTime;
            Vector3 velocity = new Vector3(speedX, 0, speedZ);
            ball.Launch(velocity);
            ball.isInPlay = true;
            arrowsPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("You have already launched the ball.");
        }
        
    }

    public void MoveStart(float xNudge)
    {
        if (!ball.isInPlay)
        {
            ball.transform.Translate(new Vector3(xNudge, 0, 0), Space.World);
        }
    }
}
