package android.support.p000v4.app;

import android.app.Notification;
import android.app.PendingIntent;
import android.content.Context;
import android.content.res.ColorStateList;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;
import android.graphics.Canvas;
import android.graphics.PorterDuff.Mode;
import android.graphics.PorterDuffColorFilter;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Parcelable;
import android.os.SystemClock;
import android.support.annotation.ColorInt;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.RequiresApi;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.compat.C0004R;
import android.support.p000v4.app.NotificationCompatBase.Action.Factory;
import android.support.p000v4.app.NotificationCompatBase.UnreadConversation;
import android.support.p000v4.app.RemoteInputCompatBase.RemoteInput;
import android.support.p000v4.text.BidiFormatter;
import android.support.p000v4.view.ViewCompat;
import android.text.SpannableStringBuilder;
import android.text.TextUtils;
import android.text.style.TextAppearanceSpan;
import android.widget.RemoteViews;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import org.apache.commons.lang3.StringUtils;

/* renamed from: android.support.v4.app.NotificationCompat */
public class NotificationCompat {
    public static final int BADGE_ICON_LARGE = 2;
    public static final int BADGE_ICON_NONE = 0;
    public static final int BADGE_ICON_SMALL = 1;
    public static final String CATEGORY_ALARM = "alarm";
    public static final String CATEGORY_CALL = "call";
    public static final String CATEGORY_EMAIL = "email";
    public static final String CATEGORY_ERROR = "err";
    public static final String CATEGORY_EVENT = "event";
    public static final String CATEGORY_MESSAGE = "msg";
    public static final String CATEGORY_PROGRESS = "progress";
    public static final String CATEGORY_PROMO = "promo";
    public static final String CATEGORY_RECOMMENDATION = "recommendation";
    public static final String CATEGORY_REMINDER = "reminder";
    public static final String CATEGORY_SERVICE = "service";
    public static final String CATEGORY_SOCIAL = "social";
    public static final String CATEGORY_STATUS = "status";
    public static final String CATEGORY_SYSTEM = "sys";
    public static final String CATEGORY_TRANSPORT = "transport";
    @ColorInt
    public static final int COLOR_DEFAULT = 0;
    public static final int DEFAULT_ALL = -1;
    public static final int DEFAULT_LIGHTS = 4;
    public static final int DEFAULT_SOUND = 1;
    public static final int DEFAULT_VIBRATE = 2;
    public static final String EXTRA_AUDIO_CONTENTS_URI = "android.audioContents";
    public static final String EXTRA_BACKGROUND_IMAGE_URI = "android.backgroundImageUri";
    public static final String EXTRA_BIG_TEXT = "android.bigText";
    public static final String EXTRA_COMPACT_ACTIONS = "android.compactActions";
    public static final String EXTRA_CONVERSATION_TITLE = "android.conversationTitle";
    public static final String EXTRA_INFO_TEXT = "android.infoText";
    public static final String EXTRA_LARGE_ICON = "android.largeIcon";
    public static final String EXTRA_LARGE_ICON_BIG = "android.largeIcon.big";
    public static final String EXTRA_MEDIA_SESSION = "android.mediaSession";
    public static final String EXTRA_MESSAGES = "android.messages";
    public static final String EXTRA_PEOPLE = "android.people";
    public static final String EXTRA_PICTURE = "android.picture";
    public static final String EXTRA_PROGRESS = "android.progress";
    public static final String EXTRA_PROGRESS_INDETERMINATE = "android.progressIndeterminate";
    public static final String EXTRA_PROGRESS_MAX = "android.progressMax";
    public static final String EXTRA_REMOTE_INPUT_HISTORY = "android.remoteInputHistory";
    public static final String EXTRA_SELF_DISPLAY_NAME = "android.selfDisplayName";
    public static final String EXTRA_SHOW_CHRONOMETER = "android.showChronometer";
    public static final String EXTRA_SHOW_WHEN = "android.showWhen";
    public static final String EXTRA_SMALL_ICON = "android.icon";
    public static final String EXTRA_SUB_TEXT = "android.subText";
    public static final String EXTRA_SUMMARY_TEXT = "android.summaryText";
    public static final String EXTRA_TEMPLATE = "android.template";
    public static final String EXTRA_TEXT = "android.text";
    public static final String EXTRA_TEXT_LINES = "android.textLines";
    public static final String EXTRA_TITLE = "android.title";
    public static final String EXTRA_TITLE_BIG = "android.title.big";
    public static final int FLAG_AUTO_CANCEL = 16;
    public static final int FLAG_FOREGROUND_SERVICE = 64;
    public static final int FLAG_GROUP_SUMMARY = 512;
    @Deprecated
    public static final int FLAG_HIGH_PRIORITY = 128;
    public static final int FLAG_INSISTENT = 4;
    public static final int FLAG_LOCAL_ONLY = 256;
    public static final int FLAG_NO_CLEAR = 32;
    public static final int FLAG_ONGOING_EVENT = 2;
    public static final int FLAG_ONLY_ALERT_ONCE = 8;
    public static final int FLAG_SHOW_LIGHTS = 1;
    public static final int GROUP_ALERT_ALL = 0;
    public static final int GROUP_ALERT_CHILDREN = 2;
    public static final int GROUP_ALERT_SUMMARY = 1;
    static final NotificationCompatImpl IMPL;
    public static final int PRIORITY_DEFAULT = 0;
    public static final int PRIORITY_HIGH = 1;
    public static final int PRIORITY_LOW = -1;
    public static final int PRIORITY_MAX = 2;
    public static final int PRIORITY_MIN = -2;
    public static final int STREAM_DEFAULT = -1;
    public static final int VISIBILITY_PRIVATE = 0;
    public static final int VISIBILITY_PUBLIC = 1;
    public static final int VISIBILITY_SECRET = -1;

    /* renamed from: android.support.v4.app.NotificationCompat$Action */
    public static class Action extends android.support.p000v4.app.NotificationCompatBase.Action {
        @RestrictTo({Scope.LIBRARY_GROUP})
        public static final Factory FACTORY = new Factory() {
            public android.support.p000v4.app.NotificationCompatBase.Action build(int i, CharSequence charSequence, PendingIntent pendingIntent, Bundle bundle, RemoteInput[] remoteInputArr, RemoteInput[] remoteInputArr2, boolean z) {
                return new Action(i, charSequence, pendingIntent, bundle, (RemoteInput[]) remoteInputArr, (RemoteInput[]) remoteInputArr2, z);
            }

            public Action[] newArray(int i) {
                return new Action[i];
            }
        };
        public PendingIntent actionIntent;
        public int icon;
        private boolean mAllowGeneratedReplies;
        private final RemoteInput[] mDataOnlyRemoteInputs;
        final Bundle mExtras;
        private final RemoteInput[] mRemoteInputs;
        public CharSequence title;

        /* renamed from: android.support.v4.app.NotificationCompat$Action$Builder */
        public static final class Builder {
            private boolean mAllowGeneratedReplies;
            private final Bundle mExtras;
            private final int mIcon;
            private final PendingIntent mIntent;
            private ArrayList<RemoteInput> mRemoteInputs;
            private final CharSequence mTitle;

            public Builder(int i, CharSequence charSequence, PendingIntent pendingIntent) {
                this(i, charSequence, pendingIntent, new Bundle(), null, true);
            }

            private Builder(int i, CharSequence charSequence, PendingIntent pendingIntent, Bundle bundle, RemoteInput[] remoteInputArr, boolean z) {
                this.mAllowGeneratedReplies = true;
                this.mIcon = i;
                this.mTitle = Builder.limitCharSequenceLength(charSequence);
                this.mIntent = pendingIntent;
                this.mExtras = bundle;
                this.mRemoteInputs = remoteInputArr == null ? null : new ArrayList<>(Arrays.asList(remoteInputArr));
                this.mAllowGeneratedReplies = z;
            }

            public Builder(Action action) {
                this(action.icon, action.title, action.actionIntent, new Bundle(action.mExtras), action.getRemoteInputs(), action.getAllowGeneratedReplies());
            }

            public Builder addExtras(Bundle bundle) {
                if (bundle != null) {
                    this.mExtras.putAll(bundle);
                }
                return this;
            }

            public Builder addRemoteInput(RemoteInput remoteInput) {
                if (this.mRemoteInputs == null) {
                    this.mRemoteInputs = new ArrayList<>();
                }
                this.mRemoteInputs.add(remoteInput);
                return this;
            }

            public Action build() {
                ArrayList arrayList = new ArrayList();
                ArrayList arrayList2 = new ArrayList();
                if (this.mRemoteInputs != null) {
                    Iterator it = this.mRemoteInputs.iterator();
                    while (it.hasNext()) {
                        RemoteInput remoteInput = (RemoteInput) it.next();
                        if (remoteInput.isDataOnly()) {
                            arrayList.add(remoteInput);
                        } else {
                            arrayList2.add(remoteInput);
                        }
                    }
                }
                return new Action(this.mIcon, this.mTitle, this.mIntent, this.mExtras, arrayList2.isEmpty() ? null : (RemoteInput[]) arrayList2.toArray(new RemoteInput[arrayList2.size()]), arrayList.isEmpty() ? null : (RemoteInput[]) arrayList.toArray(new RemoteInput[arrayList.size()]), this.mAllowGeneratedReplies);
            }

            public Builder extend(Extender extender) {
                extender.extend(this);
                return this;
            }

            public Bundle getExtras() {
                return this.mExtras;
            }

            public Builder setAllowGeneratedReplies(boolean z) {
                this.mAllowGeneratedReplies = z;
                return this;
            }
        }

        /* renamed from: android.support.v4.app.NotificationCompat$Action$Extender */
        public interface Extender {
            Builder extend(Builder builder);
        }

        /* renamed from: android.support.v4.app.NotificationCompat$Action$WearableExtender */
        public static final class WearableExtender implements Extender {
            private static final int DEFAULT_FLAGS = 1;
            private static final String EXTRA_WEARABLE_EXTENSIONS = "android.wearable.EXTENSIONS";
            private static final int FLAG_AVAILABLE_OFFLINE = 1;
            private static final int FLAG_HINT_DISPLAY_INLINE = 4;
            private static final int FLAG_HINT_LAUNCHES_ACTIVITY = 2;
            private static final String KEY_CANCEL_LABEL = "cancelLabel";
            private static final String KEY_CONFIRM_LABEL = "confirmLabel";
            private static final String KEY_FLAGS = "flags";
            private static final String KEY_IN_PROGRESS_LABEL = "inProgressLabel";
            private CharSequence mCancelLabel;
            private CharSequence mConfirmLabel;
            private int mFlags = 1;
            private CharSequence mInProgressLabel;

            public WearableExtender() {
            }

            public WearableExtender(Action action) {
                Bundle bundle = action.getExtras().getBundle(EXTRA_WEARABLE_EXTENSIONS);
                if (bundle != null) {
                    this.mFlags = bundle.getInt(KEY_FLAGS, 1);
                    this.mInProgressLabel = bundle.getCharSequence(KEY_IN_PROGRESS_LABEL);
                    this.mConfirmLabel = bundle.getCharSequence(KEY_CONFIRM_LABEL);
                    this.mCancelLabel = bundle.getCharSequence(KEY_CANCEL_LABEL);
                }
            }

            private void setFlag(int i, boolean z) {
                if (z) {
                    this.mFlags |= i;
                } else {
                    this.mFlags &= i ^ -1;
                }
            }

            public WearableExtender clone() {
                WearableExtender wearableExtender = new WearableExtender();
                wearableExtender.mFlags = this.mFlags;
                wearableExtender.mInProgressLabel = this.mInProgressLabel;
                wearableExtender.mConfirmLabel = this.mConfirmLabel;
                wearableExtender.mCancelLabel = this.mCancelLabel;
                return wearableExtender;
            }

