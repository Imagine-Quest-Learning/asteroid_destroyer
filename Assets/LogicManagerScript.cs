using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LogicManagerScript : MonoBehaviour
{
    //References to Scripts/Objects
    public AstroidSpawnerScript astroidSpawnerScript;
    public GameOverScript gameOverScript;

    //Shield Strength Variables
    public GameObject shieldStrengthUI;
    public int shieldStrength = 5;
    public Text shieldStrengthText;

    //Multiplication Question Variables
    public GameObject multiplicationUI;
    public Text questionText;
    public InputField answerInput;
    public bool globalSubmit = false;
    private GameObject currentAstroid;
    private int correctAns;
    private bool spawnLock = false;

    void Start()
    {
        spawnLock = false;
        ResetInputField();
    }

    
    [ContextMenu("Decrease Shield Strength")]
    public void DecreaseShieldStrength(){
        shieldStrength = shieldStrength - 1;
        shieldStrengthText.text = shieldStrength.ToString();

        
        if(shieldStrength < 0)
        {
            GameOver();
        }
    }

    public void StartMultiplicationQuestion(GameObject astroid)
    { 
        //asteroid that will be destroyed if question answered correctly
        currentAstroid = astroid;
        //multiplicationUI.SetActive(true);
        GenerateMultiplicationQuestion();
    }

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

    public void SubmitAnswer()
    {
        globalSubmit = true;

        //If we're waiting on the previous question's animation to finish, we exit
        if (spawnLock) return;

        int playerAnswer;

        if(int.TryParse(answerInput.text, out playerAnswer))
        {
            if(playerAnswer == correctAns)
            {
                HandleCorrectAns();
            }
            else
            {  
                HandleIncorrectAns();
            }
        }
        else
        {
            Debug.Log("No answer given");
            HandleIncorrectAns();
        }
    }

    private void HandleCorrectAns()
    {
        astroidSpawnerScript.increaseAstroidSpeed();

        DestroyAstroid();
        Debug.Log("Correct Answer, Speeding Up!");
        EndMultiplicationQuestion();
    }

    private void HandleIncorrectAns()
    {
        astroidSpawnerScript.decreaseAstroidSpeed();
        Debug.Log("Incorrect Answer, Slowing Down.");

        //allow asteroid to hit shield, that collision will handle ending question
    }

    public void EndMultiplicationQuestion()
    {
        if (spawnLock) return;
        spawnLock = true; //don't allow more asteroids to be spawned until correct answer is processed

        //multiplicationUI.SetActive(false);
        answerInput.text = "";
        answerInput.Select();
        astroidSpawnerScript.spawnAstroid(); //immediately spawn next asteroid

        //Reset spawnlock with delay
        StartCoroutine(ResetLocks());
    }

    private IEnumerator ResetLocks()
    {
        yield return new WaitForSeconds(0.1f);
        spawnLock = false;
        globalSubmit = false;
    }

    private void DestroyAstroid()
    {
        if(currentAstroid != null)
        {
            Destroy(currentAstroid);
            Debug.Log("Astroid Destroyed");
        }
    }

    
    /*
        brief: Puts a '?' in input field before every question
               Puts cursor on input field
    */
    private void ResetInputField()
    {
        answerInput.placeholder.GetComponent<Text>().text = "?";
        answerInput.Select();
        answerInput.ActivateInputField();
    }

    public void GameOver()
    {
        gameOverScript.Setup(); //display gameover screen
        astroidSpawnerScript.disableSpawning = true;

        //Hide other UI elements
        multiplicationUI.SetActive(false);
        shieldStrengthUI.SetActive(false);
    }

    void Update()
    {
        //when user presses enter, submit answer
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitAnswer();
        }
    }
}
