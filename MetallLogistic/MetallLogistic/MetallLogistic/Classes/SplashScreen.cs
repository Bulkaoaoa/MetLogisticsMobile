using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MetallLogistic.Classes
{
    public class SplashScreen : ContentPage
    {
        Image SplashImage;
        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var container = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
            };
            SplashImage = new Image
            {
                Source = "Logo.png"
            };
            //AbsoluteLayout.SetLayoutFlags(SplashImage, AbsoluteLayoutFlags.PositionProportional);
            //AbsoluteLayout.SetLayoutBounds(SplashImage, new Rectangle(1, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            container.Children.Add(SplashImage);
            this.Content = container;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await SplashImage.ScaleTo(1, 1200);
            await SplashImage.ScaleTo(0.7, 700, Easing.SpringIn);
            await SplashImage.ScaleTo(1, 800);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}
