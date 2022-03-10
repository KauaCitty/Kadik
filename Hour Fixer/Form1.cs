using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;


namespace Hour_Fixer
{
    public partial class Form1 : Form
    {
        public string ipv4;
        public bool Once = false, Once2 = false, IniciandoComWindows = false;
        public RegistryKey Reg;
   
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //
            bool Startup = Convert.ToBoolean(System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Visual Studio 2013\Projects\Hour Fixer\inicializar.txt"));

            checkBox1.Checked = Startup;

            if (Startup)
            {
                IniciandoComWindows = true;
                Reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                Reg.SetValue("Hour Fixer", Application.ExecutablePath.ToString());
            }
            else
            {
                IniciandoComWindows = false;
                Reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                Reg.SetValue("Hour Fixer", Application.ExecutablePath.ToString());

                Reg.DeleteValue("Hour Fixer", false);
                Registry.CurrentUser.DeleteValue("Hour Fixer", false);
            }
            //

            /*/ Start the child process.
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = @"D:\xampp\htdocs\Horario\loadchat.php";
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            /*/
        }

        string GetLine(string fileName, int line)
        {
            using (var sr = new StreamReader(fileName))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           if (Once == false)
           {
               Once = true;
                /*/
                   var host = Dns.GetHostEntry(Dns.GetHostName());
                   foreach (var ip in host.AddressList)
                   {
                       if (ip.AddressFamily == AddressFamily.InterNetwork)
                       {
                           ipv4 = ip.ToString();
                       }
                   }

                  webBrowser2.Url = new System.Uri("http://" + ipv4 + "/phpmyadmin/tbl_sql.php?db=mysql&table=time", System.UriKind.Absolute);
                   /*/

              //  webBrowser2.Url = new System.Uri("http://localhost/phpmyadmin/tbl_sql.php?db=mysql&table=time", System.UriKind.Absolute);

            }
         else
            {
                Console.WriteLine(webBrowser2.Url);
                timer2.Enabled =true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\xampp\htdocs\Horario\a.html", webBrowser2.Document.GetElementsByTagName("HTML")[0].OuterHtml);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (webBrowser2.Document.GetElementsByTagName("HTML")[0].OuterHtml.Contains("Código de erro: 200"))
            {
                Console.WriteLine("refresh 2");

                webBrowser2.Url = webBrowser2.Url;
            }
            else
            {
                Console.WriteLine("enviando 2");

                webBrowser2.Select();
                var links = webBrowser2.Document.GetElementsByTagName("textarea");
                links[0].Focus();

                SendKeys.Send("ALTER TABLE time DROP COLUMN post_date; ALTER TABLE time ADD post_date TIMESTAMP on update CURRENT_TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP AFTER Data;");

                timer3.Enabled = true;
            }

            timer2.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            webBrowser2.Document.GetElementById("button_submit_query").InvokeMember("Click");
            timer4.Enabled = true;

            timer3.Enabled = false;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            webBrowser2.Select();

            SendKeys.Send("{ENTER}");

            timer5.Enabled = true;

            timer4.Enabled = false;
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Visual Studio 2013\Projects\Hour Fixer\document2.txt", webBrowser2.Document.GetElementsByTagName("HTML")[0].OuterHtml);

            var post_time = GetLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Visual Studio 2013\Projects\Hour Fixer\document2.txt", 974);
            var post_time_date = post_time.Substring(post_time.IndexOf("<span>") + 6, post_time.IndexOf("</span>") - post_time.IndexOf("<span>") - 6);
            Console.WriteLine(post_time_date);

            var HoraCurrent = post_time_date.Substring(post_time_date.IndexOf("-") + 7, post_time_date.Length - (post_time_date.IndexOf("-") + 7));

            Console.WriteLine("Hora: "+HoraCurrent);

            var DataCurrent = post_time_date.Substring(0, post_time_date.Length - post_time_date.IndexOf("-", 5) - 2);

            Console.WriteLine("Data: " +DataCurrent);

            label1.Text = "Hora: " + HoraCurrent + Environment.NewLine + "Data: " + DataCurrent;

            timer5.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

                if (checkBox1.Checked)
                {
                    IniciandoComWindows = true;
                    Reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    Reg.SetValue("Hour Fixer", Application.ExecutablePath.ToString());

                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Visual Studio 2013\Projects\Hour Fixer\inicializar.txt", "true");

                }
                else
                {
                    IniciandoComWindows = false;
                    Reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    Reg.SetValue("Hour Fixer", Application.ExecutablePath.ToString());

                    Reg.DeleteValue("Hour Fixer", false);
                    Registry.CurrentUser.DeleteValue("Hour Fixer", false);

                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Visual Studio 2013\Projects\Hour Fixer\inicializar.txt", "false");
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
              webBrowser2.Url = new System.Uri("http://"+textBox1.Text+"/phpmyadmin/tbl_sql.php?db=mysql&table=time", System.UriKind.Absolute);
        }
    }
}
