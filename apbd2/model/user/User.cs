public abstract class User
{
    static List<string> UserList = new List<string>();

    public string UserName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserType { get; private set; }

    protected User(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.UserName = GenerateUserName(firstName, lastName);
        this.UserType = SetUserType();
    }

    private string GenerateUserName(string firstName, string lastName)
    {
        // guard against short last names
        string lastPart = lastName.Length >= 5 ? lastName.Substring(0, 5).ToLower() : lastName.ToLower();
        string tmpUser = firstName.Substring(0, 1).ToLower() + lastPart;
        if (UserList.Contains(tmpUser))
        {
            // Handle duplicate username case, e.g., by appending a number
            int suffix = 1;
            while (UserList.Contains(tmpUser + suffix))
            {
                suffix++;
            }
            tmpUser += suffix;
        }
        UserList.Add(tmpUser);
        return tmpUser;
    }

    private string SetUserType()
    {
        if (this is Student)
        {
            this.UserType = "Student";
        }
        else if (this is Employee)
        {
            this.UserType = "Employee";
        }
        else
        {
           throw new InvalidOperationException("Unknown user type");
        }
        return this.UserType;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"User: {UserName}");
        Console.WriteLine($"First Name: {FirstName}");
        Console.WriteLine($"Last Name: {LastName}");
        Console.WriteLine($"Type: {UserType}");
    }

    public override string ToString()
    {
        return $"{this.UserName}: ({this.FirstName} {this.LastName}, {this.UserType})";
    }
}