using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LogicManagerScript : MonoBehaviour
{
    //References to Scripts/Objects
    public AstroidSpawnerScript astroidSpawnerScript;

    //Shield Strength Variables
    public int shieldStrength = 5;
    public Text shieldStrengthText;

    //Multiplication Question Variables
    public GameObject multiplicationUI;
    public Text questionText;
    public InputField answerInput;
    private GameObject currentAstroid;
    private int correctAns;
    private bool spawnLock = false;

    void Start()
    {
        spawnLock = false;
        ResetInputField();
        astroidSpawnerScript = GameObject.FindGameObjectWithTag("AstroidSpawner").GetComponent<AstroidSpawnerScript>();
    }

    /// <summary>
    /// Decrease the shield strength by 1
    /// </summary>
    [ContextMenu("Decrease Shield Strength")]
    public void DecreaseShieldStrength(){
        shieldStrength = shieldStrength - 1;
        shieldStrengthText.text = shieldStrength.ToString();

        //will need to add function for gameover when shieldstrength = 0;
    }


    //FUNCTIONS FOR MULTIPLICATION QUESTIONS FOLLOW

    /// <summary>
    /// Sets up the multiplication question UI
    /// "Attaches" current asteroid to question 
    /// </summary>
    /// <param name="astroid">The asteroid in current scene</param>
    public void StartMultiplicationQuestion(GameObject astroid)
    { 
        //asteroid that will be destroyed if question answered correctly
        currentAstroid = astroid;
        multiplicationUI.SetActive(true);
        GenerateMultiplicationQuestion();
    }

    /// <summary>
    /// Randomly generate multiplication question with numbers 1-12
    /// Display the question on UI
    /// </summary>
    private void GenerateMultiplicationQuestion()
    {
        //Generate a random multiplication question
        int num1 = Random.Range(1, 12);
        int num2 = Random.Range(1, 12);
        correctAns = num1 * num2;

        Debug.Log(num1);
        Debug.Log(num2);
        Debug.Log(correctAns);

        //Display the question
        questionText.text = $"{num1} x {num2} = ";

        ResetInputField();
    }

    /// <summary>
    /// Handles the user submitting answer to multiplication question
    /// </summary>
    public void SubmitAnswer()
    {
        //TO DO: Add handling for empty answer

        //If we're waiting on the previous question's animation to finish, we exit
        if (spawnLock) return;

        int playerAnswer;

        if(int.TryParse(answerInput.text, out playerAnswer))
        {
            if(playerAnswer == correctAns)
            {
                //increase speed by 1f
                astroidSpawnerScript.increaseAstroidSpeed();

                DestroyAstroid();
                Debug.Log("Correct Answer, Speeding Up!");
                EndMultiplicationQuestion();
            }
            else
            {  
                //decrease speed by 1f
                astroidSpawnerScript.decreaseAstroidSpeed();
                Debug.Log("Incorrect Answer, Slowing Down.");

                //allow asteroid to hit shield, that collision will handle ending question
            }
        }
    }

    /// <summary>
    /// Handles ending the multiplication question if the answer was correct
    /// </summary>
    public void EndMultiplicationQuestion()
    {
        if (spawnLock) return;
        spawnLock = true; //don't allow more asteroids to be spawned until correct answer is processed

        multiplicationUI.SetActive(false);
        answerInput.text = "";
        answerInput.Select();
        astroidSpawnerScript.spawnAstroid(); //immediately spawn next asteroid

        //Reset spawnlock with delay
        StartCoroutine(ResetSpawnLock());
    }

    /// <summary>
    /// Resets spawn lock with slight delay (used for better performance)
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetSpawnLock()
    {
        yield return new WaitForSeconds(0.1f);
        spawnLock = false;
    }

    /// <summary>
    /// Remove current asteroid from scene
    /// </summary>
    private void DestroyAstroid()
    {
        if(currentAstroid != null)
        {
            Destroy(currentAstroid);
            Debug.Log("Astroid Destroyed");
        }
    }

    /// <summary>
    /// Puts a '?' in the input box before every question
    /// Puts the cursor on the input box before every question
    /// </summary>
    private void ResetInputField()
    {
        answerInput.placeholder.GetComponent<Text>().text = "?";
        answerInput.Select();
        answerInput.ActivateInputField();
    }

    /// <summary>
    /// If user pressed enter, submit their answer
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitAnswer();
        }
    }
}
