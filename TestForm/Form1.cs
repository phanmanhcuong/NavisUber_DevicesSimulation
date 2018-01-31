using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddDeviceToListBox();
        }

        private void AddDeviceToListBox()
        {
            string directoryPath = "C:\\Users\\Admin\\DevicesLogFiles";
            string [] devicesLogFiles = Directory.GetFiles(directoryPath);
            foreach(string deviceLogFile in devicesLogFiles)
            {
                string deviceIMEI = Path.GetFileNameWithoutExtension(deviceLogFile);
                comboBox1.Items.Add(deviceIMEI);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string line;
            string deviceLogFilePath = "C:\\Users\\Admin\\DevicesLogFiles\\" + comboBox1.Text + ".txt";
            StreamReader deviceLogFile = new StreamReader(deviceLogFilePath);
            while((line = deviceLogFile.ReadLine()) != null)
            {
                String lineData = deviceLogFile.ReadLine();
                SendData(lineData);
                
            }
        }

        private void SendData(string postData)
        {
            //send data
            WebRequest request = WebRequest.Create("http://localhost:80");
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            //acttract data
            //string[] cmd = strData.Split(',');
            //if (cmd.Length >= 16)
            //{
            //    string imei = cmd[0].Substring(1);
            //    string strReceivedTime = cmd[1];
            //    string strLon = cmd[2];
            //    string strLat = cmd[3];
            //    string strSpeed = cmd[4];
            //    string strMNC = cmd[5].Substring(1, cmd[5].Length - 2); //cellID: "32CB"
            //    string strLAC = cmd[6].Substring(1, cmd[6].Length - 2); //LacID: "32CB"

            //    string strSOS = cmd[7]; //co sos
            //    string strIsStrongboxOpen = cmd[8]; //co mo ket: dong/mo
            //    string strIsEngineOn = cmd[9]; //co dong co bat/tat
            //    string strIsStoping = cmd[10]; //co dung do dung/do
            //    string strIsGPSLost = cmd[11]; //co GPS mat/co
            //    string strTotalImageCam1 = cmd[12];
            //    string strTotalImageCam2 = cmd[13];
            //    string strRFID = cmd[14];
            //    string strOBD = cmd[15];
            //    string strVersion = cmd[cmd.Length - 2];
            //}

        }
    }
}
