using UnityEngine;

public class FloatFlowing : MonoBehaviour
{
    public float minVelocityX = 0.01f, maxVelocityX = 0.1f; // Velocity range along X
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float logSpeed = Random.Range(minVelocityX, maxVelocityX);
        transform.Translate(Vector3.left * logSpeed * Time.deltaTime);
    }
}
