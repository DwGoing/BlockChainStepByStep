using System;
using System.ServiceModel;
using ProtoBuf.Grpc.Client;
using Grpc.Net.Client;

namespace NodeService
{
    [ServiceContract]
    public interface ITestRpc
    {
        [OperationContract]
        void Do();
    }

    public class TestRpc : ITestRpc
    {
        public void Do()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            var channel = GrpcChannel.ForAddress("http://localhost:1120");
            var client = channel.CreateGrpcService<IMessageRpc>();
            client.NewMessageAsync(new Message()).AsTask().Wait();
        }
    }
}