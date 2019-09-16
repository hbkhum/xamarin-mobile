using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public enum MenuItemType
    {
        Car,
        Truck
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
