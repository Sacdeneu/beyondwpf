using BeyondWPF.Controls.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace BeyondWPF.Controls.Controls
{
    public class ExtendedContentControl : ContentControl
    {
        static ExtendedContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedContentControl), new FrameworkPropertyMetadata(typeof(ExtendedContentControl)));
            HorizontalContentAlignmentProperty.OverrideMetadata(typeof(ExtendedContentControl), new FrameworkPropertyMetadata(HorizontalAlignment.Stretch));
            VerticalContentAlignmentProperty.OverrideMetadata(typeof(ExtendedContentControl), new FrameworkPropertyMetadata(VerticalAlignment.Stretch));
        }

        #region CornerRadius

        public static readonly DependencyProperty CornerRadiusProperty =
            CornerRadiusHelper.CornerRadiusProperty.AddOwner(typeof(ExtendedContentControl));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion
    }
}