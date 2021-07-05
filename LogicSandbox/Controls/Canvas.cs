namespace Maxstupo.LogicSandbox.Controls {

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    /// <summary>
    /// Provides a surface suitable for drawing 2D graphics, with zoom and pan functionality.
    /// </summary>
    public class Canvas : Panel {

        #region Panning Properties/Variables

        /// <summary>
        /// The mouse button used for panning. Set to <see cref="MouseButtons.None"/> to disable panning functionality via the mouse.
        /// </summary>
        public MouseButtons PanButton { get; set; } = MouseButtons.Middle;

        /// <summary>
        /// If true, the canvas is panning.
        /// </summary>
        [Browsable(false)]
        public bool IsPanning { get; private set; }

        /// <summary>
        /// The offset along the x-axis for panning.
        /// </summary>
        [Browsable(false)]
        public float PanPositionX { get; set; } = 0;

        /// <summary>
        /// The offset along the y-axis for panning.
        /// </summary>
        [Browsable(false)]
        public float PanPositionY { get; set; } = 0;

        /// <summary>
        /// The x-axis point when starting to pan.
        /// </summary>
        protected float panningOriginX = 0;

        /// </summary> 
        /// The y-axis point when starting to pan.
        /// </summary>
        protected float panningOriginY = 0;

        #endregion

        #region Zoom Properties/Variables 

        /// <summary>
        /// If enabled, the scroll wheel will adjust zoom level.
        /// </summary>
        public bool ScrollWheelZoom { get; set; } = true;

        /// <summary>
        /// The sensitivity of the scroll wheel zoom. 
        /// </summary>
        public float ScrollWheelMultiplier { get; set; } = 0.03f;

        /// <summary>
        /// If true, scroll wheel zoom direction will be flipped.
        /// </summary>
        public bool InvertedScrollWheel { get; set; } = false;

        /// <summary>
        /// If true, zooming will focus on mouse cursors location.
        /// </summary>
        public bool ZoomMouseFocus { get; set; } = true;

        private float zoomMinimum = 0.05f;
        /// <summary>
        /// The minimum zoom level allowed.
        /// </summary>
        public float ZoomMinimum {
            get => zoomMinimum;
            set {
                zoomMinimum = value;
                Zoom = zoom;
            }
        }

        private float zoomMaximum = 5f;
        /// <summary>
        /// The maximum zoom level allowed.
        /// </summary>
        public float ZoomMaximum {
            get => zoomMaximum;
            set {
                zoomMaximum = value;
                Zoom = zoom;
            }
        }

        private float zoom = 1f;
        /// <summary>
        /// The current zoom level of the canvas.
        /// Value will be constrained between <see cref="ZoomMinimum"/> and <see cref="ZoomMaximum"/>.
        /// </summary>
        public float Zoom {
            get => zoom;
            set {
                float zoomMouseX = ZoomMouseFocus ? MouseX : (Width / 2f);
                float zoomMouseY = ZoomMouseFocus ? MouseY : (Height / 2f);

                float zmx = ScreenToWorldX(zoomMouseX);
                float zmy = ScreenToWorldY(zoomMouseY);

                zoom = Math.Max(ZoomMinimum, Math.Min(value, ZoomMaximum));

                zmx = WorldToScreenX(zmx);
                zmy = WorldToScreenY(zmy);

                PanPositionX += (zoomMouseX - zmx) / Zoom;
                PanPositionY += (zoomMouseY - zmy) / Zoom;

                OnZoom?.Invoke(this, EventArgs.Empty);

                Refresh();
            }
        }

        #endregion

        #region Mouse Position Properties

        /// <summary>
        /// The x position of the mouse cursor, in screen space.
        /// </summary>
        [Browsable(false)]
        public float MouseX { get; private set; }

        /// <summary>
        /// The y position of the mouse cursor, in screen space.
        /// </summary>
        [Browsable(false)]
        public float MouseY { get; private set; }

        /// <summary>
        /// The x position of the mosue cursor, in world space.
        /// </summary>
        [Browsable(false)]
        public float MouseWorldX => ScreenToWorldX(MouseX);

        /// <summary>
        /// The y position of the mosue cursor, in world space.
        /// </summary>
        [Browsable(false)]
        public float MouseWorldY => ScreenToWorldY(MouseY);

        #endregion

        #region Custom Canvas Events

        /// <summary>
        /// Occurs when panning starts.
        /// </summary>
        public event EventHandler OnPanStart;

        /// <summary>
        /// Occurs when panning (every MouseMove event).
        /// </summary>
        public event EventHandler OnPanning;

        /// <summary>
        /// Occurs when panning ends.
        /// </summary>
        public event EventHandler OnPanEnd;

        /// <summary>
        /// Occurs when the zoom level changes.
        /// </summary>
        public event EventHandler OnZoom;

        #endregion

        public bool GridVisible { get; set; } = true;
        public int GridSize { get; set; } = 20;

        public int GridLimit { get; set; } = 25000;

        /// <summary>
        /// The previous width of the <see cref="Canvas"/> as of the last resize event.
        /// </summary>
        protected float previousWidth;

        /// <summary>
        /// The previous height of the <see cref="Canvas"/> as of the last resize event.
        /// </summary>
        protected float previousHeight;

        public Canvas() {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.FromArgb(240, 240, 240);

            // Registering Events
            MouseWheel += Canvas_MouseWheel;
            MouseMove += Canvas_MouseMove;
            MouseDown += Canvas_MouseDown;
            MouseUp += Canvas_MouseUp;

            Paint += Canvas_Paint;
            Resize += Canvas_Resize;
        }

        /// <summary>
        /// Apply the scale and translation transforms to the specified <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The graphics object to apply the transforms.</param>
        public virtual void ApplyTransformsTo(Graphics g) {
            g.ScaleTransform(Zoom, Zoom);
            g.TranslateTransform(PanPositionX, PanPositionY);
        }

        #region Events

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            ApplyTransformsTo(g);

            if (GridVisible) {
                int gridWidth = (int) (Width / GridSize * 1f / Zoom) + 2;
                int gridHeight = (int) (Height / GridSize * 1f / Zoom) + 2;

                if (gridWidth * gridHeight < GridLimit) {
                    int minX = (int) -Math.Ceiling(PanPositionX / GridSize) - 1;
                    int minY = (int) -Math.Ceiling(PanPositionY / GridSize) - 1;
                    for (int x = minX; x <= minX + gridWidth; x++) {
                        for (int y = minY; y <= minY + gridHeight; y++) {
                            g.DrawEllipse(Pens.Gray, x * GridSize, y * GridSize, 1, 1);
                        }
                    }
                }
            }

        }

        private void Canvas_Resize(object sender, EventArgs e) {
            PanPositionX += (Width - previousWidth) / Zoom / 2f;
            PanPositionY += (Height - previousHeight) / Zoom / 2f;
            previousWidth = Width;
            previousHeight = Height;
        }

        #region Mouse Events

        private void Canvas_MouseDown(object sender, MouseEventArgs e) {
            if (PanButton != MouseButtons.None && e.Button == PanButton)
                StartPan(e.Location.X, e.Location.Y);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e) {
            MouseX = e.Location.X;
            MouseY = e.Location.Y;

            if (PanButton != MouseButtons.None && e.Button == PanButton)
                Pan(MouseX, MouseY);
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e) {
            if (PanButton != MouseButtons.None && e.Button == PanButton)
                EndPan();
        }

        private void Canvas_MouseWheel(object sender, MouseEventArgs e) {
            if (ScrollWheelZoom && !IsPanning)
                Zoom += Math.Sign(e.Delta) * (InvertedScrollWheel ? 1f : -1f) * ScrollWheelMultiplier;
        }

        #endregion

        #endregion

        #region Centering Methods

        public void Center() {
            Center(Width, Height);
        }

        public void Center(float x, float y) {
            PanPositionX = x / Zoom / 2f;
            PanPositionY = y / Zoom / 2f;
            Refresh();
        }

        public void Center(float x, float y, float width, float height) {
            float centerX = WorldToScreenX(0);
            float centerY = WorldToScreenY(0);

            float selectionCenterX = WorldToScreenX(x + width / 2f);
            float selectionCenterY = WorldToScreenY(y + height / 2f);

            float dx = selectionCenterX - centerX;
            float dy = selectionCenterY - centerY;

            Center(Width - dx * 2f, Height - dy * 2f);
        }

        public void Center(Rectangle rectangle) {
            Center(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void Center(RectangleF rectangle) {
            Center(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        #endregion

        #region Panning Methods

        public bool StartPan(float x, float y) {
            if (IsPanning)
                return false;

            panningOriginX = x * (1f / Zoom) - PanPositionX;
            panningOriginY = y * (1f / Zoom) - PanPositionY;
            IsPanning = true;

            OnPanStart?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public bool Pan(float x, float y) {
            if (!IsPanning)
                return false;

            PanPositionX = x * (1f / Zoom) - panningOriginX;
            PanPositionY = y * (1f / Zoom) - panningOriginY;

            OnPanning?.Invoke(this, EventArgs.Empty);

            Refresh();
            return true;
        }

        public bool EndPan() {
            if (!IsPanning)
                return false;

            IsPanning = false;
            OnPanEnd?.Invoke(this, EventArgs.Empty);

            return true;
        }

        #endregion

        #region (World Space <--> Screen Space) Converter Methods

        public float WorldToScreenX(float worldX) {
            return (PanPositionX + worldX) * Zoom;
        }

        public float WorldToScreenY(float worldY) {
            return (PanPositionY + worldY) * Zoom;
        }

        public float ScreenToWorldX(float screenX) {
            return (screenX / Zoom) - PanPositionX;
        }

        public float ScreenToWorldY(float screenY) {
            return (screenY / Zoom) - PanPositionY;
        }

        #endregion

        /// <inheritdoc cref="Control.PointToClient(Point)"/>
        /// <returns>A Point that represents the converted x,y position in c</returns>
        public Point PointToClient(float x, float y) {
            return PointToClient(new Point((int) x, (int) y));
        }

    }
}
