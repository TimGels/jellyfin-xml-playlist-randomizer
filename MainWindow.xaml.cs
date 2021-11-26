using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace Jellyfin_xml_playlist_randomizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string XML;
        public MainWindow()
        {
            InitializeComponent();
        }

        public XDocument Randomize()
        {
            //rather keep a static random if u can
            var rand = new Random();

            var xdoc = XDocument.Parse(XML);
            var ele = xdoc.Root.Element("PlaylistItems");

            var shuffle = new XElement("PlaylistItems", ele.Elements().OrderBy(x => rand.Next()));

            ele.ReplaceWith(shuffle);
            return xdoc;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                XML = File.ReadAllText(openFileDialog.FileName);
                
                var memory = new MemoryStream();
                Randomize().Save(memory);
                string xmlText = Encoding.UTF8.GetString(memory.ToArray());
                Debug.WriteLine(xmlText);
                txtEditor.Text = Randomize().ToString();
            }
        }
    }
}
