using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace pryPracticas
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        clsDatos datos = new clsDatos();
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            string SQL1 = "Select * from materias";
            DataTable mt = datos.getData(SQL1);
            foreach (DataRow fila in mt.Rows)
            {
                ListViewItem lwi = listView1.Items.Add(fila["nombre"].ToString());
                lwi.Tag = fila["materia"].ToString();
            }
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            


        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();

            Series serie = chart1.Series.Add("Alumnos".ToString());

            Int32 Aprobados = 0, Desaporbados = 0, Promocionado = 0;

            string SQL2 = "SELECT materia, dni, nota FROM notas";
            DataTable nt = datos.getData(SQL2);
            foreach (ListViewItem chk in listView1.CheckedItems)
            {
                Int32 TagMaterias = Convert.ToInt32(chk.Tag);

                foreach (DataRow dr in nt.Rows)
                {
                    Int32 Nota = Convert.ToInt32(dr["nota"].ToString());
                    Int32 Materia = Convert.ToInt32(dr["materia"].ToString());

                    if (TagMaterias == Materia)
                    {
                        if (Nota >= 7)
                        {
                            Promocionado = Promocionado + 1;
                        }
                        if (Nota >= 4 && Nota < 7)
                        {
                            Aprobados = Aprobados + 1;
                        }
                        if (Nota < 4)
                        {
                            Desaporbados = Desaporbados + 1;
                        }
                    }
                }
            }

            serie.Points.AddXY("Aprobados", Aprobados);
            serie.Points.AddXY("Promocionados", Promocionado);
            serie.Points.AddXY("Desaprobados", Desaporbados);


            Int32 NumeroSelec = 0;
            foreach (ListViewItem ms in listView1.CheckedItems)
            {
                NumeroSelec++;
            }

            toolStripProgressBar1.Maximum = listView1.Items.Count;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Value = NumeroSelec;

            toolStripStatusLabel1.Text = NumeroSelec.ToString() + "/" + listView1.Items.Count + " Materias Seleccionadas";


        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
