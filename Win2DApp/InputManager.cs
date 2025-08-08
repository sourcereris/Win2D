using Microsoft.Graphics.Canvas.UI.Xaml;        // for CanvasControl
using Microsoft.UI.Xaml;                        // for Window
using Microsoft.UI.Xaml.Input;                  // for KeyRoutedEventArgs
using System;
using Windows.Foundation;
using Windows.UI.Core;                          // for PointerEventArgs

namespace Win2DApp
{
    /// <summary>
    /// Central input‐manager.  Hooks Window + CanvasControl events
    /// and re‐exposes them as C# events (delegates).
    /// </summary>
    internal class InputManager
    {
        // Keyboard
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        // Pointer
        public event TypedEventHandler<CanvasAnimatedControl, PointerRoutedEventArgs> PointerPressed;
        public event TypedEventHandler<CanvasAnimatedControl, PointerRoutedEventArgs> PointerMoved;
        public event TypedEventHandler<CanvasAnimatedControl, PointerRoutedEventArgs> PointerReleased;

        private readonly CanvasAnimatedControl _canvas;
        private readonly UIElement _keyboardElement;

        /// <summary>
        /// If you want keyboard on a different element than the canvas (e.g. your Page),
        /// pass it in; otherwise the canvas will handle both.
        /// </summary>
        public InputManager(CanvasAnimatedControl canvas, UIElement keyboardElement = null)
        {
            _canvas = canvas
                ?? throw new ArgumentNullException(nameof(canvas));
            // if no keyboardElement supplied, listen on the canvas itself
            _keyboardElement = keyboardElement ?? canvas;
        }

        public void Initialize()
        {
            // Make sure the canvas can receive key events
            _canvas.IsTabStop = true;
            _canvas.Focus(FocusState.Programmatic);

            // Hook up keyboard
            _keyboardElement.KeyDown += (s, e) => KeyDown?.Invoke(s, e);
            _keyboardElement.KeyUp += (s, e) => KeyUp?.Invoke(s, e);

            // Hook up pointer on the canvas
            _canvas.PointerPressed += (s, e) => PointerPressed?.Invoke(_canvas, e);
            _canvas.PointerMoved += (s, e) => PointerMoved?.Invoke(_canvas, e);
            _canvas.PointerReleased += (s, e) => PointerReleased?.Invoke(_canvas, e);

        }
    }
}
