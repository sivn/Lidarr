using System;
using System.Collections.Generic;
using FluentValidation.Results;
using NzbDrone.Core.ThingiProvider;
using NzbDrone.Core.Music;

namespace NzbDrone.Core.Notifications
{
    public abstract class NotificationBase<TSettings> : INotification where TSettings : IProviderConfig, new()
    {
        protected const string ALBUM_GRABBED_TITLE = "Album Grabbed";
        protected const string TRACK_DOWNLOADED_TITLE = "Track Downloaded";

        protected const string ALBUM_GRABBED_TITLE_BRANDED = "Lidarr - " + ALBUM_GRABBED_TITLE;
        protected const string TRACK_DOWNLOADED_TITLE_BRANDED = "Lidarr - " + TRACK_DOWNLOADED_TITLE;

        public abstract string Name { get; }

        public Type ConfigContract => typeof(TSettings);

        public virtual ProviderMessage Message => null;

        public IEnumerable<ProviderDefinition> DefaultDefinitions => new List<ProviderDefinition>();

        public ProviderDefinition Definition { get; set; }
        public abstract ValidationResult Test();

        public abstract string Link { get; }

        public virtual void OnGrab(GrabMessage grabMessage)
        {

        }

        public virtual void OnDownload(DownloadMessage message)
        {

        }

        public virtual void OnRename(Artist series)
        {

        }

        public bool SupportsOnGrab => HasConcreteImplementation("OnGrab");
        public bool SupportsOnRename => HasConcreteImplementation("OnRename");
        public bool SupportsOnDownload => HasConcreteImplementation("OnDownload");
        public bool SupportsOnUpgrade => SupportsOnDownload;

        protected TSettings Settings => (TSettings)Definition.Settings;

        public override string ToString()
        {
            return GetType().Name;
        }

        public virtual object RequestAction(string action, IDictionary<string, string> query) { return null; }


        private bool HasConcreteImplementation(string methodName)
        {
            var method = GetType().GetMethod(methodName);

            if (method == null)
            {
                throw new MissingMethodException(GetType().Name, Name);
            }

            return !method.DeclaringType.IsAbstract;
        }

    }
}
