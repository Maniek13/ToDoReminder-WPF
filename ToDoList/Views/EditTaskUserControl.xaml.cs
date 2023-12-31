﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    public partial class EditTaskUserControl : UserControl
    {
        private readonly MainWindow mainWindow;
        private EditTaskUserControlViewModel ViewModel { get; }

        private readonly bool hasReminder;

#pragma warning disable CS8601, CS8618
        public EditTaskUserControl(Task task)
        {
            InitializeComponent();
            DataContext = ViewModel = new EditTaskUserControlViewModel();

            TaskValue = task;
            hasReminder = task.HasReminder;
            var reminder = ViewModel.GetReminderTask(task.Id);
            if (reminder != null)
                ReminderValue = (Reminder)reminder;



            EditForm.DataContext = this;
            mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        }
#pragma warning restore CS8601, CS8618

        #region Dependency property
        private static readonly DependencyProperty TaskValueProperty =
            DependencyProperty.Register(
                name: "TaskValue",
                propertyType: typeof(Task),
                ownerType: typeof(EditTaskUserControl),
                typeMetadata: new FrameworkPropertyMetadata(defaultValue: new Task()));
        private static readonly DependencyProperty ReminderProperty =
           DependencyProperty.Register(
               name: "ReminderValue",
               propertyType: typeof(Reminder),
               ownerType: typeof(EditTaskUserControl),
               typeMetadata: new FrameworkPropertyMetadata(defaultValue: new Reminder() { Date = DateTime.Now }));
        #endregion

        private Task TaskValue
        {
            get { return (Task)GetValue(TaskValueProperty); }
            set { SetValue(TaskValueProperty, value); }
        }
        private Reminder ReminderValue
        {
            get { return (Reminder)GetValue(ReminderProperty); }
            set { SetValue(ReminderProperty, value); }
        }

        private void EditTask_BtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow.ErrorMsg.Visibility = Visibility.Hidden;

                ViewModel.EditTask(TaskValue);
                ViewModel.EditOrCreateReminder(TaskValue, ReminderValue, hasReminder);

                mainWindow.MainContext.Content = new ListOfTasksUserControl();
            }
            catch (Exception ex)
            {
                mainWindow.ErrorMsg.Text = ex.Message;
                mainWindow.ErrorMsg.Visibility = Visibility.Visible;
            }
        }
        private void HasReminderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ReminderForm.Visibility = Visibility.Visible;
            ReminderForm.Height = 140;
        }
        private void HasReminderCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            ReminderForm.Visibility = Visibility.Hidden;
            ReminderForm.Height = 0;
        }
    }
}
