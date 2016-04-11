<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Demo.Models.Information>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>删除</title>
</head>
<body>
    <h3>确定要删除该数据吗?</h3>
    <fieldset>
        <legend>信息</legend>
    
        <div class="display-label">
            创建者
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.User.username) %>
        </div>
    
        <div class="display-label">
            数据
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.data) %>
        </div>
    
        <div class="display-label">
            创建时间
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.createdTime) %>
        </div>
    
        <div class="display-label">
            更新时间
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.updatedTime) %>
        </div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="删除" /> |
            <%: Html.ActionLink("回到列表", "Index") %>
        </p>
    <% } %>
    
</body>
</html>
