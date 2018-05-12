<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotificationSettings.aspx.cs" EnableEventValidation="false"
    Inherits="settings_NotificationSettings" %>

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
    <script src="../fonts/webfont.js" type="text/javascript"></script>
    <script type="text/javascript">
        WebFont.load({
            google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700", "Asap+Condensed:500"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>
    <!--end::Web font -->
    <!--begin::Base Styles -->
    <!--begin::Page Vendors -->
    <link href="../assets/vendors/custom/fullcalendar/fullcalendar.bundle.css" rel="stylesheet"
        type="text/css" />
    <!--end::Page Vendors -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

    <link href="../assets/vendors/base/vendors.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assets/base/style.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Base Styles -->
    <link rel="shortcut icon" href="assets/demo8/demo/media/img/logo/favicon.ico" />
     <script type="text/jscript">
         var items = null;
         var rbtnTelephoneNumber = null;
         var rbtnEmail = null;
         var ddlTelephoneNumber = null;
         var ddlEmail = null;
         var chkRemind = null;
         var chkDefaultDate = null;
         var txtBeforeDays = null;
         var txtInformedTime = null;

         $(document).ready(function () {
             rbtnTelephoneNumber = document.getElementById("<%=rbtnTelephoneNumber.ClientID %>");
             rbtnEmail = document.getElementById("<%=rbtnEmail.ClientID %>");
             ddlTelephoneNumber = document.getElementById("<%=ddlTelephoneNumber.ClientID %>");
             ddlEmail = document.getElementById("<%=ddlEmail.ClientID %>");
             chkRemind = document.getElementById("<%=chkRemind.ClientID %>");
             chkDefaultDate = document.getElementById("<%=chkDefaultDate.ClientID %>");
             txtBeforeDays = document.getElementById("txtBeforeDays");
             txtInformedTime = document.getElementById("txtInformedTime");
             PageMethods.getNotificationSettings(OnSuccess);
             function OnSuccess(response) {
                 items = response;
                 var x = JSON.parse(items);
                 for (var i in x) {

                     var option = document.createElement('option');
                     option.value = x[i].userPhoneNumber;
                     option.innerHTML = x[i].userPhoneNumber;
                     ddlTelephoneNumber.options.add(option);

                     option = document.createElement('option');
                     option.value = x[i].userPhoneNumber2;
                     option.innerHTML = x[i].userPhoneNumber2;
                     ddlTelephoneNumber.options.add(option);


                     option = document.createElement('option');
                     option.value = x[i].userEmail;
                     option.innerHTML = x[i].userEmail;
                     ddlEmail.options.add(option);


                     option = document.createElement('option');
                     option.value = x[i].userEmail2;
                     option.innerHTML = x[i].userEmail2;
                     ddlEmail.options.add(option);

                     if (x[i].userApprovalPlatform == 1) {
                         rbtnTelephoneNumber.checked = true;
                         rbtnEmail.checked = false;

                         for (var z = 0; z < ddlTelephoneNumber.options.length; z++) {
                             if (ddlTelephoneNumber.options[z].text == x[i].platformValue) {
                                 ddlTelephoneNumber.options[z].selected = true;
                                 break;
                             }
                         }
                     } else if (x[i].userApprovalPlatform == 2) {
                         rbtnEmail.checked = true;
                         rbtnTelephoneNumber.checked = false;

                         for (var z = 0; z < ddlEmail.options.length; z++) {
                             if (ddlEmail.options[z].text == x[i].platformValue) {
                                 ddlEmail.options[z].selected = true;
                                 break;
                             }
                         }
                     }

                     if (x[i].isInformedCheck == 1) {
                         chkRemind.checked = true;
                     } else {
                         chkRemind.checked = false;
                     }

                     
                     if (x[i].defaultDayTime == 1) {
                         chkDefaultDate.checked = true;
                         
                     } else {
                         chkDefaultDate.checked = false;
                         txtBeforeDays.value = x[i].userApprovalDay;
                         txtInformedTime.value = x[i].userApprovalTime;
                     }
                 }


             }

         });

         function btnSaveClick() {
             var userApprovalPlatform = 0;
             var platformValue=null;
             var userApprovalTime=null;
             var userApprovalDay=0;
             var isInformedCheck=0;

             if (rbtnTelephoneNumber.checked) {
                 userApprovalPlatform = 1;
                 platformValue=ddlTelephoneNumber.options[ddlTelephoneNumber.selectedIndex].value;
             } else if (rbtnEmail.checked) {
                userApprovalPlatform = 2;
                platformValue=ddlEmail.options[ddlEmail.selectedIndex].value;
            }

           if (userApprovalPlatform == 0) {
                alert("Lütfen geri bildirim seçeneği seçiniz.");
                return;
            }

            if (chkDefaultDate.checked==false) {
                    userApprovalTime= txtInformedTime.value;
                    userApprovalDay= txtBeforeDays.value;
            }

            if (chkRemind.checked==true) {
                isInformedCheck=1;
            }
             var obj = {};
             obj.userApprovalPlatform = userApprovalPlatform;
             obj.platformValue = platformValue;
             obj.userApprovalTime = userApprovalTime;
             obj.userApprovalDay = userApprovalDay;
             obj.isInformedCheck = isInformedCheck;
             $.ajax({
                 type: "POST",
                 url: "NotificationSettings.aspx/btnSaveClick",
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
<body style="background-image: url(../assets/app/media/img/bg/bg-7.jpg)" class="m-page--fluid m-page--loading-enabled m-page--loading m-header--fixed m-header--fixed-mobile m-footer--push m-aside--offcanvas-default">
    <!-- begin::Page loader -->
    <div class="m-page-loader m-page-loader--base">
        <div class="m-blockui">
            <span>Please wait... </span><span>
                <div class="m-loader m-loader--brand">
                </div>
            </span>
        </div>
    </div>
    <!-- end::Page Loader -->
    <!-- begin:: Page -->
    <div class="m-grid m-grid--hor m-grid--root m-page">
        <!-- begin::Header -->
        <header class="m-grid__item		m-header " data-minimize="minimize" data-minimize-mobile="minimize"
            data-minimize-offset="10" data-minimize-mobile-offset="10">
				<div class="m-header__top">
					<div class="m-container m-container--fluid m-container--full-height m-page__container">
						<div class="m-stack m-stack--ver m-stack--desktop">
							<!-- begin::Brand -->
							<div class="m-stack__item m-brand m-stack__item--left">
								<div class="m-stack m-stack--ver m-stack--general m-stack--inline">
									<div class="m-stack__item m-stack__item--middle m-brand__logo">
										<a href="../Homepage.aspx" class="m-brand__logo-wrapper">
											<img alt="" src="../assets/app/media/img/logos/logo.png" class="m-brand__logo-default"/>
											<img alt="" src="../assets/app/media/img/logos/logo_inverse1.png" class="m-brand__logo-inverse"/>
										</a>
									</div>
									<div class="m-stack__item m-stack__item--middle m-brand__tools">
										<!-- begin::Responsive Header Menu Toggler-->
										<a id="m_aside_header_menu_mobile_toggle" href="javascript:;" class="m-brand__icon m-brand__toggler m--visible-tablet-and-mobile-inline-block">
											<span></span>
										</a>
										<!-- end::Responsive Header Menu Toggler-->
			                            <!-- begin::Topbar Toggler-->
										<a id="m_aside_header_topbar_mobile_toggle" href="javascript:;" class="m-brand__icon m--visible-tablet-and-mobile-inline-block">
											<i class="flaticon-more"></i>
										</a>
										<!--end::Topbar Toggler-->
									</div>
								</div>
							</div>
							<!-- end::Brand -->		
					        <!-- begin::Topbar -->
							<div class="m-stack__item m-stack__item--right m-header-head" id="m_header_nav">
								<div id="m_header_topbar" class="m-topbar  m-stack m-stack--ver m-stack--general">
									<div class="m-stack__item m-topbar__nav-wrapper">
										<ul class="m-topbar__nav m-nav m-nav--inline">
											
											<li class="m-nav__item m-topbar__user-profile  m-dropdown m-dropdown--medium m-dropdown--arrow  m-dropdown--align-right m-dropdown--mobile-full-width m-dropdown--skin-light" data-dropdown-toggle="click">
												<a href="#" class="m-nav__link m-dropdown__toggle">
													<span class="m-topbar__userpic">
														<img src="../assets/app/media/img/users/user4.jpg" class="m--img-rounded m--marginless m--img-centered" alt=""/>
													</span>
													<span class="m-nav__link-icon m-topbar__usericon  m--hide">
														<span class="m-nav__link-icon-wrapper">
															<i class="flaticon-user-ok"></i>
														</span>
													</span>
													<span class="m-topbar__username m--hide">
														Nick
													</span>
												</a>
												<div class="m-dropdown__wrapper">
													<span class="m-dropdown__arrow m-dropdown__arrow--right m-dropdown__arrow--adjust"></span>
													<div class="m-dropdown__inner">
														<div class="m-dropdown__header m--align-center">
															<div class="m-card-user m-card-user--skin-light">
																<div class="m-card-user__pic">
																	<img src="../assets/app/media/img/users/user4.jpg" class="m--img-rounded m--marginless" alt=""/>
																</div>
																<div class="m-card-user__details">
																	<span class="m-card-user__name m--font-weight-500">
																		
																	</span>
																	<a href="" class="m-card-user__email m--font-weight-300 m-link">
																	
																	</a>
																</div>
															</div>
														</div>
														<div class="m-dropdown__body">
															<div class="m-dropdown__content">
																<ul class="m-nav m-nav--skin-light">
																	<li class="m-nav__section m--hide">
																		<span class="m-nav__section-text">
																			Section
																		</span>
																	</li>
																	<li class="m-nav__item">
																		<a href="../MyProfile.aspx" class="m-nav__link">
																			<i class="m-nav__link-icon flaticon-profile-1"></i>
																			<span class="m-nav__link-title">
																				<span class="m-nav__link-wrap">
																					<span class="m-nav__link-text">
																						My Profile
																					</span>
																					<span class="m-nav__link-badge">
																					
																					</span>
																				</span>
																			</span>
																		</a>
																	</li>																	
																	<li class="m-nav__separator m-nav__separator--fit"></li>
																	<li class="m-nav__item">
																		<a href="../Login.aspx" class="btn m-btn--pill    btn-secondary m-btn m-btn--custom m-btn--label-brand m-btn--bolder">
																			Logout
																		</a>
																	</li>
																</ul>
															</div>
														</div>
													</div>
												</div>
											</li>
											
										</ul>
									</div>
								</div>
							</div>
							<!-- end::Topbar -->
						</div>
					</div>
				</div>
				<div class="m-header__bottom">
					<div class="m-container m-container--fluid m-container--full-height m-page__container">
						<div class="m-stack m-stack--ver m-stack--desktop">
							<!-- begin::Horizontal Menu -->
							<div class="m-stack__item m-stack__item--fluid m-header-menu-wrapper">
								<button class="m-aside-header-menu-mobile-close  m-aside-header-menu-mobile-close--skin-light " id="m_aside_header_menu_mobile_close_btn">
									<i class="la la-close"></i>
								</button>
								<div id="m_header_menu" class="m-header-menu m-aside-header-menu-mobile m-aside-header-menu-mobile--offcanvas  m-header-menu--skin-dark m-header-menu--submenu-skin-light m-aside-header-menu-mobile--skin-light m-aside-header-menu-mobile--submenu-skin-light "  >
									<ul class="m-menu__nav  m-menu__nav--submenu-arrow ">
										<li class="m-menu__item  m-menu__item--submenu m-menu__item--tabs"  data-menu-submenu-toggle="tab" aria-haspopup="true">
											<a  href="../Homepage.aspx" class="m-menu__link m-menu__toggle">
												<span class="m-menu__link-text">
													Home page 
												</span>
												<i class="m-menu__hor-arrow la la-angle-down"></i>
												<i class="m-menu__ver-arrow la la-angle-right"></i>
											</a>
											<div class="m-menu__submenu m-menu__submenu--classic m-menu__submenu--left m-menu__submenu--tabs">
												<span class="m-menu__arrow m-menu__arrow--adjust"></span>
												<ul class="m-menu__subnav">
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="../HomePage.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-graphic-2"></i>
															<span class="m-menu__link-text">
																Calendar
															</span>
														</a>
													</li>
												</ul>
											</div>
										</li>
										<li class="m-menu__item  m-menu__item--submenu m-menu__item--tabs"  data-menu-submenu-toggle="tab" aria-haspopup="true">
											<a  href="#" class="m-menu__link m-menu__toggle">
												<span class="m-menu__link-text">
													Exam Requests
												</span>
												<i class="m-menu__hor-arrow la la-angle-down"></i>
												<i class="m-menu__ver-arrow la la-angle-right"></i>
											</a>
											<div class="m-menu__submenu m-menu__submenu--classic m-menu__submenu--left m-menu__submenu--tabs">
												<span class="m-menu__arrow m-menu__arrow--adjust"></span>
												<ul class="m-menu__subnav">
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="../examRequest/MakeExamRequest.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-graphic-2"></i>
															<span class="m-menu__link-text">
																Make An Exam Request
															</span>
														</a>
													</li>
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="../examRequest/MyExamRequests.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-analytics"></i>
															<span class="m-menu__link-text">
																My Exam Requests
															</span>
														</a>
													</li>
													<li class="m-menu__item "  aria-haspopup="true">
														<a  href="../examRequest/OtherExamRequests.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-notes"></i>
															<span class="m-menu__link-text">
																Other Exam Requests
															</span>
														</a>
													</li>
												</ul>
											</div>
										</li>
                                        <li class="m-menu__item  m-menu__item--active  m-menu__item--active-tab  m-menu__item--submenu m-menu__item--tabs"  data-menu-submenu-toggle="tab" aria-haspopup="true">
										
											<a  href="#" class="m-menu__link m-menu__toggle">
												<span class="m-menu__link-text">
													Settings 
												</span>
												<i class="m-menu__hor-arrow la la-angle-down"></i>
												<i class="m-menu__ver-arrow la la-angle-right"></i>
											</a>
											<div class="m-menu__submenu m-menu__submenu--classic m-menu__submenu--left m-menu__submenu--tabs">
												<span class="m-menu__arrow m-menu__arrow--adjust"></span>
												<ul class="m-menu__subnav">
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="NotificationSettings.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-analytics"></i>
															<span class="m-menu__link-text">
																Notification Settings
															</span>
														</a>
													</li>
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="PasswordSettings.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-notes"></i>
															<span class="m-menu__link-text">
																Password Settings
															</span>
														</a>
													</li>												
												</ul>
											</div>
										</li>
										
                                        <li class="m-menu__item  m-menu__item--submenu m-menu__item--tabs"  data-menu-submenu-toggle="tab" aria-haspopup="true">
											<a  href="#" class="m-menu__link m-menu__toggle">
												<span class="m-menu__link-text">
													Admin Panel
												</span>
												<i class="m-menu__hor-arrow la la-angle-down"></i>
												<i class="m-menu__ver-arrow la la-angle-right"></i>
											</a>
											<div class="m-menu__submenu m-menu__submenu--classic m-menu__submenu--left m-menu__submenu--tabs">
												<span class="m-menu__arrow m-menu__arrow--adjust"></span>
												<ul class="m-menu__subnav">
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="../adminPanel/Faculty.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-analytics"></i>
															<span class="m-menu__link-text">
																Faculty
															</span>
														</a>
													</li>
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="../adminPanel/Department.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-notes"></i>
															<span class="m-menu__link-text">
																Department
															</span>
														</a>
													</li>
                                                    <li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="../adminPanel/Course.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-analytics"></i>
															<span class="m-menu__link-text">
																Course
															</span>
														</a>
													</li>
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="../adminPanel/Class.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-notes"></i>
															<span class="m-menu__link-text">
																Class
															</span>
														</a>
													</li>	
                                                    <li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="../adminPanel/ExamCalendarRequest.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-analytics"></i>
															<span class="m-menu__link-text">
																Exam Calendar Request
															</span>
														</a>
													</li>														
												</ul>
											</div>
										</li>
									</ul>
								</div>
							</div>
							<!-- end::Horizontal Menu -->
						</div>
					</div>
				</div>
			</header>
        <!-- end::Header -->
        <!-- begin::Body -->
        <div class="m-grid__item m-grid__item--fluid  m-grid m-grid--ver-desktop m-grid--desktop m-page__container m-body">
            <div class="m-grid__item m-grid__item--fluid m-wrapper">
                <div class="m-portlet__body">
                    <div class="container">
                        <div class="row">
                            <div class="col-sm-12">
                                <h1>
                                    Notification Settings
                                </h1>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <form id="Form1" runat="server">
                                <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"
                                    EnablePageMethods="true">
                                </asp:ScriptManager>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="m-form__group form-group">
                                            <label>
                                                Choose Aproval Platform
                                            </label>
                                            <div class="m-radio-list">
                                                <table>
                                                    <tr>
                                                        <td style="width: 200px">
                                                            <label class="m-radio m-radio--bold m-radio--state-success">
                                                                <input type="radio" name="example_5_1" id="rbtnTelephoneNumber" runat="server" value="5" />
                                                                Telephone Number <span></span>
                                                            </label>
                                                        </td>
                                                        <td style="width: 250px">
                                                            <div class="form-group m-form__group">
                                                                <select runat="server" class="form-control m-input m-input--square" id="ddlTelephoneNumber">
                                                                </select>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label class="m-radio m-radio--bold m-radio--state-brand">
                                                                <input type="radio" name="example_5_1" id="rbtnEmail" runat="server" value="6" />
                                                                E-mail <span></span>
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <select runat="server" class="form-control m-input m-input--square" id="ddlEmail">
                                                            </select>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="m-checkbox-list">
                                                <label class="m-checkbox m-checkbox--bold">
                                                    <input type="checkbox" id="chkRemind" runat="server" />
                                                    I want to be informed about coming exams <span></span>
                                                </label>
                                                <label class="m-checkbox m-checkbox--bold">
                                                    <input type="checkbox" runat="server" id="chkDefaultDate" />
                                                    Use Default Day and Time : 7 Days Ago-7:00 <span></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="m-portlet__body">
                                            <div class="form-group m-form__group row">
                                                <label for="example-number-input" class="col-2 col-form-label">
                                                    How many days before:
                                                </label>
                                                <div class="col-10">
                                                    <input class="form-control m-input" type="number" value="7" max="15" min="7" id="txtBeforeDays" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-portlet__body">
                                            <div class="form-group m-form__group row">
                                                <label for="example-time-input" class="col-2 col-form-label">
                                                    When do you want to be informed:
                                                </label>
                                                <div class="col-10">
                                                    <input class="form-control m-input" type="time" value="13:45:00" id="txtInformedTime" />
                                                </div>
                                            </div>
                                        </div>
                                        <button class="btn btn-primary" type="button" id="btnSave"  onclick="btnSaveClick();" runat="server">
                                            Save
                                        </button>
                                    </div>
                                </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- end::Body -->
        <!-- begin::Footer -->
        <footer class="m-grid__item m-footer ">
				<div class="m-container m-container--fluid m-container--full-height m-page__container">
					<div class="m-footer__wrapper">
						<div class="m-stack m-stack--flex-tablet-and-mobile m-stack--ver m-stack--desktop">
							<div class="m-stack__item m-stack__item--left m-stack__item--middle m-stack__item--last">
								<span class="m-footer__copyright">
									2018 &copy; all rights reserved By DEB
								</span>
							</div>
							
						</div>
					</div>
				</div>
			</footer>
        <!-- end::Footer -->
    </div>
    <!-- end:: Page -->
    
    <!-- begin::Scroll Top -->
    <div class="m-scroll-top m-scroll-top--skin-top" data-toggle="m-scroll-top" data-scroll-offset="500"
        data-scroll-speed="300">
        <i class="la la-arrow-up"></i>
    </div>
    <!-- end::Scroll Top -->
    <!-- begin::Quick Nav -->
    <!-- begin::Quick Nav -->
    <!--begin::Base Scripts -->
    <script src="../assets/vendors/base/vendors.bundle.js" type="text/javascript"></script>
    <script src="../assets/base/scripts.bundle.js" type="text/javascript"></script>
    <!--end::Base Scripts -->
    <!--begin::Page Snippets -->
    <script src="../assets/app/js/dashboard.js" type="text/javascript"></script>
    <!--end::Page Snippets -->
    <!-- begin::Page Loader -->
    <script type="text/javascript">
        $(window).on('load', function () {
            $('body').removeClass('m-page--loading');
        });
    </script>
    <!-- end::Page Loader -->
</body>
<!-- end::Body -->
</html>
