using Android.App;
using Android.Widget;
using Android.OS;
using static Android.Manifest;
using System.IO;

namespace FileStorage
{
    [Activity(Label = "FileStorage", MainLauncher = true)]
    public class MainActivity : Activity
    {
        string backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "contents.txt");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //get save button
            Button saveButton = FindViewById<Button>(Resource.Id.SaveButton);
            //saveButton.Click += SaveButton_ClickAsync;
            saveButton.Click += SaveButton_Click;

            //get the retrieve button
            Button getButton = FindViewById<Button>(Resource.Id.GetButton);
            getButton.Click += GetButton_Click;

            //get delete button
            Button delButton = FindViewById<Button>(Resource.Id.deleteButton);
            delButton.Click += DelButton_Click;
        }

        private void DelButton_Click(object sender, System.EventArgs e)
        {
            File.Delete(backingFile);
        }

        private void GetButton_Click(object sender, System.EventArgs e)
        {
            Read();   
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            Write();
        }

        private async System.Threading.Tasks.Task Write()
        {
            //get contents of the edit text box
            EditText contents = FindViewById<EditText>(Resource.Id.textBox);

            using (var writer = File.CreateText(backingFile))
            {
                await writer.WriteLineAsync(contents.Text);
            }

        }

        public void Read()
        {
            //get the text control we're writing to
            TextView box = FindViewById<TextView>(Resource.Id.TextViewer);

            //clear the box 
            box.Text = "";

            if (backingFile == null || !File.Exists(backingFile))
            {
                //there is an error to be handled here
            }

            using (var reader = new StreamReader(backingFile, true))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    box.Text += line;
                }
            }
        }

    }
}

