﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QXS.ChatBot.Rules
{
    public class ErrorRules
    {
        public static List<BotRule> rules = new List<BotRule>()
        {
                
                new RandomAnswersBotRule("geterror", 40, new Regex("i have (error|exception)", RegexOptions.IgnoreCase), new string[] {"what kind of error ?", "whats wrong pal ?", "whats seems to be a problem ?"}),

                new BotRule(
                    Name: "error",
                    Weight: 41,
                    MessagePattern: new Regex("(I have (error|exception))", RegexOptions.IgnoreCase),
                    Process: delegate (Match match, ChatSessionInterface session) {
                        string answer = "Whats the problem ?";

                        if (session.SessionStorage.Values.ContainsKey("UserName"))
                        {
                            answer += ", " + session.SessionStorage.Values["UserName"];
                        }

                        return answer;
                    }

                )

        };
    }
}
