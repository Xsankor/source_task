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

        public WordChangerController() 
        {
            wordChanger = new WordChanger();
        }

        [HttpGet(Name = "GetWordChanger")]
        public async Task<ActionResult> Get(string text, int sortedChoice = 1)
        {
            if (wordChanger.CheckLine(text))
            {
                string reverseWord = wordChanger.ReverseString(text);
                Dictionary<char, int> countSymb = wordChanger.SymbCounter(text);
                string subStr = wordChanger.FindAndPringSubstring(reverseWord);
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
            else 
            {
                return BadRequest("Строка может состоять только из следующих символов: abcdefghijklmnopqrstuvwxyz");
            }
        }
    }
}
