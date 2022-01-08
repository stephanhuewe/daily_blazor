using Daily_Blazor_App.Data;

namespace Daily_Blazor_App.Pages
{
    public partial class AddressManagement
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
        private Person currentPerson;
        bool _forceRerender;
    }
}
