using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Petzold.Media3D;

namespace DomeCreator.Core
{
    [Serializable]
    public class Part
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public bool IsGroup { get; set; }
        public string GroupName { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public PartColors PartColor { get; set; }
        public Point3D Position { get; set; }

        public Part(string number, string description) {
            Number = number.Trim();
            Description = description.Trim();
        }
    }

    internal enum PartStatus
    {
        None,
        MouseOver,
        Selected,
        Tracking
    }

    public class PartUI : ModelVisual3D, ISelectable, IGroupable
    {
        #region Events

        //#region SelectionChanged
        //public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
        //                    "SelectionChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PartUI));


        //public event RoutedEventHandler SelectionChanged {
        //    add { this.AddHandler(SelectionChangedEvent, value); }
        //    remove { this.RemoveHandler(SelectionChangedEvent, value); }
        //}
        //#endregion

        //#region PositionChanged
        //public static readonly RoutedEvent PositionChangedEvent = EventManager.RegisterRoutedEvent(
        //                    "PositionChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PartUI));


        //public event RoutedEventHandler PositionChanged {
        //    add { this.AddHandler(PositionChangedEvent, value); }
        //    remove { this.RemoveHandler(PositionChangedEvent, value); }
        //}
        //#endregion

        //private void RaiseEvent(RoutedEvent eventToRaise) {
        //    RoutedEventArgs newArgs = new RoutedEventArgs(eventToRaise);
        //    RaiseEvent(newArgs);
        //}

        #endregion

        #region Members
        private WireLines _edges;
        #endregion

        #region Public Functions
        #endregion
        // CENTER POINT
        //Rect3D bounds = mesh.Bounds;
        //Point3D center = new Point3D(bounds.X + bounds.SizeX / 2, bounds.Y + bounds.SizeY / 2, bounds.Z + bounds.SizeZ / 2);

        //#region Overridden Methods

        //protected override int Visual3DChildrenCount {
        //    get { return _children.Count; }
        //}

        //protected override Visual3D GetVisual3DChild(int index) { return _children[index]; }

        //protected override void OnUpdateModel() {
        //    this.Model = CreatePartModel();
        //}

        //#endregion

        #region Properties

        public Guid Id { get; set; }
        public string PartDescription { get; private set; }

        #region ParentId
        public Guid ParentId {
            get { return (Guid)GetValue(ParentIdProperty); }
            set { SetValue(ParentIdProperty, value); }
        }
        public static readonly DependencyProperty ParentIdProperty = DependencyProperty.Register("ParentId", typeof(Guid), typeof(PartUI));
        #endregion

        #region IsGroup
        public bool IsGroup {
            get { return (bool)GetValue(IsGroupProperty); }
            set { SetValue(IsGroupProperty, value); }
        }
        public static readonly DependencyProperty IsGroupProperty = DependencyProperty.Register("IsGroup", typeof(bool), typeof(PartUI));
        #endregion

        #region GroupName
        public string GroupName {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }
        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.Register("GroupName", typeof(string), typeof(PartUI));
        #endregion

