<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div>
        <%: Html.ActionLink("登录", "Login") %>
        <%: Html.ActionLink("注册", "Register") %>
    </div>
</body>
</html>
