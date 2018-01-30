<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="kyfly.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="textcolor.js" type="text/javascript"></script>
        <script type="text/javascript" src="js1/easyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="js1/easyUI/jquery.easyui.min.js"></script>
<style type="text/css">
body
{
font-size:12px;
}
div,table,td,body,ul,li,tbody,p,span{margin:0; padding:0;}

.header {
	background-image: url(images/1_100517083751_10_01.jpg);
	height: 238px;
}
.footer {
	background-image: url(images/1_100517083751_10_07.jpg);
	height: 203px;
}
.container {
	width: 100%;
	height: 209px;
	
}
.conment {
	background-image:url(images/2_01.jpg);
	background-repeat:repeat-x;
	height: 209px;
	width: 100%;
}
.container .conment .loginin li {
     height:42px;
	list-style:none;
}
.font1{
	font-size:12px;
	font-family:"微软雅黑", "黑体", "仿宋";
	color: #717171;
	line-height: 1.5em;
 }
.font2 {font-size:14px;
font-family:"微软雅黑", "黑体", "仿宋";
color:#000000;}
.lineheight {
	height: 20px;
	width: 150px;
	vertical-align: middle;
}
</style>

<script type="text/javascript">
    $(document).ready(function () {

        //$.post('ashx/GetButton.ashx?goout=1', function (msg) {
            
        //});
    });
    function myFocusName(obj, color) {
        obj.style.backgroundColor = color;
        $('#<%=lblName.ClientID %>').val('');
    }
    function myblurName(obj, color) {
        obj.style.background = color;
        if($('#txtName').val()==''){
            $('#<%=lblName.ClientID %>').val('请输入账号');
        }
    }
    function myFocusPass(obj, color) {
        obj.style.backgroundColor = color;
        $('#<%=lblPass.ClientID %>').val('');
    }
    function myblurPass(obj, color) {
        obj.style.background = color;
        if($('#txtPass').val()==''){
            $('#<%=lblPass.ClientID %>').val('请输入密码');
        }
    }
    function CheckForm() {
        var name = $('#txtName').val();
        var pass = $('#txtPass').val();
        if (name == '') {
            $('#<%=lblName.ClientID %>').val('请输入账号');
            return false;
        }
        if (pass == '') {
            $('#<%=lblPass.ClientID %>').val('请输入密码');
            return false;
        }
        return true;
    }

    function myloginclick() {
        if (!CheckForm())
            return;
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
<div class="container">
<!--主体容器-->
<div class="header">
</div>
<!--头部结束-->
<div class="conment">
<table align="center" style="margin:0 auto;" cellpadding="0" cellspacing="0" border="0">
  <tbody>
  <tr>
     <td><img src="images/1_100517083751__02.jpg" /></td>
     <td style=" width:300px">
         <div class="loginin">
            <ul>
                <li><img src="images/1_01.jpg" /></li>
                <li><span class="font2">管理账号：</span>
                    <asp:TextBox ID="txtName" CssClass="lineheight" runat="server" onfocus="myFocusName(this,'white')" onblur="myblurName(this,'#f4eaf1')"></asp:TextBox>
                    <asp:Label ID="lblName" runat="server" Text="" ForeColor="Red"></asp:Label></li>
                <li><span class="font2">密码登陆：</span>
                    <asp:TextBox ID="txtPass" CssClass="lineheight" runat="server" 
                        onfocus="myFocusPass(this,'white')" onblur="myblurPass(this,'#f4eaf1')" 
                        TextMode="Password"></asp:TextBox>
                    <asp:Label ID="lblPass" runat="server" Text="" ForeColor="Red"></asp:Label></li>
                <li style=" text-align:right">
                </li>
               <a href="#" target="_blank" style="font-size: large; font-weight: bold"> 建议及意见</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Button ID="btLogin" style="width:60px; height:30px" runat="server" Text="登录" 
                        onclick="btLogin_Click" OnClientClick="return CheckForm();" />
            </ul>
         </div>
     </td>
     <td><img src="images/1_100517083751_10_05.jpg" /></td>
  </tr>
  </tbody>
</table>
</div>


<!--主内容结束-->

<div  align="center" >
    <a href="http://10.22.149.119/webphp/chrome.exe" target="_blank"  style="font-size: large; font-weight: bold">客户端建议浏览器：谷歌浏览器（点击下载）</a>
    <br />
    <br />
</div>

<div  align="center" >
    

</div>
<div class="footer">
     

</div>
<!--footer结束-->

</div>
    </form>
</body>
</html>
