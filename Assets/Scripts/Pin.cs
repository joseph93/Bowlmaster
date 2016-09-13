using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour
{
    public float standingTreshold;
    public float distanceToRaise = 40f;

    public bool isStanding()
    {
        float tiltInX = Mathf.Abs(transform.eulerAngles.x);
        float tiltInZ = Mathf.Abs(transform.eulerAngles.z);
        if (tiltInX < standingTreshold && tiltInZ < standingTreshold)
            return true;

        return false;
    }

    public void Raise()
    {
        if (isStanding())
        {
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void Lower()
    {
        if (isStanding())
        {
            transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
