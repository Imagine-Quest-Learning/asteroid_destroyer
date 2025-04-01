using UnityEngine;

public class QuestionTriggerScript : MonoBehaviour
{
    public LogicManagerScript logicScript;
    public GameObject currentAstroid;

    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    /// <summary>
    /// The question trigger is on the far right hand side of the screen.
    /// When an asteroid passes through this trigger, we will start the next multiplication question
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Astroid")
        {
            //get current astroid
            currentAstroid = collision.gameObject;

            //start multiplication questions
            logicScript.StartMultiplicationQuestion(currentAstroid);
        }
    }
}
