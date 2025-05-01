using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Services
{
    public interface IQueueService
    {
        Task SendMessageAsync(string message);
        Task<string> ReceiveMessageAsync();
    }
}
