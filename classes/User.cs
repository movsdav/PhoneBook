using System.Text;

namespace PhoneBook;

public class User
{
    private static string[] validSeparators = { ":", "-" };

    public static User CreateUserFromLine(string line, int lineIndex)
    {
        string[] splitedLine = line.Split(" ");

        string separator = splitedLine[^2];
        string phoneNumberCode = splitedLine[^1];
        string name = splitedLine[0];
        string surname = splitedLine[1] == separator ? "" : splitedLine[1];

        User user = new User(name, surname, phoneNumberCode, line)
        {
            Id = lineIndex,
            Separator = separator
        };

        user.Validate();
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

    public int Id { get; init; }
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

    private void Validate()
    {
        StringBuilder result = new StringBuilder("");

        if (!validSeparators.Contains(Separator))
            result.Append("separator should be ':' or '-'.");

        if (PhoneNumberCode.Length - 1 != 9)
        {
            result.Replace(".", ",");
            result.Append("phone number should be 9 digits.");
        }

        if (result.ToString() == "")
        {
            errorMessage = null;
        }
        else
        {
            result.Insert(0, $"Line {Id}: ");
            errorMessage = result.ToString();
        }
    }

    public string? GetErrorMessage() => errorMessage;
}