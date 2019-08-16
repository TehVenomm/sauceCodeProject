package android.support.p000v4.app;

import android.app.Activity;
import android.app.ActivityOptions;
import android.app.PendingIntent;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Rect;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.annotation.RequiresApi;
import android.support.p000v4.util.Pair;
import android.view.View;

/* renamed from: android.support.v4.app.ActivityOptionsCompat */
public class ActivityOptionsCompat {
    public static final String EXTRA_USAGE_TIME_REPORT = "android.activity.usage_time";
    public static final String EXTRA_USAGE_TIME_REPORT_PACKAGES = "android.usage_time_packages";

    @RequiresApi(16)
    /* renamed from: android.support.v4.app.ActivityOptionsCompat$ActivityOptionsCompatApi16Impl */
    private static class ActivityOptionsCompatApi16Impl extends ActivityOptionsCompat {
        protected final ActivityOptions mActivityOptions;

        ActivityOptionsCompatApi16Impl(ActivityOptions activityOptions) {
            this.mActivityOptions = activityOptions;
        }

        public Bundle toBundle() {
            return this.mActivityOptions.toBundle();
        }

        public void update(ActivityOptionsCompat activityOptionsCompat) {
            if (activityOptionsCompat instanceof ActivityOptionsCompatApi16Impl) {
                this.mActivityOptions.update(((ActivityOptionsCompatApi16Impl) activityOptionsCompat).mActivityOptions);
            }
        }
    }

    @RequiresApi(23)
    /* renamed from: android.support.v4.app.ActivityOptionsCompat$ActivityOptionsCompatApi23Impl */
    private static class ActivityOptionsCompatApi23Impl extends ActivityOptionsCompatApi16Impl {
        ActivityOptionsCompatApi23Impl(ActivityOptions activityOptions) {
            super(activityOptions);
        }

        public void requestUsageTimeReport(PendingIntent pendingIntent) {
            this.mActivityOptions.requestUsageTimeReport(pendingIntent);
        }
    }

    @RequiresApi(24)
    /* renamed from: android.support.v4.app.ActivityOptionsCompat$ActivityOptionsCompatApi24Impl */
    private static class ActivityOptionsCompatApi24Impl extends ActivityOptionsCompatApi23Impl {
        ActivityOptionsCompatApi24Impl(ActivityOptions activityOptions) {
            super(activityOptions);
        }

        public Rect getLaunchBounds() {
            return this.mActivityOptions.getLaunchBounds();
        }

        public ActivityOptionsCompat setLaunchBounds(@Nullable Rect rect) {
            return new ActivityOptionsCompatApi24Impl(this.mActivityOptions.setLaunchBounds(rect));
        }
    }

    protected ActivityOptionsCompat() {
    }

    @RequiresApi(16)
    private static ActivityOptionsCompat createImpl(ActivityOptions activityOptions) {
        return VERSION.SDK_INT >= 24 ? new ActivityOptionsCompatApi24Impl(activityOptions) : VERSION.SDK_INT >= 23 ? new ActivityOptionsCompatApi23Impl(activityOptions) : new ActivityOptionsCompatApi16Impl(activityOptions);
    }

    public static ActivityOptionsCompat makeBasic() {
        return VERSION.SDK_INT >= 23 ? createImpl(ActivityOptions.makeBasic()) : new ActivityOptionsCompat();
    }

    public static ActivityOptionsCompat makeClipRevealAnimation(View view, int i, int i2, int i3, int i4) {
        return VERSION.SDK_INT >= 23 ? createImpl(ActivityOptions.makeClipRevealAnimation(view, i, i2, i3, i4)) : new ActivityOptionsCompat();
    }

    public static ActivityOptionsCompat makeCustomAnimation(Context context, int i, int i2) {
        return VERSION.SDK_INT >= 16 ? createImpl(ActivityOptions.makeCustomAnimation(context, i, i2)) : new ActivityOptionsCompat();
    }

    public static ActivityOptionsCompat makeScaleUpAnimation(View view, int i, int i2, int i3, int i4) {
        return VERSION.SDK_INT >= 16 ? createImpl(ActivityOptions.makeScaleUpAnimation(view, i, i2, i3, i4)) : new ActivityOptionsCompat();
    }

    public static ActivityOptionsCompat makeSceneTransitionAnimation(Activity activity, View view, String str) {
        return VERSION.SDK_INT >= 21 ? createImpl(ActivityOptions.makeSceneTransitionAnimation(activity, view, str)) : new ActivityOptionsCompat();
    }

    public static ActivityOptionsCompat makeSceneTransitionAnimation(Activity activity, Pair<View, String>... pairArr) {
        if (VERSION.SDK_INT < 21) {
            return new ActivityOptionsCompat();
        }
        android.util.Pair[] pairArr2 = null;
        if (pairArr != null) {
            android.util.Pair[] pairArr3 = new android.util.Pair[pairArr.length];
            for (int i = 0; i < pairArr.length; i++) {
                pairArr3[i] = android.util.Pair.create(pairArr[i].first, pairArr[i].second);
            }
            pairArr2 = pairArr3;
        }
        return createImpl(ActivityOptions.makeSceneTransitionAnimation(activity, pairArr2));
    }

    public static ActivityOptionsCompat makeTaskLaunchBehind() {
        return VERSION.SDK_INT >= 21 ? createImpl(ActivityOptions.makeTaskLaunchBehind()) : new ActivityOptionsCompat();
    }

    public static ActivityOptionsCompat makeThumbnailScaleUpAnimation(View view, Bitmap bitmap, int i, int i2) {
        return VERSION.SDK_INT >= 16 ? createImpl(ActivityOptions.makeThumbnailScaleUpAnimation(view, bitmap, i, i2)) : new ActivityOptionsCompat();
    }

    @Nullable
    public Rect getLaunchBounds() {
        return null;
    }

    public void requestUsageTimeReport(PendingIntent pendingIntent) {
    }

    public ActivityOptionsCompat setLaunchBounds(@Nullable Rect rect) {
        return null;
    }

    public Bundle toBundle() {
        return null;
    }

    public void update(ActivityOptionsCompat activityOptionsCompat) {
    }
}
