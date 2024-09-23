Console.Write("Введите строку -> ");
string? userText = Console.ReadLine(); 
string result = "";

if (userText != null)
{
    if (userText.Length % 2 != 0)
    {
        string tempStr = userText;
        result = string.Join("", userText.Reverse().ToArray());
        result += tempStr;
    }
    else
    {
        // char[] leftStr = userText.Substring(0, userText.Length / 2).Reverse().ToArray();
        // char[] rigthStr = userText.Substring(userText.Length / 2, userText.Length / 2).Reverse().ToArray();

        // result = string.Join("", leftStr) + string.Join("", rigthStr);

        int middleLengthStr = userText.Length / 2 - 1;
        for (int i = 0; i <= middleLengthStr; ++i) 
        {
            result = userText[i] + result;
            result += userText[userText.Length - 1 - i];
        }
    }

    Console.WriteLine($"Результат -> {result}");
}
else 
{
    Console.WriteLine("Данные некорректны");
}