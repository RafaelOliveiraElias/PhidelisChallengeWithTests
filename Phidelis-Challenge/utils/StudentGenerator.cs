using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Phidelis_Challenge.Entities;

namespace Phidelis_Challenge.utils
{
    public class StudentGenerator
    {
        public async Task<string> GenarateRandomStudent()
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://gerador-nomes.wolan.net/nome/aleatorio");
                var responseTask = client.GetAsync("");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = await result.Content.ReadAsStringAsync();
                List<string> newName = JsonSerializer.Deserialize<List<string>>(readTask);
                string fullName = String.Join(" ", newName);
                return fullName;
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
        public async Task<string> DocumentGenerator()
        {
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://2devs.com.br//v1/cpf");
                var responseTask = client.GetAsync("");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = await result.Content.ReadAsStringAsync();
                return readTask.Split("[\"")[1].Split("\"]")[0];
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
        public async Task<Student> full ()
        {
            Student TestStudent = new Student();
            TestStudent.Name = await GenarateRandomStudent();
            TestStudent.ParentName = await GenarateRandomStudent();
            TestStudent.Document = await DocumentGenerator();
            return TestStudent;
        }
    }
}