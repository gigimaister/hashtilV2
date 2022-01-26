using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Iid;
using System.Collections.Generic;

namespace hashtil
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }
        void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed.
        }

        void SendNotification(string messageBody, string Title, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MessageChat));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            var pendingIntent = PendingIntent.GetActivity(this, ReportTable.NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, ReportTable.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.hashtil_logo3)
                                      .SetContentTitle(Title)
                                      .SetContentText(messageBody)
                                      .SetSound(default)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(ReportTable.NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}