
using GoogleMapsProjects1;
using System.Threading.Tasks;

namespace GoogleMapsProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkWater_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void isNatural_CheckedChanged(object sender, EventArgs e)
        {

        }

        //Method events handeler have relation with Button 
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string _type = txtType.Text.Trim();
            bool _iswater = chkWater.Checked;
            bool _isnatural = isNatural.Checked;
            string _Airquality = airQuality.SelectedItem?.ToString() ;//Good is default value
            int _maxResults = (int)numberOfPlaces.Value;

          
            List<string> result = PlaceSearcher.SearchForplace(_type, _iswater, _isnatural, _Airquality, _maxResults);

            if (result == null || result.Count() == 0)
            {
                try
                {
                    // Task.Run protects the UI thread from blocking
                    List<GoogleAPIGet> result1 = await  Task.Run( () => GooglePlaceSearcher.SearchPlacesAsyncAPI(_type, _iswater, _isnatural, _Airquality, _maxResults));
                    result = ConvertFormateTOString.GetstringFormat(result1);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error fetching " + ex.Message);
                    btnSearch.Enabled = true;
                    return;
                }                             
            }
          
            textBox1.Clear();

            foreach (var Line in result)
            {
                textBox1.AppendText(Line + Environment.NewLine+ Environment.NewLine);
            }
        }

        private void listBoxResults_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


}
