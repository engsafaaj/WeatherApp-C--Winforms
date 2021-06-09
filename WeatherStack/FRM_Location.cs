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
    public partial class FRM_Location : Form
    {
        Country country;

        // Get Data

        string country_name;
        string city_name;
        string full_name;
        List<string> ListOfCounty = new List<string>();
        public FRM_Location()
        {
            InitializeComponent();
        }

        private async void FRM_Location_Load(object sender, EventArgs e)
        {
            await Task.Run(() => load_Data());
            await Task.Run(() => Set_Data());
            combo_country.DataSource = ListOfCounty;

        }

        private void load_Data()
        {
            HttpHelper helper = new HttpHelper();
            var data = helper.Json_Convert("https://countriesnow.space/api/v0.1/countries");
            country = Newtonsoft.Json.JsonConvert.DeserializeObject<Country>(data);
        }
        private void Set_Data()
        {
           for(int i=0; i < country.data.Length; i++)
            {
                country_name = country.data[i].country;

                for (int j = 0; j < country.data[i].cities.Length; j++)
                {
                    city_name = country.data[i].cities[j];
                    full_name = city_name + ", " + country_name;
                    ListOfCounty.Add(full_name);

                }

            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FullName = combo_country.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Location Saved");
            this.Close();
        }
    }
    } 
