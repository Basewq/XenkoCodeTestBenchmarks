using BenchmarkDotNet.Attributes;
using Xenko.UI.Controls;
using Xenko.UI.Panels;
using XenkoCodeTestBenchmarks.UIPanels;

namespace XenkoCodeTestBenchmarks
{
    public class GridTests
    {
        private const int N = 100000;

        private GridOrig gridOrig;
        private GridCacheProperties gridCacheProperties;
        private GridNewStructGrouping gridNewStructGrouping;

        [GlobalSetup]
        public void GlobalSetup()
        {
            gridOrig = new GridOrig
            {
                Name = "gridOrig",
            };
            gridOrig.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Fixed, 10));
            gridOrig.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            gridOrig.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Star));
            gridOrig.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Fixed, 10));
            gridOrig.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            gridOrig.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            PopulateChildrenControls(gridOrig);

            gridCacheProperties = new GridCacheProperties
            {
                Name = "gridCacheProperties",
            };
            gridCacheProperties.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Fixed, 10));
            gridCacheProperties.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            gridCacheProperties.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Star));
            gridCacheProperties.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Fixed, 10));
            gridCacheProperties.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            gridCacheProperties.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            PopulateChildrenControls(gridCacheProperties);

            gridNewStructGrouping = new GridNewStructGrouping
            {
                Name = "gridNew",
            };
            gridNewStructGrouping.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Fixed, 10));
            gridNewStructGrouping.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            gridNewStructGrouping.ColumnDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Star));
            gridNewStructGrouping.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Fixed, 10));
            gridNewStructGrouping.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            gridNewStructGrouping.RowDefinitions.Add(new StripDefinition(Xenko.UI.StripType.Auto));
            PopulateChildrenControls(gridNewStructGrouping);

            void PopulateChildrenControls(GridBase grid)
            {
                var textBlock = new TextBlock()
                {
                    Name = "welcomeText",
                    Margin = new Xenko.UI.Thickness(left: 20, top: 0, right: 20, bottom: 0),
                    TextSize = 42,
                    Text = "Welcome to xenko UI sample. Please name your character",
                    TextAlignment = Xenko.Graphics.TextAlignment.Center,
                    WrapText = true
                };
                textBlock.DependencyProperties.Set(GridBase.RowSpanPropertyKey, 1);

                var editText = new EditText
                {
                    Name = "nameEditText",
                    Margin = new Xenko.UI.Thickness(left: 0, top: 80, right: 0, bottom: 80),
                    Padding = new Xenko.UI.Thickness(left: 30, top: 30, right: 30, bottom: 40),
                    TextSize = 32,
                    MinimumWidth = 340,
                    CaretWidth = 1,
                    MaxLength = 15,
                    Text = "John Doe",
                    HorizontalAlignment = Xenko.UI.HorizontalAlignment.Center,
                    VerticalAlignment = Xenko.UI.VerticalAlignment.Center,
                    TextAlignment = Xenko.Graphics.TextAlignment.Center,
                };
                editText.DependencyProperties.Set(GridBase.RowSpanPropertyKey, 1);

                var textBlockCancel = new TextBlock
                {
                    Name = "cancelText",
                    Text = "Cancel"
                };
                var buttonCancel = new Button()
                {
                    Name = "cancelButton",
                    Padding = new Xenko.UI.Thickness(left: 90, top: 30, right: 25, bottom: 35),
                    Content = textBlockCancel
                };
                buttonCancel.DependencyProperties.Set(GridBase.ColumnSpanPropertyKey, 1);
                buttonCancel.DependencyProperties.Set(GridBase.RowSpanPropertyKey, 2);
                var textBlockOk = new TextBlock
                {
                    Name = "okText",
                    Text = "Ok"
                };
                var buttonOk = new Button()
                {
                    Name = "okButton",
                    Padding = new Xenko.UI.Thickness(left: 90, top: 30, right: 25, bottom: 35),
                    Content = textBlockOk
                };
                buttonOk.DependencyProperties.Set(GridBase.ColumnSpanPropertyKey, 2);
                buttonOk.DependencyProperties.Set(GridBase.RowSpanPropertyKey, 2);

                grid.Children.Add(textBlock);
                grid.Children.Add(editText);
                grid.Children.Add(buttonCancel);
                grid.Children.Add(buttonOk);
            }
        }

        [Benchmark]
        public float Measure_OriginalCode()
        {
            float sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                gridOrig.Width = gridOrig.Width;    // Force MeasureOverride to work
                // ----- Test
                gridOrig.Measure(new Xenko.Core.Mathematics.Vector3(1000));
                // ----- End Test
                sum += gridOrig.DesiredSize.X;
            }
            return sum;
        }

        [Benchmark]
        public float Measure_CachePropertiesOnly()
        {
            float sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                gridCacheProperties.Width = gridCacheProperties.Width;    // Force MeasureOverride to work
                // ----- Test
                gridCacheProperties.Measure(new Xenko.Core.Mathematics.Vector3(1000));
                // ----- End Test
                sum += gridCacheProperties.DesiredSize.X;
            }
            return sum;
        }

        [Benchmark]
        public float Measure_NewStructGrouping()
        {
            float sum = 0;
            for (int ii = 0; ii < N; ii++)
            {
                gridNewStructGrouping.Width = gridNewStructGrouping.Width;      // Force MeasureOverride to work
                // ----- Test
                gridNewStructGrouping.Measure(new Xenko.Core.Mathematics.Vector3(1000));
                // ----- End Test
                sum += gridNewStructGrouping.DesiredSize.X;
            }
            return sum;
        }
    }
}
