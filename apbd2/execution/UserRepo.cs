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

    public static User getUser(string userName)
    {
        return _users.ContainsKey(userName) ? _users[userName] : null;
    }

    public static List<User> getAllUsers()
    {
        return _users.Values.ToList();
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
}