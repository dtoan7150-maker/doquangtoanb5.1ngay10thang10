
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai51_DangKy
{
    public partial class Form1 : Form
    {
        TextBox txtUsername = new TextBox();
        TextBox txtPassword = new TextBox();
        TextBox txtConfirmPassword = new TextBox();

        DateTimePicker dtpBirthDate = new DateTimePicker();

        RadioButton rdoMale = new RadioButton();
        RadioButton rdoFemale = new RadioButton();

        CheckBox chkTerms = new CheckBox();

        Button btnRegister = new Button();
        Button btnReset = new Button();

        ErrorProvider epCheck = new ErrorProvider();

        public Form1()
        {
            InitializeComponent();

            Text = "Bài 5.1 - Đăng ký tài khoản";
            Size = new Size(500, 470);
            StartPosition = FormStartPosition.CenterScreen;

            epCheck.ContainerControl = this;
            epCheck.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            TaoGiaoDien();
        }

        void TaoGiaoDien()
        {
            Label lblTitle = new Label();
            lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(105, 20);
            Controls.Add(lblTitle);

            ThemNhan("Tên đăng nhập:", 60);
            txtUsername.SetBounds(190, 58, 220, 25);
            Controls.Add(txtUsername);

            ThemNhan("Mật khẩu:", 100);
            txtPassword.SetBounds(190, 98, 220, 25);
            txtPassword.UseSystemPasswordChar = true;
            Controls.Add(txtPassword);

            ThemNhan("Xác nhận mật khẩu:", 140);
            txtConfirmPassword.SetBounds(190, 138, 220, 25);
            txtConfirmPassword.UseSystemPasswordChar = true;
            Controls.Add(txtConfirmPassword);

            ThemNhan("Ngày sinh:", 180);
            dtpBirthDate.SetBounds(190, 178, 220, 25);
            dtpBirthDate.Format = DateTimePickerFormat.Short;
            dtpBirthDate.MaxDate = DateTime.Today;
            dtpBirthDate.Value = DateTime.Today.AddYears(-18);
            Controls.Add(dtpBirthDate);

            ThemNhan("Giới tính:", 220);

            rdoMale.Text = "Nam";
            rdoMale.SetBounds(190, 218, 65, 25);
            rdoMale.Checked = true;
            Controls.Add(rdoMale);

            rdoFemale.Text = "Nữ";
            rdoFemale.SetBounds(265, 218, 65, 25);
            Controls.Add(rdoFemale);

            chkTerms.Text = "Tôi đồng ý với điều khoản dịch vụ";
            chkTerms.SetBounds(190, 258, 250, 25);
            Controls.Add(chkTerms);

            btnRegister.Text = "Đăng Ký";
            btnRegister.SetBounds(110, 310, 110, 35);
            btnRegister.Click += BtnRegister_Click;
            Controls.Add(btnRegister);

            btnReset.Text = "Làm Mới";
            btnReset.SetBounds(245, 310, 110, 35);
            btnReset.Click += BtnReset_Click;
            Controls.Add(btnReset);

            AcceptButton = btnRegister;
        }

        void ThemNhan(string noiDung, int y)
        {
            Label lbl = new Label();
            lbl.Text = noiDung;
            lbl.AutoSize = true;
            lbl.Location = new Point(35, y + 3);
            Controls.Add(lbl);
        }

        void BtnRegister_Click(object sender, EventArgs e)
        {
            epCheck.Clear();
            bool hopLe = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                epCheck.SetError(txtUsername, "Vui lòng nhập tên đăng nhập!");
                hopLe = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                epCheck.SetError(txtPassword, "Vui lòng nhập mật khẩu!");
                hopLe = false;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                epCheck.SetError(txtConfirmPassword, "Vui lòng xác nhận mật khẩu!");
                hopLe = false;
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                epCheck.SetError(txtConfirmPassword, "Mật khẩu xác nhận không khớp!");
                hopLe = false;
            }

            int tuoi = DateTime.Today.Year - dtpBirthDate.Value.Year;

            if (dtpBirthDate.Value.Date > DateTime.Today.AddYears(-tuoi))
            {
                tuoi--;
            }

            if (tuoi < 18)
            {
                epCheck.SetError(dtpBirthDate, "Bạn phải đủ 18 tuổi trở lên!");
                hopLe = false;
            }

            if (!chkTerms.Checked)
            {
                epCheck.SetError(chkTerms, "Bạn phải đồng ý điều khoản dịch vụ!");
                hopLe = false;
            }

            if (!hopLe)
            {
                MessageBox.Show(
                    "Vui lòng kiểm tra các thông tin bị báo lỗi.",
                    "Dữ liệu chưa hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string gioiTinh = rdoMale.Checked ? "Nam" : "Nữ";

            MessageBox.Show(
                "Đăng ký tài khoản thành công!\n\n" +
                "Tên đăng nhập: " + txtUsername.Text +
                "\nNgày sinh: " + dtpBirthDate.Value.ToShortDateString() +
                "\nGiới tính: " + gioiTinh,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        void BtnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();

            dtpBirthDate.Value = DateTime.Today.AddYears(-18);

            rdoMale.Checked = true;
            chkTerms.Checked = false;

            epCheck.Clear();
            txtUsername.Focus();
        }
    }
}
