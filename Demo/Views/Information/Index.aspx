<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Demo.Models.Information>>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>数据列表</title>
</head>
<body>
    <p>
        <%: Html.ActionLink("添加新数据", "Create") %>
    </p>
    <table>
        <tr>
            <th>
                创建者
            </th>
            <th>
                数据
            </th>
            <th>
                创建时间
            </th>
            <th>
                更新时间
            </th>
            <th>操作</th>
        </tr>
    
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%: Html.DisplayFor(modelItem => item.User.username) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.data) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.createdTime) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.updatedTime) %>
            </td>
            <td>
                <%: Html.ActionLink("编辑", "Edit", new { id=item.id }) %> |
                <%: Html.ActionLink("详细", "Details", new { id=item.id }) %> |
                <%: Html.ActionLink("删除", "Delete", new { id=item.id }) %>
            </td>
        </tr>
    <% } %>
    
    </table>
</body>
</html>
