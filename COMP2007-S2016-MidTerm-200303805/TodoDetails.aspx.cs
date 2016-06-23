using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using COMP2007_S2016_MidTerm_200303805.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/**
 * @author: Brighto Paul(2003003805),
 * @date: June 23, 2016
 * Desc: It will update the Todolist or add a new todo list
 * version:1.1
 */

namespace COMP2007_S2016_MidTerm_200303805
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                // Get the TodoList data
                this.GetTodoList();
            }
        }
        /**
         * <summary>
         * This method gets the Todo list data from the DB
         * </summary>
         * 
         * @method GetTodoList
         * @returns {void}
         */
        protected void GetTodoList()
        {
            // populate the form with existing TodoList data from the db
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to the EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // populate a TodoList instance with the TodoID from the URL parameter
                Todo updatedTodoList = (from todoList in db.Todos
                                          where todoList.TodoID == TodoID
                                          select todoList).FirstOrDefault();

                // map the TodoList properties to the form controls
                if (updatedTodoList != null)
                {
                    TodoNameTextBox.Text = updatedTodoList.TodoName;
                    TodoNotesTextBox.Text = updatedTodoList.TodoNotes;
                    CompletedCheckBox.Checked = updatedTodoList.Completed.Value;
                }
            }
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {

            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // use the Todolist model to create a new Todo List object and
                // save a new record
                Todo newTodoList= new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0)
                {
                    // get the id from url
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current list from EF DB
                    newTodoList = (from todoList in db.Todos
                                  where todoList.TodoID == TodoID
                                  select todoList).FirstOrDefault();
                }

                // add form data to the new Todolist record
                newTodoList.TodoName = TodoNameTextBox.Text;
                newTodoList.TodoNotes = TodoNotesTextBox.Text;
                newTodoList.Completed = CompletedCheckBox.Checked;

                // use LINQ to ADO.NET to add / insert new Todo list into the database
                // check to see if a new Todolist is being added
                if (TodoID == 0)
                {
                    db.Todos.Add(newTodoList);
                }

                // save our changes
                db.SaveChanges();

                // Redirect back to the updated TodoList page
                Response.Redirect("~/TodoList.aspx");
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //Redirect  back to Todo List page
            Response.Redirect("~/TodoList.aspx");
        }
    }
}