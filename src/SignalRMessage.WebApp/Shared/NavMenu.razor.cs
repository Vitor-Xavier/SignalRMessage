using Microsoft.AspNetCore.Components;
using SignalRMessage.WebApp.Crosscutting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRMessage.WebApp.Shared
{
    public partial class NavMenu : ComponentBase
    {
        private bool collapseNavMenu = true;

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private readonly ICollection<Contact> _contacts = new HashSet<Contact>();

        protected override void OnInitialized()
        {
            Console.WriteLine(StateContainer.User.Username);
            _contacts.Add(new() { Username = "admin" });
            _contacts.Add(new() { Username = "vitorxs" });
            _contacts.Add(new() { Username = "vxsouza" });
            if (_contacts.FirstOrDefault(c => c.Username == StateContainer.User.Username) is Contact contact)
                _contacts.Remove(contact);
            base.OnInitialized();
        }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
