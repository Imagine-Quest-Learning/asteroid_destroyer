using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LogicManagerScript : MonoBehaviour
{
    //References to Scripts/Objects
    public AstroidSpawnerScript astroidSpawnerScript;
    public GameOverScript gameOverScript;
    public ShieldCrackManager shieldCrackManagerScript;

    public GameObject explosion;

    //Shield Strength Variables
    public GameObject shieldStrengthUI;
    public int maxShieldStrength = 5;
    public int shieldStrength = 5;
    public Text shieldStrengthText;
    public Text lastHitText;

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
        maxShieldStrength = 5;
        ResetInputField();
    }

    
    [ContextMenu("Decrease Shield Strength")]
    public void DecreaseShieldStrength(){
        shieldStrength = shieldStrength - 1;
        shieldStrengthText.text = shieldStrength.ToString();

        shieldCrackManagerScript.UpdateShieldDisplay(maxShieldStrength, shieldStrength);

        if(shieldStrength == 1)
        {
            StartCoroutine(ShowLastHitText(3f));
        }
        
        if(shieldStrength <= 0)
        {
            GameOver();
        }
    }

    private IEnumerator ShowLastHitText(float duration)
    {
        lastHitText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        lastHitText.gameObject.SetActive(false);
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

    public void DestroyAstroid()
    {

        if(currentAstroid != null)
        {
            // Instantiate explosion at the asteroid's position
            GameObject newExplosion = Instantiate(explosion, currentAstroid.transform.position, Quaternion.identity);

            // Destroy the explosion effect after 1 second
            Destroy(newExplosion, 1f);
            
            // Destroy the asteroid
            Destroy(currentAstroid);
            Debug.Log("Asteroid Destroyed");
        
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
