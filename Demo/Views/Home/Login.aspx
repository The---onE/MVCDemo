<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>登录</title>
</head>
<body>
    <div>
        <form method="post">
            <table>
                <tr>
                    <td>
                        用户名：
                    </td>
                    <td>
                        <input id="username" type="text" name="username" />
                    </td>
                </tr>
                <tr>
                    <td>
                        密码：
                    </td>
                    <td>
                        <input id="password" type="password" name="password" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input id="register" type="submit" value="登录" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
