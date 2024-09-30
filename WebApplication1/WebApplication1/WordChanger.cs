using Microsoft.Extensions.Configuration;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class WordChanger
    {
        public readonly WordChangerSetting settingSection;
        private readonly IConfigurationRoot config;

        public WordChanger(IConfigurationRoot config) 
        {
            this.config = config;
            settingSection = config.GetSection("Setting").Get<WordChangerSetting>();
        }

        public string ReverseString(string userText)
        {
            string result = string.Empty;
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
            return result;
        }
        public string CheckLine(string text)
        {
            string? testStr = settingSection.ApproveSymbols;

            char[] chars = text.Where(c => testStr?.IndexOf(c) == -1).ToArray();

            return string.Join("", chars);
        }

        public bool CheckBlackList(string text) 
        {
            if(settingSection.BlackList?.Count > 0 && settingSection.BlackList.IndexOf(text) >= 0) 
            {
                return false;   
            }
            return true;
        }
        public Dictionary<char, int> SymbCounter(string text)
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

            //Console.WriteLine("Строка состоит из следующих символов: ");
            //countSymb.ToList().ForEach(x => Console.WriteLine($"{x.Key} -> {x.Value}"));
            return countSymb;
        }
        public string FindSubstring(string text)
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

            string resultSubstring = string.Empty;
            if (startIndex < endIndex && endIndex > 0)
            {
                resultSubstring = text.Substring(startIndex, endIndex - startIndex + 1);                
            }
            return resultSubstring;
        }
        public string SortStringAndPrint(string text, int sortedChoice)
        {
            switch (sortedChoice)
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
                    {
                        text = QuickSort(text.ToCharArray(), 0, text.Length - 1);
                        text = $"Ошибка в выборе сортировки. Нужно ввести 1 или 2! Выбрана сортировка по умолчанию (Quick sort). {text}";
                    }
                    break;
            }

            //Console.WriteLine($"Результат сортировки -> {text}");
            return text;
        }
        private string QuickSort(char[] charArr, int start, int end)
        {
            if (start < end)
            {
                int partIndex = ArrangeAndGetIndex(charArr, start, end);

                QuickSort(charArr, start, partIndex - 1);
                QuickSort(charArr, partIndex + 1, end);
            }
            return new string(charArr);
        }
        private int ArrangeAndGetIndex(char[] charArr, int start, int end)
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
        private void SwapElem(char[] charArr, int start, int end)
        {
            char tempChr = charArr[start];
            charArr[start] = charArr[end];
            charArr[end] = tempChr;
        }
        private string TreeSort(string text)
        {
            Tree tree = new Tree(text);
            return tree.GetSortedResult(tree.root);
        }
        public async Task<string> GetRandomNumberAndRemoveChar(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                int lengthText = text.Length;
                string urlRandomNumber = $"{config.GetSection("RandomAPI").Value}api/v1.0/random?min=0&max={lengthText}&count=1"; //!!
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
                return result;
            }
        }
    }
}
