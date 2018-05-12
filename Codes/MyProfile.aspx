<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="MyProfile" EnableEventValidation="false" %>

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
            google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700", "Asap+Condensed:500"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>
    <!--end::Web font -->
    <!--begin::Base Styles -->
    <!--begin::Page Vendors -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <!--end::Page Vendors -->
    <link href="assets/vendors/base/vendors.bundle.css" rel="stylesheet" type="text/css" />
    <link href="assets/base/style.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Base Styles -->
    <link rel="shortcut icon" href="assets/demo8/demo/media/img/logo/favicon.ico" />

    <script type="text/jscript">
        var items = null;
        var txtTCNumber = null;
        var txtUsername = null;
        var txtSurname = null;
        var txtTitle = null;
        var txtFacultyName = null;
        var txtDepartmentName = null;
        var txtClassDegree = null;
        var txtStudentNo = null;
        var txtTelephoneNumber1 = null;
        var txtTelephoneNumber2 = null;
        var txtEmailAddress1 = null;
        var txtEmailAddress2 = null;

        $(document).ready(function () {
            txtTCNumber = document.getElementById("<%=txtTCNumber.ClientID %>");
            txtUsername = document.getElementById("<%=txtUsername.ClientID %>");
            txtSurname = document.getElementById("<%=txtSurname.ClientID %>");
            txtTitle = document.getElementById("<%=txtTitle.ClientID %>");
            txtFacultyName = document.getElementById("<%=txtFacultyName.ClientID %>");
            txtDepartmentName = document.getElementById("<%=txtDepartmentName.ClientID %>");
            txtClassDegree = document.getElementById("<%=txtDepartmentName.ClientID %>");
            txtStudentNo = document.getElementById("<%=txtStudentNo.ClientID %>");
            txtTelephoneNumber1 = document.getElementById("<%=txtTelephoneNumber1.ClientID %>");
            txtTelephoneNumber2 = document.getElementById("<%=txtTelephoneNumber2.ClientID %>");
            txtEmailAddress1 = document.getElementById("<%=txtEmailAddress1.ClientID %>");
            txtEmailAddress2 = document.getElementById("<%=txtEmailAddress2.ClientID %>");
            PageMethods.getUserInfo(OnSuccess);
            function OnSuccess(response) {
                items = response;
                var x = JSON.parse(items);
                for (var i in x) {
                    txtTCNumber.value = x[i].TCNumber;
                    txtUsername.value = x[i].Username;
                    txtSurname.value = x[i].Surname;
                    txtTitle.value = x[i].Title;
                    txtFacultyName.value = x[i].FacultyName;
                    txtDepartmentName.value = x[i].DepartmentName;
                    txtClassDegree.value = x[i].ClassDegree;
                    txtStudentNo.value = x[i].StudentNo;
                    txtTelephoneNumber1.value = x[i].TelephoneNumber1;
                    txtTelephoneNumber2.value = x[i].TelephoneNumber2;
                    txtEmailAddress1.value = x[i].EmailAddress1;
                    txtEmailAddress2.value = x[i].EmailAddress2;
                    document.getElementById('lbProfileName').innerHTML = x[i].Username;
                    document.getElementById('lbEmail').innerHTML = x[i].EmailAddress1;
                }
            }

        });

        function btnSaveClick() {
            var TCNumber = null;
            var TelephoneNumber2 = null;
            var EmailAddress2 = null;

            TCNumber = txtTCNumber.value;
            TelephoneNumber2 = txtTelephoneNumber2.value;
            EmailAddress2 = txtEmailAddress2.value;
   
            var obj = {};
            obj.TCNumber = TCNumber;
            obj.TelephoneNumber2 = TelephoneNumber2;
            obj.EmailAddress2 = EmailAddress2;
            $.ajax({
                type: "POST",
                url: "MyProfile.aspx/btnSaveClick",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = response.d;
                    alert(result);
                    document.location.reload(true);
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
<body style="background-image: url(assets/app/media/img/bg/bg-7.jpg)" class="m-page--fluid m-page--loading-enabled m-page--loading m-header--fixed m-header--fixed-mobile m-footer--push m-aside--offcanvas-default">
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
										<a href="Homepage.aspx" class="m-brand__logo-wrapper">
											<img alt="" src="assets/app/media/img/logos/logo.png" class="m-brand__logo-default"/>
											<img alt="" src="assets/app/media/img/logos/logo_inverse1.png" class="m-brand__logo-inverse"/>
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
											<li class="m-nav__item m-topbar__notifications m-dropdown m-dropdown--large m-dropdown--arrow m-dropdown--align-center 	m-dropdown--mobile-full-width" data-dropdown-toggle="click" data-dropdown-persistent="true">
												<a href="#" class="m-nav__link m-dropdown__toggle" id="m_topbar_notification_icon">
													<span class="m-nav__link-badge m-badge m-badge--dot m-badge--dot-small m-badge--danger"></span>
													<span class="m-nav__link-icon">
														<span class="m-nav__link-icon-wrapper">
															<i class="flaticon-music-2"></i>
														</span>
													</span>
												</a>
												<div class="m-dropdown__wrapper">
													<span class="m-dropdown__arrow m-dropdown__arrow--center"></span>
													<div class="m-dropdown__inner">
														<div class="m-dropdown__header m--align-center">
															<span class="m-dropdown__header-title">
																9 New
															</span>
															<span class="m-dropdown__header-subtitle">
																User Notifications
															</span>
														</div>
														<div class="m-dropdown__body">
															<div class="m-dropdown__content">
																<ul class="nav nav-tabs m-tabs m-tabs-line m-tabs-line--brand" role="tablist">
																	<li class="nav-item m-tabs__item">
																		<a class="nav-link m-tabs__link active" data-toggle="tab" href="#topbar_notifications_notifications" role="tab">
																			Alerts
																		</a>
																	</li>
																	<li class="nav-item m-tabs__item">
																		<a class="nav-link m-tabs__link" data-toggle="tab" href="#topbar_notifications_events" role="tab">
																			Events
																		</a>
																	</li>
																	<li class="nav-item m-tabs__item">
																		<a class="nav-link m-tabs__link" data-toggle="tab" href="#topbar_notifications_logs" role="tab">
																			Logs
																		</a>
																	</li>
																</ul>
																<div class="tab-content">
																	<div class="tab-pane active" id="topbar_notifications_notifications" role="tabpanel">
																		<div class="m-scrollable" data-scrollable="true" data-max-height="250" data-mobile-max-height="200">
																			<div class="m-list-timeline m-list-timeline--skin-light">
																				<div class="m-list-timeline__items">
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge -m-list-timeline__badge--state-success"></span>
																						<span class="m-list-timeline__text">
																							12 new users registered
																						</span>
																						<span class="m-list-timeline__time">
																							Just now
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge"></span>
																						<span class="m-list-timeline__text">
																							System shutdown
																							<span class="m-badge m-badge--success m-badge--wide">
																								pending
																							</span>
																						</span>
																						<span class="m-list-timeline__time">
																							14 mins
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge"></span>
																						<span class="m-list-timeline__text">
																							New invoice received
																						</span>
																						<span class="m-list-timeline__time">
																							20 mins
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge"></span>
																						<span class="m-list-timeline__text">
																							DB overloaded 80%
																							<span class="m-badge m-badge--info m-badge--wide">
																								settled
																							</span>
																						</span>
																						<span class="m-list-timeline__time">
																							1 hr
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge"></span>
																						<span class="m-list-timeline__text">
																							System error -
																							<a href="#" class="m-link">
																								Check
																							</a>
																						</span>
																						<span class="m-list-timeline__time">
																							2 hrs
																						</span>
																					</div>
																					<div class="m-list-timeline__item m-list-timeline__item--read">
																						<span class="m-list-timeline__badge"></span>
																						<span href="" class="m-list-timeline__text">
																							New order received
																							<span class="m-badge m-badge--danger m-badge--wide">
																								urgent
																							</span>
																						</span>
																						<span class="m-list-timeline__time">
																							7 hrs
																						</span>
																					</div>
																					<div class="m-list-timeline__item m-list-timeline__item--read">
																						<span class="m-list-timeline__badge"></span>
																						<span class="m-list-timeline__text">
																							Production server down
																						</span>
																						<span class="m-list-timeline__time">
																							3 hrs
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge"></span>
																						<span class="m-list-timeline__text">
																							Production server up
																						</span>
																						<span class="m-list-timeline__time">
																							5 hrs
																						</span>
																					</div>
																				</div>
																			</div>
																		</div>
																	</div>
																	<div class="tab-pane" id="topbar_notifications_events" role="tabpanel">
																		<div class="m-scrollable" m-scrollabledata-scrollable="true" data-max-height="250" data-mobile-max-height="200">
																			<div class="m-list-timeline m-list-timeline--skin-light">
																				<div class="m-list-timeline__items">
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge m-list-timeline__badge--state1-success"></span>
																						<a href="" class="m-list-timeline__text">
																							New order received
																						</a>
																						<span class="m-list-timeline__time">
																							Just now
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge m-list-timeline__badge--state1-danger"></span>
																						<a href="" class="m-list-timeline__text">
																							New invoice received
																						</a>
																						<span class="m-list-timeline__time">
																							20 mins
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge m-list-timeline__badge--state1-success"></span>
																						<a href="" class="m-list-timeline__text">
																							Production server up
																						</a>
																						<span class="m-list-timeline__time">
																							5 hrs
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge m-list-timeline__badge--state1-info"></span>
																						<a href="" class="m-list-timeline__text">
																							New order received
																						</a>
																						<span class="m-list-timeline__time">
																							7 hrs
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge m-list-timeline__badge--state1-info"></span>
																						<a href="" class="m-list-timeline__text">
																							System shutdown
																						</a>
																						<span class="m-list-timeline__time">
																							11 mins
																						</span>
																					</div>
																					<div class="m-list-timeline__item">
																						<span class="m-list-timeline__badge m-list-timeline__badge--state1-info"></span>
																						<a href="" class="m-list-timeline__text">
																							Production server down
																						</a>
																						<span class="m-list-timeline__time">
																							3 hrs
																						</span>
																					</div>
																				</div>
																			</div>
																		</div>
																	</div>
																	<div class="tab-pane" id="topbar_notifications_logs" role="tabpanel">
																		<div class="m-stack m-stack--ver m-stack--general" style="min-height: 180px;">
																			<div class="m-stack__item m-stack__item--center m-stack__item--middle">
																				<span class="">
																					All caught up!
																					<br>
																					No new logs.
																				</span>
																			</div>
																		</div>
																	</div>
																</div>
															</div>
														</div>
													</div>
												</div>
											</li>
											
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
																		Mark Andre
																	</span>
																	<a href="" class="m-card-user__email m--font-weight-300 m-link">
																		mark.andre@gmail.com
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
																		<a href="profile.html" class="m-nav__link">
																			<i class="m-nav__link-icon flaticon-profile-1"></i>
																			<span class="m-nav__link-title">
																				<span class="m-nav__link-wrap">
																					<span class="m-nav__link-text">
																						My Profile
																					</span>
																					<span class="m-nav__link-badge">
																						<span class="m-badge m-badge--success">
																							2
																						</span>
																					</span>
																				</span>
																			</span>
																		</a>
																	</li>
																	
																	
																	<li class="m-nav__separator m-nav__separator--fit"></li>
																	<li class="m-nav__item">
																		<a href="Login.aspx" class="btn m-btn--pill    btn-secondary m-btn m-btn--custom m-btn--label-brand m-btn--bolder">
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
										<li class="m-menu__item  m-menu__item--active  m-menu__item--active-tab  m-menu__item--submenu m-menu__item--tabs"  data-menu-submenu-toggle="tab" aria-haspopup="true">
											<a  href="Homepage.aspx" class="m-menu__link m-menu__toggle">
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
														<a  href="HomePage.aspx" class="m-menu__link ">
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
														<a  href="OtherExamRequest.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-notes"></i>
															<span class="m-menu__link-text">
																Other Exam Requests
															</span>
														</a>
													</li>
												</ul>
											</div>
										</li>
										<li class="m-menu__item  m-menu__item--submenu m-menu__item--tabs"  data-menu-submenu-toggle="tab" aria-haspopup="true">
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
														<a  href="settings/NotificationSettings.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-analytics"></i>
															<span class="m-menu__link-text">
																Notification Settings
															</span>
														</a>
													</li>
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="settings/PasswordSettings.aspx" class="m-menu__link ">
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
														<a  href="adminPanel/Faculty.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-analytics"></i>
															<span class="m-menu__link-text">
																Faculty
															</span>
														</a>
													</li>
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="adminPanel/Department.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-notes"></i>
															<span class="m-menu__link-text">
																Department
															</span>
														</a>
													</li>
                                                    <li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="adminPanel/Course.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-analytics"></i>
															<span class="m-menu__link-text">
																Course
															</span>
														</a>
													</li>
													<li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="adminPanel/Class.aspx" class="m-menu__link ">
															<i class="m-menu__link-icon flaticon-notes"></i>
															<span class="m-menu__link-text">
																Class
															</span>
														</a>
													</li>	
                                                    <li class="m-menu__item "  data-redirect="true" aria-haspopup="true">
														<a  href="adminPanel/ExamCalendarRequest.aspx" class="m-menu__link ">
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
                <div class="m-content">
                    <div class="row">
                        <div class="col-xl-3 col-lg-4">
                            <div class="m-portlet m-portlet--full-height  ">
                                <div class="m-portlet__body">
                                    <div class="m-card-profile">
                                        <div class="m-card-profile__title m--hide">
                                            Your Profile
                                        </div>
                                        <div class="m-card-profile__pic">
                                            <div class="m-card-profile__pic-wrapper">
                                                <img src="../assets/app/media/img/users/user4.jpg" alt="">
                                            </div>
                                        </div>
                                        <div class="m-card-profile__details">
                                            <label id="lbProfileName"  class="m-card-profile__name"></label>
                                             <label id="lbEmail"  class="m-card-profile__email m-link"></label>
                                        </div>
                                    </div>
                                    <div class="m-portlet__body-separator">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-9 col-lg-8">
                            <div class="m-portlet m-portlet--full-height m-portlet--tabs  ">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="m_user_profile_tab_1">
                                        <form class="m-form m-form--fit m-form--label-align-right" runat="server">
                                        <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"
                                        EnablePageMethods="true">
                                        </asp:ScriptManager>
                                        <div class="m-portlet__body">
                                            <div class="form-group m-form__group m--margin-top-10 m--hide">
                                                <div class="alert m-alert m-alert--default" role="alert">
                                                    The example form below demonstrates common HTML form elements that receive updated
                                                    styles from Bootstrap with additional classes.
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <div class="col-10 ml-auto">
                                                    <h3 class="m-form__section">
                                                        1. Personal Details
                                                    </h3>
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    TC Number
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtTCNumber" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Username
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtUsername" runat="server"  class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input"  class="col-2 col-form-label">
                                                    Surname
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtSurname" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Title
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtTitle" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Faculty Name
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtFacultyName" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Department Name
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtDepartmentName" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    ClassDegree
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtClassDegree" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Student No
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtStudentNo" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Telephone Number-1
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtTelephoneNumber1" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Telephone Number-2
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtTelephoneNumber2" runat="server" class="form-control m-input" type="text" value="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Email Address -1
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtEmailAddress1" runat="server" class="form-control m-input" type="text" value="" disabled="">
                                                </div>
                                            </div>
                                            <div class="form-group m-form__group row">
                                                <label for="example-text-input" class="col-2 col-form-label">
                                                    Email Address -2
                                                </label>
                                                <div class="col-7">
                                                    <input id="txtEmailAddress2" runat="server" class="form-control m-input" type="text" value="">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-portlet__foot m-portlet__foot--fit">
                                            <div class="m-form__actions">
                                                <div class="row">
                                                    <div class="col-2">
                                                    </div>
                                                    <div class="col-7">
                                                        <button type="reset" id="btnSaveChanges" runat="server" class="btn btn-accent m-btn m-btn--air m-btn--custom" onclick="btnSaveClick();">
                                                            Save changes
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </form>
                                    </div>
                                </div>
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
    <!-- begin::Quick Sidebar -->

    <!-- end::Quick Sidebar -->
    <!-- begin::Scroll Top -->
    <div class="m-scroll-top m-scroll-top--skin-top" data-toggle="m-scroll-top" data-scroll-offset="500"
        data-scroll-speed="300">
        <i class="la la-arrow-up"></i>
    </div>
    <!-- end::Scroll Top -->
    <!-- begin::Quick Nav -->
    <!-- begin::Quick Nav -->
    <!--begin::Base Scripts -->
    <script src="assets/vendors/base/vendors.bundle.js" type="text/javascript"></script>
    <script src="assets/base/scripts.bundle.js" type="text/javascript"></script>
    <!--end::Base Scripts -->
    <!--begin::Page Vendors -->
    <script src="assets/vendors/custom/fullcalendar/fullcalendar.bundle.js" type="text/javascript"></script>
    <!--end::Page Vendors -->
    <!--begin::Page Snippets -->
    <script src="assets/app/js/dashboard.js" type="text/javascript"></script>
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
