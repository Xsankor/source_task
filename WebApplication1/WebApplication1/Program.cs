using WebApplication1;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async context =>
{
    // task 1, 2
    string text = ReadUserInput();
    string result = ReverseString(text);
    
    // task 3
    SymbCounter(text);
    
    // task 4
    FindAndPringSubstring(result);
    
    // task 5
    SortStringAndPrint(result);
    
    // task 6
    GetRandomNumberAndRemoveChar(result);
});

app.Run();
string ReadUserInput()
{
    Console.Write("Введите строку -> ");
    string? userText = Console.ReadLine();
    return userText == null ? "" : userText;  
}
string ReverseString(string userText) 
{
    string result = string.Empty;
    if (CheckLine(userText))
    {
        if (userText.Length % 2 != 0)
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

        Console.WriteLine($"Результат разворота строки -> {result}");
    }
    else
    {
        Console.WriteLine("Данные некорректны");
    }

    return result;
}
bool CheckLine(string text)
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
void SortStringAndPrint(string text)
{
    Console.Write("Какой сортировкой воспользоваться? 1) Quicksort 2) Tree sort -> ");
    string? userChoice = Console.ReadLine();
    if (userChoice == null)
    {
        Console.WriteLine("Вы ничего не ввели.");
    }

    int.TryParse(userChoice, out int numberSort);

    switch (numberSort)
    {
        case 1:
            {
                text = QuickSort(text.ToCharArray(), 0, text.Length - 1);
            }
            break;
        case 2:
            {
                text = TreeSort(text);
            }
            break;
        default:
            Console.WriteLine("Ошибка в выборе сортировки. Нужно ввести 1 или 2!");
            return;
    }

    Console.WriteLine($"Результат сортировки -> {text}");
}
string QuickSort(char[] charArr, int start, int end)
{
    if (start < end)
    {
        int partIndex = ArrangeAndGetIndex(charArr, start, end);

        QuickSort(charArr, start, partIndex - 1);
        QuickSort(charArr, partIndex + 1, end);
    }
    return new string(charArr);
}
int ArrangeAndGetIndex(char[] charArr, int start, int end)
{
    char rightValue = charArr[end];

    int leftPos = start - 1;

    for (int i = start; i <= end; ++i)
    {
        if (charArr[i] < rightValue)
        {
            ++leftPos;
            SwapElem(charArr, leftPos, i);
        }
    }
    SwapElem(charArr, leftPos + 1, end);
    return leftPos + 1;
}
void SwapElem(char[] charArr, int start, int end)
{
    char tempChr = charArr[start];
    charArr[start] = charArr[end];
    charArr[end] = tempChr;
}
string TreeSort(string text)
{
    Tree tree = new Tree(text);
    return tree.GetSortedResult(tree.root);
}
async void GetRandomNumberAndRemoveChar(string text) 
{
    using (HttpClient client = new HttpClient()) 
    {
        int lengthText = text.Length;
        string urlRandomNumber = $"https://www.randomnumberapi.com/api/v1.0/random?min=0&max={lengthText}&count=1";
        using HttpResponseMessage response = await client.GetAsync(urlRandomNumber);

        Random rnd = new Random();

        int index = -1;
        if (response.IsSuccessStatusCode)
        {
            string content = response.Content.ReadAsStringAsync().Result;
            content = content.Replace("[", "").Replace("]", "");
            bool isCorrect = int.TryParse(content, out index);
            if (!isCorrect)
            {
                index = rnd.Next(0, text.Length);
            }
        }
        else
        {
            index = rnd.Next(0, text.Length);
        }
        string result = text.Remove(index, 1);
        Console.WriteLine($"Символ \"{text[index]}\" под индексом {index} удалён. Результат -> {result}");
    } 
}
