<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bulk360Test.aspx.cs" Inherits="csa.Admin.Bulk360Test" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            From <input type="text" runat="server" id="txtFrom"/> <br />
            To <input type="tel" runat="server" id="txtTo"/><br />
            Message <textarea runat="server" id="txtMessage"></textarea><br />
            <button runat="server" id="btnSend" type="button" onserverclick="btnSend_ServerClick">Send</button><br />
            <asp:Literal runat="server" ID="txtResult" />
        </div>
    </form>
</body>
</html>
