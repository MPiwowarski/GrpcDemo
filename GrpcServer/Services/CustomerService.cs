using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService: Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if(request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if(request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else
            {
                output.FirstName = "Greg";
                output.LastName = "Thomas";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, 
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Marcin",
                    LastName = "Piwko",
                    EmailAddress = "marcin.piwko@test.com",
                    Age = 30,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Tim",
                    LastName = "Smith",
                    EmailAddress = "tim.smith@test.com",
                    Age = 30,
                    IsAlive = false
                },
                new CustomerModel
                {
                    FirstName = "Tony",
                    LastName = "Stark",
                    EmailAddress = "tony.stark@test.com",
                    Age = 30,
                    IsAlive = true
                },
            };
            
            foreach (var cust in customers)
            {
                await Task.Delay(10000);
                await responseStream.WriteAsync(cust);
            }
           
        }
    }
}
