using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace OpenOCD_Helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string? pathOpenOCD;
        string? pathTarget;
        string? pathInterface;


        public MainWindow()
        {
            InitializeComponent();

            foreach (string? path in Environment.GetEnvironmentVariable("PATH").Split(';'))
            {
                if (Directory.Exists(path))
                {
                    foreach (string files in Directory.GetFiles(path))
                    {
                        if (File.Exists(path + "\\openocd.exe"))
                        {
                            pathOpenOCD = path + "\\openocd.exe";

                            var pathOpenOcdDirectory = path.Substring(0, path.LastIndexOf("\\"));

                            pathInterface = Directory.GetDirectories(pathOpenOcdDirectory, "*.*", searchOption: SearchOption.AllDirectories).Where(res => res.EndsWith("interface")).ToArray().FirstOrDefault();

                            pathTarget = Directory.GetDirectories(pathOpenOcdDirectory, "*.*", searchOption: SearchOption.AllDirectories).Where(res => res.EndsWith("target")).ToArray().FirstOrDefault();

                            return;
                        }
                    }
                }                
            }

            if (pathOpenOCD == null)
            {
                //var result = MessageBox.Show("OpenOCD variable PATH not found \nUse custom path in program");

                return;
            }

            if (pathInterface == null)
            {
                //var result = MessageBox.Show("Interface path not found \nUse custom path in program");
                return;
            }

            if(pathTarget == null)
            {
                //var result = MessageBox.Show("Interface path not found \nUse custom path in program");
                return;
            }


            
        }

        private void CbDevice_DropDownOpened(object sender, EventArgs e)
        {
            string folderPath = "D:\\GD_Flasher\\OpenOCD-20230202-0.12.0\\share\\openocd\\scripts\\target"; // Укажите свой путь к папке
            var cb = sender as ComboBox;

            cb.Items.Clear();

            if (Directory.Exists(folderPath))
            {
                // Получаем все файлы в папке и ее подпапках
                string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

                Debug.WriteLine("Названия вложенных файлов:");

                foreach (string file in files)
                {
                    Debug.WriteLine(file);
                    if (file.EndsWith(".cfg"))
                    {
                        cb.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    
                }
            }
            else
            {
                Debug.WriteLine("Указанной папки не существует.");
            }
        }

        private void CbDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }





        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "D:\\GD_Flasher\\flasher_jlink f4.bat";
            //p.StartInfo.Arguments = "-R C:\\";

            p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    TbLog.Text += e.Data;
                    TbLog.Text += "\n";
                });
                
                Debug.WriteLine(e.Data);
            });
            p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    TbLog.Text += e.Data;
                    TbLog.Text += "\n";
                });
                Debug.WriteLine(e.Data);
            });

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            
        }

        private void ButtonPathCustomOpenOCD_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFolderDialog dialog = new Microsoft.Win32.OpenFolderDialog
            {
                Title = "Select OpenOCD folder"
                
            };
            if (dialog.ShowDialog() == true)
            {
                var test = dialog.FolderName;
            }

        }

        private void ButtonPathCustomTarget_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFolderDialog dialog = new Microsoft.Win32.OpenFolderDialog
            {
                Title = "Select target folder"
            };
            if (dialog.ShowDialog() == true)
            {
                var test = dialog.FolderName;
            }
        }

        private void ButtonPathCustomOpenInterface_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFolderDialog dialog = new Microsoft.Win32.OpenFolderDialog
            {
                Title = "Select interface folder"
            };
            if (dialog.ShowDialog() == true)
            {
                var test = dialog.FolderName;
            }
        }
    }
}