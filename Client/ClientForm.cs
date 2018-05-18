﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form, Model.MessageListener
    {
        private Controller controller;

        public ClientForm()
        {
            InitializeComponent();
            Model model = new Model();
            model.AddListener(this);
            controller = new Controller(model);          
        }
        
        private void sendButton_Click(object sender, EventArgs e)
        {
            string message = messageTextBox.Text.ToString();
            messageTextBox.Clear();
            controller.OnSendMessageButton(message);
            messageTextBox.Focus();
        }

        public void Update(string message)
        {            
            messageListBox.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                messageListBox.Items.Add(message + "\n");
            });            
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            if (loginForm.DialogResult == DialogResult.OK)
            {
                string user = loginForm.UserName;
                controller.OnConnectButton(user);
            } else
            {
                Close();
            }
        }
    }
}
