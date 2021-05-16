using System;
using System.Threading.Tasks;
using System.Numerics;

namespace NodeService
{
    public sealed class MessageService
    {
        public async ValueTask<NewMessageResponse> NewMessageAsync(Message message)
        {
            if (HashUtil.GetHash(message) != message.Hash) return new NewMessageResponse() { Code = 1001, Error = "Illegal Message" };
            return new NewMessageResponse();
        }
    }
}