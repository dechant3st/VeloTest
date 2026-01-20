using System.Threading.Tasks;
using Velopack;
using Velopack.Sources;

namespace VelopackTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.PerformClick();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                await UpdateMyApp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        public async Task UpdateMyApp()
        {
            // Point to your GitHub Repository
            var source = new GithubSource("https://github.com/dechant3st/VeloTest", null, false);
            var mgr = new UpdateManager(source);//"https://github.com/dechant3st/VeloTest/releases/latest/download");

            // 1. Check for new version
            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null) return;

            // 2. Download the update
            await mgr.DownloadUpdatesAsync(newVersion);

            // 3. Install and restart
            mgr.ApplyUpdatesAndRestart(newVersion);
        }
    }
}
