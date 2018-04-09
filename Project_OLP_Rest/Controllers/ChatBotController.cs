﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QXS.ChatBot;

namespace Project_OLP_Rest.Controllers
{
    [Produces("application/json")]
    [Route("api/ChatBot")]
    public class ChatBotController : Controller
    {
        public static BotRule generateVarDumpRule()
        {
            return new BotRule(
                    Name: "var_dump",
                    Weight: 200,
                    MessagePattern: new Regex("^var_?dump$", RegexOptions.IgnoreCase),
                    Process: delegate (Match match, ChatSessionInterface session)
                    {
                        string answer = "Variables: \n";
                        foreach (string key in session.SessionStorage.Values.Keys)
                        {
                            answer += "  " + key + " = " + session.SessionStorage.Values[key] + "\n";
                        }
                        answer += "---\n";
                        answer += "History: \n";
                        int i = 0;
                        foreach (BotResponse response in session.GetResponseHistory())
                        {
                            answer += "  " + (++i) + ". " + response.RuleName + "\n";
                            answer += "      " + response.Question.Replace("\n", "\n      ") + "\n";
                            answer += "          " + response.Answer.Split('\n')[0] + "\n";
                        }
                        return answer;
                    }
                );
        }

        public static List<BotRule> createBotRulesFromCs()
        {
            return new List<BotRule>()
            {
                new PowershellBotRule("pstest", 10, new Regex("powershell"), @" ( ""Hi from PowerShell "" + $PSVersionTable.PSVersion) "),
                // debug rule
                generateVarDumpRule(),
                // chatbot specific behaviour
                new ConditionBotRule(
                    "conditionBot",
                    50,
                    new Tuple<string, ConditionBotRule.Operator, string>[] {
                        new Tuple<string, ConditionBotRule.Operator, string>("BotName", ConditionBotRule.Operator.EqualIgnoreCase, "chatbot")
                    },
                    new BotRule[] {
                        // chatbot just knows positive feelings...
                        new RandomAnswersBotRule("getfeeling2", 40, new Regex("how (are you|do you feel)", RegexOptions.IgnoreCase), new string[] {"i feel super", "i feel perfect", "i feel happy"}),
                    }
                ),
                // repet the last known sentence
                new ReplacementBotRule("repeatLast", 41, new Regex("(please )?repeat the last sentence", RegexOptions.IgnoreCase), new string[] { "i repeat your last sentence:$s$sentence$", "$s$BotName$ repeats your last sentence:$s$sentence$"}),
                // repet a sentence
                new ReplacementBotRule("repeat", 40, new Regex("(please )?repeat(?<sentence> .*)", RegexOptions.IgnoreCase), new string[] { "i repeat your sentence:$r$sentence$", "$s$BotName$ repeats your sentence:$r$sentence$"}, new Dictionary<string,string>() { {"sentence", "$r$sentence$"} }),
                // reports your feelings
                new RandomAnswersBotRule("getfeeling", 40, new Regex("how (are you|do you feel)", RegexOptions.IgnoreCase), new string[] {"i feel great", "i feel tired", "i feel bored"}),

                // set the name of the bot
                new BotRule(
                    Name: "setbotname",
                    Weight: 10,
                    MessagePattern: new Regex("(your name is|you are) (now )?(.*)", RegexOptions.IgnoreCase),
                    Process: delegate(Match match, ChatSessionInterface session) {
                        session.SessionStorage.Values["BotName"] = match.Groups[3].Value;
                        return "My name is now " + session.SessionStorage.Values["BotName"];
                    }
                ),
                // show the bot's name
                new BotRule(
                    Name: "getbotname",
                    Weight: 10,
                    MessagePattern: new Regex("(who are you|(what is|say) your name)", RegexOptions.IgnoreCase),
                    Process: delegate(Match match, ChatSessionInterface session) {
                        if (!session.SessionStorage.Values.ContainsKey("BotName"))
                        {
                            return "I do not have a name";
                        }
                        if (match.Value.ToLower() == "who are you")
                        {
                            return "i am " + session.SessionStorage.Values["BotName"];
                        }
                        return "My name is " + session.SessionStorage.Values["BotName"];
                    }
                ),

                // set the name of the user
                new BotRule(
                    Name: "setusername",
                    Weight: 10,
                    MessagePattern: new Regex("my name is (now )?(.*)", RegexOptions.IgnoreCase),
                    Process: delegate(Match match, ChatSessionInterface session) {
                        session.SessionStorage.Values["UserName"] = match.Groups[2].Value;
                        return "Hi " + session.SessionStorage.Values["UserName"];
                    }
                ),
                // show the user's name
                new BotRule(
                    Name: "getusername",
                    Weight: 20,
                    MessagePattern: new Regex("(what is|say) my name", RegexOptions.IgnoreCase),
                    Process: delegate(Match match, ChatSessionInterface session) {
                        if (!session.SessionStorage.Values.ContainsKey("UserName"))
                        {
                            return "Sorry, but you have not told my your name";
                        }
                        return "Your name is " + session.SessionStorage.Values["UserName"];
                    }
                ),

                // greet
                new BotRule(
                    Name: "greet",
                    Weight: 2,
                    MessagePattern: new Regex("(hi|hello)", RegexOptions.IgnoreCase),
                    Process: delegate(Match match, ChatSessionInterface session) {
                        string answer = "Hi";

                        if (session.SessionStorage.Values.ContainsKey("UserName"))
                        {
                            answer += " " + session.SessionStorage.Values["UserName"];
                        }
                        answer += "!";
                        if (session.SessionStorage.Values.ContainsKey("BotName"))
                        {
                            answer += " I'm " + session.SessionStorage.Values["BotName"];
                        }
                        return answer;
                    }

                ),
                // greet
                new BotRule(
                    Name: "default",
                    Weight: 1,
                    MessagePattern: new Regex(".*", RegexOptions.IgnoreCase),
                    Process: delegate(Match match, ChatSessionInterface session) {
                        string answer = "well, i have to think about that";

                        if (session.SessionStorage.Values.ContainsKey("UserName"))
                        {
                            answer += ", " + session.SessionStorage.Values["UserName"];
                        }

                        return answer;
                    }

                )
            };
        }

        [HttpPost]
        public void StartBot()
        {
            List<BotRule> rules;
            string xmlfile;

            xmlfile = "C#";
            rules = createBotRulesFromCs();

            Console.WriteLine(Environment.NewLine + "Created ruleset from: " + xmlfile);

            // Visualize the rules
            (new ConsoleBotRuleVisualizer()).Visualize(rules);


            Console.WriteLine(Environment.NewLine + "Console Chat - Please use your keyboard and write something");
            (new ChatBot(rules)).talkWith(new ConsoleChatSession()); ;

        }
    }
}