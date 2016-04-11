<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>登录结果</title>
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
        <%= Html.ActionLink("查看信息", "Index", "Information") %>
        <%  
            }
            else
            { 
        %>
        <%= Html.ActionLink("重新登录", "Login") %>
        <%= Html.ActionLink("返回主页", "Index") %>
        <%  
            }
        %>
    </div>
</body>
</html>