        #region Part Number
        public string PartNumber {
            get { return (string)GetValue(PartNumberProperty); }
            set { SetValue(PartNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PartNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PartNumberProperty =
            DependencyProperty.Register("PartNumber", typeof(string), typeof(PartUI), new PropertyMetadata(PartPropertyChanged));

        private static void PartPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            //((PartUI)d).InvalidateModel();
            PartUI p = d as PartUI;
            if (p != null) p.Content = p.CreatePartModel();
        }

        #endregion

        #region Part Color
        public PartColors PartColor {
            get { return (PartColors)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PartNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("PartColor", typeof(PartColors), typeof(PartUI), new PropertyMetadata(ColorPropertyChanged));

        private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PartUI s = (PartUI)d;
            if (s.Visual3DModel != null) {
                s.PartColor = (PartColors)e.NewValue;
                Model3DGroup mg = (Model3DGroup)s.Visual3DModel;
                DiffuseMaterial mat = ((GeometryModel3D)mg.Children[0]).Material as DiffuseMaterial;

                PartColor newColor = KnownPartColors.GetColor(s.PartColor);
                if (mat != null) mat.Brush = newColor.Color;
            }
        }
        #endregion

        #region Position

        public Point3D Position {
            get { return (Point3D)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PartNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point3D), typeof(PartUI), new PropertyMetadata(PositionPropertyChanged));

        private static void PositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PartUI s = (PartUI)d;
            s.Position = (Point3D)e.NewValue;

            TranslateTransform3D trans = s.Transform as TranslateTransform3D;

            if (trans != null) {
                trans.OffsetX = s.Position.X;
                trans.OffsetY = s.Position.Y;
                trans.OffsetZ = s.Position.Z;
            }
            else {
                s.Transform = null;
                s.Transform = new TranslateTransform3D(s.Position.X, s.Position.Y, s.Position.Z);
            }
            // DO NOT RAISE THIS EVENT HERE.
            //s.RaiseEvent(PositionChangedEvent);
        }
        #endregion

        #region Rotation

        //public double AngleX {
        //    get { return (double)GetValue(AngleXProperty); }
        //    set { SetValue(AngleXProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PartNumber.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty AngleXProperty =
        //    DependencyProperty.Register("AngleX", typeof(double), typeof(PartUI), new PropertyMetadata(AngleXPropertyChanged));

        //private static void AngleXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        //    PartUI s = (PartUI)d;
        //    s.AngleX = (double)e.NewValue;

        //    RotateTransform3D rotateX = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), s.AngleX));
        //    s.Transform = rotateX;
        //    s._edges.Transform = rotateX;

        //    // TODO: RAISE THIS EVENT HERE.
        //}
        #endregion

        #region IsSelected
        public bool IsSelected {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PartNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(PartUI), new PropertyMetadata(IsSelectedPropertyChanged));

        private static void IsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            PartUI s = (PartUI)d;
            s.IsSelected = (bool)e.NewValue;

            if (s.IsSelected)
                s.ChangeApperence(PartStatus.Selected);
            else
                s.ChangeApperence(PartStatus.None);

            //s.RaiseEvent(SelectionChangedEvent);
        }
        #endregion

        #region Model
        //public Model3D Model {
        //    get { return (Model3D)GetValue(ModelProperty); }
        //    set { SetValue(ModelProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PartNumber.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ModelProperty =
        //    DependencyProperty.Register("Model", typeof(Model3D), typeof(PartUI), new PropertyMetadata(ModelPropertyChanged));

        //private static void ModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        //    PartUI s = (PartUI)d;
        //    s.Model = (Model3D)e.NewValue;

        //    s.Visual3DModel = s.Model;
        //}
        #endregion

        #endregion

        #region ctor

        public PartUI(string partNumber, string description) : this(partNumber, description, PartColors.Red) { }
        public PartUI(string partNumber, string description, PartColors color) {
            Id = Guid.NewGuid();
            ParentId = Guid.Empty;
            PartColor = color;

            // Get the instance of Visual3DCollection
            //_children = (Visual3DCollection)typeof(Visual3DCollection).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null,
            //        new[] { typeof(Visual3D) }, null).Invoke(new object[] { this });

            PartDescription = description;
            PartNumber = partNumber.Trim();
        }

        #endregion

        #region Private Functions

        private T FindParentControl<T>() where T : DependencyObject {
            T parent = null;
            DependencyObject innerDepObj = this;

            while (innerDepObj != null) {
                innerDepObj = VisualTreeHelper.GetParent(innerDepObj);

                if (innerDepObj is T) {
                    parent = innerDepObj as T;
                    break;
                }
            }

            return parent;
        }

        private void ChangeApperence(PartStatus partStatus) {
            Model3DGroup mg = (Model3DGroup)Visual3DModel;
            if (mg == null) return;

            //DiffuseMaterial mat = ((GeometryModel3D)mg.Children[0]).Material as DiffuseMaterial;

            switch (partStatus) {
                case PartStatus.MouseOver:
                    _edges.Color = Colors.Purple;
                    break;
                case PartStatus.Selected:
                    _edges.Color = Colors.LimeGreen;
                    break;
                case PartStatus.Tracking:
                    //mat.Brush.Opacity = 0.5;
                    break;
                default:
                    _edges.Color = Colors.Black;
                    //mat.Brush.Opacity = 1.0;
                    break;
            }
        }

        private Model3DGroup CreatePartModel() {

            if (string.IsNullOrEmpty(PartNumber)) {
                return null;
            }

            _edges = new WireLines();
            _edges.Thickness = 1.5;

            // Create a model to hold the part geometry
            Model3DGroup model = new Model3DGroup();

            // Create the Geometery for main mesh.
            MeshGeometry3D mesh = new MeshGeometry3D();
            Material material = SetMaterial(PartColor);
            GeometryModel3D geometry = new GeometryModel3D(mesh, material) {BackMaterial = material};

            model.Children.Add(geometry);

            GeneratePartMesh(PartNumber + ".dat", false, Matrix3D.Identity, model, PartColor);

            // Ldraw follows -Y so invert the part on the Y axis.
            RotateTransform3D invertY = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 180));
            model.Transform = invertY;
            _edges.Transform = invertY;

            //Children.Add(_edges);

            if (model.CanFreeze) {
                model.Freeze();                
            }
            return model;
        }

        [System.Diagnostics.DebuggerStepThrough]
        private Winding ApplyInvert(bool invertNext, Winding direction) {
            if (invertNext) {
                if (direction == Winding.CCW)
                    direction = Winding.CW;
                else if (direction == Winding.CW)
                    direction = Winding.CCW;
            }
            return direction;
        }

