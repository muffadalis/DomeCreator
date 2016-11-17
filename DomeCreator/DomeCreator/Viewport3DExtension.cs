using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Collections.ObjectModel;
using LDrawPartLib;

namespace DomeCreator
{
    class Viewport3DExtension {

        public static readonly DependencyProperty ChildrenSourceProperty =
            DependencyProperty.RegisterAttached("ChildrenSource",
                                                typeof(List<ModelVisual3D>), typeof(Viewport3DExtension),
                                                new FrameworkPropertyMetadata(null,
                                                                              new  PropertyChangedCallback(
                                                                                  OnChildrenSourceChanged), new CoerceValueCallback(OnChildrenValueChanged)));

        private static object OnChildrenValueChanged(DependencyObject d, object e) {
            return e;
        }

        private static void OnChildrenSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Viewport3D viewport = d as Viewport3D;
            List<ModelVisual3D> children = e.NewValue as List<ModelVisual3D>;

            if (viewport == null) return;
            if (children == null) return;

            //viewport.Children.CopyTo(children.ToArray(), 0);
            foreach (var child in children) {
                viewport.Children.Add(child);
            }
        }

        public static List<ModelVisual3D> GetChildrenSource(DependencyObject d) {
            return (List<ModelVisual3D>)d.GetValue(ChildrenSourceProperty);
        }

        public static void SetChildrenSource(DependencyObject d, List<ModelVisual3D> value) {
            d.SetValue(ChildrenSourceProperty, value);
        }
    }
}
