﻿using System;

namespace ToDoList.Interfaces
{
    internal interface ITask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public bool HasReminder { get; set; }
        public DateTime EndDate { get; set; }
        public string GetColor { get; }
    }
}
