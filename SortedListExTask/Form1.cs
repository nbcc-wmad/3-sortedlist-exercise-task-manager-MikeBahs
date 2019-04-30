using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
*Author: Mike
* Date: 30/04/2019
* Name: 3-sortedlist-exercise-task-manager
*/
namespace SortedListExTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SortedList<string, string> toDoTasks = new SortedList<string, string>();

        /// <summary>
        /// Validation we use it to catch possible  errors like empty Value entrance or repetative key(date value)
        /// </summary>
        /// <returns></returns>
        private bool Validation()
        {
            if(txtTask.Text == "")
            {
                MessageBox.Show("You must enter a taks", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (toDoTasks.ContainsKey(dtpTaskDate.Value.ToShortDateString()))
            {
                MessageBox.Show("Only one task per date is allowed", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// This event handler clear all items from the list control first , 
        /// then add one more item to sorted list 
        /// and then populate all item from it to the list again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                lstTasks.Items.Clear();
                string taskTime = dtpTaskDate.Value.ToShortDateString();
                string taskDescription = txtTask.Text;
                toDoTasks.Add(taskTime, taskDescription);

                FillListWithData();
                txtTask.Text = "";
            }
        }

        /// <summary>
        /// This method adds an item (key value) to the listbox from toDoList
        /// </summary>
        private void FillListWithData()
        {
            foreach (var time in toDoTasks)
            {
                lstTasks.Items.Add(time.Key);
            }
        }

        /// <summary>
        /// When we select (click on an item) in a listbox of dates it will populate related
        /// description in a label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            var t = toDoTasks.Where(time => time.Key == (string)lstTasks.SelectedItem).FirstOrDefault();

            lblTaskDetails.Text = t.Value;
        }

        /// <summary>
        /// Here we are removing selected Key from the List 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveTask_Click(object sender, EventArgs e)
        {
            toDoTasks.Remove((string)lstTasks.SelectedItem);
            lstTasks.Items.Clear();
            lblTaskDetails.Text = "";
            FillListWithData();
        }

        /// <summary>
        /// This event handler print all data Key -> Value pairs that are currently in the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            string showAllItems = "";
            foreach (var t in toDoTasks)
            {
                showAllItems += $"{t.Key + " " + t.Value}\n";
            }

            MessageBox.Show(showAllItems);
        }
    }
}
