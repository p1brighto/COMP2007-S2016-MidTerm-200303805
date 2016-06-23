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
 * Desc: It will show the Todo lists
 * version:1.1
 */

namespace COMP2007_S2016_MidTerm_200303805
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if  loading page for the first time populate the Todo grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID";
                Session["SortDirection"] = "ASC";
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
            string sortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                // query the Todo Table using EF and LINQ
                var TodoLists = (from alllist in db.Todos
                                select alllist);

                // bind the result to the GridView
                TodoGridView.DataSource = TodoLists.AsQueryable().OrderBy(sortString).ToList();
                TodoGridView.DataBind();
                CheckBox cb = new CheckBox();
            }
        }
        protected void PageSizeDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set new page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDownList.SelectedValue);

            //refresh the grid
            this.GetTodoList();
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected TodoID using the Grid's DataKey Collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected Todo list from DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                Todo deletedList = (from todoRecords in db.Todos
                                          where todoRecords.TodoID == TodoID
                                          select todoRecords).FirstOrDefault();

                // perform the removal in the DB
                db.Todos.Remove(deletedList);
                db.SaveChanges();

                // refresh the grid
                this.GetTodoList();
            }
        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page number
            TodoGridView.PageIndex = e.NewPageIndex;

            //refresh the grid
            this.GetTodoList();
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the coloumb to sort by
            Session["SortColumn"] = e.SortExpression;

            //refresh the grid
            this.GetTodoList();

            //toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)//check to see if the click is on the header row
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int i = 0; i < TodoGridView.Columns.Count; i++)
                    {
                        if (TodoGridView.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = "<i class='fa fa-caret-down fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = "<i class='fa fa-caret-up fa-lg'></i>";
                            }
                            e.Row.Cells[i].Controls.Add(linkbutton);
                        }
                    }
                    /*if(e.Row.RowType==DataControlRowType.DataRow)
                        ((CheckBox)e.Row.FindControl("Completedcheckbox")).Checked = ;*/
                }
            }
        }
    }
}