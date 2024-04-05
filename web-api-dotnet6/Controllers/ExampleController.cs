using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Mvc;
using web_api_dotnet6.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection;

namespace web_api_dotnet6.Controllers
{
    public class InputData
    {
        public string p1 { get; set; }
    }


    [ApiController]
    [Route("/")]
    public class ExampleController : Controller
    {
        //private readonly ApiContext _apiContext;
        readonly IAuthorRepository _authorRepository;
        private readonly HttpClient _httpClient;


        public ExampleController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
            _httpClient = new HttpClient();

        }


        //Ex1
        //Getbooks
        [HttpGet("Ex1")]
        public ActionResult<List<Author>> GetEx1()
        {
            return Ok(_authorRepository.GetAuthors());
        }

        //Ex2.1
        [HttpPost("Postfucion1")]
        public IActionResult Postfucion1([FromBody] InputData data)
        {
            // แยกค่าที่เป็นตัวเลขออกมาเป็นอาร์เรย์
            string[] values = data.p1.Split(',');

            // เลือกค่าที่ซ้ำกัน
            var duplicates = values.GroupBy(x => x)
                                   .Where(group => group.Count() > 1)
                                   .Select(group => group.Key)
                                   .OrderBy(x => x) // เรียงลำดับค่า
                                   .Select(x => new { rank = x }); 

            // ส่งผลลัพธ์ที่เป็น JSON array
            return Ok(duplicates);
        }

        //Ex2.2
        [HttpPost("Postfucion2")]
        public IActionResult Postfucion2([FromBody] InputData data)
        {
            // แยกข้อมูลเป็นอาร์เรย์ของตัวเลขและอักขระ
            var numbers = data.p1.Split(',').Where(x => char.IsDigit(x[0]));
            var letters = data.p1.Split(',').Where(x => char.IsLetter(x[0]));

            // เรียงลำดับตัวอักษรและตัวเลข
            var sortedValues = letters.OrderBy(x => x).Concat(numbers.OrderBy(x => int.Parse(x)));

            // เลือกข้อมูลที่ซ้ำกัน
            var duplicates = sortedValues.GroupBy(x => x)
                                          .Where(group => group.Count() > 1)
                                          .Select(group => new { rank = group.Key });


            // ส่งข้อมูลที่ซ้ำกันและเรียงลำดับกลับไป
            return Ok(duplicates);
        }

        //Ex3

        [HttpGet("Ex3/{name}")]
        public async Task<IActionResult> GetEx3(string name)
        {
            var result = new Response();
            try
            {
                string apiFreeUrl = $"https://api.genderize.io?name={name}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiFreeUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var freeApi = new FreeAPI
                    {
                        url = apiFreeUrl,
                        method = "GET",
                        response = JsonConvert.DeserializeObject<Response>(responseData)
                };

                    return Ok(freeApi);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



    }   
}
