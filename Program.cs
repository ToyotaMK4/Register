//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------
using System;
using Register.Broker.Storeage;
using Register.Models;
using Register.Service;

IUserService flowerService = new UserService();
bool isContinue = true;
do
{
    Console.WriteLine("1. SignUp");
    Console.WriteLine("2. LogIn");
    Console.Write("Enter command ");
    string command = Console.ReadLine();
    if (command.Contains("1") is true)
    {
        Users users = new Users();
        Console.WriteLine("Your Gmail ");
        users.Email = Console.ReadLine();
        Console.WriteLine("Your password ");
        users.Password = Console.ReadLine();
        flowerService.SignUp(users);
    }
    if (command.Contains("2") is true)
    {
        Users users1 = new Users();
        Console.WriteLine(" Your Gmail ");
        users1.Email = Console.ReadLine();
        Console.WriteLine("Your password ");
        users1.Password = Console.ReadLine();
        flowerService.LogIn(users1);
    }
    Console.Write("Is Continue ");
    string isCommand = Console.ReadLine();
    if (isCommand.ToLower().Contains("no") is true)
    {
        isContinue = false;
    }
} while (isContinue is true);