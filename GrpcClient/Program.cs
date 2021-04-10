using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //****** TEST 1 ********
            //var input = new HelloRequest { Name = "Marcin" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);


            //****** TEST 2 ********
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var customerClient = new Customer.CustomerClient(channel);

            //var clientRequested = new CustomerLookupModel { UserId = 2 };

            //var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

            //Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            //Console.WriteLine();
            //Console.WriteLine("New customer list");
            //Console.WriteLine();

            //using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            //{
            //    while (await call.ResponseStream.MoveNext())
            //    {
            //        var currentCustomer = call.ResponseStream.Current;

            //        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} : {currentCustomer.Age} ");
            //    }
            //}

            //****** TEST 3 ********
            var options = new GrpcChannelOptions()
            {
                MaxReceiveMessageSize = 100 * 1024 * 1024 // 100 MB
            };

            var channel = GrpcChannel.ForAddress("https://localhost:5001", options);
            var interviewClient = new Interview.InterviewClient(channel);

            var clientRequested = new InterviewLookupModel { InterviewId = "exampleId" };

            using (var call = interviewClient.GetInterview(clientRequested))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var interviewCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{interviewCustomer.Id} interview data received.");
                }
            }

            Console.ReadLine();
        }
    }
}
