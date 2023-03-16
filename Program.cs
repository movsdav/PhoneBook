namespace PhoneBook;

public class Program
{
    private static List<User> userStorage;

    public static void Main(string[] args)
    {
        SetUpData();
        ShowSortingOptions();
        ShowUsers();
        ShowValidationMessages();
    }

    private static string GetPath()
    {
        Console.Write("Enter the path: ");
        string path = Console.ReadLine();

        Console.WriteLine();

        return path;
    }
    private static string[] GetLinesFromFile(string path)
    {
        string[] lines;
        using (StreamReader sr = new StreamReader(path))
        {
            lines = sr.ReadToEnd().Trim().Split("\n");
        }

        return lines;
    }
    private static List<User> GetUsersFromLines(string[] lines)
    {
        List<User> result = new List<User>();
        for (int i = 0; i < lines.Length; i++)
            result.Add(User.CreateUserFromLine(lines[i]));

        return result;
    }
    private static string GetSortOrder()
    {
        Console.Write("Please choose an ordering to sort \"Ascending\" or \"Descending\": ");
        string order = Console.ReadLine();
        return order;
    }
    private static string GetSortCriteria()
    {
        Console.Write("Please choose criteria \"Name\", \"Surname\" or \"PhoneNumberCode\": ");
        string criteria = Console.ReadLine();
        return criteria;
    }

    private static void ShowSortingOptions()
    {
        string order = GetSortOrder();
        string criteria = GetSortCriteria();

        SortStorage(criteria, order, (criteria, order) =>
        {
            int sortOrder = (order == "Ascending") ? 1 : -1;

            for (int i = 0; i < userStorage.Count; i++)
            {
                for (int j = i + 1; j < userStorage.Count; j++)
                {
                    if (string.Compare(userStorage[i][criteria], userStorage[j][criteria]) == sortOrder)
                    {
                        (userStorage[i], userStorage[j]) = (userStorage[j], userStorage[i]);
                    }
                }
            }
        });
    }
    private static void ShowUsers()
    {
        Console.WriteLine("\nFile structure:");
        foreach (var el in userStorage)
        {
            Console.WriteLine(el);

        }
    }
    private static void ShowValidationMessages()
    {
        ValidateUsers();

        Console.WriteLine("\nValidation messages:");

        string? errorMessage;
        foreach (User user in userStorage)
        {
            errorMessage = user.GetErrorMessage();
            if (errorMessage != null)
            {
                
                Console.WriteLine(errorMessage);
            }
        }
    }

    private static void ValidateUsers()
    {
        for (int i = 0; i < userStorage.Count; i++)
            userStorage[i].Validate(i + 1);
    }
    private static void SetUpData()
    {
        string path = GetPath();
        string[] lines = GetLinesFromFile(path);
        userStorage = GetUsersFromLines(lines);
    }
    private static void SortStorage(string criteria, string order, Action<string, string> algorithm)
    {
        void FormatEmptySurnameSortedStorage()
        {
            Console.WriteLine();
            List<User> buffer = new List<User>();

            int i = 0;
            while (i < userStorage.Count)
            {
                if (userStorage[i].Surname == "")
                {
                    buffer.Add(userStorage[i]);
                    userStorage.RemoveAt(i);
                    i--;
                }
                i++;
            }
            userStorage.AddRange(buffer);
            Console.WriteLine();
        }

        algorithm(criteria, order);
        if (criteria == "Surname")
            FormatEmptySurnameSortedStorage();
    }
}