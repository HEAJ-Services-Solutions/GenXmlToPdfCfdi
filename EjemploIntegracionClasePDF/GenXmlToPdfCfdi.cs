using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EjemploIntegracionClasePDF
{
    public partial class GenXmlToPdfCfdi : Form
    {
        public GenXmlToPdfCfdi()
        {
            InitializeComponent();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCrearPDF_Click(object sender, EventArgs e)
        {    
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                Generador.CreaPDF crearPDF = new Generador.CreaPDF(OneXmlRuta.Text, OnePdfRuta.Text, Image.FromFile(OneXmlLogo.Text), true );
            }
            else if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                DirectoryInfo di = new DirectoryInfo(AllRutaXml.Text);

                Progress.Maximum = di.GetFiles("*.xml").Length;

                string nuevaRuta;
                foreach (var fi in di.GetFiles("*.xml"))
                {
                    nuevaRuta = AllRutaPdf.Text + "\\" + fi.Name.Replace(".xml", ".pdf");

                    if (string.IsNullOrEmpty(AllRutaLogo.Text))
                    {
                        Generador.CreaPDF crearPDF = new Generador.CreaPDF(fi.FullName, nuevaRuta, null, false);
                    }
                    else
                    {
                        Generador.CreaPDF crearPDF = new Generador.CreaPDF(fi.FullName, nuevaRuta, Image.FromFile(AllRutaLogo.Text), false);
                    }
                    Progress.Increment(1);
                }
                MessageBox.Show("Los archivos XML indicados an sido convertidos a pdf. y guardados en: " + AllRutaPdf.Text, "OUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(AllRutaPdf.Text);
                AllRutaLogo.Text = "";
                AllRutaPdf.Text = "";
                AllRutaXml.Text = "";
                Progress.Value = 0;
            }

        }

        private void btnGuardarEn_Click(object sender, EventArgs e)
        {
            

        }

        private void btnCarpetaXML_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog guardar = new FolderBrowserDialog();
            guardar.RootFolder = System.Environment.SpecialFolder.Desktop;
            guardar.ShowNewFolderButton = false;
            guardar.Description = "Selecciona la carpeta contenedora de archivos en formato XML"; 
            if (guardar.ShowDialog() != DialogResult.OK)
                return;
            AllRutaXml.Text = guardar.SelectedPath;
        }

        private void btnCarpetaPDF_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog guardar = new FolderBrowserDialog();
            guardar.RootFolder = System.Environment.SpecialFolder.Desktop;
            guardar.Description = "Selecciona la carpeta donde se almacenarán los nuevos archivos en formato PDF"; 
            if (guardar.ShowDialog() != DialogResult.OK)
                return;
            AllRutaPdf.Text = guardar.SelectedPath;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Archivo XML (*.xml)|*.xml";
            open.FilterIndex = 4;
            if (open.ShowDialog() != DialogResult.OK)
                return;
            OneXmlRuta.Text = open.FileName;
            OnePdfRuta.Text = open.FileName.Replace(".xml",".pdf");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Archivo PDF (*.pdf)|*.pdf";
            if (guardar.ShowDialog() != DialogResult.OK)
                return;
            OnePdfRuta.Text = guardar.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Imagen JPG (*.jpg;*.jpeg;*.jpe)|*.jpg;*.jpeg;*.jpe |PNG (*.png)|*.png |Archivo de mapa de bits(*.bmp)|*.bmp | Todos los formatos |*.jpg;*.jpeg;*.jpe;*.png;*.bmp";
            open.FilterIndex = 4;
            if (open.ShowDialog() != DialogResult.OK)
                return;

            OneXmlLogo.Text = open.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Imagen JPG (*.jpg;*.jpeg;*.jpe)|*.jpg;*.jpeg;*.jpe |PNG (*.png)|*.png |Archivo de mapa de bits(*.bmp)|*.bmp | Todos los formatos |*.jpg;*.jpeg;*.jpe;*.png;*.bmp";
            open.FilterIndex = 4;
            if (open.ShowDialog() != DialogResult.OK)
                return;

            AllRutaLogo.Text = open.FileName;
        }
    }
}
