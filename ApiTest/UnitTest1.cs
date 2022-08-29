
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Phidelis_Challenge;
using System.Text.Json;
using Phidelis_Challenge.Entities;

namespace ApiTest;
public class UnitTest1 
    {
        HttpClient client = new HttpClient();

        [Fact]
        public async Task TestingAllStudents()
        {
            client.BaseAddress = new Uri("https://desafiophidelisrafael.azurewebsites.net/students/");
            var responseTask = client.GetAsync("");
            responseTask.Wait();
            var result = responseTask.Result;
            string readTask = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(readTask.Contains("Rafael Oliveira Elias"), true);
        }
    }