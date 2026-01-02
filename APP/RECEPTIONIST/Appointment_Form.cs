using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RECEPTIONIST
{
    public partial class Appointment_Form : Form
    {
        ReceptionistMainForm mainForm;
        public Appointment_Form()
        {
            InitializeComponent();
        }

        public Appointment_Form(ReceptionistMainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void btnAppointmentRegister_Click(object sender, EventArgs e)
        {
            Modify modify = new Modify();
            if(modify.RegisterAppointment(txtCusID.Text, modify.GetEmployeeBranchID(mainForm.lblEmployeeID.Text), txtServiceID.Text, mainForm.lblEmployeeID.Text, dtpDate.Value, dtpTime.Value.TimeOfDay))
            {
                MessageBox.Show("Đặt lịch hẹn thành công");
            }
            else
            {
                MessageBox.Show("Đặt lịch hẹn thất bại");
            }
        }
    }
}
