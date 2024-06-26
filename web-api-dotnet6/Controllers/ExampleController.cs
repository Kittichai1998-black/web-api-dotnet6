﻿using System;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace web_api_dotnet6.Controllers
{
    public class InputData
    {
        public string p1 { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController, Authorize]

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

        [HttpGet("GetEx1")]
        public ActionResult<List<Author>> GetEx1()
        {
            return Ok(_authorRepository.GetAuthors());
        }

        //Ex2.1
        [HttpPost("Postfunction1")]
        public IActionResult Postfunction1([FromBody] InputData data)
        {
            // แยกค่าที่เป็นตัวเลขออกมาเป็นอาร์เรย์
            var message = "";
            string[] values = data.p1.Split(',');
                // เลือกค่าที่ซ้ำกัน
                var duplicates = values.GroupBy(x => x)
                                       .Where(group => group.Count() > 1)
                                       .Select(group => group.Key)
                                       .OrderBy(x => x) // เรียงลำดับค่า
                                       .Select(x => new { rank = x });

                return Ok(duplicates);
        }

        //Ex2.2
        [HttpPost("Postfunction2")]
        public IActionResult Postfunction2([FromBody] InputData data)
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


            return Ok(duplicates);
        }

        //Ex3
        [HttpGet("GetFreeAPI")]
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
