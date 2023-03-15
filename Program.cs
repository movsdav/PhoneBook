using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneBook;

public class Program
{
    private static User[] userStorage;

    public static void Main(string[] args)
    {
        ShowUsers();
        ShowValidationMessages();
    }

    private static string GetPath()
    {
        Console.Write("Enter the path: ");
        string path = Console.ReadLine(); // TODO: handle invalid path exception!

        Console.WriteLine();

        return path;
    }
    private static string[] GetLinesFromFile(string path)
    {
        string[] lines;
        using (StreamReader sr = new StreamReader(path))
            lines = sr.ReadToEnd().Trim().Split("\n");

        return lines;
    }
    private static User[] GetUsersFromLines(string[] lines)
    {
        List<User> result = new List<User>();
        for (int i = 0; i < lines.Length; i++)
            result.Add(User.CreateUserFromLine(lines[i], i + 1));

        return result.ToArray<User>();
    }

    private static void ShowUsers()
    {
        string path = GetPath();

        string[] lines = GetLinesFromFile(path);

        userStorage = GetUsersFromLines(lines);

        Console.WriteLine("File structure:");
        foreach (User user in userStorage)
            Console.WriteLine(user);

        Console.WriteLine();
    }
    private static void ShowValidationMessages()
    {
        Console.WriteLine("\nValidation messages:");

        string? errorMessage;
        foreach (User user in userStorage)
        {
            errorMessage = user.GetErrorMessage();
            if (errorMessage != null)
                Console.WriteLine(errorMessage);
        }
    }
}