        private void GeneratePartMesh(string fileName, bool invertNext, Matrix3D matrix, Model3DGroup partModels, PartColors currentColor) {
            MeshGeometry3D mesh;
            Winding direction = Winding.CCW;

            // If the part color is the same as the main color then re-use the mesh other wise create a new one.
            // This will happen only if the part has two colors like windows & doors with glasses.
            if (currentColor != PartColor)
                mesh = new MeshGeometry3D();
            else
                mesh = (MeshGeometry3D)((GeometryModel3D)partModels.Children[0]).Geometry;

            string[] info = LDrawHelper.ReadFile(fileName);

            foreach (string currentLine in info) {
                string line = currentLine.Trim();

                // if line is blank then ignore.
                if (line.Length == 0) continue;

                char startsWith = line[0];

                switch (startsWith) {
                    case '0': // Comment/Meta Commands
                        MetaCommand cmd = new MetaCommand(line);

                        if (cmd.Type == MetaCommandType.BFC) {

                            if (cmd.Commands[1] != null)
                                direction = (Winding)Enum.Parse(typeof(Winding), cmd.Commands[1]);

                            if (!invertNext)
                                Boolean.TryParse(cmd.Commands[3], out invertNext);
                        }
                        break;
                    case '1': // Sub Part
                        CreateSubpart(invertNext, line, matrix, partModels, currentColor);
                        break;
                    case '2': // Line
                        CreateLine(matrix, line);
                        break;
                    case '3': // Triangle
                        CreateTriangle(invertNext, direction, line, matrix, mesh);
                        break;
                    case '4': // Quadlilateral
                        CreateQuad(invertNext, direction, line, matrix, mesh);
                        break;
                }
            }

            if (currentColor != PartColor && mesh.Positions.Count > 0) {
                Material material = SetMaterial(currentColor);
                GeometryModel3D model = new GeometryModel3D(mesh, material);
                model.BackMaterial = material;

                partModels.Children.Add(model);
            }
        }

        private void CreateSubpart(bool invertNext, string line, Matrix3D matrix, Model3DGroup partModels, PartColors currentColor) {
            SubfileCommand s = new SubfileCommand(line);

            Matrix3D subMat = s.Matrix;

            subMat.Scale(new Vector3D(matrix.M11, matrix.M22, matrix.M33));
            subMat.Translate(new Vector3D(matrix.OffsetX, matrix.OffsetY, matrix.OffsetZ));

            PartColors color = (s.Color == PartColors.Current) ? currentColor : s.Color;
            GeneratePartMesh(s.FileName, invertNext, subMat, partModels, color);

            //if (s.Color == PartColors.Current)
            //    GeneratePartMesh(s.FileName, invertNext, subMat, partModels, currentColor);
            //else
            //    GeneratePartMesh(s.FileName, invertNext, subMat, partModels, s.Color);

            // Reset in invert for the next sub file.
            //invertNext = false;
        }

        private void CreateLine(Matrix3D matrix, string line) {
            LineCommand lineCmd = new LineCommand(line);

            MatrixTransform3D mt = new MatrixTransform3D(matrix);
            mt.Transform(lineCmd.Points);

            _edges.LineCollection.Add(lineCmd.Points[0]);
            _edges.LineCollection.Add(lineCmd.Points[1]);
        }

        private void CreateQuad(bool invertNext, Winding direction, string line, Matrix3D matrix, MeshGeometry3D mesh) {
            QuadrilateralCommand cmd = new QuadrilateralCommand(line);
            Winding dir = ApplyInvert(invertNext, direction);

            MatrixTransform3D mt = new MatrixTransform3D(matrix);
            mt.Transform(cmd.Points);

            if (dir == Winding.CCW) {
                CreateTriangle(cmd.Points[0], cmd.Points[2], cmd.Points[1], mesh);
                CreateTriangle(cmd.Points[0], cmd.Points[3], cmd.Points[2], mesh);
            }
            else {
                CreateTriangle(cmd.Points[0], cmd.Points[1], cmd.Points[2], mesh);
                CreateTriangle(cmd.Points[0], cmd.Points[2], cmd.Points[3], mesh);
            }
        }

        private void CreateTriangle(bool invertNext, Winding direction, string line, Matrix3D matrix, MeshGeometry3D mesh) {
            TriangleCommand cmd = new TriangleCommand(line);

            Winding dir = ApplyInvert(invertNext, direction);

            MatrixTransform3D mt = new MatrixTransform3D(matrix);
            mt.Transform(cmd.Points);

            if (dir == Winding.CCW)
                CreateTriangle(cmd.Points[0], cmd.Points[2], cmd.Points[1], mesh);
            else
                CreateTriangle(cmd.Points[0], cmd.Points[1], cmd.Points[2], mesh);
        }

        private void CreateTriangle(Point3D p0, Point3D p1, Point3D p2, MeshGeometry3D mesh) {
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);

            mesh.TriangleIndices.Add(mesh.Positions.Count - 3); //0
            mesh.TriangleIndices.Add(mesh.Positions.Count - 1); //2
            mesh.TriangleIndices.Add(mesh.Positions.Count - 2); //1

            Vector3D normal = CalculateNormal(p0, p1, p2);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
        }

        private Material SetMaterial(PartColors materialColor) {
            PartColor pc = KnownPartColors.GetColor(materialColor);

            //SolidColorBrush sb = new SolidColorBrush(color.Color);
            //if (color.Alpha > 0)
            //    sb.Opacity = color.Alpha / 255.0;

            return new DiffuseMaterial(pc.Color);
        }

        [System.Diagnostics.DebuggerStepThrough]
        private Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2) {
            Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }

        #endregion
    }
}
