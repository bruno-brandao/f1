using AppF1.Domain;
using AppF1.Repository;
using AppF1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppF1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string excelPath;
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = @"C:\",
                    Title = "Buscar excel de pessoas",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "xls",
                    Filter = "Xls files (*.xls)|*.xls",
                    FilterIndex = 2,
                    RestoreDirectory = true,

                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    excelPath = openFileDialog1.FileName;

                    using (var dbContext = new MyDbContext())
                    {
                        if (!dbContext.People.Any())
                        {
                            var people = ExcelService.ExcelFileToListObject(excelPath);
                            dbContext.People.AddRange(people);
                            dbContext.SaveChanges();
                            button1.Enabled = false;
                            MessageBox.Show("Dados importados com sucesso");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var dbContext = new MyDbContext())
            {
                if (dbContext.People.Any())
                {
                    button1.Enabled = false;

                    populateDataGrid(dbContext);
                }
            }
        }

        private void populateDataGrid(MyDbContext dbContext)
        {
            var people = dbContext.People.Select(p => new
            {
                OP_MAT = p.OP_MAT,
                OP_CPF = p.OP_CPF,
                OP_NM = p.OP_NM,
                DN = p.DN,
                PAI = p.PAI,
                MAE = p.MAE,
                PROFISSAO = p.PROFISSAO,
            }).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = people;
            dataGridView1.Refresh();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
