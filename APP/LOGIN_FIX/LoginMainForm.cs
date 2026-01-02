using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LOGIN_FIX
{
    public partial class LoginMainForm : Form
    {
        private Form _current;

        public LoginMainForm()
        {
            InitializeComponent();
            pnlHost.Dock = DockStyle.Fill;
            ShowSignIn();
        }

        private void ShowChild(Form child)
        {
            if (_current != null)
            {
                _current.Close();
                _current.Dispose();
                _current = null;
            }

            _current = child;

            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;

            pnlHost.Controls.Clear();
            pnlHost.Controls.Add(child);
            child.Show();
        }

        private void ShowSignIn()
        {
            var f = new SignIn();

            f.ForgotPasswordClicked += () => ShowForgot();

            f.LoginSuccess += (nextForm) =>
            {
                this.Hide();           // ✅ ẩn LoginMainForm
                nextForm.FormClosed += (_, __) =>
                {
                    this.Show();       // ✅ đóng role-form thì hiện login lại (tuỳ bạn)
                    ShowSignIn();      // ✅ reset về SignIn sạch sẽ
                };
                nextForm.Show();
            };

            ShowChild(f);
        }

        private void ShowForgot()
        {
            var f = new Forget_Pass();
            f.BackToSignin += () => ShowSignIn();
            f.VerifiedOk += (username) => ShowReset(username);
            ShowChild(f);
        }

        private void ShowReset(string username)
        {
            var f = new Reset_Pass(username);
            f.ResetSuccess += () => ShowSignIn();
            f.BackToForgot += () => ShowForgot(); // nếu có nút back
            ShowChild(f);
        }
    }
}