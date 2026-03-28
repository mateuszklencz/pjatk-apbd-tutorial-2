using System;
using System.Collections.Generic;
using System.Linq;

public static class UserRepo
{
    static private Dictionary<string, User> _users = new();

    public static void addUser(User user)
    {
        _users[user.UserName] = user;
    }

    public static User? getUser(string userName)
    {
        return _users.ContainsKey(userName) ? _users[userName] : null;
    }

    public static List<User> getAllUsers()
    {
        return new List<User>(_users.Values);
    }

    public static void displayAllUsers()
    {
        Console.WriteLine("=== All Users ===");
        foreach (var user in _users.Values)
        {
            user.DisplayInfo();
            Console.WriteLine();
        }
    }

    public static void createUserEntry(string userType, string firstName, string lastName)
    {
        User newUser;

        if (userType == "Student")
        {
            newUser = new Student(firstName, lastName);
        }
        else if (userType == "Employee")
        {
            newUser = new Employee(firstName, lastName);
        }
        else
        {
            Console.WriteLine($"Unknown user type: {userType}. Defaulting to Student.");
            newUser = new Student(firstName, lastName);
        }

        addUser(newUser);
    }
}