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
            Random rand = new Random();

            XDocument xdoc = XDocument.Parse(XML);
            XElement ele = xdoc.Root.Element("PlaylistItems");

            XElement shuffle = new XElement("PlaylistItems", ele.Elements().OrderBy(x => rand.Next()));

            ele.ReplaceWith(shuffle);
            return xdoc;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                XML = File.ReadAllText(openFileDialog.FileName);
                txtEditor.Text = Randomize().ToString();
            }
        }
    }
}
