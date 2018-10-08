package android.support.v4.media;

import android.os.Bundle;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import com.google.android.gms.nearby.messages.Strategy;

@RestrictTo({Scope.LIBRARY_GROUP})
public class MediaBrowserCompatUtils {
    public static boolean areSameOptions(Bundle bundle, Bundle bundle2) {
        return bundle == bundle2 ? true : bundle == null ? bundle2.getInt(MediaBrowserCompat.EXTRA_PAGE, -1) == -1 && bundle2.getInt(MediaBrowserCompat.EXTRA_PAGE_SIZE, -1) == -1 : bundle2 == null ? bundle.getInt(MediaBrowserCompat.EXTRA_PAGE, -1) == -1 && bundle.getInt(MediaBrowserCompat.EXTRA_PAGE_SIZE, -1) == -1 : bundle.getInt(MediaBrowserCompat.EXTRA_PAGE, -1) == bundle2.getInt(MediaBrowserCompat.EXTRA_PAGE, -1) && bundle.getInt(MediaBrowserCompat.EXTRA_PAGE_SIZE, -1) == bundle2.getInt(MediaBrowserCompat.EXTRA_PAGE_SIZE, -1);
    }

    public static boolean hasDuplicatedItems(Bundle bundle, Bundle bundle2) {
        int i;
        int i2;
        int i3 = bundle == null ? -1 : bundle.getInt(MediaBrowserCompat.EXTRA_PAGE, -1);
        int i4 = bundle2 == null ? -1 : bundle2.getInt(MediaBrowserCompat.EXTRA_PAGE, -1);
        int i5 = bundle == null ? -1 : bundle.getInt(MediaBrowserCompat.EXTRA_PAGE_SIZE, -1);
        int i6 = bundle2 == null ? -1 : bundle2.getInt(MediaBrowserCompat.EXTRA_PAGE_SIZE, -1);
        if (i3 == -1 || i5 == -1) {
            i3 = Integer.MAX_VALUE;
            i5 = 0;
        } else {
            i3 *= i5;
            i5 = i3;
            i3 = (i3 + i5) - 1;
        }
        if (i4 == -1 || i6 == -1) {
            i = Strategy.TTL_SECONDS_INFINITE;
            i2 = 0;
        } else {
            i2 = i6 * i4;
            i = (i2 + i6) - 1;
        }
        return (i5 > i2 || i2 > i3) ? i5 <= i && i <= i3 : true;
    }
}
