﻿// See https://aka.ms/new-console-template for more information
using MessageLogger.Models;
using MessageLogger.Data;
using Microsoft.EntityFrameworkCore;

using (var context = new MessageLoggerContext())
{
    welcomeUser();
    
    User user = createUser(context);

    if (!context.Users.Contains(user))
    {
        context.Users.Add(user);
    }
 
    // Telling the user the text commands that allow them to log out or exit
    Console.WriteLine("To log out of your user profile, enter `log out`. \n");
    Console.Write("\n Add a message (or `quit` to exit): ");

    // Get user input for next action
    string userInput = Console.ReadLine();

    // Add user to list of users
    List<User> users = new List<User>() { user };

    // Loop While the user input is not 'quit'
    while (userInput.ToLower() != "quit")
    { // Loop While the user input is not 'log out' or 'quit'
        while (userInput.ToLower() != "log out")
        {
            // Add the message from user input to message list 
            Message messageToUse = new Message() { Content = userInput, CreatedAt = DateTime.Now.ToUniversalTime() };
            
            if(userInput.ToLower() == "quit")
            {
                break;
            }

            user.Messages.Add(messageToUse);
            context.Messages.Add(messageToUse); 
            context.SaveChanges();

            var userIncludingMessages = context.Users.Include(user => user.Messages).Where(cUser => cUser.Id == user.Id).Single();

                foreach (var currentMessage in userIncludingMessages.Messages)
                {
                    Console.WriteLine($"{user.Name} {currentMessage.CreatedAt.ToLocalTime():t}: {currentMessage.Content}");
                }
               
            Console.Write("Add a message: ");

            userInput = Console.ReadLine();
            Console.WriteLine();
        }

        Console.Write("Would you like to log in a `new` or `existing` user? Or, `quit`? ");
        userInput = Console.ReadLine();

        // If user wants to make a new profile
        if (userInput.ToLower() == "new")
        {
            // Create New user and save to user var
            user = createUser(context);

            // Add user to user list
            users.Add(user);
            context.Users.Add(user);

            // Ask user for message
            Console.Write("Add a message: ");

            // Get user input for message
            userInput = Console.ReadLine();

        } // Log into existing account
        else if (userInput.ToLower() == "existing")
        {
            // Ask for username to check against
            Console.Write("What is your username? ");
            var username = Console.ReadLine();

            // Set User to Null untill profile is found
            user = null;
            
            foreach (var existingUser in context.Users)
            {   
                if (existingUser.Username == username)
                {
                    user = existingUser;
                }
            }

            if (user != null)
            { // If user is not null (user is found) ask for message input
                Console.Write("Add a message: ");
                userInput = Console.ReadLine();
            }
            else
            { // If user is not found output "could not find user" and quit program
                Console.WriteLine("could not find user");
                userInput = "quit";

            }
        }

        context.SaveChanges();
    }
    // Output thank you message 
    Console.WriteLine("Thanks for using Message Logger!");

    // PLAN FOR READING USERS
    // Change forEach loop to use Context.Users instead of users to loop through table of users

    var usersToLoop = context.Users.Include(user => user.Messages);
    foreach (var u in usersToLoop)
    { // Loop through users 
      // Display all messages a user wrote
        Console.WriteLine($"{u.Name} has written {u.Messages.Count} messages.");
    }
}

static void welcomeUser()
{
    Console.WriteLine("Welcome to Message Logger!");
    Console.WriteLine();

    // Before doing the bellow line, check if any profiles exist in the users table
    Console.WriteLine("Let's create a user pofile for you.");
}

static User createUser(MessageLoggerContext context)
{
    Console.WriteLine("Would you like to log into an existing user? Y for yes N for no");
    string userInput = Console.ReadLine();

    if(userInput.ToLower() == "y")
    {
        Console.Write("What is your username? ");
        var existingUserName = Console.ReadLine();

        foreach (var existingUser in context.Users)
        {    // Update bellow to use context.existingUser.Username
            if (existingUser.Username == existingUserName)
            {   // If found set user(null rn) to existingUser with same username
                return existingUser;
            }
        }

        Console.WriteLine("Coult not find User, making new profile");
    }
    // Asking user for name and saving to var name 
    Console.Write("What is your name? ");
    string name = Console.ReadLine();

    // Asking user for username and saving to var username 
    Console.Write("What is your username? (one word, no spaces!) ");
    string username = Console.ReadLine();

    User user = new User() { Name = name, Username = username };

    return user;
}