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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            UpdateMyApp().GetAwaiter().GetResult();
            button1.Enabled = true;
        }

        public async Task UpdateMyApp()
        {
            // Point to your GitHub Repository
            var source = new GithubSource("https://github.com", null, false);
            var mgr = new UpdateManager(source);

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
