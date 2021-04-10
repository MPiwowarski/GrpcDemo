using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class InterviewService : Interview.InterviewBase
    {
        private readonly ILogger<InterviewService> _logger;

        public InterviewService(ILogger<InterviewService> logger)
        {
            _logger = logger;
        }

        public override async Task GetInterview(InterviewLookupModel request, IServerStreamWriter<InterviewModel> responseStream, ServerCallContext context)
        {
            //db query comes here...
            var oneMb = 1048576;
            var answerSet =  new string('A', oneMb * 1);
            var interview = new InterviewModel()
            {
                Id = "541ca5f9-4a2c-4591-8a3e-379bbb33d51a",
                InterviewUrl = "someUrl",
                AnswerSet = answerSet
            };
            await responseStream.WriteAsync(interview);
        }
    }
}
