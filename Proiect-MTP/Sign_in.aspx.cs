﻿using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proiect_MTP
{
    public partial class Sign_in : System.Web.UI.Page
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "GYGuqf4TtckWwYi9IYktNRK24TjNw2uEJZwbhUAf",
            BasePath = "https://users-482c3-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                lblMsg.Text = "Conexiunea a fost realizata cu succes!";
            }
            else
            {
                lblMsg.Text = "Conexiune esuata!";
            }
        }
        protected async void ButtonCreate_Click1(object sender, EventArgs e)
        {
            string username = TextBoxName.Text;
            string email = TextBoxEmail.Text;
            string password = TextBoxPassword.Text;

            if (password.Length < 8)
            {
                lblMsg2.Text = "Password must be at least 8 characters long.";
                lblMsg2.ForeColor = System.Drawing.Color.Red;
                return;
            }
            FirebaseResponse response = await client.GetAsync($"DateUsers/{username}");
            if (response.Body != "null")
            {
                lblMsg1.Text = "Utilizatorul există deja. Încearcă să te loghezi.";
                Response.Redirect("Login.aspx");
            }
            else
            {
                var user = new { Username = username, Email = email, Password = password, DateCreated = DateTime.Now };
                SetResponse setResponse = await client.SetAsync($"DateUsers/{username}", user);

                if (setResponse != null) 
                {
                    lblMsg1.Text = "Contul a fost creat cu succes!";
                    Response.Redirect("PaginaPrincipala.aspx");
                }
                else
                {
                    lblMsg1.Text = "A apărut o eroare la crearea contului.";
                }
            }
        }
    }
}