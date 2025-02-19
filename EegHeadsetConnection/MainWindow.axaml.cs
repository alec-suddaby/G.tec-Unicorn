using System;
using System.Threading;
using Avalonia.Controls;
using ScottPlot;
using ScottPlot.Avalonia;
using ScottPlot.AxisLimitManagers;
using ScottPlot.AxisPanels;
using ScottPlot.Plottables;
using ScottPlot.TickGenerators;
using ScottPlot.TickGenerators.TimeUnits;

namespace EegHeadsetConnection;

public partial class MainWindow : Window
{
    private AvaPlot avaPlot1;
    private DataLogger Streamer;
    private const int samplesPerSecond = 250;
    private const int displayDuration = 5;
    
    public MainWindow()
    {
        InitializeComponent();

        // add a scatter plot to the plot
        avaPlot1 = this.Find<AvaPlot>("AvaPlot1");
        avaPlot1.UserInputProcessor.Disable();

        Streamer = avaPlot1.Plot.Add.DataLogger();
        // Logger1.Period = 1f / 250f;
        
        // use the right axis (already there by default) for the first logger
        RightAxis axis1 = (RightAxis)avaPlot1.Plot.Axes.Right;
        Streamer.Axes.YAxis = axis1;
        Streamer.Axes.XAxis = (BottomAxis)avaPlot1.Plot.Axes.Bottom;
        
        axis1.Color(Streamer.Color);

        Streamer.ViewSlide();
        
        avaPlot1.Refresh();
        
        Thread thread = new Thread(Loop);
        thread.Start();
    }

    void Loop()
    {
        int maxCount = 5000;
        // int startX = 1;
        
        Thread.Sleep(5000);
        
        Random rand = new Random();
        double timeElapsed = 0;
        double tick = 1000 / samplesPerSecond;
        
        for (int i = 0; i < maxCount; i++)
        {
            Thread.Sleep((int)tick);
            timeElapsed += tick;
            // Logger1.Add(startX + i);
            Streamer.Add(rand.Next(0, 25));
            avaPlot1.Refresh();
        }
    }
}