<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Demo.Models.Information>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>编辑</title>
</head>
<body>
    <script src="<%: Url.Content("~/Scripts/jquery-1.7.1.min.js") %>"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>"></script>
    
    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
    
        <fieldset>
            <legend>信息</legend>
    
            <%: Html.HiddenFor(model => model.id) %>
    
            <div class="editor-label">
                数据
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.data) %>
                <%: Html.ValidationMessageFor(model => model.data) %>
            </div>
    
            <p>
                <input type="submit" value="保存" />
            </p>
        </fieldset>
    <% } %>
    
    <div>
        <%: Html.ActionLink("回到列表", "Index") %>
    </div>
</body>
</html>
