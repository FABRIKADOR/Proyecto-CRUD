namespace Proyecto.Views;

public partial class AccelerometerPage : ContentPage
{
    public AccelerometerPage()
    {
        InitializeComponent();
    }

    void OnStartClicked(object sender, EventArgs e)
    {
        if (!Accelerometer.Default.IsMonitoring)
        {
            Accelerometer.Default.ReadingChanged += OnReadingChanged;
            Accelerometer.Default.Start(SensorSpeed.UI);
        }
    }

    void OnStopClicked(object sender, EventArgs e)
    {
        Accelerometer.Default.Stop();
        Accelerometer.Default.ReadingChanged -= OnReadingChanged;
    }

    void OnReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
        var d = e.Reading;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ReadingLabel.Text = $"X: {d.Acceleration.X:F2}  Y: {d.Acceleration.Y:F2}  Z: {d.Acceleration.Z:F2}";
        });
    }
}
