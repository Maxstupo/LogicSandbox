namespace Maxstupo.LogicSandbox.Forms {

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows.Forms;
    using Maxstupo.LogicSandbox.Shapes;
    using Maxstupo.LogicSandbox.Utility.Interaction;

    public partial class FormMain : Form {

        private readonly List<Shape> shapes = new List<Shape>();
        private readonly Selector<Shape> selector;
        private readonly Transformer<Shape> transformer = new Transformer<Shape>();

        public FormMain() {
            InitializeComponent();

            selector = new Selector<Shape>(shapes);
            selector.OnDragging += (s, e) => canvas.Refresh();
            selector.OnEndDrag += Selector_OnEndDrag;

            transformer.OnMoving += (s, e) => canvas.Refresh();

            // TEMP:
            shapes.Add(new RectangleShape(5, -53, 120, 32) {
                CornerRadius = 5,
                BackgroundColor = Color.Teal,
                OutlineColor = Color.Blue,
                OutlineThickness = 2
            });
            // TEMP:
            shapes.Add(new RectangleShape(-112, 4, 32, 32) {
                BackgroundColor = Color.Green,
                OutlineColor = Color.Olive,
                OutlineThickness = 3
            });
            // TEMP:
            shapes.Add(new RectangleShape(5, 65, 33, 48) {
                CornerRadius = 2,
                BackgroundColor = Color.DarkGray,
                OutlineColor = Color.Cyan,
                OutlineThickness = 3
            });

            canvas.Paint += Canvas_Paint;

            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;
        }


        private void FormMain_Load(object sender, EventArgs e) {
            canvas.Center();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            foreach (Shape shape in shapes)
                shape.Draw(g);

            selector.Draw(g);
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {

                Shape shape = GetItemOver(canvas.MouseWorldX, canvas.MouseWorldY);
                if (shape == null) {
                    selector.StartDrag(canvas.MouseWorldX, canvas.MouseWorldY);

                } else {

                    IReadOnlyList<Shape> selectedItems = selector.SelectedItems;

                    bool additiveMode = ModifierKeys.HasFlag(Keys.Control); // Previous selection not cleared.

                    bool containsShape = selectedItems.Contains(shape);

                    if (additiveMode && !containsShape) {
                        transformer.AddItem(shape);
                        selector.Select(shape);

                    } else if ((selectedItems.Count == 1 && selector.SelectedItems[0] != shape) || !containsShape) {
                        selector.DeselectAll();
                    }

                    if (selectedItems.Count == 0) {
                        selector.Select(shape);

                        transformer.Clear();
                        transformer.AddItem(shape);

                    }


                    transformer.StartDrag(canvas.MouseWorldX, canvas.MouseWorldY);
                    canvas.Refresh();
                }

            }

        }

        // TODO: Improve method.
        private Shape GetItemOver(float x, float y) {
            foreach (var a in shapes) {
                if (a.ContainsPoint(x, y))
                    return a;
            }
            return null;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e) {
            selector.Drag(canvas.MouseWorldX, canvas.MouseWorldY);

            transformer.Drag(canvas.MouseWorldX, canvas.MouseWorldY);
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {

                bool additiveMode = ModifierKeys.HasFlag(Keys.Control); // Previous selection not cleared.
                bool inclusiveMode = ModifierKeys.HasFlag(Keys.Shift); // Item must be inside of selection bounds. 

                selector.EndDrag(canvas.MouseWorldX, canvas.MouseWorldY, additiveMode, inclusiveMode);
                transformer.EndDrag();
            }
        }

        private void Selector_OnEndDrag(object sender, List<Shape> selectedItems) {
            transformer.Clear();
            transformer.AddItem(selectedItems);

            canvas.Refresh();
        }

    }

}