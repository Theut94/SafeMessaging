using Application.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        public Task<Chat> GetChat(string GUID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Chat>> GetChats()
        {
            var ListofChats = new List<Chat>()
    {
        new Chat
        {
            GUID = Guid.NewGuid().ToString(),
            Messages = new List<Message>
            {
                new Message
                {
                    Text = "Hello! How are you?",
                    GUID = Guid.NewGuid().ToString(),
                    Sender = "User1"
                },
                new Message
                {
                    Text = "I'm good, thanks! How about you?",
                    GUID = Guid.NewGuid().ToString(),
                    Sender = "User2"
                },
                new Message
                {
                    Text = "I'm good, thanks! How about you?",
                    GUID = Guid.NewGuid().ToString(),
                    Sender = "User2"
                }
            }
        },
        new Chat
        {
            GUID = Guid.NewGuid().ToString(),
            Messages = new List<Message>
            {
                new Message
                {
                    Text = "Did you complete the project?",
                    GUID = Guid.NewGuid().ToString(),
                    Sender = "User3"
                },
                new Message
                {
                    Text = "Yes, I just submitted it.",
                    GUID = Guid.NewGuid().ToString(),
                    Sender = "User4"
                }
            }
        },
        new Chat
        {
            GUID = Guid.NewGuid().ToString(),
            Messages = new List<Message>
            {
                new Message
                {
                    Text = "What are your plans for the weekend?",
                    GUID = Guid.NewGuid().ToString(),
                    Sender = "User5"
                },
                new Message
                {
                    Text = "Not sure yet, maybe just relax.",
                    GUID = Guid.NewGuid().ToString(),
                    Sender = "User6"
                }
            }
        }
    };

            return Task.FromResult(ListofChats);
        }
    }
}

