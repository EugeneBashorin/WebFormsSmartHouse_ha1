<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsSmartHouse.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Smart House</title>
    <link runat="server" href="Style.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="dropDownDeviceList" runat="server">
                <asp:ListItem>TV</asp:ListItem>
                <asp:ListItem>Radio</asp:ListItem>
                <asp:ListItem>Heater</asp:ListItem>
                <asp:ListItem>AirCond</asp:ListItem>
                <asp:ListItem>Illuminator</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="addDeviceButton" runat="server" Text="Add" />
            <br />
            <asp:Panel ID="devicePanel" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
