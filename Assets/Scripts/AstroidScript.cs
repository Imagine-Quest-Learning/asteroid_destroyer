using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public float moveSpeed = 5f;
  
    void Start()
    {

    }

    /// <summary>
    /// Used to move the asteroid across the screen towards the shield
    /// </summary>
    void Update()
    {
        //Time.deltaTime ensures the speed does not depend on system's frame rate
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

    }

    /// <summary>
    /// Set the speed of the asteroid
    /// </summary>
    /// <param name="speed">The asteroid's new speed</param>
    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
