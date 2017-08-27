using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertPlaylist.App.View
{
    public partial class LoadTransition : UserControl
    {
        public LoadTransition(string action)
        {
            InitializeComponent();

            lblAction.Text = action;
        }
    }
}
