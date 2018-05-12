<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" EnableEventValidation="false"
    Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<!-- begin::Head -->
<head>
    <meta charset="utf-8" />
    <title>Exam Proctor and Class Assignment System </title>
    <meta name="description" content="Latest updates and statistic charts">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!--begin::Web font -->
    <script src="fonts/webfont.js" type="text/javascript"></script>
    <script type="text/javascript">
        WebFont.load({
            google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>
    <!--end::Web font -->
    <!--begin::Base Styles -->
    <link href="assets/vendors/base/vendors.bundle.css" rel="stylesheet" type="text/css" />
    <link href="default/base/style.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Base Styles -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var txtCode = document.getElementById("<%=txtCode.ClientID %>");
            txtCode.style.visibility = "hidden";

        });
        var ddl;
        var txtCode;
        var btnSendMessage;
        var loginCodeCounter = 0;
        var myTimer;
        var emailcontrol = 0;
        function openModal() {
            $("#btnShowPopup").click();
            ddl = document.getElementById("<%=ddlContactInformation.ClientID %>");
            btnSendMessage = document.getElementById("<%=btnSendMessage.ClientID %>");
            txtCode = document.getElementById("<%=txtCode.ClientID %>");
            PageMethods.getPhoneNumber(OnSuccess);
        }

        function OnSuccess(response) {

            ddl.options.length = 0;
            for (var i in response) {
                AddOption(response[i].Text, response[i].value);
            }
        }
        function AddOption(text, value) {
            var option = document.createElement('option');
            option.value = value;
            option.innerHTML = text;
            ddl.options.add(option);
        }

        function sendCode(senderType) {

            if (emailcontrol == 1) {
                senderType = 2;
            }
            var activationCode = "";
            if (txtCode.style.visibility == "visible") {
                senderType = 3;
                activationCode = txtCode.value;
            }
            var selectedText;
            ddl = document.getElementById("<%=ddlContactInformation.ClientID %>");
            selectedText = ddl.options[ddl.selectedIndex].text;
            var ipAddress = '<%= Request.UserHostAddress%>'.toString();

            var TCNumber = document.getElementById("<%=txtUserTC.ClientID %>").value;

            var obj = {};
            obj.TCNumber = TCNumber;
            obj.selectedText = selectedText;
            obj.senderType = senderType;
            obj.ipAddress = ipAddress;
            obj.activationCode_ = activationCode;
            $.ajax({
                type: "POST",
                url: "Login.aspx/sendCode",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    result = response.d;
                    if (result == "1" || result == "-1") {

                        if (result == "1") {
                            loginCodeCounter = loginCodeCounter + 1;
                            ddl.style.visibility = "hidden";
                            txtCode.style.visibility = "visible";
                            btnSendMessage.value = "Check My Code";
                            clearInterval(myTimer);
                            DelayRedirect();

                        }
                    } else if (result == "100" || result == "-100") {
                        clearInterval(myTimer);
                        if (result == "100") {
                            window.location = "Homepage.aspx";
                        } else {
                            dvCountDown.style.display = "none";
                            ddl.style.visibility = "visible";
                            txtCode.style.visibility = "hidden";
                            btnSendMessage.value = "Send Message";

                            if (loginCodeCounter == 3) {
                                PageMethods.getEmailAddr(OnSuccess);
                                emailcontrol = 1;
                            } else if (loginCodeCounter == 6) {
                                alert("Bloke");
                                window.location = "Login.aspx";
                            }

                        }
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function DelayRedirect() {
            var seconds = 180;
            var dvCountDown = document.getElementById("dvCountDown");
            var lblCount = document.getElementById("lblCount");
            dvCountDown.style.display = "block";
            lblCount.innerHTML = seconds;

            myTimer = setInterval(function () {
                seconds--;
                if (seconds > -1) {
                    lblCount.innerHTML = seconds;
                }
                if (seconds == 0) {
                    dvCountDown.style.display = "none";
                    ddl.style.visibility = "visible";
                    txtCode.style.visibility = "hidden";
                    btnSendMessage.value = "Send Message";
                    if (loginCodeCounter == 3) {
                        PageMethods.getEmailAddr(OnSuccess);
                    } else if (loginCodeCounter == 6) {
                        alert("Bloke");
                        window.location = "Login.aspx";
                    }

                }
            }, 1000);
        }


        function SignUp() {

            var userTC = $("#txt_SingupUserTC").val();
            var userStudentNo = $("#txt_SingupUserStudentNo").val();

            var obj = {};
            obj.userTC = userTC;
            obj.userStudentNo = userStudentNo;
            $.ajax({
                type: "POST",
                url: "Login.aspx/signUp",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = response.d;
                    alert(result);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


        function ForgottenPassword() {

            var email = $("#txtEmail").val();
            var obj = {};
            obj.email = email;
            $.ajax({
                type: "POST",
                url: "Login.aspx/forgottenPassword",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = response.d;
                    alert(result);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

    </script>
</head>
<!-- end::Head -->
<!-- end::Body -->
<body>
    <!-- begin:: Page -->
    <div class="m-grid m-grid--hor m-grid--root m-page">
        <div class="m-login m-login--signin  m-login--5" id="m_login" style="background-image: url(assets/app/media/img/bg/bg-3.jpg);">
            <div class="m-login__wrapper-2 m-portlet-full-height">
                <div class="m-login__contanier">
                    <form id="Form1" runat="server">
                    <div class="m-login__signin">
                        <div class="m-login__head">
                            <h3 class="m-login__title">
                                Login To Your Account
                            </h3>
                        </div>
                        <form class="m-login__form m-form" action="">
                        <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"
                            EnablePageMethods="true">
                        </asp:ScriptManager>
                        <div class="form-group m-form__group">
                            <asp:TextBox ID="txtUserTC" CssClass="form-control m-input" placeholder="Username"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="form-group m-form__group">
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control m-input m-login__form-input--last"
                                placeholder="Password" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="row m-login__form-sub">
                            <div class="col m--align-left">
                                <label class="m-checkbox m-checkbox--focus">
                                    <input type="checkbox" name="remember">
                                    Remember me <span></span>
                                </label>
                            </div>
                            <div class="col m--align-right">
                                <a href="javascript:;" id="m_login_forget_password" class="m-link">Forget Password ?
                                </a>
                            </div>
                        </div>
                        <div class="m-login__form-action">
                            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                <ContentTemplate>
                                    <input type="button" name="btnSignIn" id="btn_Login" value="Sign In" class="btn btn-focus m-btn m-btn--pill m-btn--custom m-btn--air"
                                        runat="server" onserverclick="btnSignIn_Click" />
                                    <button type="button" id="m_login_signup" class="btn btn-outline-focus m-btn--pill">
                                        Sign Up
                                    </button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <input type="button" id="btnShowPopup" value="Sign In" class="btn btn-focus m-btn m-btn--pill m-btn--custom m-btn--air"
                            runat="server" data-toggle="modal" data-target="#smscode" style="visibility: hidden" />
                        <div class="modal fade" id="smscode" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
                            aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLongTitle">
                                            Please choose your contact information
                                        </h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times; </span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:DropDownList ID="ddlContactInformation" runat="server" Width="200px">
                                        </asp:DropDownList>
                                        <div id="dvCountDown" style="display: none">
                                            Remaining <span id="lblCount"></span>&nbsp;seconds.
                                        </div>
                                        <asp:TextBox ID="txtCode" CssClass="form-control m-input" placeholder="Code" runat="server"
                                            autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                            Close
                                        </button>
                                        <input type="button" name="btnSendMessage" id="btnSendMessage" value="Send Message"
                                            class="btn btn-primary" runat="server" onclick="sendCode(1); return false;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        </form>
                    </div>
                    <div class="m-login__signup">
                        <div class="m-login__head">
                            <h3 class="m-login__title">
                                Sign Up
                            </h3>
                            <div class="m-login__desc">
                                Enter your details to create your account:
                            </div>
                        </div>
                        <form class="m-login__form m-form" action="">
                        <div class="form-group m-form__group">
                            <asp:TextBox ID="txt_SingupUserTC" placeholder="TCNO" CssClass="form-control m-input"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="m-login__form-sub">
                            <label class="m-checkbox m-checkbox--focus">
                                <input type="checkbox" name="agree">
                                I Agree the <a href="#" class="m-link m-link--focus">terms and conditions </a>.
                                <span></span>
                            </label>
                            <span class="m-form__help"></span>
                        </div>
                        <div class="m-login__form-action">
                            <input type="button" name="btnSignUp" id="btnSignUp" value="Sign Up" class="btn btn-focus m-btn m-btn--pill m-btn--custom m-btn--air"
                                runat="server" onclick="SignUp(); return false;" />
                            <button id="m_login_signup_cancel" class="btn btn-outline-focus m-btn m-btn--pill m-btn--custom">
                                Cancel
                            </button>
                        </div>
                        </form>
                    </div>
                    <div class="m-login__forget-password">
                        <div class="m-login__head">
                            <h3 class="m-login__title">
                                Forgotten Password ?
                            </h3>
                            <div class="m-login__desc">
                                Enter your email to reset your password:
                            </div>
                        </div>
                        <form class="m-login__form m-form" action="">
                        <div class="form-group m-form__group">
                            <asp:TextBox ID="txtEmail" placeholder="Email" CssClass="form-control m-input" runat="server"
                                autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="m-login__form-action">
                            <input type="button" name="btnRequest" id="btnRequest" value="Request" class="btn btn-focus m-btn m-btn--pill m-btn--custom m-btn--air"
                                runat="server" onclick="ForgottenPassword(); return false;" />
                            <button id="m_login_forget_password_cancel" class="btn btn-outline-focus m-btn m-btn--pill m-btn--custom ">
                                Cancel
                            </button>
                        </div>
                        </form>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- end:: Page -->
    <!--begin::Base Scripts -->
    <script src="assets/vendors/base/vendors.bundle.js" type="text/javascript"></script>
    <script src="default/base/scripts.bundle.js" type="text/javascript"></script>
    <!--end::Base Scripts -->
    <!--begin::Page Snippets -->
    <script src="assets/snippets/pages/user/login.js" type="text/javascript"></script>
    <!--end::Page Snippets -->
</body>
<!-- end::Body -->
</html>
