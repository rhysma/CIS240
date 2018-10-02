using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace SharedPrefExample
{
    [Activity(Label = "SharedPrefExample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ISharedPreferences prefs =
            Android.App.Application.Context.GetSharedPreferences("APP_DATA", FileCreationMode.Private);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //get the button and setup the onclick event
            Button SubmitButton = FindViewById<Button>(Resource.Id.SubmitButton);
            SubmitButton.Click += SubmitButton_Click;

            Button ShowSaved = FindViewById<Button>(Resource.Id.ShowSavedButton);
            ShowSaved.Click += ShowSaved_Click;

        }

        private void SubmitButton_Click(object sender, System.EventArgs e)
        {

            //get the two text box fields
            EditText name = FindViewById<EditText>(Resource.Id.NameBox);
            EditText email = FindViewById<EditText>(Resource.Id.EmailBox);

            //store the information in shared preferences
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("name", name.Text);
            editor.PutString("email", email.Text);

            //write key pairs to SP
            editor.Apply();


            //open the second activity
            var open = new Intent(this, typeof(DisplayDataActivity));
            StartActivity(open);
        }

        private void ShowSaved_Click(object sender, System.EventArgs e)
        {
            //open the second activity
            var open = new Intent(this, typeof(DisplayDataActivity));
            StartActivity(open);
        }


        public void ClearSP()
        {
            //setup the shared preferences file where event items will be stored
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Clear();
            editor.Commit();
        }
    }
}

