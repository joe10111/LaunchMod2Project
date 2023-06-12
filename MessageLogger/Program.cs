// See https://aka.ms/new-console-template for more information
using MessageLogger.Models;

Console.WriteLine("Welcome to Message Logger!");
Console.WriteLine();

// Before doing the bellow line, check if any profiles exist in the users table
Console.WriteLine("Let's create a user pofile for you.");

 // Asking user for name and saving to var name 
Console.Write("What is your name? "); 
string name = Console.ReadLine();

 // Asking user for username and saving to var username 
Console.Write("What is your username? (one word, no spaces!) ");
string username = Console.ReadLine();
User user = new User(name, username);

// PLAN FOR USER
// Make a new user object by instating a new user object using 
// Name and username from above input
// Save to context

 // Telling the user the text commands that allow them to log out or exit
Console.WriteLine();
Console.WriteLine("To log out of your user profile, enter `log out`.");
Console.WriteLine();
Console.Write("Add a message (or `quit` to exit): ");

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
        user.Messages.Add(new Message(userInput));

        // PLAN FOR MESSAGES
        // Message is created above and added to users message list
        // Take the new message being created and save it first before adding above
        // Using the new object message var, add the new message to the conext of the message table
        // save changes to the context

         // Loop through user.Messages 
        foreach (var message in user.Messages)
        {   // Display all messages
            Console.WriteLine($"{user.Name} {message.CreatedAt:t}: {message.Content}");
        }

        // Prompt for message
        Console.Write("Add a message: ");

         // Asing again for user input
        userInput = Console.ReadLine();
        Console.WriteLine();
    }
     // If the user breaks out of above loop ask for new action
    Console.Write("Would you like to log in a `new` or `existing` user? Or, `quit`? ");
    userInput = Console.ReadLine();

     // If user wants to make a new profile
    if (userInput.ToLower() == "new")
    {
         // Asking user for name and saving to name var 
        Console.Write("What is your name? ");
        name = Console.ReadLine();
         // Asking user for username and saving to username var 
        Console.Write("What is your username? (one word, no spaces!) ");
        username = Console.ReadLine();

         // Create New user and save to user var
        user = new User(name, username);

         // Add user to user list
        users.Add(user);

         // Ask user for message
        Console.Write("Add a message: ");

         // Get user input for message
        userInput = Console.ReadLine();

    } // Log into existing account
    else if (userInput.ToLower() == "existing")
    {
         // Ask for username to check against
        Console.Write("What is your username? ");
        username = Console.ReadLine();

         // Set User to Null untill profile is found
        user = null;

         // Loop through users in users list
        foreach (var existingUser in users)
        {    // Check if username is present in existing users
            if (existingUser.Username == username)
            {   // If found set user(null rn) to existingUser with same username
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

}
 // Output thank you message 
Console.WriteLine("Thanks for using Message Logger!");
foreach (var u in users)
{ // Loop through users 
     // Display all messages a user wrote
    Console.WriteLine($"{u.Name} wrote {u.Messages.Count} messages.");
}
