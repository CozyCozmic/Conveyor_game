using UnityEngine;

public class BinController : MonoBehaviour
{
    private int cubeCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the bin: " + other.name);

        if (other.CompareTag("Pickup"))
        {
            cubeCount++;

            Debug.Log("Ate " + cubeCount + " cube(s)!");

            Destroy(other.gameObject);
        }
    }
}