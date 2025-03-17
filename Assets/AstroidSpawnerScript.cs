using UnityEngine;

public class AstroidSpawnerScript : MonoBehaviour
{
    public GameObject astroid;
    public float spawnRate = 5;
    public float heightOffset = 6;
    public float globalAstroidSpeed = 5f;
    public float fastestSpeed = 2f;
    public float slowestSpeed = 10f;

    void Start()
    {
        spawnAstroid(); 
        Debug.Log("Spawn 1st Astroid");
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Increase the asteroid speed by 1f without going over the maximum asteroid speed
    /// </summary>
    public void increaseAstroidSpeed()
    {
        globalAstroidSpeed = globalAstroidSpeed + 1f;

        if (globalAstroidSpeed < fastestSpeed)
        {
            globalAstroidSpeed = fastestSpeed;
            Debug.Log("Enforce fastest speed");
        }
        Debug.Log($"New Speed: {globalAstroidSpeed}");
    }

    /// <summary>
    /// Decrease the asteroid speed by 1f without going below the minimum asteroid speed
    /// </summary>
    public void decreaseAstroidSpeed()
    {
        globalAstroidSpeed = globalAstroidSpeed - 1f;

        if (globalAstroidSpeed > slowestSpeed)
        {
            globalAstroidSpeed = slowestSpeed;
            Debug.Log("Enforce slowest speed");
        }
        Debug.Log($"New Speed: {globalAstroidSpeed}");
    }

    /// <summary>
    /// Used to spawn asteroid with random starting position
    /// Once spawned, sets the asteroid's speed
    /// </summary>
    public void spawnAstroid()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        //Using Random.Range ensures the astroids will spawn anywhere between the lowest/highest points
        GameObject newAstroid = Instantiate(astroid, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        //Set speed of new astroid to be global speed
        AstroidScript astroidScript = newAstroid.GetComponent<AstroidScript>();
        if(astroidScript != null)
        {
            astroidScript.setSpeed(globalAstroidSpeed);
            Debug.Log($"Speed: {globalAstroidSpeed}");
        }
        Debug.Log($"Asteroid Spawned! Total asteroids in scene: {GameObject.FindGameObjectsWithTag("Astroid").Length}");
    }
}
