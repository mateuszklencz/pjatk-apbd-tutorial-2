abstract class User
{
    static List<String> UserList;
    
    string UserName;
    string FirstName;
    string LastName;
    string UserType; 

    protected User(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.UserName = GenerateUserName(firstName, lastName);
        this.UserType = SetUserType();

    }

    private string GenerateUserName(string firstName, string lastName)
    {
        string tmpUser = firstName[0] + lastName[0:5]; 
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

    private SetUserType()
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
    }
}