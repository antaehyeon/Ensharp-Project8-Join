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
using System.Windows.Shapes;

namespace Join
{
    /// <summary>
    /// CalendarWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CalendarWindow : Window
    {
        CalendarControl calendarControl = new CalendarControl();
        JoinControl join;

        public CalendarWindow(JoinControl join)
        {
            InitializeComponent();
            CalendarGrid.Children.Add(calendarControl);
            this.join = join;

            calendarControl.MouseDoubleClick += Calendar_MouseDoubleClick;
        }

        private void Calendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((System.Windows.FrameworkElement)e.OriginalSource).DataContext == null) return;

            string selectDate = ((System.Windows.FrameworkElement)e.OriginalSource).DataContext.ToString();
            string[] trimDate = selectDate.Split(new Char[] { '-', ' ' });

            join.txtBox_year.Text = trimDate[0];
            join.txtBox_month.Text = trimDate[1];
            join.txtBox_day.Text = trimDate[2];

            this.Close();

            join.DateChk = true;
            join.btn_calendar.IsEnabled = true;
        }
    }
}
