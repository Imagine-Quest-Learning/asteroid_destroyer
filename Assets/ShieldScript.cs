using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public LogicManagerScript logicScript;
    public AstroidSpawnerScript astroidSpawnerScript;

    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        astroidSpawnerScript = GameObject.FindGameObjectWithTag("AstroidSpawner").GetComponent<AstroidSpawnerScript>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Handles asteroid's colliding into shield
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Astroid") 
        {
            //Prevent multiple triggers of collider
            Collider2D astroidCollider = collision.GetComponent<Collider2D>();
            if (astroidCollider != null)
            {
                astroidCollider.enabled = false;
            }

            //if user didn't press enter yet, submit answer
            if(logicScript.globalSubmit == false)
            {
                logicScript.SubmitAnswer();
            }

            //Get rigid body of asteroid
            Rigidbody2D astroidRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if(astroidRb != null)
            {   
                //after hitting shield, send asteroid in opposite direction
                astroidRb.linearVelocityX = 10;
            }

            Debug.Log("Shield Strength MINUS 1.");
            logicScript.DecreaseShieldStrength();

            Debug.Log("Destroy Astroid Hitting Shield.");
            //Destroy(collision.gameObject, 0.5f); //Allow time for bounce before destroying asteroid
            logicScript.DestroyAstroid();
            logicScript.EndMultiplicationQuestion();
        }
    }
}
