using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace PeteBrown.PowerShellMidi
{
    public class MidiDeviceWatcher : IDisposable
    {
        public delegate void MidiDeviceAddedEventHandler(object sender, MidiDeviceAddedEventArgs e);
        public delegate void MidiDeviceRemovedEventHandler(object sender, MidiDeviceRemovedEventArgs e);
        public delegate void MidiDeviceUpdatedEventHandler(object sender, MidiDeviceUpdatedEventArgs e);

        public event MidiDeviceAddedEventHandler MidiDeviceAdded;
        public event MidiDeviceRemovedEventHandler MidiDeviceRemoved;
        public event MidiDeviceUpdatedEventHandler MidiDeviceUpdated;
        public event EventHandler EnumerationCompleted;

        private DeviceWatcher _watcher;

        public MidiDeviceWatcher(DeviceWatcher watcher)
        {
            _watcher = watcher;

            // wire up events

            _watcher.Added += Watcher_Added;
            _watcher.Removed += Watcher_Removed;
            _watcher.Updated += Watcher_Updated;
            _watcher.EnumerationCompleted += Watcher_EnumerationCompleted;
        }

        private void Watcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            var ev = EnumerationCompleted;

            if (ev != null)
                ev(this, EventArgs.Empty);
        }

        public void StartWatching()
        {
            _watcher.Start();
        }

        private void Watcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            var ev = MidiDeviceUpdated;

            if (ev != null)
                ev(this, new MidiDeviceUpdatedEventArgs(args.Id));
        }

        private void Watcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            var ev = MidiDeviceRemoved;

            if (ev != null)
                ev(this, new MidiDeviceRemovedEventArgs(args.Id));
        }

        private void Watcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            MidiDeviceInformation deviceInfo = new MidiDeviceInformation();

            deviceInfo.Id = args.Id;
            deviceInfo.IsDefault = args.IsDefault;
            deviceInfo.IsEnabled = args.IsEnabled;
            deviceInfo.Name = args.Name;

            var ev = MidiDeviceAdded;

            if (ev != null)
                ev(this, new MidiDeviceAddedEventArgs(deviceInfo));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).          


                    _watcher.Updated -= Watcher_Updated;
                    _watcher.Removed -= Watcher_Removed;
                    _watcher.Added -= Watcher_Added;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _watcher.Stop();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MidiDeviceWatcher() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion



    }
}
