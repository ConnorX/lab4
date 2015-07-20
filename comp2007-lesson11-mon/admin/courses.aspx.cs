using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using comp2007_lesson11_mon.Models;
using System.Linq.Dynamic;

namespace comp2007_lesson11_mon
{
    public partial class courses : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SortDirection"] = "ASC";
                Session["SortColumn"] = "CourseID";
                GetCourses();
            }
        }

        protected void GetCourses()
        {
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    var courses = (from c in db.Courses
                                   select new { c.CourseID, c.Title, c.Credits, c.Department.Name });

                    //append the current direction to the Sort Column
                    String Sort = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                    grdCourses.DataSource = courses.AsQueryable().OrderBy(Sort).ToList();
                    grdCourses.DataBind();
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                Server.Transfer("/error.aspx", true);
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Server.Transfer("/error.aspx", true);
            }
        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 CourseID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["CourseID"].ToString());
            try
            {
                using (DefaultConnectionEF db = new DefaultConnectionEF())
                {
                    Course objC = (from c in db.Courses
                                   where c.CourseID == CourseID
                                   select c).FirstOrDefault();

                    db.Courses.Remove(objC);
                    db.SaveChanges();
                }
            }
            catch (System.Data.SqlClient.SqlException e1)
            {
                Server.Transfer("/error.aspx", true);
            }
            catch (System.Data.Entity.Core.EntityException e1)
            {
                Server.Transfer("/error.aspx", true);
            }

            GetCourses();
        }

        protected void grdCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the page index and refresh the grid
            grdCourses.PageIndex = e.NewPageIndex;
            GetCourses();

        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the page size and refresh the grid
            grdCourses.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetCourses();
        }

        protected void grdCourses_Sorting(object sender, GridViewSortEventArgs e)
        {
            //set the global sort column to column clicked on by the user
            Session["SortColumn"] = e.SortExpression;
            GetCourses();

            //toggle the direction
            if (Session["SortDirection"].ToString() == "ASC")
            {
                Session["SortDirection"] = "DESC";
            }
            else
            {
                Session["SortDirection"] = "ASC";
            }
        }

        protected void grdCourses_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdCourses.Columns.Count - 1; i++)
                    {
                        if (grdCourses.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "DESC")
                            {
                                SortImage.ImageUrl = "/images/desc.jpg";
                                SortImage.AlternateText = "Sort Descending";
                            }
                            else
                            {
                                SortImage.ImageUrl = "/images/asc.jpg";
                                SortImage.AlternateText = "Sort Ascending";
                            }

                            e.Row.Cells[i].Controls.Add(SortImage);

                        }
                    }
                }

            }
        }
    }
}