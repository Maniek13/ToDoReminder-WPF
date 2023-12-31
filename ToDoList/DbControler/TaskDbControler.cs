﻿using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Contexts;
using ToDoList.DbModels;

namespace ToDoList.DbControler
{
    public class TaskDbControler
    {
        readonly ToDoAppDbContext dbContext = new();
        public int AddTask(Task task)
        {
            try
            {
                dbContext.Tasks.Add(task);
                dbContext.SaveChanges();

                return task.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<Task> GetAllTasks()
        {
            try
            {
                return dbContext.Tasks.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public Task? GetTask(int id)
        {
            try
            {
                return dbContext.Tasks.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void EditTask(Task task)
        {
            try
            {
                dbContext.Tasks.Update(task);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void DeleteTask(int id)
        {
            try
            {
                var task = dbContext.Tasks.Find(id) ?? throw new Exception("Task was not existing");

                dbContext.Tasks.Remove(task);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
