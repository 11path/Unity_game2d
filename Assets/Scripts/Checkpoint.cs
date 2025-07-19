using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            JohnMovement john = other.GetComponent<JohnMovement>();
            if (john != null)
            {
                john.UpdateCheckpoint(transform);
                Debug.Log("Checkpoint activado en: " + transform.position);
                
            }
        }
    }

}
