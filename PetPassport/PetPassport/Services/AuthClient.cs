using System;
using System.Threading.Tasks;
using PetPassport.Models.UserModels;

namespace PetPassport.Services
{
    public class AuthClient : ApiClient
    {
        public bool HasUser => User != null;

        public async Task LoginAsync(Login login)
        {
            var response = await HttpClient.PostAsync($"{Url}/User/Login", CreateContent(login));

            var user = await ReadContentAsync<User>(response);

            if (user.Role != "Власник домашньої тварини")
            {
                throw new Exception("Невірний логін або пароль");
            }

            User = user;
        }
    }
}