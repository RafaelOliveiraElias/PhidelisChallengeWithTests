using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Phidelis_Challenge.Entities;

// GERADOR DE ESTUDANTE ALEATÓRIO COM API DE CPF E NOMES
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
        public async Task<Student> full()
        {
            Student newStudent = new Student();
            newStudent.Name = await GenarateRandomStudent();
            string parentName = await GenarateRandomStudent();
            // pegando o sobrenome do estudante.
            for (int i = 0; i < parentName.Split(" ").Count(); i++)
            {
                if(i != parentName.Split(" ").Count() - 1)
                {
                    newStudent.ParentName += parentName.Split(" ")[i] + " ";
                } else 
                {
                    newStudent.ParentName += newStudent.Name.Split(" ")[newStudent.Name.Split(" ").Count() - 1];
                }
            }
            newStudent.ParentName = parentName.Split(" ")[0] + " " + parentName.Split(" ")[1] + " " + newStudent.Name.Split(" ")[newStudent.Name.Split(" ").Count() - 1];
            newStudent.Document = await DocumentGenerator();
            return newStudent;
        }
    }
}