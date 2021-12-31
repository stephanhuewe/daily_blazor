using Daily_Blazor_App.Data;
using MudBlazor;
using Parse;

namespace Daily_Blazor_App.Pages
{
    public partial class FetchData
    {
        private Person[] forecasts;
        private string newFirstName;
        private string newLastName;
        private string nameReverse;
        private string searchText;
        private bool visible;
        private bool detailsVisbile;
        private int rating;
        private HashSet<Person> selectedItems = new HashSet<Person>();
        bool _forceRerender;

        private void OpenDialog() => visible = true;
        void Submit() => visible = false;

        private void OpenDetailDialog() => detailsVisbile = true;
        void SubmitDetail() => detailsVisbile = false;

        private ParseClient GetParseClient()
        {
            ParseClient client = new ParseClient(Daily_Blazor_App_Logic.Consts.APP_ID, Daily_Blazor_App_Logic.Consts.APP_URI, Daily_Blazor_App_Logic.Consts.NETKEY);
            return client;
        }

        private DialogOptions dialogOptions = new() { FullWidth = true };

        private void RowClickEvent(TableRowClickEventArgs<Person> tableRowClickEventArgs)
        {
            OpenDetailDialog();
        }

        /// <summary>
        /// Just some random UI actions
        /// </summary>
        public string NameReverse { 
            get {
                if (!String.IsNullOrEmpty(newLastName))
                {
                    char[] array = newLastName.ToCharArray();
                    Array.Reverse(array);
                    return new string(array);
                }
                return "";
            } set => nameReverse = value; }

        protected async Task SearchPerson()
        {
            // Code Samples
            // https://github.com/parse-community/Parse-SDK-dotNET
            var client = GetParseClient();

            // Login is necessary from the SDK, even if public r/w access is active
            // See: https://github.com/parse-community/Parse-SDK-dotNET/issues/337
            // Create this user within the back4app backend
            await client.LogInAsync("demo", "demo");

            var query = client.GetQuery("Person").WhereContains("lastName", searchText).OrderByDescending("createdAt");
            IEnumerable<ParseObject>? OnlinePersons = await query.FindAsync();

            List<Person> Persons = new List<Person>();

            foreach (ParseObject? onlinePerson in OnlinePersons)
            {
                Person p = new Person();
                p.ObjectId = onlinePerson.ObjectId.ToString();
                p.FirstName = onlinePerson["firstName"].ToString();
                p.LastName = onlinePerson["lastName"].ToString();
                Persons.Add(p);
            }

            forecasts = Persons.ToArray();
        }
            protected async Task DeletePerson(List<string> objectIds)
        {
            var client = GetParseClient();
            await client.LogInAsync("demo", "demo");
            
            foreach (var objectId in objectIds)
            {
                var person = await client.GetQuery("Person").WhereEqualTo("objectId", objectId).FirstAsync();
                await person.DeleteAsync();
            }

            StateHasChanged();
            await OnInitializedAsync();

            int count = objectIds.Count;

            if (count > 0)
            {
                Snackbar.Add($"{count} Person(s) deleted", Severity.Success);
            }
        }

        private async Task CreatePerson(string firstName, string lastName)
        {
            var client = GetParseClient();

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

            StateHasChanged();
            await OnInitializedAsync();

        }

        protected override bool ShouldRender()
        {
            if (_forceRerender)
            {
                _forceRerender = false;
                return true;
            }
            return base.ShouldRender();
        }

        protected override async Task OnInitializedAsync()
        {
            // Code Samples
            // https://github.com/parse-community/Parse-SDK-dotNET
            var client = GetParseClient();

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
                p.ObjectId = onlinePerson.ObjectId.ToString();
                p.FirstName = onlinePerson["firstName"].ToString();
                p.LastName = onlinePerson["lastName"].ToString();
                Persons.Add(p);
            }

            forecasts = Persons.ToArray();
        }

        private async Task AddPerson()
        {
            if (!string.IsNullOrWhiteSpace(newFirstName) && !string.IsNullOrWhiteSpace(newLastName))
            {
                await CreatePerson(newFirstName, newLastName);
                newFirstName = string.Empty;
                newLastName = string.Empty;
            }
            visible = false;

            Snackbar.Add("Person saved", Severity.Success);
        }
    }
}
