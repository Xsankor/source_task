using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/WordChanger")]
    public class WordChangerController : ControllerBase
    {
        private readonly WordChanger wordChanger;

        public WordChangerController(ConfigurationManager config) 
        {
            wordChanger = new WordChanger(config);
        }

        [HttpGet(Name = "GetWordChanger")]
        public async Task<ActionResult> Get(string text, int sortedChoice = 1)
        {
            string extraСhars = wordChanger.CheckLine(text);
            if (extraСhars.Length > 0) 
            {
                return BadRequest($"Строка содержит лишние символы -> {extraСhars}. " +
                                  $"Можно использовать только следующие символы: {wordChanger.settingSection.ApproveSymbols}");
            }

            if (!wordChanger.CheckBlackList(text)) 
            {
                string[] blackList = wordChanger.settingSection.BlackList?.Select(word => word).ToArray() ?? new string[] { "" };
                string blackListInString = string.Join(", ", blackList);
                return BadRequest($"Строка не может соответствовать следующим словам: {blackListInString}");
            }

            string reverseWord = wordChanger.ReverseString(text);
            Dictionary<char, int> countSymb = wordChanger.SymbCounter(text);
            string subStr = wordChanger.FindSubstring(reverseWord);
            string sortedString = wordChanger.SortStringAndPrint(reverseWord, sortedChoice);
            string strWithoutRandSymbol = await wordChanger.GetRandomNumberAndRemoveChar(reverseWord);

            object result = new
            {
                reverseWord = reverseWord,
                countSymb = countSymb,
                subStr = subStr,
                sortedString = sortedString,
                strWithoutRandSymbol = strWithoutRandSymbol,
            };

            return Ok(result);
        }
    }
}
