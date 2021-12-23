using Daily_Blazor_App.Data;
using Parse;

namespace Daily_Blazor_App.Pages
{
    public partial class FetchData
    {
        private Person[] forecasts;
        private Person[] selectedItems;
        private string newFirstName;
        private string newLastName;

        private async void CreatePerson(string firstName, string lastName)
        {
            ParseClient client = new ParseClient(Daily_Blazor_App_Logic.Consts.APP_ID, Daily_Blazor_App_Logic.Consts.APP_URI, Daily_Blazor_App_Logic.Consts.NETKEY);

            // Login is necessary from the SDK, even if public r/w access is active
            // See: https://github.com/parse-community/Parse-SDK-dotNET/issues/337
            // Create this user within the back4app backend
            await client.LogInAsync("demo", "demo");

            // Demo Code
            // Create this class first on the back4app backend
            // And add two Columns
            ParseObject person = new ParseObject("Person");

            // Bind Object to Client
            person.Bind(client);

            // Set some Properties
            person.Set("firstName", firstName);
            person.Set("lastName", lastName);

            // Save
            await person.SaveAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            // Code Samples
            // https://github.com/parse-community/Parse-SDK-dotNET
            ParseClient client = new ParseClient(Daily_Blazor_App_Logic.Consts.APP_ID, Daily_Blazor_App_Logic.Consts.APP_URI, Daily_Blazor_App_Logic.Consts.NETKEY);

            // Login is necessary from the SDK, even if public r/w access is active
            // See: https://github.com/parse-community/Parse-SDK-dotNET/issues/337
            // Create this user within the back4app backend
            await client.LogInAsync("demo", "demo");

            var query = client.GetQuery("Person").OrderByDescending("createdAt");
            IEnumerable<ParseObject>? OnlinePersons = await query.FindAsync();

            List<Person> Persons = new List<Person>();
            
            foreach (ParseObject? onlinePerson in OnlinePersons)
            {
                Person p = new Person();
                p.FirstName = onlinePerson["firstName"].ToString();
                p.LastName = onlinePerson["lastName"].ToString();
                Persons.Add(p);
            }

            forecasts = Persons.ToArray();
        }

        private void AddPerson()
        {
            if (!string.IsNullOrWhiteSpace(newFirstName) && !string.IsNullOrWhiteSpace(newLastName))
            {
                CreatePerson(newFirstName, newLastName);
                newFirstName = string.Empty;
                newLastName = string.Empty;
            }
        }
    }
}
