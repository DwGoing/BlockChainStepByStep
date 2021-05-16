using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Concurrent;

namespace NodeService
{
    public sealed class UnverifiedMessagePool
    {
        private readonly ConcurrentDictionary<string, Message> _unverifiedMessage = new();

        public bool Add(Message message) => _unverifiedMessage.TryAdd(message.Hash, message);

        public bool Remove(string messageHash) => _unverifiedMessage.TryRemove(messageHash, out _);

        public Message Get(string messageHash) => _unverifiedMessage.TryGetValue(messageHash, out var message) ? message : null;

        public Message Get(Expression<Func<Message, bool>> expression) => _unverifiedMessage.Values.Where(expression.Compile()).FirstOrDefault();
    }
}
