﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PasswordBox;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PasswordBox
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
            ShowHideButton();
            LoadUserHead();
        }
        private byte[] userHead;
        /// <summary>
        /// 检验密码是否正确
        /// 若正确则跳转到home
        /// 否则提示密码错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CheckPassword(object sender, RoutedEventArgs e)
        {
            // check the password success
            if (checkPassword.Password == Services.UserInfo.GetInfo("LoginPassword"))
            {
                App.loginFlag = true;
                Frame.Navigate(typeof(Home));
                MainPage.Current.ShowMenu();
            }
            // check the password fail
            else
            {
                // caution the password error
                ContentDialog dialog;
                dialog = new ContentDialog()
                {
                    Title = "提示",
                    PrimaryButtonText = "确认",
                    Content = "密码错误",
                    FullSizeDesired = false,
                };
                dialog.PrimaryButtonClick += (_s, _e) => { };
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// 忘记密码，跳转到更改密码页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForgetPassword(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangePassword));
        }

        /// <summary>
        /// load the user head
        /// </summary>
        private async void LoadUserHead()
        {
            if (Services.UserInfo.GetImage("Head") == null)
            {
                poster.ImageSource = new BitmapImage { UriSource = new Uri("ms-appx:///Assets/cat.png") };
            }
            else userHead = await Services.UserInfo.GetImage("Head");
        }

        /// <summary>
        /// decide the forgetButton hide or show
        /// </summary>
        private void ShowHideButton()
        {
            if (Services.UserInfo.CheckIfExist("LoginPassword") == false)
            {
                forgetButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                forgetButton.Visibility = Visibility.Visible;
            }
        }
    }
}
