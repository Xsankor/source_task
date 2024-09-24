using System.Text.RegularExpressions;

void ReadAndReverse() 
{
    Console.Write("Введите строку -> ");
    string? userText = Console.ReadLine();
    string result = "";

    if (userText != null)
    {
        if (!checkLine(userText)) return;

        else if (userText.Length % 2 != 0)
        {
            string tempStr = userText;
            result = string.Join("", userText.Reverse().ToArray());
            result += tempStr;
        }
        else
        {
            int middleLengthStr = userText.Length / 2 - 1;
            for (int i = 0; i <= middleLengthStr; ++i)
            {
                result = userText[i] + result;
                result += userText[userText.Length - 1 - i];
            }
        }

        Console.WriteLine($"Результат -> {result}");

        SymbCounter(userText);
        FindAndPringSubstring(result);
    }
    else
    {
        Console.WriteLine("Данные некорректны");
    }
}

bool checkLine(string text) 
{
    string testStr = "abcdefghijklmnopqrstuvwxyz";

    char[] chars = text.Where(c => testStr.IndexOf(c) == -1).ToArray();

    if (chars.Length > 0)
    {
        Console.WriteLine($"Ошибка ввода. Неподходящие символы -> {string.Join("", chars)}");
        return false;
    }
    return true;
}

void SymbCounter(string text) 
{
    Dictionary<char, int> countSymb = new Dictionary<char, int>();

    foreach (char chr in text)
    {
        if (countSymb.ContainsKey(chr))
        {
            ++countSymb[chr];
        }
        else
        {
            countSymb.Add(chr, 1);
        }
    }

    Console.WriteLine("Строка состоит из следующих символов: ");
    countSymb.ToList().ForEach(x => Console.WriteLine($"{x.Key} -> {x.Value}"));
}

void FindAndPringSubstring(string text) 
{
    int startIndex = text.Length, endIndex = 0;
    string vowelString = "aeiouy";
    for (int i = 0, pos = 0; i < vowelString.Length; ++i) 
    {
        pos = text.IndexOf(vowelString[i]);
        pos = pos < 0 ? startIndex : pos;
        startIndex = Math.Min(startIndex, pos);
        endIndex = Math.Max(endIndex, text.LastIndexOf(vowelString[i]));
    }

    if (startIndex < endIndex && endIndex > 0) 
    {
        string resultSubstring = text.Substring(startIndex, endIndex - startIndex + 1);
        Console.WriteLine($"Строка, которая начинается и заканчивается гласной -> {resultSubstring}");
    }
}

ReadAndReverse();