.class public abstract Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;
.super Landroid/app/Service;
.source "CustomTabsService.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$Relation;,
        Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$Result;
    }
.end annotation


# static fields
.field public static final ACTION_CUSTOM_TABS_CONNECTION:Ljava/lang/String; = "android.support.customtabs.action.CustomTabsService"

.field public static final KEY_URL:Ljava/lang/String; = "android.support.customtabs.otherurls.URL"

.field public static final RELATION_HANDLE_ALL_URLS:I = 0x2

.field public static final RELATION_USE_AS_ORIGIN:I = 0x1

.field public static final RESULT_FAILURE_DISALLOWED:I = -0x1

.field public static final RESULT_FAILURE_MESSAGING_ERROR:I = -0x3

.field public static final RESULT_FAILURE_REMOTE_ERROR:I = -0x2

.field public static final RESULT_SUCCESS:I


# instance fields
.field private mBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub;

.field private final mDeathRecipientMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Landroid/os/IBinder;",
            "Landroid/os/IBinder$DeathRecipient;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 40
    invoke-direct {p0}, Landroid/app/Service;-><init>()V

    .line 94
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->mDeathRecipientMap:Ljava/util/Map;

    .line 96
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->mBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;)Ljava/util/Map;
    .locals 0

    .line 40
    iget-object p0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->mDeathRecipientMap:Ljava/util/Map;

    return-object p0
.end method


# virtual methods
.method protected cleanUpSession(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Z
    .locals 3

    const/4 v0, 0x0

    .line 178
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->mDeathRecipientMap:Ljava/util/Map;

    monitor-enter v1
    :try_end_0
    .catch Ljava/util/NoSuchElementException; {:try_start_0 .. :try_end_0} :catch_0

    .line 179
    :try_start_1
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->getCallbackBinder()Landroid/os/IBinder;

    move-result-object p1

    .line 180
    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->mDeathRecipientMap:Ljava/util/Map;

    .line 181
    invoke-interface {v2, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/os/IBinder$DeathRecipient;

    .line 182
    invoke-interface {p1, v2, v0}, Landroid/os/IBinder;->unlinkToDeath(Landroid/os/IBinder$DeathRecipient;I)Z

    .line 183
    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->mDeathRecipientMap:Ljava/util/Map;

    invoke-interface {v2, p1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 184
    monitor-exit v1

    const/4 p1, 0x1

    return p1

    :catchall_0
    move-exception p1

    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :try_start_2
    throw p1
    :try_end_2
    .catch Ljava/util/NoSuchElementException; {:try_start_2 .. :try_end_2} :catch_0

    :catch_0
    return v0
.end method

.method protected abstract extraCommand(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;
.end method

.method protected abstract mayLaunchUrl(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;Landroid/net/Uri;Landroid/os/Bundle;Ljava/util/List;)Z
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;",
            "Landroid/net/Uri;",
            "Landroid/os/Bundle;",
            "Ljava/util/List<",
            "Landroid/os/Bundle;",
            ">;)Z"
        }
    .end annotation
.end method

.method protected abstract newSession(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Z
.end method

.method public onBind(Landroid/content/Intent;)Landroid/os/IBinder;
    .locals 0

    .line 164
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->mBinder:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub;

    return-object p1
.end method

.method protected abstract postMessage(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;Ljava/lang/String;Landroid/os/Bundle;)I
.end method

.method protected abstract requestPostMessageChannel(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;Landroid/net/Uri;)Z
.end method

.method protected abstract updateVisuals(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;Landroid/os/Bundle;)Z
.end method

.method protected abstract validateRelationship(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;ILandroid/net/Uri;Landroid/os/Bundle;)Z
.end method

.method protected abstract warmup(J)Z
.end method
