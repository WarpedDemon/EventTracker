using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EventTracker
{
    public class eventObject
    {
        public string Name { get; set; }
        public DateTime RecordTime { get; set; }
        public bool Important { get; set; }
        public bool DisplayDelete { get; set; }
        public bool Display { get; set; }
        public Color TextColor { get; set; }

        public eventObject(string name, DateTime recordTime, bool important, bool displayDelete, bool display, Color textColor){
            Name = name;
            RecordTime = recordTime;
            Important = important;
            DisplayDelete = displayDelete;
            Display = display;
            TextColor = textColor;
        }
    }

    public partial class MainPage : ContentPage
    {
        public Color red = Color.FromRgb(166, 31, 52);
        public Color white = Color.FromRgb(240, 255, 240);

        public ObservableCollection<eventObject> EventsListVirtual = new ObservableCollection<eventObject>
        {
            new eventObject("HeadAche", DateTime.Now, false, false, true, Color.FromRgb(105,105,105)),
            new eventObject("HeadAche", DateTime.Now, false, false, true, Color.FromRgb(105,105,105)),
            new eventObject("HeadAche", DateTime.Now, true, true, false, Color.FromRgb(166, 31, 52)),
            new eventObject("HeadAche", DateTime.Now, false, false, true, Color.FromRgb(105,105,105)),
            new eventObject("HeadAche", DateTime.Now, false, false, true, Color.FromRgb(105,105,105)),
            new eventObject("HeadAche", DateTime.Now, true, false, true, Color.FromRgb(166, 31, 52)),
            new eventObject("HeadAche", DateTime.Now, false, false, true, Color.FromRgb(105,105,105)),
            new eventObject("HeadAche", DateTime.Now, false, false, true, Color.FromRgb(105,105,105))
        };

        public MainPage()
        {
            InitializeComponent();

            EventsList.ItemsSource = EventsListVirtual;
        }

        private void ChangeImportance(object sender, ItemTappedEventArgs e)
        {
            int index = EventsListVirtual.IndexOf((sender as MenuItem).BindingContext as eventObject);
            if (EventsListVirtual[index].Important == true)
            {
                EventsListVirtual[index].Important = false;
            }
            else if (EventsListVirtual[index].Important == false)
            {
                EventsListVirtual[index].Important = true;
            }

            colorImportant();
            EventsList.ItemsSource = null;
            EventsList.ItemsSource = EventsListVirtual;
        }

        private void ChangeImportanceItemTapped(object sender, ItemTappedEventArgs e)
        {
            int index = EventsListVirtual.IndexOf(e.Item as eventObject);
            if (EventsListVirtual[index].Important == true)
            {
                EventsListVirtual[index].Important = false;
            }
            else if (EventsListVirtual[index].Important == false)
            {
                EventsListVirtual[index].Important = true;
            }

            colorImportant();
            EventsList.ItemsSource = null;
            EventsList.ItemsSource = EventsListVirtual;
        }

        private void AddButton(object sender, ItemTappedEventArgs e)
        {
            EventsListVirtual.Add(new eventObject("HeadAche", DateTime.Now, false, false, true, Color.FromRgb(105, 105, 105)));
            EventsList.ItemsSource = null;
            EventsList.ItemsSource = EventsListVirtual;
        }

        private async void DeleteButton(object sender, ItemTappedEventArgs e)
        {
            bool answer = await DisplayAlert("ALERT", "Would you like to clear list? ", "Yes", "No");

            if (answer == true)
            {
                EventsListVirtual.Clear();
            }

            colorImportant();
            EventsList.ItemsSource = null;
            EventsList.ItemsSource = EventsListVirtual;
        }

        private async void DeleteItem(object sender, ItemTappedEventArgs e)
        {
            int index = EventsListVirtual.IndexOf((sender as MenuItem).CommandParameter as eventObject);

            bool answer = await DisplayAlert("Danger", "Would you like to delete item? ", "Yes", "No");

            if (answer == true)
            {
                EventsListVirtual.RemoveAt(index);
            }

            colorImportant();
            EventsList.ItemsSource = null;
            EventsList.ItemsSource = EventsListVirtual;
        }

        private async void DeleteItemSwipeButton(object sender, ItemTappedEventArgs e)
        {
            int index = EventsListVirtual.IndexOf((sender as Button).CommandParameter as eventObject);

            bool answer = await DisplayAlert("Danger", "Would you like to delete item? ", "Yes", "No");

            if (answer == true)
            {
                EventsListVirtual.RemoveAt(index);
            }
            else
            {
                EventsListVirtual[index].DisplayDelete = false;
                EventsListVirtual[index].Display = true;
            }

            colorImportant();
            EventsList.ItemsSource = null;
            EventsList.ItemsSource = EventsListVirtual;
        }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            int index = EventsListVirtual.IndexOf(e.Parameter as eventObject);

            Debug.WriteLine("Swiped my lord: Index: " + index);

            if (EventsListVirtual[index].DisplayDelete == false)
            {
                EventsListVirtual[index].DisplayDelete = true;
                EventsListVirtual[index].Display = false;
            }
            else if(EventsListVirtual[index].DisplayDelete == true)
            {
                EventsListVirtual[index].DisplayDelete = false;
                EventsListVirtual[index].Display = true;
            }

            colorImportant();
            EventsList.ItemsSource = null;
            EventsList.ItemsSource = EventsListVirtual;
        }

        private void colorImportant()
        {
            foreach (eventObject e in EventsListVirtual)
            {
                if (e.Important)
                {
                    e.TextColor = Color.FromRgb(166, 31, 52);
                }
                else if(!e.Important)
                {
                    e.TextColor = Color.FromRgb(105, 105, 105);
                }
            }
        }
    }
}
