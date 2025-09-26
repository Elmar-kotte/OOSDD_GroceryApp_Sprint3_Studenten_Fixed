using System.Collections.ObjectModel;
using Grocery.Core.Data.Repositories;
using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Moq;
using Grocery.Core.Models;
using Grocery.Core.Services;

namespace TestCore;
public class TestRegistration
{
    
    [SetUp]
    public void Setup()
    {
    }
    
    private static readonly object[] RegisterReturnsClientCases =
    {
        new object[]
        {
            "user4", 
            "user4@mail.com", 
            "password,password", 
            new Client(4, "user4", "user4@mail.com", "unknown")
        },
        new object[]
        {
            "testUser", 
            "testUser@mail.com", 
            "SuperPassword", 
            new Client(4, "testUser", "testUser@mail.com", "unknown")
        }
    };
    
    [TestCaseSource(nameof(RegisterReturnsClientCases))]
    public void TestRegisterReturnsClient(string name, string email, string password, Client? expectedClient)
    {
        // Arrange
        IClientRepository clientRepository = new ClientRepository();
        IClientService clientService = new ClientService(clientRepository);
        IAuthService authService = new AuthService(clientService);
        
        // Act
        Client? registeredClient = authService.Register(name, email, password);
        
        // Assert
        if (expectedClient == null)
            Assert.Fail();
        
        if (PasswordHelper.VerifyPassword(password, registeredClient.Password) == false)
            Assert.Fail("Password is not hashed");
        
        Assert.That((registeredClient.Name, registeredClient.EmailAddress), Is.EqualTo((expectedClient.Name, expectedClient.EmailAddress)));
    }
}