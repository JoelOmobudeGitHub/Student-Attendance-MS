
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.Controllers;
using StudentAttendanceManagementSystem.Data;
using StudentAttendanceManagementSystem.Models;
using Xunit;
namespace test;

public class Class1
{
        public class PrimeService_IsPrimeShould

    {
        
        readonly HttpClient _client;    
        const string BASE_URL = "/Admin/Login";

        [Fact]
        public void TestName()
        {
            // Given
        
            // When
        
            // Then
        }
        public async void UserCanLogin()
        {
            
             LoginViewModel loginViewModel = new LoginViewModel();
             loginViewModel.Username = "joel.test@gmail.com";
             loginViewModel.Password = "Test123@Logic123";

             var response = await _client.GetAsync(BASE_URL);

            response.StatusCode.Equals(HttpStatusCode.OK);
            
        }
    }
}
