﻿using mobile_poverka_asset.Database;
using mobile_poverka_asset.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_poverka_asset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DB_Search : ContentPage
    {
        public DB_Search()
        {
            InitializeComponent();
            Title = "Поиск по имени пула (Spisok)";
            searchResultsSpisok.ItemsSource = SearchDevice.GetSearchResultsSpisok("",picker.SelectedItem.ToString());
            //BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //_viewModel.OnAppearing();
            //if()
            searchResultsSpisok.ItemsSource = SearchDevice.GetSearchResultsSpisok("", picker.SelectedItem.ToString());
        }
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchResultsSpisok.ItemsSource = SearchDevice.GetSearchResultsSpisok("", picker.SelectedItem.ToString());
            searchBar.Text = "";
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if(searchBar.Text=="")
                searchResultsSpisok.ItemsSource = SearchDevice.GetSearchResultsSpisok(searchBar.Text, picker.SelectedItem.ToString());
            else
                searchResultsSpisok.ItemsSource = SearchDevice.GetSearchResultsSpisok(searchBar.Text, "-1");
        }
    }
}