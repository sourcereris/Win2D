using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using WinRT.Interop;

using Win2DApp.MyMath;
using Win2DApp.Programs;
using Win2DApp.Utils;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Win2DApp
{
    public sealed partial class MainWindow : Window
    {
        private readonly InputManager _inputManager;

        bool isMousePressed = false;
        Vec2DProjection vec2DProjection;
        Triangle trig = new();
        MVector2 MousePos = MVector2.Zero;
        private MVector2 screenSize = MVector2.Zero;
        public MainWindow()
        {
            InitializeComponent();

            // Option A: keyboard on the canvas
            _inputManager = new InputManager(AnimatedCanvas);

            AnimatedCanvas.SizeChanged += (_, e) =>
            {
                screenSize.x = (float)e.NewSize.Width;
                screenSize.y = (float)e.NewSize.Height;
            };
            // Option B: keyboard on the Window’s content (e.g. if you have other XAML around it)
            // var root = (UIElement)this.Content;
            // _inputManager = new InputManager(GameCanvas, root);

            _inputManager.Initialize();
            SubscribeInputHandler();
        }

        private void AnimatedCanvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());
            ScreenSize.SetSize(sender.ActualWidth, sender.ActualHeight);
            //vec2DProjection = new Vec2DProjection(); //--- 1 ---
        }


        private void AnimatedCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {

        }

        private void AnimatedCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var d = args.DrawingSession;

            d.DrawText($"{screenSize.x}, {screenSize.y}", 300, 330, Colors.White);

        }

        private void AnimatedCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isMousePressed = true;

            //trig.AddVertex(MousePos);
            //vec2DProjection.MousePressed(MousePos); //--- 1 ---
        }

        private void AnimatedCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {   
            var pt = e.GetCurrentPoint(AnimatedCanvas).Position;
            MousePos.x = (float)pt.X; MousePos.y = (float)pt.Y;

            //if(isMousePressed) vec2DProjection.MouseDragged(MousePos); //--- 1 ---
        }

        private void AnimatedCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            isMousePressed = false;

            //vec2DProjection.MouseReleased(); //--- 1 ---
        }
        
        void SubscribeInputHandler()
        { 
            // subscribe just to the bits you care about:
            //_inputManager.KeyDown += OnKeyDown;
            _inputManager.PointerPressed += AnimatedCanvas_PointerPressed;
            _inputManager.PointerMoved += AnimatedCanvas_PointerMoved;
            _inputManager.PointerReleased += AnimatedCanvas_PointerReleased;
        }
        private async Task LoadResourcesAsync(CanvasAnimatedControl sender)
        {
            await Task.CompletedTask;
        }
    }
}
