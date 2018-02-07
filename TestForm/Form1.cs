using System;
using System.IO;
using System.Net;
using System.Text;
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
            string Url = "http://localhost:56985/Service1.svc/GetData";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            // declare httpwebrequet wrt url defined above
            WebRequest request = WebRequest.Create(Url);
            // set method as post
            request.Method = "POST";
            // set content type
            request.ContentType = "application/x-www-form-urlencoded";
            // set content length
            request.ContentLength = data.Length;
            // get stream data out of webrequest object
            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();
        }
    }
}
