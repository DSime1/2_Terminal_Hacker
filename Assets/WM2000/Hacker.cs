using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{

    //Department names array
    String[] dep = { "Old Archive", "Secret Operations", "National Security" };


    //variable that keeps track of the level
    int level = 0;

    //variable for final test stage
    int stage = 0;


    //Passwords variables and arrays
    String password;
    String shuffledPass;

    String[] Password1 = { "Casino", "Royale", "Momentum", "Quantum", "Spectre" };
    String[] Password2 = { "JamesBond", "SeanConnery", "PierceBrosnan", "DanielCraig", "RogerMoore" };
    String[] Password3 = { "VesperLynd", "PussyGalore", "TatianaRomanova", "Solitaire", "Octopussy" };

    //enumerator that defines the game status
    enum Screen { MainMenu, AskPassword, CorrectPassword, FinalTest };

    Screen currentScreen;



    // Use this for initialization
    void Start()
    {
        ShowMainMenu("Hello Stranger");
    }


    //intro function that defines main menu, 
    //has an input parameter (totally optional) named 'greet' that
    //shoul be provided when calling this method
    void ShowMainMenu(string greet)
    {

        Terminal.ClearScreen();
        currentScreen = Screen.MainMenu;
        Terminal.WriteLine(greet);
        Terminal.WriteLine("You just logged into MI6 server.");
        Terminal.WriteLine("Please choose a department number:");
        Terminal.WriteLine("1) " + dep[0]);
        Terminal.WriteLine("2) " + dep[1]);
        Terminal.WriteLine("3) " + dep[2]);
        Terminal.WriteLine("Type 'U' anytime to uncover");
        Terminal.WriteLine("the secret agent identity.");
        Terminal.WriteLine("Type 'Q' anytime to quit. ");
        Terminal.WriteLine("Type 1,2 or 3");
        Terminal.WriteLine("to enter your selection:");

    }


    //this is a Message, doesn't need to be called in start method 
    //as it works whenever user types something and press enter key.

    void OnUserInput(string input)
    {
        if (input == "Menu") //User should always be able to go to Menu
        {
            level = 0;
            currentScreen = Screen.MainMenu;
            ShowMainMenu("Welcome Back Agent X");
        }
        else if (input == "Q")
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Quitting.");
            Application.Quit();


        }

        else if (input == "U")
        {
            stage = 1;
            Terminal.ClearScreen();
            Terminal.WriteLine("Agent:");


        }
        else if (stage == 1) { 
            FinalTest(input); 
            stage = 2; }

        else if (stage == 2)
        {
            stage = 3;
            FinalTest2(input);
        }
        else if (stage == 3)
        {
            stage = 0;
            FinalTest3(input);
        }

        else if (currentScreen == Screen.MainMenu)//go to department selection
        {
            RunMainMenu(input);
        }

        else if (currentScreen == Screen.AskPassword)//in game password control
        {
            CheckPassword(input);
        }
        //showing the reward after success message
        else if (currentScreen == Screen.CorrectPassword && input == "Go")
        {
            ShowReward();
        }

    }


    //Selecting Departments and changing game status (StartGame())
    //if department inserted is not valid game status remain unchanged

    void RunMainMenu(string input)
    {
        if (input == "1")
        {
            StartGame(input);
            AskPassword(input);

        }
        else if (input == "2")
        {
            StartGame(input);
            AskPassword(input);
        }
        else if (input == "3")
        {
            StartGame(input);
            AskPassword(input);
        }

        else
        {
            Terminal.WriteLine("Please select a valid department number");
        }
    }


    //Method that checks if the password inserted is correct or incorrect.
    void CheckPassword(String input)
    {
        if (input == password)
        {

            CorrectPassword("Access Granted");

        }
        else { Terminal.WriteLine("Incorrect Password, try again."); }
    }


    //Method starts the game when selecting a valid department from available ones
    void StartGame(String choosenLvl)
    {
        int lvl = int.Parse(choosenLvl);
        currentScreen = Screen.AskPassword;
        Terminal.ClearScreen();
        Terminal.WriteLine("Type 'Menu' to go back.");
        Terminal.WriteLine("You are connecting to:");
        Terminal.WriteLine(dep[--lvl]);
    }

    //Password asking and initialization depending on department (level) selected
    void AskPassword(string choosenLvl)
    {
        bool isValidLevel = (choosenLvl == "1" || choosenLvl == "2" || choosenLvl == "3");

        level = int.Parse(choosenLvl);

        int index = UnityEngine.Random.Range(0, 5);

        switch (level)
        {
            case 1:
                password = Password1[index];

                print(password);

                break;

            case 2:
                password = Password2[index];

                print(password);
                break;

            case 3:
                password = Password3[index];
                print(password);
                break;

            default:
                print("no password required");
                break;

        }

        Shuffle(password);

        Terminal.WriteLine("Current password hint: "+ shuffledPass);
        Terminal.WriteLine("Please provide correct password:");

    }

    //Shuffling characters method
    void Shuffle(String pass)
    {
        char[] array = pass.ToCharArray();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0,(n + 1));
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        shuffledPass = new string(array);
    }

    //called when password is correct

    void CorrectPassword(string Success)
    {

        currentScreen = Screen.CorrectPassword;
        Terminal.ClearScreen();
        Terminal.WriteLine(Success);
        Terminal.WriteLine("Type 'Go' ");
        Terminal.WriteLine("to access files.");
        Terminal.WriteLine("Type 'U' to uncover agent");
        Terminal.WriteLine("Type 'Menu' to go back to main.");
        Terminal.WriteLine("Type 'Q' to quit.");
    }

    //Reward showing secret data depending on level chosen.
   void ShowReward()
    {
            var agentName = Environment.UserName;
            switch (level)
        {
            case 1:
                Terminal.WriteLine(@"
      ___________________ 
  .-/| ***Confidential*** \
  ||||                    |
  |||| Agent: 007         |
  |||| Code Name: ████████|
  |||| Rank: Operative    |
  |||| Real Name: ████████| 
  ||||___________________ |
  ||/====================\|
  `---------------------~_/
");
                break;

            case 2:
                Terminal.WriteLine(@"
      ___________________ 
  .-/| ***Confidential*** \
  ||||                    |
  |||| Agent: ████████    |
  |||| Code Name: J.Bond  |
  |||| Rank: Operative    |
  |||| Real Name: ████████| 
  ||||___________________ |
  ||/====================\|
  `---------------------~_/
");
                break;

            case 3:
                Terminal.WriteLine(@"
      ___________________ 
  .-/| ***Confidential*** \
  ||||                    |
  |||| Agent: ████████    |
  |||| Code Name: ███████ |
  |||| Rank: Operative    |
  |||| Real Name:" + agentName + @"   | 
  ||||___________________ |
  ||/====================\|
  `---------------------~_/
");
                break;

        }
    }
    
    void FinalTest(String input)
    {
        Terminal.ClearScreen();
        currentScreen = Screen.FinalTest;


        if (input == "007")
        {
                Terminal.ClearScreen();

                Terminal.WriteLine("Code Name:");
        
            }
        else { Terminal.WriteLine(@"You Failed
Type 'U' to try again.
Type 'Menu' to go back.
Type 'Q' to quit"); }
        }


    void FinalTest2(String input)
    {
        Terminal.ClearScreen();
        currentScreen = Screen.FinalTest;

       


        if (input == "J.Bond")
        {

            Terminal.ClearScreen();
            Terminal.WriteLine("Real Name:");

        }
        else { Terminal.WriteLine(@"You Failed
Type 'U' to try again.
Type 'Menu' to go back
Type 'Q' to quit"); }


    }
    void FinalTest3(String input)
    {
        Terminal.ClearScreen();
        currentScreen = Screen.FinalTest;

        var agentName = Environment.UserName;


        if (input == agentName )
        {
            
            Terminal.WriteLine(@"Mission Completed

             .-.  .-.  --..::==
            (   )(   )  //''
             '-'  '-'  //

Type 'Menu' to restart the game
or 'Q' to quit");

        }
        else { 
            Terminal.WriteLine(@"You Failed
Type 'U' to try again.
Type 'Menu' to go back
Type 'Q' to quit");
        }


    }
   

	// Update is called once per frame
	void Update () {
	
	}
}
