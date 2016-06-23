<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200303805.TodoList" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Todo List</h1>
                <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Todo</a>
                <label for="PageSizeDownList">records per page:</label>
                <asp:DropDownList ID="PageSizeDownList" runat="server" DataValueField="2" AutoPostBack="true"
                    CssClass="btn btn-default btn-sm dropdown-toogle" OnSelectedIndexChanged="PageSizeDownList_SelectedIndexChanged">
                    <asp:ListItem Text="1" Value="1" />
                    <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="4" Value="4" />
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="6" Value="6" />
                    <asp:ListItem Text="All" Value="1000" />
                </asp:DropDownList>

                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover"  ID="TodoGridView" AutoGenerateColumns="false" DataKeyNames="TodoID" 
                    OnRowDeleting="TodoGridView_RowDeleting" PageSize="2" AllowPaging="true" OnPageIndexChanging="TodoGridView_PageIndexChanging" 
                    AllowSorting="true" OnSorting="TodoGridView_Sorting" OnRowDataBound="TodoGridView_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="TodoID" HeaderText="Todo ID" Visible="true" SortExpression="TodoID" />
                        <asp:BoundField DataField="TodoName" HeaderText="Todo Name" Visible="true" SortExpression="TodoName" />
                        <asp:BoundField DataField="TodoNotes" HeaderText="TodoNotes" Visible="true" SortExpression="TodoNotes" />
                        <asp:CheckBoxField DataField="Completed" HeaderText="Completed" Visible="true" />
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" NavigateUrl="~/TodoDetails.aspx.cs"
                            DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}" 
                            ControlStyle-CssClass="btn btn-primary btn-sm"/>
                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i>Delete" ShowDeleteButton="true" 
                            ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
