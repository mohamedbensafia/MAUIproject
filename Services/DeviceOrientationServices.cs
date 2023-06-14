using System.Collections;

namespace MyQualityApp.Services;

public partial class DeviceOrientationServices
{

    public QueueBuffer MyQueueBuffer;
    public DeviceOrientationServices()
    { }

    public partial void ConfigureScanner();
    public class QueueBuffer : Queue
    {
        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            // if (Changed != null) Changed(this, EventArgs.Empty);
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public override void Enqueue(object item)
        {
            base.Enqueue(item);
            OnChanged();
        }
    }
}