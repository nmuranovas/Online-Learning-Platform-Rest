﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ChatBot.Rest;
using ChatBot.Rest.ChatSessions;
using ChatBot.Rest.RuleSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QXS.ChatBot;
using QXS.ChatBot.ChatSessions;

namespace Project_OLP_Rest.Test.Tests
{
    [TestClass]
    public class JavaCourseRegexTest
    {
        private RestChatBot javaChatBot;
        JavaCourseRuleSet javaCourseRules = new JavaCourseRuleSet();

        [TestMethod]
        public void CreateJavaBot() => javaChatBot = new RestChatBot(javaCourseRules.Rules);


        [TestMethod]
        public void JavaCourseAskTest()
        {
            CreateJavaBot();
            string Message = "What course name";

            ChatSessionInterface session = new RestChatSession();
            Assert.AreEqual(javaChatBot.FindAnswer(session, Message).Item1, "I do not know course name");
        }


        [TestMethod]
        public void JavaCourseAskTestSucces()
        {
            CreateJavaBot();
            string Message = "Course name is Java Course";

            ChatSessionInterface session = new RestChatSession();
            Assert.AreEqual(javaChatBot.FindAnswer(session, Message).Item1, "Course name now is Java Course");

            Console.WriteLine(javaChatBot.FindAnswer(session, Message).Item1);

        }
        [TestMethod]
        public void JavaGiveTask()
        {
            CreateJavaBot();
            string Message = "give me task";
            ChatSessionInterface session = new RestChatSession();
            Console.WriteLine(javaChatBot.FindAnswer(session, Message).Item1);


        }
        [TestMethod]
        public void JavaAllCourses()
        {
            CreateJavaBot();
            string Message = "show me tasks";
            ChatSessionInterface session = new RestChatSession();
            Console.WriteLine(javaChatBot.FindAnswer(session, Message).Item1);
        }
        [TestMethod]
        public void JavaLearning()
        {
            CreateJavaBot();
            string Message = "suggest me java learning sites";
            ChatSessionInterface session = new RestChatSession();
            Console.WriteLine(javaChatBot.FindAnswer(session, Message).Item1);
        }
        [TestMethod]
        public void JavaFact()
        {
            CreateJavaBot();
            string Message = "tell me interesting fact about java";
            ChatSessionInterface session = new RestChatSession();
            Console.WriteLine(javaChatBot.FindAnswer(session, Message).Item1);
        }
    }
}
