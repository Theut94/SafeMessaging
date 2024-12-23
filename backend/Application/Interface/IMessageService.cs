﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.DTO;

namespace Application.Interface
{
    public interface IMessageService
    {
        public Task<Message> AddMessage(Chat chat, MessageDTO messageDTO);

    }
}
