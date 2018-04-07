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
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GK10._2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void ChangeToRegister(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));

        }

        private async void LoginUser(object sender, RoutedEventArgs e)
        {
            string param = "email=" + this.email.Text + "&pw=" + this.pw.Password;

            Uri geturi = new Uri("https://gk10-2-ertl.herokuapp.com/ertl/login?" + param);
            //Uri geturi = new Uri("http://localhost:8080/ertl/login?" + param); //replace your url  //Abfangen von Exception
            string response = "";
            try
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage responseGet = await client.GetAsync(geturi);
                response = await responseGet.Content.ReadAsStringAsync();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                this.errormessages.NavigateToString("Fehler beim Verbinden mit den Server.");
                return;
            }

            Newtonsoft.Json.Linq.JArray result = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(response);
            if (((string)result[0]).Equals("error"))
            {
                this.errormessages.NavigateToString((string)result[1]);
                return;
            }
            else if (((string)result[0]).Equals("success"))
            {
                this.errormessages.NavigateToString(response);
                // Change to successful register
                // give vname, nname, email to page
                Frame.Navigate(typeof(MainPage), (string) result[1]);
                return;
            }

            this.errormessages.NavigateToString("Fehler beim Verarbeiten der Antwort vom Server." + response);

        }
    }
}
