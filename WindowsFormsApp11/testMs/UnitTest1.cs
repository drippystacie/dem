using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WindowsFormsApp11;

namespace testMs
{
    [TestClass]
    public class testingLoginForms
    {
        [TestMethod]
        public void PositiveTestLoginForms()
        {
            var loginForm = new LoginForm();
            loginForm.txtLogin.Text = "zaharkooo";
            loginForm.txtPassword.Text = "polina09";
            loginForm.button1_Click(null, null);
        }

        [TestMethod]
        public void NegativeTestLoginForms()
        {
            var loginForm = new LoginForm();
            loginForm.txtLogin.Text = "";
            loginForm.txtPassword.Text = "";
            loginForm.button1_Click(null, null);
        }

    }
}
