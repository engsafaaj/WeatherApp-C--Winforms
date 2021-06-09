using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherStack
{
    public partial class Main : Form
    {
        string FullName;
        WeatherData weather;
        public Main()
        {
            InitializeComponent();

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            FRM_Location fRM_Location = new FRM_Location();
            fRM_Location.Show();
        }


        // Load form event
        private async void Main_Load(object sender, EventArgs e)
        {

           await Task.Run(() => load_Data());
            Set_Data();
            
        }
        private void load_Data()
        {
            FullName = Properties.Settings.Default.FullName;
            var url = "http://api.weatherstack.com/current?access_key=0b059063b0ba11604e04fd31779402bb&query=" + FullName;
            HttpHelper helper = new HttpHelper();
            var data = helper.Json_Convert(url);
            weather = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherData>(data);
        }

       private void Set_Data()
        {
            lb_city.Text = weather.request.query;
            lb_des.Text = string.Join("", weather.current.weather_descriptions);
            pic_state.ImageLocation = string.Join("", weather.current.weather_icons);
            lb_tep.Text = weather.current.temperature.ToString();
            lb_wind.Text = weather.current.wind_speed.ToString();
            lb_hum.Text = weather.current.humidity.ToString();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private async void btn_load_Click(object sender, EventArgs e)
        {
            FullName = Properties.Settings.Default.FullName;
            await Task.Run(() => load_Data());
            Set_Data();
        }
    }
}
