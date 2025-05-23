namespace ScottPlotCookbook.Recipes.Axis;

public class MultiAxis : ICategory
{
    public Chapter Chapter => Chapter.General;
    public string CategoryName => "Multiple Axes";
    public string CategoryDescription => "Tick mark customization and creation of multi-Axis plots";

    public class RightAxis : RecipeBase
    {
        public override string Name => "Right Axis";
        public override string Description => "New plots have one axis on every side. " +
            "Axes on the right and top are invisible by default. " +
            "To use the right axis, make it visible, then tell a plottable to use it. ";

        [Test]
        public override void Execute()
        {
            // plot data with very different scales
            var sig1 = myPlot.Add.Signal(Generate.Sin(mult: 0.01));
            var sig2 = myPlot.Add.Signal(Generate.Cos(mult: 100));

            // tell each signal plot to use a different axis
            sig1.Axes.YAxis = myPlot.Axes.Left;
            sig2.Axes.YAxis = myPlot.Axes.Right;

            // add additional styling options to each axis
            myPlot.Axes.Left.Label.Text = "Left Axis";
            myPlot.Axes.Right.Label.Text = "Right Axis";
            myPlot.Axes.Left.Label.ForeColor = sig1.Color;
            myPlot.Axes.Right.Label.ForeColor = sig2.Color;
        }
    }

    public class MultiAxisQuickstart : RecipeBase
    {
        public override string Name => "Multi-Axis";
        public override string Description => "Additional axes may be added to plots. " +
            "Plottables are displayed using the coordinate system of the primary axes by default, " +
            "but any plottable can be displayed using any X and Y axis.";

        [Test]
        public override void Execute()
        {
            // plottables use the standard X and Y axes by default
            var sig1 = myPlot.Add.Signal(Generate.Sin(51, mult: 0.01));
            sig1.Axes.XAxis = myPlot.Axes.Bottom; // standard X axis
            sig1.Axes.YAxis = myPlot.Axes.Left; // standard Y axis
            myPlot.Axes.Left.Label.Text = "Primary Y Axis";

            // create a second axis and add it to the plot
            var yAxis2 = myPlot.Axes.AddLeftAxis();

            // add a new plottable and tell it to use the custom Y axis
            var sig2 = myPlot.Add.Signal(Generate.Cos(51, mult: 100));
            sig2.Axes.XAxis = myPlot.Axes.Bottom; // standard X axis
            sig2.Axes.YAxis = yAxis2; // custom Y axis
            yAxis2.LabelText = "Secondary Y Axis";
        }
    }

    public class RightAxisOnly : RecipeBase
    {
        public override string Name => "Right Axis Only";
        public override string Description => "The default Y axis is the one on the left of the plot, " +
            "but the right Y axis may be used instead.";

        [Test]
        public override void Execute()
        {
            // add a plottable to the plot
            var sig = myPlot.Add.Signal(Generate.Sin());

            // configure the plottable to use the right Y axis
            sig.Axes.YAxis = myPlot.Axes.Right;

            // configure the grid to display ticks from the right Y axis
            myPlot.Grid.YAxis = myPlot.Axes.Right;

            // style the right axis as desired
            myPlot.Axes.Right.Label.Text = "Hello, Right Axis";
            myPlot.Axes.Right.Label.FontSize = 18;

            // it is recommended to remove tick generators from unused axes
            myPlot.Axes.Left.RemoveTickGenerator();

            // pass in the custom axis when calling SetLimits()
            myPlot.Axes.SetLimitsY(bottom: -2, top: 2, yAxis: myPlot.Axes.Right);
        }
    }
}
