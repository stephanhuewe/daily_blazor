using Daily_Blazor_App.Data;
using Daily_Blazor_App_Logic;
using MudBlazor;
using Parse;

namespace Daily_Blazor_App.Pages
{
    public partial class AddressManagement
    {
        private Person[] people;
        private string newFirstName;
        private string newLastName;
        private string nameReverse;
        private string searchText;
        private bool visible;
        private bool detailsVisbile;
        private int rating;
        private HashSet<Person> selectedItems = new HashSet<Person>();
        object selectedValue;
        MudListItem selectedItem;
        bool _forceRerender;

        protected override async Task OnInitializedAsync()
        {
            // Code Samples
            // https://github.com/parse-community/Parse-SDK-dotNET
            var client = Helper.GetParseClient();

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

            people = Persons.ToArray();
           
        }
    }
}
