using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Xacor.Platform.Windows.Properties;

namespace Xacor.Platform.Windows
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();

            HandleCreated += OnHandleCreated;
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            using (var iconStream = new MemoryStream(Resources.AppLogo_AppIcon))
            {
                Icon = new Icon(iconStream);
            }
        }
    }
}