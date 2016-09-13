using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public Text pinCount;
    public int lastStandingCount;
    public GameObject pinSet;

    private bool ballEnteredBox;
    private float lastChangeTime;
    private BowlingBall ball;

	// Use this for initialization
	void Start ()
	{
	    lastStandingCount = -1;
	    ball = FindObjectOfType<BowlingBall>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (ballEnteredBox)
	    {
            UpdateStandingCountAndSettle();
            pinCount.text = CountStanding().ToString();
        }
	}

    private void UpdateStandingCountAndSettle()
    {
        int standingPins = CountStanding();
        //Update the last standing count
        //Call method PinsHaveSettled() when they have
        if (lastStandingCount != standingPins)
        {
            lastChangeTime = Time.time;
            lastStandingCount = standingPins;
            return;
        }

        float timeTreshold = 3f;

        if (Time.time - lastChangeTime > timeTreshold)
        {
            StartCoroutine(PinsHaveSettled());
        }
    }

    private IEnumerator PinsHaveSettled()
    {
        pinCount.color = Color.green;
        lastStandingCount = -1;
        ballEnteredBox = false;

        yield return new WaitForSeconds(2);
        ball.Reset();
        pinCount.color = Color.white;
    }

    private int CountStanding()
    {
        int nbOfStandingPins = 0;

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin != null && pin.isStanding())
            {
                nbOfStandingPins++;
            }
        }

        return nbOfStandingPins;
    }

    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Raise();
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    public void RenewPins()
    {
        Instantiate(pinSet, new Vector3(0, 40, 0), Quaternion.identity);
        pinCount.text = CountStanding().ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BowlingBall>())
        {
            ballEnteredBox = true;
            pinCount.color = Color.red;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        GameObject pin = null;

        if (other.transform.parent)
            pin = other.transform.parent.gameObject;

        if (pin != null && pin.GetComponent<Pin>())
        {

            Destroy(pin);
        }
        
    }
}
