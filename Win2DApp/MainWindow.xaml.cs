using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
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

using Win2DApp.MyMath;
using Win2DApp.Programs;
using Win2DApp.Utils;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Win2DApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly InputManager _inputManager;

        bool isMousePressed = false;
        Vec2DProjection vec2DProjection;
        Triangle trig = new();
        MVector2 MousePos = MVector2.zero;
        public MainWindow()
        {
            InitializeComponent();

            // Option A: keyboard on the canvas
            _inputManager = new InputManager(AnimatedCanvas);

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
            vec2DProjection = new Vec2DProjection();
        }
        private async Task LoadResourcesAsync(CanvasAnimatedControl sender)
        {
            await Task.CompletedTask;
        }
        
        private void AnimatedCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {

        }

        private void AnimatedCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var d = args.DrawingSession;

            MVector2 pos = new (200, 150);

            MVector3 v = new(3, 5, 6);
            MVector3 w = new(-6, 1, -8);

            MVector3 c = MVector3.Cross(v, w);

            var f = MVector3.Dot(c, w);

            trig.DrawTriangle(args);

            vec2DProjection.DrawVectors(d);
        }

        private void AnimatedCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            isMousePressed = true;

            trig.AddVertex(MousePos);
            vec2DProjection.MoussePressed(MousePos);
        }

        private void AnimatedCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {   
            var pt = e.GetCurrentPoint(AnimatedCanvas).Position;
            MVector2 v = new MVector2(pt.X, pt.Y);
            MousePos = v;
            if(isMousePressed) vec2DProjection.MoussePressed(MousePos);
            else vec2DProjection.isMousePessedOnCircle = false;
        }

        private void AnimatedCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            isMousePressed = false;
        }
        
        void SubscribeInputHandler()
        { 
            // subscribe just to the bits you care about:
            //_inputManager.KeyDown += OnKeyDown;
            _inputManager.PointerPressed += AnimatedCanvas_PointerPressed;
            _inputManager.PointerMoved += AnimatedCanvas_PointerMoved;
            _inputManager.PointerReleased += AnimatedCanvas_PointerReleased;
        }
    }
}