            public Builder extend(Builder builder) {
                Bundle bundle = new Bundle();
                if (this.mFlags != 1) {
                    bundle.putInt(KEY_FLAGS, this.mFlags);
                }
                if (this.mInProgressLabel != null) {
                    bundle.putCharSequence(KEY_IN_PROGRESS_LABEL, this.mInProgressLabel);
                }
                if (this.mConfirmLabel != null) {
                    bundle.putCharSequence(KEY_CONFIRM_LABEL, this.mConfirmLabel);
                }
                if (this.mCancelLabel != null) {
                    bundle.putCharSequence(KEY_CANCEL_LABEL, this.mCancelLabel);
                }
                builder.getExtras().putBundle(EXTRA_WEARABLE_EXTENSIONS, bundle);
                return builder;
            }

            public CharSequence getCancelLabel() {
                return this.mCancelLabel;
            }

            public CharSequence getConfirmLabel() {
                return this.mConfirmLabel;
            }

            public boolean getHintDisplayActionInline() {
                return (this.mFlags & 4) != 0;
            }

            public boolean getHintLaunchesActivity() {
                return (this.mFlags & 2) != 0;
            }

            public CharSequence getInProgressLabel() {
                return this.mInProgressLabel;
            }

            public boolean isAvailableOffline() {
                return (this.mFlags & 1) != 0;
            }

            public WearableExtender setAvailableOffline(boolean z) {
                setFlag(1, z);
                return this;
            }

            public WearableExtender setCancelLabel(CharSequence charSequence) {
                this.mCancelLabel = charSequence;
                return this;
            }

            public WearableExtender setConfirmLabel(CharSequence charSequence) {
                this.mConfirmLabel = charSequence;
                return this;
            }

            public WearableExtender setHintDisplayActionInline(boolean z) {
                setFlag(4, z);
                return this;
            }

            public WearableExtender setHintLaunchesActivity(boolean z) {
                setFlag(2, z);
                return this;
            }

            public WearableExtender setInProgressLabel(CharSequence charSequence) {
                this.mInProgressLabel = charSequence;
                return this;
            }
        }

        public Action(int i, CharSequence charSequence, PendingIntent pendingIntent) {
            this(i, charSequence, pendingIntent, new Bundle(), null, null, true);
        }

        Action(int i, CharSequence charSequence, PendingIntent pendingIntent, Bundle bundle, RemoteInput[] remoteInputArr, RemoteInput[] remoteInputArr2, boolean z) {
            this.icon = i;
            this.title = Builder.limitCharSequenceLength(charSequence);
            this.actionIntent = pendingIntent;
            if (bundle == null) {
                bundle = new Bundle();
            }
            this.mExtras = bundle;
            this.mRemoteInputs = remoteInputArr;
            this.mDataOnlyRemoteInputs = remoteInputArr2;
            this.mAllowGeneratedReplies = z;
        }

        public PendingIntent getActionIntent() {
            return this.actionIntent;
        }

        public boolean getAllowGeneratedReplies() {
            return this.mAllowGeneratedReplies;
        }

        public RemoteInput[] getDataOnlyRemoteInputs() {
            return this.mDataOnlyRemoteInputs;
        }

        public Bundle getExtras() {
            return this.mExtras;
        }

        public int getIcon() {
            return this.icon;
        }

        public RemoteInput[] getRemoteInputs() {
            return this.mRemoteInputs;
        }

        public CharSequence getTitle() {
            return this.title;
        }
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: android.support.v4.app.NotificationCompat$BadgeIconType */
    public @interface BadgeIconType {
    }

    /* renamed from: android.support.v4.app.NotificationCompat$BigPictureStyle */
    public static class BigPictureStyle extends Style {
        private Bitmap mBigLargeIcon;
        private boolean mBigLargeIconSet;
        private Bitmap mPicture;

        public BigPictureStyle() {
        }

