using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrincaChurras.Models;
using TrincaChurras.ViewModels;
using Xamarin.Forms;

namespace TrincaChurras.Views
{
    public partial class ParticipantsPage : ContentPage
    {
        uint duration = 100;
        double openY = (Device.RuntimePlatform == "Android") ? 20 : 60;
        bool isBackdropEnabled = true;

        public double lastPanY =0;
    
        public ParticipantsPage(string id)
        {
            InitializeComponent();
            BindingContext = new ParticipantsViewModel(Navigation, id);
        }

        void PanGestureRecognizer_PanUpdated(System.Object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            if(e.StatusType == GestureStatus.Running)
            {
                isBackdropEnabled = false;
                lastPanY = e.TotalY;

                if(e.TotalY>0)
                {
                    BottomDrawer.TranslationY = openY + e.TotalY;
                }
            }
            else if(e.StatusType == GestureStatus.Completed)
            {
                if(lastPanY < 110)
                {
                    Task.Run(async () =>
                    {
                        await OpenDrawer();
                    });
                }
                else
                {
                    Task.Run(async () =>
                    {
                        await CloseDrawer();
                    });
                }
                
            }
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (isBackdropEnabled)
                Task.Run(async () =>
                {
                    await CloseDrawer();
                });
                        
        }

        void collectionParticipants_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var collection = (CollectionView)sender;

            Task.Run(async () =>
            {
                if (Backdrop.Opacity == 0)
                {
                    await OpenDrawer();
                }
                else
                {
                    await CloseDrawer();
                }
            });

            collection.SelectedItem = null;
        }

        async Task OpenDrawer()
        {
            await Task.WhenAll
            (
                Backdrop.FadeTo(1, length: duration),
                BottomDrawer.TranslateTo(0, openY, length: duration, easing: Easing.SinIn)
            );
            Backdrop.InputTransparent = false;
            isBackdropEnabled = true;
        }

        async Task CloseDrawer()
        {
            await Task.WhenAll
            (
                Backdrop.FadeTo(0, length: duration),
                BottomDrawer.TranslateTo(0, 440, length: duration, easing: Easing.SinIn)
            );
            Backdrop.InputTransparent = true;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Task.Run(async () =>
            {
                await CloseDrawer();
            });
            isBackdropEnabled = false;
        }

    }
}
