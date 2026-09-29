.class public final Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;
.super Ljava/lang/Object;
.source "CustomTabsSession.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "CustomTabsSession"


# instance fields
.field private final mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

.field private final mComponentName:Landroid/content/ComponentName;

.field private final mLock:Ljava/lang/Object;

.field private final mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/content/ComponentName;)V
    .locals 1

    .line 54
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 37
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mLock:Ljava/lang/Object;

    .line 55
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    .line 56
    iput-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    .line 57
    iput-object p3, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mComponentName:Landroid/content/ComponentName;

    return-void
.end method

.method public static createDummySessionForTesting(Landroid/content/ComponentName;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;
    .locals 3

    .line 50
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$DummyCallback;

    invoke-direct {v1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$DummyCallback;-><init>()V

    const/4 v2, 0x0

    invoke-direct {v0, v2, v1, p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/content/ComponentName;)V

    return-object v0
.end method


# virtual methods
.method getBinder()Landroid/os/IBinder;
    .locals 1

    .line 224
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object v0

    return-object v0
.end method

.method getComponentName()Landroid/content/ComponentName;
    .locals 1

    .line 228
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mComponentName:Landroid/content/ComponentName;

    return-object v0
.end method

.method public mayLaunchUrl(Landroid/net/Uri;Landroid/os/Bundle;Ljava/util/List;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/net/Uri;",
            "Landroid/os/Bundle;",
            "Ljava/util/List<",
            "Landroid/os/Bundle;",
            ">;)Z"
        }
    .end annotation

    .line 78
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {v0, v1, p1, p2, p3}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->mayLaunchUrl(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/net/Uri;Landroid/os/Bundle;Ljava/util/List;)Z

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    const/4 p1, 0x0

    return p1
.end method

.method public postMessage(Ljava/lang/String;Landroid/os/Bundle;)I
    .locals 3

    .line 187
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mLock:Ljava/lang/Object;

    monitor-enter v0

    .line 189
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {v1, v2, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->postMessage(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Ljava/lang/String;Landroid/os/Bundle;)I

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :try_start_1
    monitor-exit v0

    return p1

    :catchall_0
    move-exception p1

    goto :goto_0

    :catch_0
    const/4 p1, -0x2

    .line 191
    monitor-exit v0

    return p1

    .line 193
    :goto_0
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method public requestPostMessageChannel(Landroid/net/Uri;)Z
    .locals 2

    .line 167
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {v0, v1, p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->requestPostMessageChannel(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/net/Uri;)Z

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    const/4 p1, 0x0

    return p1
.end method

.method public setActionButton(Landroid/graphics/Bitmap;Ljava/lang/String;)Z
    .locals 2

    .line 93
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "android.support.customtabs.customaction.ICON"

    .line 94
    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    const-string p1, "android.support.customtabs.customaction.DESCRIPTION"

    .line 95
    invoke-virtual {v0, p1, p2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 97
    new-instance p1, Landroid/os/Bundle;

    invoke-direct {p1}, Landroid/os/Bundle;-><init>()V

    const-string p2, "android.support.customtabs.extra.ACTION_BUTTON_BUNDLE"

    .line 98
    invoke-virtual {p1, p2, v0}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 100
    :try_start_0
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {p2, v0, p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->updateVisuals(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)Z

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    const/4 p1, 0x0

    return p1
.end method

.method public setSecondaryToolbarViews(Landroid/widget/RemoteViews;[ILandroid/app/PendingIntent;)Z
    .locals 2

    .line 118
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "android.support.customtabs.extra.EXTRA_REMOTEVIEWS"

    .line 119
    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    const-string p1, "android.support.customtabs.extra.EXTRA_REMOTEVIEWS_VIEW_IDS"

    .line 120
    invoke-virtual {v0, p1, p2}, Landroid/os/Bundle;->putIntArray(Ljava/lang/String;[I)V

    const-string p1, "android.support.customtabs.extra.EXTRA_REMOTEVIEWS_PENDINGINTENT"

    .line 121
    invoke-virtual {v0, p1, p3}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 123
    :try_start_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {p1, p2, v0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->updateVisuals(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)Z

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    const/4 p1, 0x0

    return p1
.end method

.method public setToolbarItem(ILandroid/graphics/Bitmap;Ljava/lang/String;)Z
    .locals 2
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 142
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "android.support.customtabs.customaction.ID"

    .line 143
    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string p1, "android.support.customtabs.customaction.ICON"

    .line 144
    invoke-virtual {v0, p1, p2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    const-string p1, "android.support.customtabs.customaction.DESCRIPTION"

    .line 145
    invoke-virtual {v0, p1, p3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 147
    new-instance p1, Landroid/os/Bundle;

    invoke-direct {p1}, Landroid/os/Bundle;-><init>()V

    const-string p2, "android.support.customtabs.extra.ACTION_BUTTON_BUNDLE"

    .line 148
    invoke-virtual {p1, p2, v0}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 150
    :try_start_0
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    iget-object p3, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {p2, p3, p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->updateVisuals(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)Z

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    const/4 p1, 0x0

    return p1
.end method

.method public validateRelationship(ILandroid/net/Uri;Landroid/os/Bundle;)Z
    .locals 3

    const/4 v0, 0x0

    const/4 v1, 0x1

    if-lt p1, v1, :cond_1

    const/4 v1, 0x2

    if-le p1, v1, :cond_0

    goto :goto_0

    .line 217
    :cond_0
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->mCallback:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    invoke-interface {v1, v2, p1, p2, p3}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->validateRelationship(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;ILandroid/net/Uri;Landroid/os/Bundle;)Z

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    return v0

    :cond_1
    :goto_0
    return v0
.end method
