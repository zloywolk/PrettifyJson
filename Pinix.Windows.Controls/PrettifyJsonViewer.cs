using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pinix.Windows.Controls
{
    [TemplatePart(Name = "PART_RootTree", Type = typeof(TreeView))]
    [StyleTypedProperty(Property = "ExpandButtonStyle", StyleTargetType = typeof(ToggleButton))]
    public class PrettifyJsonViewer : Control
    {
        private TreeView _partRootTree;
        static PrettifyJsonViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PrettifyJsonViewer), new FrameworkPropertyMetadata(typeof(PrettifyJsonViewer)));
            DataContextProperty.OverrideMetadata(typeof(PrettifyJsonViewer), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, DataContextChangedCallback));
        }

        /// <summary>
        /// Gets or sets color brush for JSON string values
        /// </summary>
        public Brush StringBrush
        {
            get { return (Brush) GetValue(StringBrushProperty); }
            set { SetValue(StringBrushProperty, value); }
        }

        public static readonly DependencyProperty StringBrushProperty =
            DependencyProperty.Register("StringBrush", typeof (Brush), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(Brushes.Black));

        /// <summary>
        /// Gets or sets color brush for JSON integer values
        /// </summary>
        public Brush IntegerBrush
        {
            get { return (Brush) GetValue(IntegerBrushProperty); }
            set { SetValue(IntegerBrushProperty, value); }
        }

        public static readonly DependencyProperty IntegerBrushProperty =
            DependencyProperty.Register("IntegerBrush", typeof (Brush), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(Brushes.Black));

        /// <summary>
        /// Gets or sets color brush for JSON float/double values
        /// </summary>
        public Brush FloatBrush
        {
            get { return (Brush) GetValue(FloatBrushProperty); }
            set { SetValue(FloatBrushProperty, value); }
        }

        public static readonly DependencyProperty FloatBrushProperty =
            DependencyProperty.Register("FloatBrush", typeof (Brush), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(Brushes.Black));

        /// <summary>
        /// Gets or sets color brush for JSON property name
        /// </summary>
        public Brush PropertyBrush
        {
            get { return (Brush) GetValue(PropertyBrushProperty); }
            set { SetValue(PropertyBrushProperty, value); }
        }

        public static readonly DependencyProperty PropertyBrushProperty =
            DependencyProperty.Register("PropertyBrush", typeof (Brush), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(Brushes.Black));

        /// <summary>
        /// Gets or sets number of decimal digits for double values
        /// </summary>
        public int DoublePrecision
        {
            get { return (int) GetValue(DoublePrecisionProperty); }
            set { SetValue(DoublePrecisionProperty, value); }
        }

        public static readonly DependencyProperty DoublePrecisionProperty =
            DependencyProperty.Register("DoublePrecision", typeof (int), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(0));

        /// <summary>
        ///  Gets or sets whether the nested items in a <see cref="T:System.Windows.Controls.TreeViewItem"/> are expanded or collapsed.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool) GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof (bool), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets created JObject from object
        /// </summary>
        public JToken JsonContent
        {
            get { return (JToken)GetValue(JsonContentProperty); }
            protected set { SetValue(JsonContentPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey JsonContentPropertyKey = DependencyProperty.RegisterReadOnly("JsonContent", typeof(JToken), typeof(PrettifyJsonViewer),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty JsonContentProperty = JsonContentPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets whether the length of arrays are shown in square brackets 
        /// </summary>
        public bool ShowArrayLength
        {
            get { return (bool) GetValue(ShowArrayLengthProperty); }
            set { SetValue(ShowArrayLengthProperty, value); }
        }

        public static readonly DependencyProperty ShowArrayLengthProperty =
            DependencyProperty.Register("ShowArrayLength", typeof (bool), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets or sets font weight for JSON property name
        /// </summary>
        public FontWeight PropertyFontWeight
        {
            get { return (FontWeight) GetValue(PropertyFontWeightProperty); }
            set { SetValue(PropertyFontWeightProperty, value); }
        }

        public static readonly DependencyProperty PropertyFontWeightProperty =
            DependencyProperty.Register("PropertyFontWeight", typeof (FontWeight), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(FontWeights.Normal));

        /// <summary>
        /// Gets or sets whether the nested items in a <see cref="T:System.Windows.Controls.TreeViewItem"/> are shown with icons or not
        /// </summary>
        public bool ShowIcons
        {
            get { return (bool) GetValue(ShowIconsProperty); }
            set { SetValue(ShowIconsProperty, value); }
        }

        public static readonly DependencyProperty ShowIconsProperty =
            DependencyProperty.Register("ShowIcons", typeof (bool), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(false));

        public Style ExpandButtonStyle
        {
            get { return (Style) GetValue(ExpandButtonStyleProperty); }
            set { SetValue(ExpandButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExpandButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpandButtonStyleProperty =
            DependencyProperty.Register("ExpandButtonStyle", typeof (Style), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(null));

        public bool ConnectingLines
        {
            get { return (bool) GetValue(ConnectingLinesProperty); }
            set { SetValue(ConnectingLinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConnectingLines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConnectingLinesProperty =
            DependencyProperty.Register("ConnectingLines", typeof (bool), typeof (PrettifyJsonViewer), new FrameworkPropertyMetadata(true));

        private static void DataContextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = d as PrettifyJsonViewer;
            if (viewer == null || e.NewValue == e.OldValue) return;

            viewer.PrepareJsonData(e.NewValue);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _partRootTree = GetTemplateChild("PART_RootTree") as TreeView;

            _partRootTree?.Items.Clear();
            _partRootTree?.Items.Add(JsonContent);
        }

        public ICommand CopyRgbaToClipboardCommand => new Commands.RelayCommand<object>(p => Clipboard.SetText(p.ToString()));

        private void PrepareJsonData(object value)
        {
            if (value == null || value.GetType() != typeof(string))
            {
                var jObject = JsonConvert.SerializeObject(value, Formatting.Indented);
                JsonContent = JToken.Parse(jObject);
            }
            else
                JsonContent = JToken.Parse((string)value);

            _partRootTree?.Items.Clear();
            _partRootTree?.Items.Add(JsonContent);
        }
    }
}
