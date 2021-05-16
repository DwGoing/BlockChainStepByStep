using System;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Text.Json.Serialization;
using ProtoBuf;

namespace NodeService
{
    [ProtoContract]
    public class Message
    {
        [JsonIgnore]
        [ProtoMember(1)]
        public string Hash { get; set; }
        [ProtoMember(2)]
        public string Content { get; set; }
        [ProtoMember(3)]
        public string Extend { get; set; }
        [ProtoMember(4)]
        public long Timestamp { get; set; }
        [ProtoMember(5)]
        public string From { get; set; }
        [ProtoMember(6)]
        public long Fee { get; set; }

        public Message()
        {
            Hash = HashUtil.GetHash(this);
        }
    }

    [ProtoContract]
    public class VerifiedMessage : Message
    {
        [JsonIgnore]
        [ProtoMember(7)]
        public string Nonce { get; set; }
        [JsonIgnore]
        [ProtoMember(8)]
        public string Miner { get; set; }
    }
}