        public BigPictureStyle(Builder builder) {
            setBuilder(builder);
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public void apply(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            if (VERSION.SDK_INT >= 16) {
                NotificationCompatJellybean.addBigPictureStyle(notificationBuilderWithBuilderAccessor, this.mBigContentTitle, this.mSummaryTextSet, this.mSummaryText, this.mPicture, this.mBigLargeIcon, this.mBigLargeIconSet);
            }
        }

        public BigPictureStyle bigLargeIcon(Bitmap bitmap) {
            this.mBigLargeIcon = bitmap;
            this.mBigLargeIconSet = true;
            return this;
        }

        public BigPictureStyle bigPicture(Bitmap bitmap) {
            this.mPicture = bitmap;
            return this;
        }

        public BigPictureStyle setBigContentTitle(CharSequence charSequence) {
            this.mBigContentTitle = Builder.limitCharSequenceLength(charSequence);
            return this;
        }

        public BigPictureStyle setSummaryText(CharSequence charSequence) {
            this.mSummaryText = Builder.limitCharSequenceLength(charSequence);
            this.mSummaryTextSet = true;
            return this;
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$BigTextStyle */
    public static class BigTextStyle extends Style {
        private CharSequence mBigText;

        public BigTextStyle() {
        }

        public BigTextStyle(Builder builder) {
            setBuilder(builder);
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public void apply(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            if (VERSION.SDK_INT >= 16) {
                NotificationCompatJellybean.addBigTextStyle(notificationBuilderWithBuilderAccessor, this.mBigContentTitle, this.mSummaryTextSet, this.mSummaryText, this.mBigText);
            }
        }

        public BigTextStyle bigText(CharSequence charSequence) {
            this.mBigText = Builder.limitCharSequenceLength(charSequence);
            return this;
        }

        public BigTextStyle setBigContentTitle(CharSequence charSequence) {
            this.mBigContentTitle = Builder.limitCharSequenceLength(charSequence);
            return this;
        }

        public BigTextStyle setSummaryText(CharSequence charSequence) {
            this.mSummaryText = Builder.limitCharSequenceLength(charSequence);
            this.mSummaryTextSet = true;
            return this;
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$Builder */
    public static class Builder {
        private static final int MAX_CHARSEQUENCE_LENGTH = 5120;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public ArrayList<Action> mActions;
        int mBadgeIcon;
        RemoteViews mBigContentView;
        String mCategory;
        String mChannelId;
        int mColor;
        boolean mColorized;
        boolean mColorizedSet;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public CharSequence mContentInfo;
        PendingIntent mContentIntent;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public CharSequence mContentText;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public CharSequence mContentTitle;
        RemoteViews mContentView;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public Context mContext;
        Bundle mExtras;
        PendingIntent mFullScreenIntent;
        /* access modifiers changed from: private */
        public int mGroupAlertBehavior;
        String mGroupKey;
        boolean mGroupSummary;
        RemoteViews mHeadsUpContentView;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public Bitmap mLargeIcon;
        boolean mLocalOnly;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public Notification mNotification;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public int mNumber;
        public ArrayList<String> mPeople;
        int mPriority;
        int mProgress;
        boolean mProgressIndeterminate;
        int mProgressMax;
        Notification mPublicVersion;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public CharSequence[] mRemoteInputHistory;
        String mShortcutId;
        boolean mShowWhen;
        String mSortKey;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public Style mStyle;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public CharSequence mSubText;
        RemoteViews mTickerView;
        long mTimeout;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public boolean mUseChronometer;
        int mVisibility;

        @Deprecated
        public Builder(Context context) {
            this(context, null);
        }

        public Builder(@NonNull Context context, @NonNull String str) {
            this.mShowWhen = true;
            this.mActions = new ArrayList<>();
            this.mLocalOnly = false;
            this.mColor = 0;
            this.mVisibility = 0;
            this.mBadgeIcon = 0;
            this.mGroupAlertBehavior = 0;
            this.mNotification = new Notification();
            this.mContext = context;
            this.mChannelId = str;
            this.mNotification.when = System.currentTimeMillis();
            this.mNotification.audioStreamType = -1;
            this.mPriority = 0;
            this.mPeople = new ArrayList<>();
        }

        protected static CharSequence limitCharSequenceLength(CharSequence charSequence) {
            return (charSequence != null && charSequence.length() > MAX_CHARSEQUENCE_LENGTH) ? charSequence.subSequence(0, MAX_CHARSEQUENCE_LENGTH) : charSequence;
        }

        private void setFlag(int i, boolean z) {
            if (z) {
                this.mNotification.flags |= i;
                return;
            }
            this.mNotification.flags &= i ^ -1;
        }

        public Builder addAction(int i, CharSequence charSequence, PendingIntent pendingIntent) {
            this.mActions.add(new Action(i, charSequence, pendingIntent));
            return this;
        }

        public Builder addAction(Action action) {
            this.mActions.add(action);
            return this;
        }

        public Builder addExtras(Bundle bundle) {
            if (bundle != null) {
                if (this.mExtras == null) {
                    this.mExtras = new Bundle(bundle);
                } else {
                    this.mExtras.putAll(bundle);
                }
            }
            return this;
        }

        public Builder addPerson(String str) {
            this.mPeople.add(str);
            return this;
        }

        public Notification build() {
            return NotificationCompat.IMPL.build(this, getExtender());
        }

        public Builder extend(Extender extender) {
            extender.extend(this);
            return this;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews getBigContentView() {
            return this.mBigContentView;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public int getColor() {
            return this.mColor;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews getContentView() {
            return this.mContentView;
        }

        /* access modifiers changed from: protected */
        @RestrictTo({Scope.LIBRARY_GROUP})
        public BuilderExtender getExtender() {
            return new BuilderExtender();
        }

        public Bundle getExtras() {
            if (this.mExtras == null) {
                this.mExtras = new Bundle();
            }
            return this.mExtras;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews getHeadsUpContentView() {
            return this.mHeadsUpContentView;
        }

        @Deprecated
        public Notification getNotification() {
            return build();
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public int getPriority() {
            return this.mPriority;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public long getWhenIfShowing() {
            if (this.mShowWhen) {
                return this.mNotification.when;
            }
            return 0;
        }

        public Builder setAutoCancel(boolean z) {
            setFlag(16, z);
            return this;
        }

        public Builder setBadgeIconType(int i) {
            this.mBadgeIcon = i;
            return this;
        }

        public Builder setCategory(String str) {
            this.mCategory = str;
            return this;
        }

        public Builder setChannelId(@NonNull String str) {
            this.mChannelId = str;
            return this;
        }

        public Builder setColor(@ColorInt int i) {
            this.mColor = i;
            return this;
        }

        public Builder setColorized(boolean z) {
            this.mColorized = z;
            this.mColorizedSet = true;
            return this;
        }

        public Builder setContent(RemoteViews remoteViews) {
            this.mNotification.contentView = remoteViews;
            return this;
        }

        public Builder setContentInfo(CharSequence charSequence) {
            this.mContentInfo = limitCharSequenceLength(charSequence);
            return this;
        }

        public Builder setContentIntent(PendingIntent pendingIntent) {
            this.mContentIntent = pendingIntent;
            return this;
        }

        public Builder setContentText(CharSequence charSequence) {
            this.mContentText = limitCharSequenceLength(charSequence);
            return this;
        }

        public Builder setContentTitle(CharSequence charSequence) {
            this.mContentTitle = limitCharSequenceLength(charSequence);
            return this;
        }

        public Builder setCustomBigContentView(RemoteViews remoteViews) {
            this.mBigContentView = remoteViews;
            return this;
        }

        public Builder setCustomContentView(RemoteViews remoteViews) {
            this.mContentView = remoteViews;
            return this;
        }

        public Builder setCustomHeadsUpContentView(RemoteViews remoteViews) {
            this.mHeadsUpContentView = remoteViews;
            return this;
        }

        public Builder setDefaults(int i) {
            this.mNotification.defaults = i;
            if ((i & 4) != 0) {
                this.mNotification.flags |= 1;
            }
            return this;
        }

        public Builder setDeleteIntent(PendingIntent pendingIntent) {
            this.mNotification.deleteIntent = pendingIntent;
            return this;
        }

        public Builder setExtras(Bundle bundle) {
            this.mExtras = bundle;
            return this;
        }

        public Builder setFullScreenIntent(PendingIntent pendingIntent, boolean z) {
            this.mFullScreenIntent = pendingIntent;
            setFlag(128, z);
            return this;
        }

        public Builder setGroup(String str) {
            this.mGroupKey = str;
            return this;
        }

        public Builder setGroupAlertBehavior(int i) {
            this.mGroupAlertBehavior = i;
            return this;
        }

        public Builder setGroupSummary(boolean z) {
            this.mGroupSummary = z;
            return this;
        }

        public Builder setLargeIcon(Bitmap bitmap) {
            this.mLargeIcon = bitmap;
            return this;
        }

        public Builder setLights(@ColorInt int i, int i2, int i3) {
            int i4 = 1;
            this.mNotification.ledARGB = i;
            this.mNotification.ledOnMS = i2;
            this.mNotification.ledOffMS = i3;
            boolean z = (this.mNotification.ledOnMS == 0 || this.mNotification.ledOffMS == 0) ? false : true;
            Notification notification = this.mNotification;
            int i5 = this.mNotification.flags;
            if (!z) {
                i4 = 0;
            }
            notification.flags = (i5 & -2) | i4;
            return this;
        }

        public Builder setLocalOnly(boolean z) {
            this.mLocalOnly = z;
            return this;
        }

        public Builder setNumber(int i) {
            this.mNumber = i;
            return this;
        }

        public Builder setOngoing(boolean z) {
            setFlag(2, z);
            return this;
        }

        public Builder setOnlyAlertOnce(boolean z) {
            setFlag(8, z);
            return this;
        }

        public Builder setPriority(int i) {
            this.mPriority = i;
            return this;
        }

        public Builder setProgress(int i, int i2, boolean z) {
            this.mProgressMax = i;
            this.mProgress = i2;
            this.mProgressIndeterminate = z;
            return this;
        }

        public Builder setPublicVersion(Notification notification) {
            this.mPublicVersion = notification;
            return this;
        }

        public Builder setRemoteInputHistory(CharSequence[] charSequenceArr) {
            this.mRemoteInputHistory = charSequenceArr;
            return this;
        }

        public Builder setShortcutId(String str) {
            this.mShortcutId = str;
            return this;
        }

        public Builder setShowWhen(boolean z) {
            this.mShowWhen = z;
            return this;
        }

        public Builder setSmallIcon(int i) {
            this.mNotification.icon = i;
            return this;
        }

        public Builder setSmallIcon(int i, int i2) {
            this.mNotification.icon = i;
            this.mNotification.iconLevel = i2;
            return this;
        }

        public Builder setSortKey(String str) {
            this.mSortKey = str;
            return this;
        }

        public Builder setSound(Uri uri) {
            this.mNotification.sound = uri;
            this.mNotification.audioStreamType = -1;
            return this;
        }

        public Builder setSound(Uri uri, int i) {
            this.mNotification.sound = uri;
            this.mNotification.audioStreamType = i;
            return this;
        }

        public Builder setStyle(Style style) {
            if (this.mStyle != style) {
                this.mStyle = style;
                if (this.mStyle != null) {
                    this.mStyle.setBuilder(this);
                }
            }
            return this;
        }

        public Builder setSubText(CharSequence charSequence) {
            this.mSubText = limitCharSequenceLength(charSequence);
            return this;
        }

        public Builder setTicker(CharSequence charSequence) {
            this.mNotification.tickerText = limitCharSequenceLength(charSequence);
            return this;
        }

        public Builder setTicker(CharSequence charSequence, RemoteViews remoteViews) {
            this.mNotification.tickerText = limitCharSequenceLength(charSequence);
            this.mTickerView = remoteViews;
            return this;
        }

        public Builder setTimeoutAfter(long j) {
            this.mTimeout = j;
            return this;
        }

        public Builder setUsesChronometer(boolean z) {
            this.mUseChronometer = z;
            return this;
        }

        public Builder setVibrate(long[] jArr) {
            this.mNotification.vibrate = jArr;
            return this;
        }

        public Builder setVisibility(int i) {
            this.mVisibility = i;
            return this;
        }

        public Builder setWhen(long j) {
            this.mNotification.when = j;
            return this;
        }
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    /* renamed from: android.support.v4.app.NotificationCompat$BuilderExtender */
    protected static class BuilderExtender {
        protected BuilderExtender() {
        }

        public Notification build(Builder builder, NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            RemoteViews remoteViews = builder.mStyle != null ? builder.mStyle.makeContentView(notificationBuilderWithBuilderAccessor) : null;
            Notification build = notificationBuilderWithBuilderAccessor.build();
            if (remoteViews != null) {
                build.contentView = remoteViews;
            } else if (builder.mContentView != null) {
                build.contentView = builder.mContentView;
            }
            if (VERSION.SDK_INT >= 16 && builder.mStyle != null) {
                RemoteViews makeBigContentView = builder.mStyle.makeBigContentView(notificationBuilderWithBuilderAccessor);
                if (makeBigContentView != null) {
                    build.bigContentView = makeBigContentView;
                }
            }
            if (VERSION.SDK_INT >= 21 && builder.mStyle != null) {
                RemoteViews makeHeadsUpContentView = builder.mStyle.makeHeadsUpContentView(notificationBuilderWithBuilderAccessor);
                if (makeHeadsUpContentView != null) {
                    build.headsUpContentView = makeHeadsUpContentView;
                }
            }
            return build;
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$CarExtender */
    public static final class CarExtender implements Extender {
        private static final String EXTRA_CAR_EXTENDER = "android.car.EXTENSIONS";
        private static final String EXTRA_COLOR = "app_color";
        private static final String EXTRA_CONVERSATION = "car_conversation";
        private static final String EXTRA_LARGE_ICON = "large_icon";
        private static final String TAG = "CarExtender";
        private int mColor = 0;
        private Bitmap mLargeIcon;
        private UnreadConversation mUnreadConversation;

        /* renamed from: android.support.v4.app.NotificationCompat$CarExtender$UnreadConversation */
        public static class UnreadConversation extends android.support.p000v4.app.NotificationCompatBase.UnreadConversation {
            static final android.support.p000v4.app.NotificationCompatBase.UnreadConversation.Factory FACTORY = new android.support.p000v4.app.NotificationCompatBase.UnreadConversation.Factory() {
                public UnreadConversation build(String[] strArr, RemoteInput remoteInput, PendingIntent pendingIntent, PendingIntent pendingIntent2, String[] strArr2, long j) {
                    return new UnreadConversation(strArr, (RemoteInput) remoteInput, pendingIntent, pendingIntent2, strArr2, j);
                }
            };
            private final long mLatestTimestamp;
            private final String[] mMessages;
            private final String[] mParticipants;
            private final PendingIntent mReadPendingIntent;
            private final RemoteInput mRemoteInput;
            private final PendingIntent mReplyPendingIntent;

            /* renamed from: android.support.v4.app.NotificationCompat$CarExtender$UnreadConversation$Builder */
            public static class Builder {
                private long mLatestTimestamp;
                private final List<String> mMessages = new ArrayList();
                private final String mParticipant;
                private PendingIntent mReadPendingIntent;
                private RemoteInput mRemoteInput;
                private PendingIntent mReplyPendingIntent;

                public Builder(String str) {
                    this.mParticipant = str;
                }

                public Builder addMessage(String str) {
                    this.mMessages.add(str);
                    return this;
                }

                public UnreadConversation build() {
                    String[] strArr = (String[]) this.mMessages.toArray(new String[this.mMessages.size()]);
                    String str = this.mParticipant;
                    return new UnreadConversation(strArr, this.mRemoteInput, this.mReplyPendingIntent, this.mReadPendingIntent, new String[]{str}, this.mLatestTimestamp);
                }

                public Builder setLatestTimestamp(long j) {
                    this.mLatestTimestamp = j;
                    return this;
                }

                public Builder setReadPendingIntent(PendingIntent pendingIntent) {
                    this.mReadPendingIntent = pendingIntent;
                    return this;
                }

                public Builder setReplyAction(PendingIntent pendingIntent, RemoteInput remoteInput) {
                    this.mRemoteInput = remoteInput;
                    this.mReplyPendingIntent = pendingIntent;
                    return this;
                }
            }

            UnreadConversation(String[] strArr, RemoteInput remoteInput, PendingIntent pendingIntent, PendingIntent pendingIntent2, String[] strArr2, long j) {
                this.mMessages = strArr;
                this.mRemoteInput = remoteInput;
                this.mReadPendingIntent = pendingIntent2;
                this.mReplyPendingIntent = pendingIntent;
                this.mParticipants = strArr2;
                this.mLatestTimestamp = j;
            }

            public long getLatestTimestamp() {
                return this.mLatestTimestamp;
            }

            public String[] getMessages() {
                return this.mMessages;
            }

            public String getParticipant() {
                if (this.mParticipants.length > 0) {
                    return this.mParticipants[0];
                }
                return null;
            }

            public String[] getParticipants() {
                return this.mParticipants;
            }

            public PendingIntent getReadPendingIntent() {
                return this.mReadPendingIntent;
            }

            public RemoteInput getRemoteInput() {
                return this.mRemoteInput;
            }

            public PendingIntent getReplyPendingIntent() {
                return this.mReplyPendingIntent;
            }
        }

        public CarExtender() {
        }

        public CarExtender(Notification notification) {
            if (VERSION.SDK_INT >= 21) {
                Bundle bundle = NotificationCompat.getExtras(notification) == null ? null : NotificationCompat.getExtras(notification).getBundle(EXTRA_CAR_EXTENDER);
                if (bundle != null) {
                    this.mLargeIcon = (Bitmap) bundle.getParcelable(EXTRA_LARGE_ICON);
                    this.mColor = bundle.getInt(EXTRA_COLOR, 0);
                    this.mUnreadConversation = (UnreadConversation) NotificationCompat.IMPL.getUnreadConversationFromBundle(bundle.getBundle(EXTRA_CONVERSATION), UnreadConversation.FACTORY, RemoteInput.FACTORY);
                }
            }
        }

        public Builder extend(Builder builder) {
            if (VERSION.SDK_INT >= 21) {
                Bundle bundle = new Bundle();
                if (this.mLargeIcon != null) {
                    bundle.putParcelable(EXTRA_LARGE_ICON, this.mLargeIcon);
                }
                if (this.mColor != 0) {
                    bundle.putInt(EXTRA_COLOR, this.mColor);
                }
                if (this.mUnreadConversation != null) {
                    bundle.putBundle(EXTRA_CONVERSATION, NotificationCompat.IMPL.getBundleForUnreadConversation(this.mUnreadConversation));
                }
                builder.getExtras().putBundle(EXTRA_CAR_EXTENDER, bundle);
            }
            return builder;
        }

        @ColorInt
        public int getColor() {
            return this.mColor;
        }

        public Bitmap getLargeIcon() {
            return this.mLargeIcon;
        }

        public UnreadConversation getUnreadConversation() {
            return this.mUnreadConversation;
        }

        public CarExtender setColor(@ColorInt int i) {
            this.mColor = i;
            return this;
        }

        public CarExtender setLargeIcon(Bitmap bitmap) {
            this.mLargeIcon = bitmap;
            return this;
        }

        public CarExtender setUnreadConversation(UnreadConversation unreadConversation) {
            this.mUnreadConversation = unreadConversation;
            return this;
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$DecoratedCustomViewStyle */
    public static class DecoratedCustomViewStyle extends Style {
        private static final int MAX_ACTION_BUTTONS = 3;

        /* JADX WARNING: Removed duplicated region for block: B:11:0x0041  */
        /* JADX WARNING: Removed duplicated region for block: B:14:0x0050  */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        private android.widget.RemoteViews createRemoteViews(android.widget.RemoteViews r8, boolean r9) {
            /*
                r7 = this;
                r2 = 1
                r1 = 0
                int r0 = android.support.compat.C0004R.C0007layout.notification_template_custom_big
                android.widget.RemoteViews r4 = r7.applyStandardTemplate(r2, r0, r1)
                int r0 = android.support.compat.C0004R.C0006id.actions
                r4.removeAllViews(r0)
                if (r9 == 0) goto L_0x0053
                android.support.v4.app.NotificationCompat$Builder r0 = r7.mBuilder
                java.util.ArrayList<android.support.v4.app.NotificationCompat$Action> r0 = r0.mActions
                if (r0 == 0) goto L_0x0053
                android.support.v4.app.NotificationCompat$Builder r0 = r7.mBuilder
                java.util.ArrayList<android.support.v4.app.NotificationCompat$Action> r0 = r0.mActions
                int r0 = r0.size()
                r3 = 3
                int r5 = java.lang.Math.min(r0, r3)
                if (r5 <= 0) goto L_0x0053
                r3 = r1
            L_0x0025:
                if (r3 >= r5) goto L_0x003e
                android.support.v4.app.NotificationCompat$Builder r0 = r7.mBuilder
                java.util.ArrayList<android.support.v4.app.NotificationCompat$Action> r0 = r0.mActions
                java.lang.Object r0 = r0.get(r3)
                android.support.v4.app.NotificationCompat$Action r0 = (android.support.p000v4.app.NotificationCompat.Action) r0
                android.widget.RemoteViews r0 = r7.generateActionButton(r0)
                int r6 = android.support.compat.C0004R.C0006id.actions
                r4.addView(r6, r0)
                int r0 = r3 + 1
                r3 = r0
                goto L_0x0025
            L_0x003e:
                r0 = r2
            L_0x003f:
                if (r0 == 0) goto L_0x0050
                r0 = r1
            L_0x0042:
                int r1 = android.support.compat.C0004R.C0006id.actions
                r4.setViewVisibility(r1, r0)
                int r1 = android.support.compat.C0004R.C0006id.action_divider
                r4.setViewVisibility(r1, r0)
                r7.buildIntoRemoteViews(r4, r8)
                return r4
            L_0x0050:
                r0 = 8
                goto L_0x0042
            L_0x0053:
                r0 = r1
                goto L_0x003f
            */
            throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.app.NotificationCompat.DecoratedCustomViewStyle.createRemoteViews(android.widget.RemoteViews, boolean):android.widget.RemoteViews");
        }

        private RemoteViews generateActionButton(Action action) {
            boolean z = action.actionIntent == null;
            RemoteViews remoteViews = new RemoteViews(this.mBuilder.mContext.getPackageName(), z ? C0004R.C0007layout.notification_action_tombstone : C0004R.C0007layout.notification_action);
            remoteViews.setImageViewBitmap(C0004R.C0006id.action_image, createColoredBitmap(action.getIcon(), this.mBuilder.mContext.getResources().getColor(C0004R.color.notification_action_color_filter)));
            remoteViews.setTextViewText(C0004R.C0006id.action_text, action.title);
            if (!z) {
                remoteViews.setOnClickPendingIntent(C0004R.C0006id.action_container, action.actionIntent);
            }
            if (VERSION.SDK_INT >= 15) {
                remoteViews.setContentDescription(C0004R.C0006id.action_container, action.title);
            }
            return remoteViews;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public void apply(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            if (VERSION.SDK_INT >= 24) {
                notificationBuilderWithBuilderAccessor.getBuilder().setStyle(new android.app.Notification.DecoratedCustomViewStyle());
            }
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews makeBigContentView(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            if (VERSION.SDK_INT >= 24) {
                return null;
            }
            RemoteViews bigContentView = this.mBuilder.getBigContentView();
            if (bigContentView == null) {
                bigContentView = this.mBuilder.getContentView();
            }
            if (bigContentView != null) {
                return createRemoteViews(bigContentView, true);
            }
            return null;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews makeContentView(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            if (VERSION.SDK_INT < 24 && this.mBuilder.getContentView() != null) {
                return createRemoteViews(this.mBuilder.getContentView(), false);
            }
            return null;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews makeHeadsUpContentView(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            if (VERSION.SDK_INT >= 24) {
                return null;
            }
            RemoteViews headsUpContentView = this.mBuilder.getHeadsUpContentView();
            RemoteViews contentView = headsUpContentView != null ? headsUpContentView : this.mBuilder.getContentView();
            if (headsUpContentView != null) {
                return createRemoteViews(contentView, true);
            }
            return null;
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$Extender */
    public interface Extender {
        Builder extend(Builder builder);
    }

    /* renamed from: android.support.v4.app.NotificationCompat$InboxStyle */
    public static class InboxStyle extends Style {
        private ArrayList<CharSequence> mTexts = new ArrayList<>();

        public InboxStyle() {
        }

        public InboxStyle(Builder builder) {
            setBuilder(builder);
        }

        public InboxStyle addLine(CharSequence charSequence) {
            this.mTexts.add(Builder.limitCharSequenceLength(charSequence));
            return this;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public void apply(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            if (VERSION.SDK_INT >= 16) {
                NotificationCompatJellybean.addInboxStyle(notificationBuilderWithBuilderAccessor, this.mBigContentTitle, this.mSummaryTextSet, this.mSummaryText, this.mTexts);
            }
        }

        public InboxStyle setBigContentTitle(CharSequence charSequence) {
            this.mBigContentTitle = Builder.limitCharSequenceLength(charSequence);
            return this;
        }

        public InboxStyle setSummaryText(CharSequence charSequence) {
            this.mSummaryText = Builder.limitCharSequenceLength(charSequence);
            this.mSummaryTextSet = true;
            return this;
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$MessagingStyle */
    public static class MessagingStyle extends Style {
        public static final int MAXIMUM_RETAINED_MESSAGES = 25;
        CharSequence mConversationTitle;
        List<Message> mMessages = new ArrayList();
        CharSequence mUserDisplayName;

        /* renamed from: android.support.v4.app.NotificationCompat$MessagingStyle$Message */
        public static final class Message {
            static final String KEY_DATA_MIME_TYPE = "type";
            static final String KEY_DATA_URI = "uri";
            static final String KEY_EXTRAS_BUNDLE = "extras";
            static final String KEY_SENDER = "sender";
            static final String KEY_TEXT = "text";
            static final String KEY_TIMESTAMP = "time";
            private String mDataMimeType;
            private Uri mDataUri;
            private Bundle mExtras = new Bundle();
            private final CharSequence mSender;
            private final CharSequence mText;
            private final long mTimestamp;

            public Message(CharSequence charSequence, long j, CharSequence charSequence2) {
                this.mText = charSequence;
                this.mTimestamp = j;
                this.mSender = charSequence2;
            }

            static Bundle[] getBundleArrayForMessages(List<Message> list) {
                Bundle[] bundleArr = new Bundle[list.size()];
                int size = list.size();
                for (int i = 0; i < size; i++) {
                    bundleArr[i] = ((Message) list.get(i)).toBundle();
                }
                return bundleArr;
            }

            static Message getMessageFromBundle(Bundle bundle) {
                try {
                    if (!bundle.containsKey(KEY_TEXT) || !bundle.containsKey(KEY_TIMESTAMP)) {
                        return null;
                    }
                    Message message = new Message(bundle.getCharSequence(KEY_TEXT), bundle.getLong(KEY_TIMESTAMP), bundle.getCharSequence(KEY_SENDER));
                    if (bundle.containsKey("type") && bundle.containsKey("uri")) {
                        message.setData(bundle.getString("type"), (Uri) bundle.getParcelable("uri"));
                    }
                    if (!bundle.containsKey("extras")) {
                        return message;
                    }
                    message.getExtras().putAll(bundle.getBundle("extras"));
                    return message;
                } catch (ClassCastException e) {
                    return null;
                }
            }

            static List<Message> getMessagesFromBundleArray(Parcelable[] parcelableArr) {
                ArrayList arrayList = new ArrayList(parcelableArr.length);
                int i = 0;
                while (true) {
                    int i2 = i;
                    if (i2 >= parcelableArr.length) {
                        return arrayList;
                    }
                    if (parcelableArr[i2] instanceof Bundle) {
                        Message messageFromBundle = getMessageFromBundle(parcelableArr[i2]);
                        if (messageFromBundle != null) {
                            arrayList.add(messageFromBundle);
                        }
                    }
                    i = i2 + 1;
                }
            }

            private Bundle toBundle() {
                Bundle bundle = new Bundle();
                if (this.mText != null) {
                    bundle.putCharSequence(KEY_TEXT, this.mText);
                }
                bundle.putLong(KEY_TIMESTAMP, this.mTimestamp);
                if (this.mSender != null) {
                    bundle.putCharSequence(KEY_SENDER, this.mSender);
                }
                if (this.mDataMimeType != null) {
                    bundle.putString("type", this.mDataMimeType);
                }
                if (this.mDataUri != null) {
                    bundle.putParcelable("uri", this.mDataUri);
                }
                if (this.mExtras != null) {
                    bundle.putBundle("extras", this.mExtras);
                }
                return bundle;
            }

            public String getDataMimeType() {
                return this.mDataMimeType;
            }

            public Uri getDataUri() {
                return this.mDataUri;
            }

            public Bundle getExtras() {
                return this.mExtras;
            }

            public CharSequence getSender() {
                return this.mSender;
            }

            public CharSequence getText() {
                return this.mText;
            }

            public long getTimestamp() {
                return this.mTimestamp;
            }

            public Message setData(String str, Uri uri) {
                this.mDataMimeType = str;
                this.mDataUri = uri;
                return this;
            }
        }

        MessagingStyle() {
        }

        public MessagingStyle(@NonNull CharSequence charSequence) {
            this.mUserDisplayName = charSequence;
        }

        public static MessagingStyle extractMessagingStyleFromNotification(Notification notification) {
            Bundle extras = NotificationCompat.getExtras(notification);
            if (extras != null && !extras.containsKey(NotificationCompat.EXTRA_SELF_DISPLAY_NAME)) {
                return null;
            }
            try {
                MessagingStyle messagingStyle = new MessagingStyle();
                messagingStyle.restoreFromCompatExtras(extras);
                return messagingStyle;
            } catch (ClassCastException e) {
                return null;
            }
        }

        @Nullable
        private Message findLatestIncomingMessage() {
            for (int size = this.mMessages.size() - 1; size >= 0; size--) {
                Message message = (Message) this.mMessages.get(size);
                if (!TextUtils.isEmpty(message.getSender())) {
                    return message;
                }
            }
            if (!this.mMessages.isEmpty()) {
                return (Message) this.mMessages.get(this.mMessages.size() - 1);
            }
            return null;
        }

        private boolean hasMessagesWithoutSender() {
            for (int size = this.mMessages.size() - 1; size >= 0; size--) {
                if (((Message) this.mMessages.get(size)).getSender() == null) {
                    return true;
                }
            }
            return false;
        }

        @NonNull
        private TextAppearanceSpan makeFontColorSpan(int i) {
            return new TextAppearanceSpan(null, 0, 0, ColorStateList.valueOf(i), null);
        }

        private CharSequence makeMessageLine(Message message) {
            BidiFormatter instance = BidiFormatter.getInstance();
            SpannableStringBuilder spannableStringBuilder = new SpannableStringBuilder();
            boolean z = VERSION.SDK_INT >= 21;
            int i = z ? ViewCompat.MEASURED_STATE_MASK : -1;
            CharSequence sender = message.getSender();
            if (TextUtils.isEmpty(message.getSender())) {
                sender = this.mUserDisplayName == null ? "" : this.mUserDisplayName;
                if (z && this.mBuilder.getColor() != 0) {
                    i = this.mBuilder.getColor();
                }
            }
            CharSequence unicodeWrap = instance.unicodeWrap(sender);
            spannableStringBuilder.append(unicodeWrap);
            spannableStringBuilder.setSpan(makeFontColorSpan(i), spannableStringBuilder.length() - unicodeWrap.length(), spannableStringBuilder.length(), 33);
            spannableStringBuilder.append("  ").append(instance.unicodeWrap(message.getText() == null ? "" : message.getText()));
            return spannableStringBuilder;
        }

        public void addCompatExtras(Bundle bundle) {
            super.addCompatExtras(bundle);
            if (this.mUserDisplayName != null) {
                bundle.putCharSequence(NotificationCompat.EXTRA_SELF_DISPLAY_NAME, this.mUserDisplayName);
            }
            if (this.mConversationTitle != null) {
                bundle.putCharSequence(NotificationCompat.EXTRA_CONVERSATION_TITLE, this.mConversationTitle);
            }
            if (!this.mMessages.isEmpty()) {
                bundle.putParcelableArray(NotificationCompat.EXTRA_MESSAGES, Message.getBundleArrayForMessages(this.mMessages));
            }
        }

        public MessagingStyle addMessage(Message message) {
            this.mMessages.add(message);
            if (this.mMessages.size() > 25) {
                this.mMessages.remove(0);
            }
            return this;
        }

        public MessagingStyle addMessage(CharSequence charSequence, long j, CharSequence charSequence2) {
            this.mMessages.add(new Message(charSequence, j, charSequence2));
            if (this.mMessages.size() > 25) {
                this.mMessages.remove(0);
            }
            return this;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public void apply(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            if (VERSION.SDK_INT >= 24) {
                ArrayList arrayList = new ArrayList();
                ArrayList arrayList2 = new ArrayList();
                ArrayList arrayList3 = new ArrayList();
                ArrayList arrayList4 = new ArrayList();
                ArrayList arrayList5 = new ArrayList();
                for (Message message : this.mMessages) {
                    arrayList.add(message.getText());
                    arrayList2.add(Long.valueOf(message.getTimestamp()));
                    arrayList3.add(message.getSender());
                    arrayList4.add(message.getDataMimeType());
                    arrayList5.add(message.getDataUri());
                }
                NotificationCompatApi24.addMessagingStyle(notificationBuilderWithBuilderAccessor, this.mUserDisplayName, this.mConversationTitle, arrayList, arrayList2, arrayList3, arrayList4, arrayList5);
                return;
            }
            Message findLatestIncomingMessage = findLatestIncomingMessage();
            if (this.mConversationTitle != null) {
                notificationBuilderWithBuilderAccessor.getBuilder().setContentTitle(this.mConversationTitle);
            } else if (findLatestIncomingMessage != null) {
                notificationBuilderWithBuilderAccessor.getBuilder().setContentTitle(findLatestIncomingMessage.getSender());
            }
            if (findLatestIncomingMessage != null) {
                notificationBuilderWithBuilderAccessor.getBuilder().setContentText(this.mConversationTitle != null ? makeMessageLine(findLatestIncomingMessage) : findLatestIncomingMessage.getText());
            }
            if (VERSION.SDK_INT >= 16) {
                SpannableStringBuilder spannableStringBuilder = new SpannableStringBuilder();
                boolean z = this.mConversationTitle != null || hasMessagesWithoutSender();
                for (int size = this.mMessages.size() - 1; size >= 0; size--) {
                    Message message2 = (Message) this.mMessages.get(size);
                    CharSequence text = z ? makeMessageLine(message2) : message2.getText();
                    if (size != this.mMessages.size() - 1) {
                        spannableStringBuilder.insert(0, StringUtils.f1199LF);
                    }
                    spannableStringBuilder.insert(0, text);
                }
                NotificationCompatJellybean.addBigTextStyle(notificationBuilderWithBuilderAccessor, null, false, null, spannableStringBuilder);
            }
        }

        public CharSequence getConversationTitle() {
            return this.mConversationTitle;
        }

        public List<Message> getMessages() {
            return this.mMessages;
        }

        public CharSequence getUserDisplayName() {
            return this.mUserDisplayName;
        }

        /* access modifiers changed from: protected */
        @RestrictTo({Scope.LIBRARY_GROUP})
        public void restoreFromCompatExtras(Bundle bundle) {
            this.mMessages.clear();
            this.mUserDisplayName = bundle.getString(NotificationCompat.EXTRA_SELF_DISPLAY_NAME);
            this.mConversationTitle = bundle.getString(NotificationCompat.EXTRA_CONVERSATION_TITLE);
            Parcelable[] parcelableArray = bundle.getParcelableArray(NotificationCompat.EXTRA_MESSAGES);
            if (parcelableArray != null) {
                this.mMessages = Message.getMessagesFromBundleArray(parcelableArray);
            }
        }

        public MessagingStyle setConversationTitle(CharSequence charSequence) {
            this.mConversationTitle = charSequence;
            return this;
        }
    }

    @RequiresApi(16)
    /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatApi16Impl */
    static class NotificationCompatApi16Impl extends NotificationCompatBaseImpl {
        NotificationCompatApi16Impl() {
        }

        public Notification build(Builder builder, BuilderExtender builderExtender) {
            android.support.p000v4.app.NotificationCompatJellybean.Builder builder2 = new android.support.p000v4.app.NotificationCompatJellybean.Builder(builder.mContext, builder.mNotification, builder.mContentTitle, builder.mContentText, builder.mContentInfo, builder.mTickerView, builder.mNumber, builder.mContentIntent, builder.mFullScreenIntent, builder.mLargeIcon, builder.mProgressMax, builder.mProgress, builder.mProgressIndeterminate, builder.mUseChronometer, builder.mPriority, builder.mSubText, builder.mLocalOnly, builder.mExtras, builder.mGroupKey, builder.mGroupSummary, builder.mSortKey, builder.mContentView, builder.mBigContentView);
            NotificationCompat.addActionsToBuilder(builder2, builder.mActions);
            if (builder.mStyle != null) {
                builder.mStyle.apply(builder2);
            }
            Notification build = builderExtender.build(builder, builder2);
            if (builder.mStyle != null) {
                Bundle extras = NotificationCompat.getExtras(build);
                if (extras != null) {
                    builder.mStyle.addCompatExtras(extras);
                }
            }
            return build;
        }

        public Action getAction(Notification notification, int i) {
            return (Action) NotificationCompatJellybean.getAction(notification, i, Action.FACTORY, RemoteInput.FACTORY);
        }

        public Action[] getActionsFromParcelableArrayList(ArrayList<Parcelable> arrayList) {
            return (Action[]) NotificationCompatJellybean.getActionsFromParcelableArrayList(arrayList, Action.FACTORY, RemoteInput.FACTORY);
        }

        public ArrayList<Parcelable> getParcelableArrayListForActions(Action[] actionArr) {
            return NotificationCompatJellybean.getParcelableArrayListForActions(actionArr);
        }
    }

    @RequiresApi(19)
    /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatApi19Impl */
    static class NotificationCompatApi19Impl extends NotificationCompatApi16Impl {
        NotificationCompatApi19Impl() {
        }

        public Notification build(Builder builder, BuilderExtender builderExtender) {
            android.support.p000v4.app.NotificationCompatKitKat.Builder builder2 = new android.support.p000v4.app.NotificationCompatKitKat.Builder(builder.mContext, builder.mNotification, builder.mContentTitle, builder.mContentText, builder.mContentInfo, builder.mTickerView, builder.mNumber, builder.mContentIntent, builder.mFullScreenIntent, builder.mLargeIcon, builder.mProgressMax, builder.mProgress, builder.mProgressIndeterminate, builder.mShowWhen, builder.mUseChronometer, builder.mPriority, builder.mSubText, builder.mLocalOnly, builder.mPeople, builder.mExtras, builder.mGroupKey, builder.mGroupSummary, builder.mSortKey, builder.mContentView, builder.mBigContentView);
            NotificationCompat.addActionsToBuilder(builder2, builder.mActions);
            if (builder.mStyle != null) {
                builder.mStyle.apply(builder2);
            }
            return builderExtender.build(builder, builder2);
        }

        public Action getAction(Notification notification, int i) {
            return (Action) NotificationCompatKitKat.getAction(notification, i, Action.FACTORY, RemoteInput.FACTORY);
        }
    }

    @RequiresApi(20)
    /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatApi20Impl */
    static class NotificationCompatApi20Impl extends NotificationCompatApi19Impl {
        NotificationCompatApi20Impl() {
        }

        public Notification build(Builder builder, BuilderExtender builderExtender) {
            android.support.p000v4.app.NotificationCompatApi20.Builder builder2 = new android.support.p000v4.app.NotificationCompatApi20.Builder(builder.mContext, builder.mNotification, builder.mContentTitle, builder.mContentText, builder.mContentInfo, builder.mTickerView, builder.mNumber, builder.mContentIntent, builder.mFullScreenIntent, builder.mLargeIcon, builder.mProgressMax, builder.mProgress, builder.mProgressIndeterminate, builder.mShowWhen, builder.mUseChronometer, builder.mPriority, builder.mSubText, builder.mLocalOnly, builder.mPeople, builder.mExtras, builder.mGroupKey, builder.mGroupSummary, builder.mSortKey, builder.mContentView, builder.mBigContentView, builder.mGroupAlertBehavior);
            NotificationCompat.addActionsToBuilder(builder2, builder.mActions);
            if (builder.mStyle != null) {
                builder.mStyle.apply(builder2);
            }
            Notification build = builderExtender.build(builder, builder2);
            if (builder.mStyle != null) {
                builder.mStyle.addCompatExtras(NotificationCompat.getExtras(build));
            }
            return build;
        }

        public Action getAction(Notification notification, int i) {
            return (Action) NotificationCompatApi20.getAction(notification, i, Action.FACTORY, RemoteInput.FACTORY);
        }

        public Action[] getActionsFromParcelableArrayList(ArrayList<Parcelable> arrayList) {
            return (Action[]) NotificationCompatApi20.getActionsFromParcelableArrayList(arrayList, Action.FACTORY, RemoteInput.FACTORY);
        }

        public ArrayList<Parcelable> getParcelableArrayListForActions(Action[] actionArr) {
            return NotificationCompatApi20.getParcelableArrayListForActions(actionArr);
        }
    }

    @RequiresApi(21)
    /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatApi21Impl */
    static class NotificationCompatApi21Impl extends NotificationCompatApi20Impl {
        NotificationCompatApi21Impl() {
        }

        public Notification build(Builder builder, BuilderExtender builderExtender) {
            android.support.p000v4.app.NotificationCompatApi21.Builder builder2 = new android.support.p000v4.app.NotificationCompatApi21.Builder(builder.mContext, builder.mNotification, builder.mContentTitle, builder.mContentText, builder.mContentInfo, builder.mTickerView, builder.mNumber, builder.mContentIntent, builder.mFullScreenIntent, builder.mLargeIcon, builder.mProgressMax, builder.mProgress, builder.mProgressIndeterminate, builder.mShowWhen, builder.mUseChronometer, builder.mPriority, builder.mSubText, builder.mLocalOnly, builder.mCategory, builder.mPeople, builder.mExtras, builder.mColor, builder.mVisibility, builder.mPublicVersion, builder.mGroupKey, builder.mGroupSummary, builder.mSortKey, builder.mContentView, builder.mBigContentView, builder.mHeadsUpContentView, builder.mGroupAlertBehavior);
            NotificationCompat.addActionsToBuilder(builder2, builder.mActions);
            if (builder.mStyle != null) {
                builder.mStyle.apply(builder2);
            }
            Notification build = builderExtender.build(builder, builder2);
            if (builder.mStyle != null) {
                builder.mStyle.addCompatExtras(NotificationCompat.getExtras(build));
            }
            return build;
        }

        public Bundle getBundleForUnreadConversation(UnreadConversation unreadConversation) {
            return NotificationCompatApi21.getBundleForUnreadConversation(unreadConversation);
        }

        public UnreadConversation getUnreadConversationFromBundle(Bundle bundle, UnreadConversation.Factory factory, RemoteInput.Factory factory2) {
            return NotificationCompatApi21.getUnreadConversationFromBundle(bundle, factory, factory2);
        }
    }

    @RequiresApi(24)
    /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatApi24Impl */
    static class NotificationCompatApi24Impl extends NotificationCompatApi21Impl {
        NotificationCompatApi24Impl() {
        }

        public Notification build(Builder builder, BuilderExtender builderExtender) {
            android.support.p000v4.app.NotificationCompatApi24.Builder builder2 = new android.support.p000v4.app.NotificationCompatApi24.Builder(builder.mContext, builder.mNotification, builder.mContentTitle, builder.mContentText, builder.mContentInfo, builder.mTickerView, builder.mNumber, builder.mContentIntent, builder.mFullScreenIntent, builder.mLargeIcon, builder.mProgressMax, builder.mProgress, builder.mProgressIndeterminate, builder.mShowWhen, builder.mUseChronometer, builder.mPriority, builder.mSubText, builder.mLocalOnly, builder.mCategory, builder.mPeople, builder.mExtras, builder.mColor, builder.mVisibility, builder.mPublicVersion, builder.mGroupKey, builder.mGroupSummary, builder.mSortKey, builder.mRemoteInputHistory, builder.mContentView, builder.mBigContentView, builder.mHeadsUpContentView, builder.mGroupAlertBehavior);
            NotificationCompat.addActionsToBuilder(builder2, builder.mActions);
            if (builder.mStyle != null) {
                builder.mStyle.apply(builder2);
            }
            Notification build = builderExtender.build(builder, builder2);
            if (builder.mStyle != null) {
                builder.mStyle.addCompatExtras(NotificationCompat.getExtras(build));
            }
            return build;
        }

        public Action getAction(Notification notification, int i) {
            return (Action) NotificationCompatApi24.getAction(notification, i, Action.FACTORY, RemoteInput.FACTORY);
        }

        public Action[] getActionsFromParcelableArrayList(ArrayList<Parcelable> arrayList) {
            return (Action[]) NotificationCompatApi24.getActionsFromParcelableArrayList(arrayList, Action.FACTORY, RemoteInput.FACTORY);
        }

        public ArrayList<Parcelable> getParcelableArrayListForActions(Action[] actionArr) {
            return NotificationCompatApi24.getParcelableArrayListForActions(actionArr);
        }
    }

    @RequiresApi(26)
    /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatApi26Impl */
    static class NotificationCompatApi26Impl extends NotificationCompatApi24Impl {
        NotificationCompatApi26Impl() {
        }

        public Notification build(Builder builder, BuilderExtender builderExtender) {
            android.support.p000v4.app.NotificationCompatApi26.Builder builder2 = new android.support.p000v4.app.NotificationCompatApi26.Builder(builder.mContext, builder.mNotification, builder.mContentTitle, builder.mContentText, builder.mContentInfo, builder.mTickerView, builder.mNumber, builder.mContentIntent, builder.mFullScreenIntent, builder.mLargeIcon, builder.mProgressMax, builder.mProgress, builder.mProgressIndeterminate, builder.mShowWhen, builder.mUseChronometer, builder.mPriority, builder.mSubText, builder.mLocalOnly, builder.mCategory, builder.mPeople, builder.mExtras, builder.mColor, builder.mVisibility, builder.mPublicVersion, builder.mGroupKey, builder.mGroupSummary, builder.mSortKey, builder.mRemoteInputHistory, builder.mContentView, builder.mBigContentView, builder.mHeadsUpContentView, builder.mChannelId, builder.mBadgeIcon, builder.mShortcutId, builder.mTimeout, builder.mColorized, builder.mColorizedSet, builder.mGroupAlertBehavior);
            NotificationCompat.addActionsToBuilder(builder2, builder.mActions);
            if (builder.mStyle != null) {
                builder.mStyle.apply(builder2);
            }
            Notification build = builderExtender.build(builder, builder2);
            if (builder.mStyle != null) {
                builder.mStyle.addCompatExtras(NotificationCompat.getExtras(build));
            }
            return build;
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatBaseImpl */
    static class NotificationCompatBaseImpl implements NotificationCompatImpl {

        /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatBaseImpl$BuilderBase */
        public static class BuilderBase implements NotificationBuilderWithBuilderAccessor {
            private android.app.Notification.Builder mBuilder;

            BuilderBase(Context context, Notification notification, CharSequence charSequence, CharSequence charSequence2, CharSequence charSequence3, RemoteViews remoteViews, int i, PendingIntent pendingIntent, PendingIntent pendingIntent2, Bitmap bitmap, int i2, int i3, boolean z) {
                this.mBuilder = new android.app.Notification.Builder(context).setWhen(notification.when).setSmallIcon(notification.icon, notification.iconLevel).setContent(notification.contentView).setTicker(notification.tickerText, remoteViews).setSound(notification.sound, notification.audioStreamType).setVibrate(notification.vibrate).setLights(notification.ledARGB, notification.ledOnMS, notification.ledOffMS).setOngoing((notification.flags & 2) != 0).setOnlyAlertOnce((notification.flags & 8) != 0).setAutoCancel((notification.flags & 16) != 0).setDefaults(notification.defaults).setContentTitle(charSequence).setContentText(charSequence2).setContentInfo(charSequence3).setContentIntent(pendingIntent).setDeleteIntent(notification.deleteIntent).setFullScreenIntent(pendingIntent2, (notification.flags & 128) != 0).setLargeIcon(bitmap).setNumber(i).setProgress(i2, i3, z);
            }

            public Notification build() {
                return this.mBuilder.getNotification();
            }

            public android.app.Notification.Builder getBuilder() {
                return this.mBuilder;
            }
        }

        NotificationCompatBaseImpl() {
        }

        public Notification build(Builder builder, BuilderExtender builderExtender) {
            return builderExtender.build(builder, new BuilderBase(builder.mContext, builder.mNotification, builder.mContentTitle, builder.mContentText, builder.mContentInfo, builder.mTickerView, builder.mNumber, builder.mContentIntent, builder.mFullScreenIntent, builder.mLargeIcon, builder.mProgressMax, builder.mProgress, builder.mProgressIndeterminate));
        }

        public Action getAction(Notification notification, int i) {
            return null;
        }

        public Action[] getActionsFromParcelableArrayList(ArrayList<Parcelable> arrayList) {
            return null;
        }

        public Bundle getBundleForUnreadConversation(UnreadConversation unreadConversation) {
            return null;
        }

        public ArrayList<Parcelable> getParcelableArrayListForActions(Action[] actionArr) {
            return null;
        }

        public UnreadConversation getUnreadConversationFromBundle(Bundle bundle, UnreadConversation.Factory factory, RemoteInput.Factory factory2) {
            return null;
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$NotificationCompatImpl */
    interface NotificationCompatImpl {
        Notification build(Builder builder, BuilderExtender builderExtender);

        Action getAction(Notification notification, int i);

        Action[] getActionsFromParcelableArrayList(ArrayList<Parcelable> arrayList);

        Bundle getBundleForUnreadConversation(UnreadConversation unreadConversation);

        ArrayList<Parcelable> getParcelableArrayListForActions(Action[] actionArr);

        UnreadConversation getUnreadConversationFromBundle(Bundle bundle, UnreadConversation.Factory factory, RemoteInput.Factory factory2);
    }

    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: android.support.v4.app.NotificationCompat$NotificationVisibility */
    public @interface NotificationVisibility {
    }

    /* renamed from: android.support.v4.app.NotificationCompat$Style */
    public static abstract class Style {
        CharSequence mBigContentTitle;
        @RestrictTo({Scope.LIBRARY_GROUP})
        protected Builder mBuilder;
        CharSequence mSummaryText;
        boolean mSummaryTextSet = false;

        private int calculateTopPadding() {
            Resources resources = this.mBuilder.mContext.getResources();
            int dimensionPixelSize = resources.getDimensionPixelSize(C0004R.dimen.notification_top_pad);
            int dimensionPixelSize2 = resources.getDimensionPixelSize(C0004R.dimen.notification_top_pad_large_text);
            float constrain = (constrain(resources.getConfiguration().fontScale, 1.0f, 1.3f) - 1.0f) / 0.29999995f;
            return Math.round((constrain * ((float) dimensionPixelSize2)) + (((float) dimensionPixelSize) * (1.0f - constrain)));
        }

        private static float constrain(float f, float f2, float f3) {
            return f < f2 ? f2 : f <= f3 ? f : f3;
        }

        private Bitmap createColoredBitmap(int i, int i2, int i3) {
            Drawable drawable = this.mBuilder.mContext.getResources().getDrawable(i);
            int i4 = i3 == 0 ? drawable.getIntrinsicWidth() : i3;
            if (i3 == 0) {
                i3 = drawable.getIntrinsicHeight();
            }
            Bitmap createBitmap = Bitmap.createBitmap(i4, i3, Config.ARGB_8888);
            drawable.setBounds(0, 0, i4, i3);
            if (i2 != 0) {
                drawable.mutate().setColorFilter(new PorterDuffColorFilter(i2, Mode.SRC_IN));
            }
            drawable.draw(new Canvas(createBitmap));
            return createBitmap;
        }

        private Bitmap createIconWithBackground(int i, int i2, int i3, int i4) {
            int i5 = C0004R.C0005drawable.notification_icon_background;
            if (i4 == 0) {
                i4 = 0;
            }
            Bitmap createColoredBitmap = createColoredBitmap(i5, i4, i2);
            Canvas canvas = new Canvas(createColoredBitmap);
            Drawable mutate = this.mBuilder.mContext.getResources().getDrawable(i).mutate();
            mutate.setFilterBitmap(true);
            int i6 = (i2 - i3) / 2;
            mutate.setBounds(i6, i6, i3 + i6, i3 + i6);
            mutate.setColorFilter(new PorterDuffColorFilter(-1, Mode.SRC_ATOP));
            mutate.draw(canvas);
            return createColoredBitmap;
        }

        private void hideNormalContent(RemoteViews remoteViews) {
            remoteViews.setViewVisibility(C0004R.C0006id.title, 8);
            remoteViews.setViewVisibility(C0004R.C0006id.text2, 8);
            remoteViews.setViewVisibility(C0004R.C0006id.text, 8);
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public void addCompatExtras(Bundle bundle) {
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public void apply(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews applyStandardTemplate(boolean z, int i, boolean z2) {
            boolean z3;
            boolean z4;
            boolean z5;
            boolean z6;
            Resources resources = this.mBuilder.mContext.getResources();
            RemoteViews remoteViews = new RemoteViews(this.mBuilder.mContext.getPackageName(), i);
            boolean z7 = this.mBuilder.getPriority() < -1;
            if (VERSION.SDK_INT >= 16 && VERSION.SDK_INT < 21) {
                if (z7) {
                    remoteViews.setInt(C0004R.C0006id.notification_background, "setBackgroundResource", C0004R.C0005drawable.notification_bg_low);
                    remoteViews.setInt(C0004R.C0006id.icon, "setBackgroundResource", C0004R.C0005drawable.notification_template_icon_low_bg);
                } else {
                    remoteViews.setInt(C0004R.C0006id.notification_background, "setBackgroundResource", C0004R.C0005drawable.notification_bg);
                    remoteViews.setInt(C0004R.C0006id.icon, "setBackgroundResource", C0004R.C0005drawable.notification_template_icon_bg);
                }
            }
            if (this.mBuilder.mLargeIcon != null) {
                if (VERSION.SDK_INT >= 16) {
                    remoteViews.setViewVisibility(C0004R.C0006id.icon, 0);
                    remoteViews.setImageViewBitmap(C0004R.C0006id.icon, this.mBuilder.mLargeIcon);
                } else {
                    remoteViews.setViewVisibility(C0004R.C0006id.icon, 8);
                }
                if (z && this.mBuilder.mNotification.icon != 0) {
                    int dimensionPixelSize = resources.getDimensionPixelSize(C0004R.dimen.notification_right_icon_size);
                    int dimensionPixelSize2 = resources.getDimensionPixelSize(C0004R.dimen.notification_small_icon_background_padding);
                    if (VERSION.SDK_INT >= 21) {
                        remoteViews.setImageViewBitmap(C0004R.C0006id.right_icon, createIconWithBackground(this.mBuilder.mNotification.icon, dimensionPixelSize, dimensionPixelSize - (dimensionPixelSize2 * 2), this.mBuilder.getColor()));
                    } else {
                        remoteViews.setImageViewBitmap(C0004R.C0006id.right_icon, createColoredBitmap(this.mBuilder.mNotification.icon, -1));
                    }
                    remoteViews.setViewVisibility(C0004R.C0006id.right_icon, 0);
                }
            } else if (z && this.mBuilder.mNotification.icon != 0) {
                remoteViews.setViewVisibility(C0004R.C0006id.icon, 0);
                if (VERSION.SDK_INT >= 21) {
                    remoteViews.setImageViewBitmap(C0004R.C0006id.icon, createIconWithBackground(this.mBuilder.mNotification.icon, resources.getDimensionPixelSize(C0004R.dimen.notification_large_icon_width) - resources.getDimensionPixelSize(C0004R.dimen.notification_big_circle_margin), resources.getDimensionPixelSize(C0004R.dimen.notification_small_icon_size_as_large), this.mBuilder.getColor()));
                } else {
                    remoteViews.setImageViewBitmap(C0004R.C0006id.icon, createColoredBitmap(this.mBuilder.mNotification.icon, -1));
                }
            }
            if (this.mBuilder.mContentTitle != null) {
                remoteViews.setTextViewText(C0004R.C0006id.title, this.mBuilder.mContentTitle);
            }
            if (this.mBuilder.mContentText != null) {
                remoteViews.setTextViewText(C0004R.C0006id.text, this.mBuilder.mContentText);
                z3 = true;
            } else {
                z3 = false;
            }
            boolean z8 = VERSION.SDK_INT < 21 && this.mBuilder.mLargeIcon != null;
            if (this.mBuilder.mContentInfo != null) {
                remoteViews.setTextViewText(C0004R.C0006id.info, this.mBuilder.mContentInfo);
                remoteViews.setViewVisibility(C0004R.C0006id.info, 0);
                z4 = true;
                z5 = true;
            } else if (this.mBuilder.mNumber > 0) {
                if (this.mBuilder.mNumber > resources.getInteger(C0004R.integer.status_bar_notification_info_maxnum)) {
                    remoteViews.setTextViewText(C0004R.C0006id.info, resources.getString(C0004R.string.status_bar_notification_info_overflow));
                } else {
                    remoteViews.setTextViewText(C0004R.C0006id.info, NumberFormat.getIntegerInstance().format((long) this.mBuilder.mNumber));
                }
                remoteViews.setViewVisibility(C0004R.C0006id.info, 0);
                z4 = true;
                z5 = true;
            } else {
                remoteViews.setViewVisibility(C0004R.C0006id.info, 8);
                z4 = z3;
                z5 = z8;
            }
            if (this.mBuilder.mSubText == null || VERSION.SDK_INT < 16) {
                z6 = false;
            } else {
                remoteViews.setTextViewText(C0004R.C0006id.text, this.mBuilder.mSubText);
                if (this.mBuilder.mContentText != null) {
                    remoteViews.setTextViewText(C0004R.C0006id.text2, this.mBuilder.mContentText);
                    remoteViews.setViewVisibility(C0004R.C0006id.text2, 0);
                    z6 = true;
                } else {
                    remoteViews.setViewVisibility(C0004R.C0006id.text2, 8);
                    z6 = false;
                }
            }
            if (z6 && VERSION.SDK_INT >= 16) {
                if (z2) {
                    remoteViews.setTextViewTextSize(C0004R.C0006id.text, 0, (float) resources.getDimensionPixelSize(C0004R.dimen.notification_subtext_size));
                }
                remoteViews.setViewPadding(C0004R.C0006id.line1, 0, 0, 0, 0);
            }
            if (this.mBuilder.getWhenIfShowing() != 0) {
                if (!this.mBuilder.mUseChronometer || VERSION.SDK_INT < 16) {
                    remoteViews.setViewVisibility(C0004R.C0006id.time, 0);
                    remoteViews.setLong(C0004R.C0006id.time, "setTime", this.mBuilder.getWhenIfShowing());
                } else {
                    remoteViews.setViewVisibility(C0004R.C0006id.chronometer, 0);
                    remoteViews.setLong(C0004R.C0006id.chronometer, "setBase", this.mBuilder.getWhenIfShowing() + (SystemClock.elapsedRealtime() - System.currentTimeMillis()));
                    remoteViews.setBoolean(C0004R.C0006id.chronometer, "setStarted", true);
                }
                z5 = true;
            }
            remoteViews.setViewVisibility(C0004R.C0006id.right_side, z5 ? 0 : 8);
            remoteViews.setViewVisibility(C0004R.C0006id.line3, z4 ? 0 : 8);
            return remoteViews;
        }

        public Notification build() {
            if (this.mBuilder != null) {
                return this.mBuilder.build();
            }
            return null;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public void buildIntoRemoteViews(RemoteViews remoteViews, RemoteViews remoteViews2) {
            hideNormalContent(remoteViews);
            remoteViews.removeAllViews(C0004R.C0006id.notification_main_column);
            remoteViews.addView(C0004R.C0006id.notification_main_column, remoteViews2.clone());
            remoteViews.setViewVisibility(C0004R.C0006id.notification_main_column, 0);
            if (VERSION.SDK_INT >= 21) {
                remoteViews.setViewPadding(C0004R.C0006id.notification_main_column_container, 0, calculateTopPadding(), 0, 0);
            }
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public Bitmap createColoredBitmap(int i, int i2) {
            return createColoredBitmap(i, i2, 0);
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews makeBigContentView(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            return null;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews makeContentView(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            return null;
        }

        @RestrictTo({Scope.LIBRARY_GROUP})
        public RemoteViews makeHeadsUpContentView(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor) {
            return null;
        }

        /* access modifiers changed from: protected */
        @RestrictTo({Scope.LIBRARY_GROUP})
        public void restoreFromCompatExtras(Bundle bundle) {
        }

        public void setBuilder(Builder builder) {
            if (this.mBuilder != builder) {
                this.mBuilder = builder;
                if (this.mBuilder != null) {
                    this.mBuilder.setStyle(this);
                }
            }
        }
    }

    /* renamed from: android.support.v4.app.NotificationCompat$WearableExtender */
    public static final class WearableExtender implements Extender {
        private static final int DEFAULT_CONTENT_ICON_GRAVITY = 8388613;
        private static final int DEFAULT_FLAGS = 1;
        private static final int DEFAULT_GRAVITY = 80;
        private static final String EXTRA_WEARABLE_EXTENSIONS = "android.wearable.EXTENSIONS";
        private static final int FLAG_BIG_PICTURE_AMBIENT = 32;
        private static final int FLAG_CONTENT_INTENT_AVAILABLE_OFFLINE = 1;
        private static final int FLAG_HINT_AVOID_BACKGROUND_CLIPPING = 16;
        private static final int FLAG_HINT_CONTENT_INTENT_LAUNCHES_ACTIVITY = 64;
        private static final int FLAG_HINT_HIDE_ICON = 2;
        private static final int FLAG_HINT_SHOW_BACKGROUND_ONLY = 4;
        private static final int FLAG_START_SCROLL_BOTTOM = 8;
        private static final String KEY_ACTIONS = "actions";
        private static final String KEY_BACKGROUND = "background";
        private static final String KEY_BRIDGE_TAG = "bridgeTag";
        private static final String KEY_CONTENT_ACTION_INDEX = "contentActionIndex";
        private static final String KEY_CONTENT_ICON = "contentIcon";
        private static final String KEY_CONTENT_ICON_GRAVITY = "contentIconGravity";
        private static final String KEY_CUSTOM_CONTENT_HEIGHT = "customContentHeight";
        private static final String KEY_CUSTOM_SIZE_PRESET = "customSizePreset";
        private static final String KEY_DISMISSAL_ID = "dismissalId";
        private static final String KEY_DISPLAY_INTENT = "displayIntent";
        private static final String KEY_FLAGS = "flags";
        private static final String KEY_GRAVITY = "gravity";
        private static final String KEY_HINT_SCREEN_TIMEOUT = "hintScreenTimeout";
        private static final String KEY_PAGES = "pages";
        public static final int SCREEN_TIMEOUT_LONG = -1;
        public static final int SCREEN_TIMEOUT_SHORT = 0;
        public static final int SIZE_DEFAULT = 0;
        public static final int SIZE_FULL_SCREEN = 5;
        public static final int SIZE_LARGE = 4;
        public static final int SIZE_MEDIUM = 3;
        public static final int SIZE_SMALL = 2;
        public static final int SIZE_XSMALL = 1;
        public static final int UNSET_ACTION_INDEX = -1;
        private ArrayList<Action> mActions = new ArrayList<>();
        private Bitmap mBackground;
        private String mBridgeTag;
        private int mContentActionIndex = -1;
        private int mContentIcon;
        private int mContentIconGravity = 8388613;
        private int mCustomContentHeight;
        private int mCustomSizePreset = 0;
        private String mDismissalId;
        private PendingIntent mDisplayIntent;
        private int mFlags = 1;
        private int mGravity = 80;
        private int mHintScreenTimeout;
        private ArrayList<Notification> mPages = new ArrayList<>();

        public WearableExtender() {
        }

        public WearableExtender(Notification notification) {
            Bundle extras = NotificationCompat.getExtras(notification);
            Bundle bundle = extras != null ? extras.getBundle(EXTRA_WEARABLE_EXTENSIONS) : null;
            if (bundle != null) {
                Action[] actionsFromParcelableArrayList = NotificationCompat.IMPL.getActionsFromParcelableArrayList(bundle.getParcelableArrayList(KEY_ACTIONS));
                if (actionsFromParcelableArrayList != null) {
                    Collections.addAll(this.mActions, actionsFromParcelableArrayList);
                }
                this.mFlags = bundle.getInt(KEY_FLAGS, 1);
                this.mDisplayIntent = (PendingIntent) bundle.getParcelable(KEY_DISPLAY_INTENT);
                Notification[] notificationArrayFromBundle = NotificationCompat.getNotificationArrayFromBundle(bundle, KEY_PAGES);
                if (notificationArrayFromBundle != null) {
                    Collections.addAll(this.mPages, notificationArrayFromBundle);
                }
                this.mBackground = (Bitmap) bundle.getParcelable(KEY_BACKGROUND);
                this.mContentIcon = bundle.getInt(KEY_CONTENT_ICON);
                this.mContentIconGravity = bundle.getInt(KEY_CONTENT_ICON_GRAVITY, 8388613);
                this.mContentActionIndex = bundle.getInt(KEY_CONTENT_ACTION_INDEX, -1);
                this.mCustomSizePreset = bundle.getInt(KEY_CUSTOM_SIZE_PRESET, 0);
                this.mCustomContentHeight = bundle.getInt(KEY_CUSTOM_CONTENT_HEIGHT);
                this.mGravity = bundle.getInt(KEY_GRAVITY, 80);
                this.mHintScreenTimeout = bundle.getInt(KEY_HINT_SCREEN_TIMEOUT);
                this.mDismissalId = bundle.getString(KEY_DISMISSAL_ID);
                this.mBridgeTag = bundle.getString(KEY_BRIDGE_TAG);
            }
        }

        private void setFlag(int i, boolean z) {
            if (z) {
                this.mFlags |= i;
            } else {
                this.mFlags &= i ^ -1;
            }
        }

        public WearableExtender addAction(Action action) {
            this.mActions.add(action);
            return this;
        }

        public WearableExtender addActions(List<Action> list) {
            this.mActions.addAll(list);
            return this;
        }

        public WearableExtender addPage(Notification notification) {
            this.mPages.add(notification);
            return this;
        }

        public WearableExtender addPages(List<Notification> list) {
            this.mPages.addAll(list);
            return this;
        }

        public WearableExtender clearActions() {
            this.mActions.clear();
            return this;
        }

        public WearableExtender clearPages() {
            this.mPages.clear();
            return this;
        }

        public WearableExtender clone() {
            WearableExtender wearableExtender = new WearableExtender();
            wearableExtender.mActions = new ArrayList<>(this.mActions);
            wearableExtender.mFlags = this.mFlags;
            wearableExtender.mDisplayIntent = this.mDisplayIntent;
            wearableExtender.mPages = new ArrayList<>(this.mPages);
            wearableExtender.mBackground = this.mBackground;
            wearableExtender.mContentIcon = this.mContentIcon;
            wearableExtender.mContentIconGravity = this.mContentIconGravity;
            wearableExtender.mContentActionIndex = this.mContentActionIndex;
            wearableExtender.mCustomSizePreset = this.mCustomSizePreset;
            wearableExtender.mCustomContentHeight = this.mCustomContentHeight;
            wearableExtender.mGravity = this.mGravity;
            wearableExtender.mHintScreenTimeout = this.mHintScreenTimeout;
            wearableExtender.mDismissalId = this.mDismissalId;
            wearableExtender.mBridgeTag = this.mBridgeTag;
            return wearableExtender;
        }

        public Builder extend(Builder builder) {
            Bundle bundle = new Bundle();
            if (!this.mActions.isEmpty()) {
                bundle.putParcelableArrayList(KEY_ACTIONS, NotificationCompat.IMPL.getParcelableArrayListForActions((Action[]) this.mActions.toArray(new Action[this.mActions.size()])));
            }
            if (this.mFlags != 1) {
                bundle.putInt(KEY_FLAGS, this.mFlags);
            }
            if (this.mDisplayIntent != null) {
                bundle.putParcelable(KEY_DISPLAY_INTENT, this.mDisplayIntent);
            }
            if (!this.mPages.isEmpty()) {
                bundle.putParcelableArray(KEY_PAGES, (Parcelable[]) this.mPages.toArray(new Notification[this.mPages.size()]));
            }
            if (this.mBackground != null) {
                bundle.putParcelable(KEY_BACKGROUND, this.mBackground);
            }
            if (this.mContentIcon != 0) {
                bundle.putInt(KEY_CONTENT_ICON, this.mContentIcon);
            }
            if (this.mContentIconGravity != 8388613) {
                bundle.putInt(KEY_CONTENT_ICON_GRAVITY, this.mContentIconGravity);
            }
            if (this.mContentActionIndex != -1) {
                bundle.putInt(KEY_CONTENT_ACTION_INDEX, this.mContentActionIndex);
            }
            if (this.mCustomSizePreset != 0) {
                bundle.putInt(KEY_CUSTOM_SIZE_PRESET, this.mCustomSizePreset);
            }
            if (this.mCustomContentHeight != 0) {
                bundle.putInt(KEY_CUSTOM_CONTENT_HEIGHT, this.mCustomContentHeight);
            }
            if (this.mGravity != 80) {
                bundle.putInt(KEY_GRAVITY, this.mGravity);
            }
            if (this.mHintScreenTimeout != 0) {
                bundle.putInt(KEY_HINT_SCREEN_TIMEOUT, this.mHintScreenTimeout);
            }
            if (this.mDismissalId != null) {
                bundle.putString(KEY_DISMISSAL_ID, this.mDismissalId);
            }
            if (this.mBridgeTag != null) {
                bundle.putString(KEY_BRIDGE_TAG, this.mBridgeTag);
            }
            builder.getExtras().putBundle(EXTRA_WEARABLE_EXTENSIONS, bundle);
            return builder;
        }

        public List<Action> getActions() {
            return this.mActions;
        }

        public Bitmap getBackground() {
            return this.mBackground;
        }

        public String getBridgeTag() {
            return this.mBridgeTag;
        }

        public int getContentAction() {
            return this.mContentActionIndex;
        }

        public int getContentIcon() {
            return this.mContentIcon;
        }

        public int getContentIconGravity() {
            return this.mContentIconGravity;
        }

        public boolean getContentIntentAvailableOffline() {
            return (this.mFlags & 1) != 0;
        }

        public int getCustomContentHeight() {
            return this.mCustomContentHeight;
        }

        public int getCustomSizePreset() {
            return this.mCustomSizePreset;
        }

        public String getDismissalId() {
            return this.mDismissalId;
        }

        public PendingIntent getDisplayIntent() {
            return this.mDisplayIntent;
        }

        public int getGravity() {
            return this.mGravity;
        }

        public boolean getHintAmbientBigPicture() {
            return (this.mFlags & 32) != 0;
        }

        public boolean getHintAvoidBackgroundClipping() {
            return (this.mFlags & 16) != 0;
        }

        public boolean getHintContentIntentLaunchesActivity() {
            return (this.mFlags & 64) != 0;
        }

        public boolean getHintHideIcon() {
            return (this.mFlags & 2) != 0;
        }

        public int getHintScreenTimeout() {
            return this.mHintScreenTimeout;
        }

        public boolean getHintShowBackgroundOnly() {
            return (this.mFlags & 4) != 0;
        }

        public List<Notification> getPages() {
            return this.mPages;
        }

        public boolean getStartScrollBottom() {
            return (this.mFlags & 8) != 0;
        }

        public WearableExtender setBackground(Bitmap bitmap) {
            this.mBackground = bitmap;
            return this;
        }

        public WearableExtender setBridgeTag(String str) {
            this.mBridgeTag = str;
            return this;
        }

        public WearableExtender setContentAction(int i) {
            this.mContentActionIndex = i;
            return this;
        }

        public WearableExtender setContentIcon(int i) {
            this.mContentIcon = i;
            return this;
        }

        public WearableExtender setContentIconGravity(int i) {
            this.mContentIconGravity = i;
            return this;
        }

        public WearableExtender setContentIntentAvailableOffline(boolean z) {
            setFlag(1, z);
            return this;
        }

        public WearableExtender setCustomContentHeight(int i) {
            this.mCustomContentHeight = i;
            return this;
        }

        public WearableExtender setCustomSizePreset(int i) {
            this.mCustomSizePreset = i;
            return this;
        }

        public WearableExtender setDismissalId(String str) {
            this.mDismissalId = str;
            return this;
        }

        public WearableExtender setDisplayIntent(PendingIntent pendingIntent) {
            this.mDisplayIntent = pendingIntent;
            return this;
        }

        public WearableExtender setGravity(int i) {
            this.mGravity = i;
            return this;
        }

        public WearableExtender setHintAmbientBigPicture(boolean z) {
            setFlag(32, z);
            return this;
        }

        public WearableExtender setHintAvoidBackgroundClipping(boolean z) {
            setFlag(16, z);
            return this;
        }

        public WearableExtender setHintContentIntentLaunchesActivity(boolean z) {
            setFlag(64, z);
            return this;
        }

        public WearableExtender setHintHideIcon(boolean z) {
            setFlag(2, z);
            return this;
        }

        public WearableExtender setHintScreenTimeout(int i) {
            this.mHintScreenTimeout = i;
            return this;
        }

        public WearableExtender setHintShowBackgroundOnly(boolean z) {
            setFlag(4, z);
            return this;
        }

        public WearableExtender setStartScrollBottom(boolean z) {
            setFlag(8, z);
            return this;
        }
    }

    static {
        if (VERSION.SDK_INT >= 26) {
            IMPL = new NotificationCompatApi26Impl();
        } else if (VERSION.SDK_INT >= 24) {
            IMPL = new NotificationCompatApi24Impl();
        } else if (VERSION.SDK_INT >= 21) {
            IMPL = new NotificationCompatApi21Impl();
        } else if (VERSION.SDK_INT >= 20) {
            IMPL = new NotificationCompatApi20Impl();
        } else if (VERSION.SDK_INT >= 19) {
            IMPL = new NotificationCompatApi19Impl();
        } else if (VERSION.SDK_INT >= 16) {
            IMPL = new NotificationCompatApi16Impl();
        } else {
            IMPL = new NotificationCompatBaseImpl();
        }
    }

    static void addActionsToBuilder(NotificationBuilderWithActions notificationBuilderWithActions, ArrayList<Action> arrayList) {
        Iterator it = arrayList.iterator();
        while (it.hasNext()) {
            notificationBuilderWithActions.addAction((Action) it.next());
        }
    }

    public static Action getAction(Notification notification, int i) {
        return IMPL.getAction(notification, i);
    }

    public static int getActionCount(Notification notification) {
        if (VERSION.SDK_INT >= 19) {
            if (notification.actions != null) {
                return notification.actions.length;
            }
            return 0;
        } else if (VERSION.SDK_INT >= 16) {
            return NotificationCompatJellybean.getActionCount(notification);
        } else {
            return 0;
        }
    }

    public static int getBadgeIconType(Notification notification) {
        if (VERSION.SDK_INT >= 26) {
            return notification.getBadgeIconType();
        }
        return 0;
    }

    public static String getCategory(Notification notification) {
        if (VERSION.SDK_INT >= 21) {
            return notification.category;
        }
        return null;
    }

    public static String getChannelId(Notification notification) {
        if (VERSION.SDK_INT >= 26) {
            return notification.getChannelId();
        }
        return null;
    }

    public static Bundle getExtras(Notification notification) {
        if (VERSION.SDK_INT >= 19) {
            return notification.extras;
        }
        if (VERSION.SDK_INT >= 16) {
            return NotificationCompatJellybean.getExtras(notification);
        }
        return null;
    }

    public static String getGroup(Notification notification) {
        if (VERSION.SDK_INT >= 20) {
            return notification.getGroup();
        }
        if (VERSION.SDK_INT >= 19) {
            return notification.extras.getString(NotificationCompatExtras.EXTRA_GROUP_KEY);
        }
        if (VERSION.SDK_INT >= 16) {
            return NotificationCompatJellybean.getExtras(notification).getString(NotificationCompatExtras.EXTRA_GROUP_KEY);
        }
        return null;
    }

    public static int getGroupAlertBehavior(Notification notification) {
        if (VERSION.SDK_INT >= 26) {
            return notification.getGroupAlertBehavior();
        }
        return 0;
    }

    public static boolean getLocalOnly(Notification notification) {
        if (VERSION.SDK_INT >= 20) {
            return (notification.flags & 256) != 0;
        }
        if (VERSION.SDK_INT >= 19) {
            return notification.extras.getBoolean(NotificationCompatExtras.EXTRA_LOCAL_ONLY);
        }
        if (VERSION.SDK_INT >= 16) {
            return NotificationCompatJellybean.getExtras(notification).getBoolean(NotificationCompatExtras.EXTRA_LOCAL_ONLY);
        }
        return false;
    }

    static Notification[] getNotificationArrayFromBundle(Bundle bundle, String str) {
        Parcelable[] parcelableArray = bundle.getParcelableArray(str);
        if ((parcelableArray instanceof Notification[]) || parcelableArray == null) {
            return (Notification[]) parcelableArray;
        }
        Notification[] notificationArr = new Notification[parcelableArray.length];
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 < parcelableArray.length) {
                notificationArr[i2] = (Notification) parcelableArray[i2];
                i = i2 + 1;
            } else {
                bundle.putParcelableArray(str, notificationArr);
                return notificationArr;
            }
        }
    }

    public static String getShortcutId(Notification notification) {
        if (VERSION.SDK_INT >= 26) {
            return notification.getShortcutId();
        }
        return null;
    }

    public static String getSortKey(Notification notification) {
        if (VERSION.SDK_INT >= 20) {
            return notification.getSortKey();
        }
        if (VERSION.SDK_INT >= 19) {
            return notification.extras.getString(NotificationCompatExtras.EXTRA_SORT_KEY);
        }
        if (VERSION.SDK_INT >= 16) {
            return NotificationCompatJellybean.getExtras(notification).getString(NotificationCompatExtras.EXTRA_SORT_KEY);
        }
        return null;
    }

    public static long getTimeoutAfter(Notification notification) {
        if (VERSION.SDK_INT >= 26) {
            return notification.getTimeoutAfter();
        }
        return 0;
    }

    public static boolean isGroupSummary(Notification notification) {
        if (VERSION.SDK_INT >= 20) {
            return (notification.flags & 512) != 0;
        }
        if (VERSION.SDK_INT >= 19) {
            return notification.extras.getBoolean(NotificationCompatExtras.EXTRA_GROUP_SUMMARY);
        }
        if (VERSION.SDK_INT >= 16) {
            return NotificationCompatJellybean.getExtras(notification).getBoolean(NotificationCompatExtras.EXTRA_GROUP_SUMMARY);
        }
        return false;
    }
}
