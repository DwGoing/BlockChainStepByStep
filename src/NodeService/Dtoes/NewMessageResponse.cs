using System;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Text.Json.Serialization;
using ProtoBuf;

namespace NodeService
{
    [ProtoContract]
    public sealed class NewMessageResponse
    {
        [ProtoMember(1)]
        public int Code { get; set; }
        [ProtoMember(2)]
        public string Error { get; set; }
    }
}