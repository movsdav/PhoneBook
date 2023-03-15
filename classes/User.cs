using System.Text;

namespace PhoneBook;

public class User
{
    private static string[] validSeparators = { ":", "-" };

    public static User CreateUserFromLine(string line)
    {
        string[] splitedLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        string separator = splitedLine[^2].Trim();
        string phoneNumberCode = splitedLine[^1].Trim();
        string name = splitedLine[0].Trim();
        string surname = splitedLine[1] == separator ? "" : splitedLine[1].Trim();

        User user = new User(name, surname, phoneNumberCode, line)
        {
            Separator = separator
        };
        return user;
    }
    public override string ToString()
    {
        if (initialUserLine != null)
            return initialUserLine;

        return $"{Name} {Surname} {Separator} {PhoneNumberCode}";
    }

    private string? initialUserLine;
    private string? errorMessage;

    public string Separator { get; init; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumberCode { get; set; }

    public User(string name, string surname, string phoneNumberCode, string? initalLine = null)
    {
        initialUserLine = initalLine;
        Name = name;
        Surname = surname;
        PhoneNumberCode = phoneNumberCode;
    }

    public string this[string propName]
    {
        get
        {
            return propName switch
            {
                "Name" => Name,
                "Surname" => Surname,
                "PhoneNumberCode" => PhoneNumberCode,
                _ => "",
            };
        }
    }

    public void Validate(int lineIndex)
    {
        StringBuilder result = new StringBuilder("");

        if (!validSeparators.Contains(Separator))
            result.Append(" separator should be ':' or '-'.");

        if (PhoneNumberCode.Length != 9)
        {
            result.Replace(".", ",");
            result.Append(" phone number should be 9 digits.");
        }
        
        if (result.ToString() == "")
        {
            errorMessage = null;
        }
        else
        {
            result.Insert(0, $"Line {lineIndex}:");
            errorMessage = result.ToString();
        }
    }

    public string? GetErrorMessage() => errorMessage;
}