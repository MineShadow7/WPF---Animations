using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace WPF___Animations
{
   
    public partial class MainWindow : Window
    {
        DispatcherTimer myTimer;
        int TimeStep = 100; // ms
        MyGraphic myGraphic;
        public MainWindow()
        {
            InitializeComponent();

            myGraphic = new MyGraphic();
            this.AddChild(myGraphic);

            myGraphic.myObject.LoadImage();

            myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromMilliseconds(TimeStep);
            myTimer.Tick += MyTimer_Tick;
            myTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            myGraphic.ReDraw();
            myGraphic.myObject.MoveObjects();
        }
    }
}
