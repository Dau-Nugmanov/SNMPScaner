<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="WebApplication.Reports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <RSWEB:ReportViewer ID="ReportViewer1" AsyncRendering="false" runat="server" ProcessingMode="Remote" ShowParameterPrompts="false" Width="100%">
        </RSWEB:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <asp:Button ID="btn_export" runat="server" OnClick="btn_export_Click" Text="Экспортировать в" />
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>PDF</asp:ListItem>
                <asp:ListItem>Excel</asp:ListItem>
                <asp:ListItem>Word</asp:ListItem>
            </asp:DropDownList>
        </div>
        <hr />
<%--        <div>
            <p>Отправить пользователям в формате</p>
            <asp:DropDownList ID="drpSubmit" runat="server">
                <asp:ListItem>PDF</asp:ListItem>
                <asp:ListItem>Excel</asp:ListItem>
                <asp:ListItem>Word</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>

            <asp:CheckBoxList ID="chklstUsers" runat="server" DataValueField="email" DataTextField="name"></asp:CheckBoxList>
        </div>
        <div>
            <asp:Button ID="btnSubmitForUsers" runat="server" Text="Отправить" OnClick="btnSubmitForUsers_Click"/>
        </div>--%>
    </form>
</body>
</html>
