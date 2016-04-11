<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>注册结果</title>
</head>
<body>
    <div>
        <%= ViewData["result"] %>
    </div>
    <div>
        <% 
            if (ViewBag.Success)
            { 
        %>
        <%= Html.ActionLink("返回主页", "Index") %>
        <%  
            }
            else
            { 
        %>
        <%= Html.ActionLink("重新注册", "Register") %>
        <%= Html.ActionLink("返回主页", "Index") %>
        <%  
            }
        %>
    </div>
</body>
</html>
