﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_OLP_Rest.Domain
{
    public class ChatBot : Entity
    {
        public int ChatBotId { get; set; }

        [Required]
        [StringLength(maximumLength: 255, MinimumLength = 6)]
        public string Name { get; set; }

        public List<ChatSession> ChatSessions { get; set; }

        public Course Course { get; set; }
    }
}
