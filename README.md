# Asteroid Destroyer

This is a sub-repository of the Imagine Quest Learning Github Project.

It will track all the development towards the Asteroid Destroyer Multiplication Game.


## Setting Up Testing 

### Installing Test Framework in Project
Install UTF via **Window > Package Manager**. Search for Test Framework under the **Unity Registry** in the Package Manager. Make sure to select latest version.

Once UTF is installed, open the **Packages/manifest.json** file with a text editor, and add a testables section after dependencies, like this:

```
,
"testables": [
"com.unity.inputsystem"
]
```

Save the file. This will be useful later on, when you’ll need to reference the Unity.InputSystem.TestFramework assembly for testing and emulating player input.

### Creating Tests Folder

With the root of your Project Assets folder highlighted, right-click and choose **Create > Testing > Tests Assembly Folder**.

This will create a Test folder, and inside should be a Test Assembly Definition called Tests.asmdef

### Creating Assembly Definition for Game Code

Make sure all of your game scripts are under a seperate folder in Assets. For example, mine are under Assets/Scripts/...

Right click the folder with all your scripts, in my case "Scripts/", then click **Create>Scripting>Assembly Definition**. Name the newly created file something like GameLogic. This should create a file called GameLogic.asmdef. Click on this file to open it in the Inspector and make sure **"Auto Referenced"** is checked.

### Setting up the Test.asmdef file
Go back to the Test.asmdef file (Assets/Tests/...) and click it to open it in Inspector. Under General Options, make sure only **"Override References"** is checked. 

Under Assembly Definition References add the following if not there:
> UnityEngine.TestRunner
>
> UnityEditor.TestRunner
>
> Unity.InputSystem
>
> Unity.InputSystem.TestFramework
>
> GameLogic

Where GameLogic is the name of the assembly definition file we created earlier for our game scripts. 

### Creating Your First Test File
Now you can go under Assets/Tests and create a new test file, something like LogicManagerTests.cs. 

You can find an example of how to setup your tests for my code in Assets/Tests/LogicManagerTests.cs

### Running Your Tests
Go to **Window>General>TestRunner** to run your tests. 

If you're getting **Null Exception Errors**, this normally means you're missing a reference to something in your test script.

Happy Testing!!!

### Testing Documentation Reference (Within Unity)
https://unity.com/how-to/automated-tests-unity-test-framework

## CI/CD Pipeline Testing

### Github Action Workflow
To setup a CI/CD pipeline you need to create a .github/workflows/ folder at the root of your project.
Then, add a .yml file (Ex: unity_tests.yml)

### Github Secret Setup
Under **Repository Settings > Secrets** you'll need to create the following: \n
- UNITY_EMAIL (contains your unity email)
- UNITY_PASSWORD (contains your unity password)
- UNITY_LICENSE (contains the contents of your .ulf file)

To find your .ulf file please see https://game.ci/docs/github/activation.

If you're using a **personal license** it will be here:
```
Windows: C:\ProgramData\Unity\Unity_lic.ulf
Mac: /Library/Application\ Support/Unity/Unity_lic.ulf
Linux: ~/.local/share/unity3d/Unity/Unity_lic.ulf
```

### Workflow File Setup
Please see .github/workflows/unity_tests.yml for an exmaple of a workflow.
```
#Important Info

permissions => needed to allow github to run the action

the env portion is crucial for connecting Unity with Github!
env:
    UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
    UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
    UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
```

### Example workflow runs can be found in this repo!
