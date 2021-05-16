using System;
using System.Threading.Tasks;
using System.ServiceModel;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Client;
using Grpc.Net.Client;

namespace NodeService
{
    [ServiceContract]
    public interface IMessageRpc
    {
        [OperationContract]
        ValueTask NewMessageAsync(Message message, CallContext context = default);
    }

    public class MessageRpc : IMessageRpc
    {
        private readonly MessageService _service;
        private readonly NodeService _nodeService;

        public MessageRpc(MessageService service, NodeService nodeService)
        {
            _service = service;
            _nodeService = nodeService;
        }

        public async ValueTask NewMessageAsync(Message message, CallContext context = default)
        {
            await _service.NewMessageAsync(message);
        }
    }
}