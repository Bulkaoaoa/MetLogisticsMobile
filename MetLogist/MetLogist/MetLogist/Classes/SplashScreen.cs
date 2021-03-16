using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MetLogist.Classes
{
    public class SplashScreen : ContentPage
    {
        Image SplashImage;
        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var container = new AbsoluteLayout();
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center,
            //};
            SplashImage = new Image 
            {
                Source = "Logo.png",
                HeightRequest = 200,
                WidthRequest = 200
            };
            AbsoluteLayout.SetLayoutFlags(SplashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SplashImage, new Rectangle(1, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            container.Children.Add(SplashImage);
            this.Content = container;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await SplashImage.ScaleTo(1, 1500);
            await SplashImage.ScaleTo(0.8, 1000, Easing.SpringIn);
            await SplashImage.ScaleTo(1, 1500);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

    }
}
