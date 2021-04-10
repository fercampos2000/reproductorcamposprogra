using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reproductorcampos
{
    public partial class Reproductor : Form
    {

        Lista addlist = new Lista();
        OpenFileDialog addpath = new OpenFileDialog();


        public Reproductor()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }



        private void buttonagregar_Click(object sender, EventArgs e)
        {
            try { 
            addpath.Multiselect = true;
            addpath.Filter = "Archivos audios (*.mp3),(*.mp4),(*.wav),(*.png)|";

            if (addpath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                for (int i = 0; i < addpath.SafeFileNames.Length; i++)
                {
                    addlist.insertarCanciones(addpath.FileNames[i]);
                    CANCIONES.Items.Add(addpath.SafeFileNames[i]);

                }
                axWindowsMediaPlayer1.URL = addpath.FileNames[0];
                CANCIONES.SelectedIndex = 0;


                    int pausa;
                    pausa = 0;
                }

            
            }catch(Exception en)
            {
                MessageBox.Show(en.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void CANCIONES_SelectedIndexChanged(object sender, EventArgs e)
        {
            //muestra las canciones seleccionadas
            if (CANCIONES.SelectedIndex != -1)
            {
                axWindowsMediaPlayer1.URL = addpath.FileNames[CANCIONES.SelectedIndex];
            }
        }

        private void buttoneliminar_Click(object sender, EventArgs e)
        {
            int indice = CANCIONES.SelectedIndex;

            addlist.deleteMusic(indice);
            CANCIONES.Items.RemoveAt(indice);
            axWindowsMediaPlayer1.Ctlcontrols.stop();

           
        }

        private void buttonanterior_Click(object sender, EventArgs e)
        {
            if (CANCIONES.SelectedIndex > 0)
            {
                CANCIONES.SelectedIndex -= 1;
            }
        }

        private void buttonsiguiente_Click(object sender, EventArgs e)
        {
            if (CANCIONES.SelectedIndex < CANCIONES.Items.Count - 1)
            {
                CANCIONES.SelectedIndex += 1;
            }
        }

        private void buttonpausa_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        
        }

        private void buttonplay_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
          

        }

        private void macTrackBar2_ValueChanged(object sender, decimal value)
        {
            minutero.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration;

            if (minutero.Value == (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition)
            {

            }
            else
            {

                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = minutero.Value;

            }
        }

        private void buttonstop_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
           

        }

        public void actualizadatostrack()
        {
            if(axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                volumen.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                timer1.Start();
            }else if(axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }else if(axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                timer1.Stop();
                volumen.Value = 0; 
            }
        }

        private void volumen_ValueChanged(object sender, decimal value)
        {
            
                axWindowsMediaPlayer1.settings.volume = volumen.Value;
                labelvolumen.Text = axWindowsMediaPlayer1.settings.volume.ToString();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                minutero.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                time1.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            }
            catch
            {


            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            actualizadatostrack();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void time2_Click(object sender, EventArgs e)
        {

        }

        private void btnvolumen_Click(object sender, EventArgs e)
        {
            volumen.Visible = true;
        }

        private void volumen_MouseLeave(object sender, EventArgs e)
        {
            volumen.Visible = false;
        }
    }
}
