namespace Trainvalley2
{
	using System.IO;
    using System.Windows.Forms;

    public sealed partial class ProfileManager : Form
    {
        public ProfileManager()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var savegame = RegistryHandler.Load();

            if (savegame == null)
            {
                MessageBox.Show("There is no Game to Save!", "Saved game not found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Train Valley Savegame|*.tv2";
                dialog.Title = "Save Train Valley profile";
                dialog.ShowDialog();

                if (!string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    File.WriteAllBytes(dialog.FileName, savegame);
                }
            }

            MessageBox.Show("File saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            var savegame = RegistryHandler.Load();

            if (savegame != null)
            {
                if (MessageBox.Show("I found an active profile, that will be overwritten!\nDo you want to overwrite it?", "Overwrite profile?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }

            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Train Valley Savegame|*.tv2";
                dialog.Title = "Save Train Valley profile";
                dialog.ShowDialog();

                if (!string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    RegistryHandler.Save(File.ReadAllBytes(dialog.FileName));
                }
            }

            MessageBox.Show("Profile loaded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            var savegame = RegistryHandler.Load();

            if (savegame == null)
            {
                MessageBox.Show("There is no saved Profile!", "No Savegame found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("The active profile will be DELETED!\nDo you want to DELETE the active profile?\n", "DELETE profile?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            RegistryHandler.Save(null);
        }
    }
}