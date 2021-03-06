﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project_OLP_Rest.Domain
{
    public class Course : Entity
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
     
        //M to M
        public List<GroupCourse> GroupCourses { get; set; }
        public List<TeacherCourse> TeacherCourses { get; set; }

        //O to M
        public List<Module> Modules { get; set; }

        //One to one
        public int ChatBotId { get; set; }
        public ChatBot ChatBot { get; set; }
    }
}
