﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

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
</body>
</html>