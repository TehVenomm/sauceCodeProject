.class Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;
.super Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub;
.source "CustomTabsService.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;)V
    .locals 0

    .line 96
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub;-><init>()V

    return-void
.end method


# virtual methods
.method public extraCommand(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;
    .locals 1

    .line 132
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    invoke-virtual {v0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->extraCommand(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object p1

    return-object p1
.end method

.method public mayLaunchUrl(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/net/Uri;Landroid/os/Bundle;Ljava/util/List;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;",
            "Landroid/net/Uri;",
            "Landroid/os/Bundle;",
            "Ljava/util/List<",
            "Landroid/os/Bundle;",
            ">;)Z"
        }
    .end annotation

    .line 126
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V

    invoke-virtual {v0, v1, p2, p3, p4}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->mayLaunchUrl(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;Landroid/net/Uri;Landroid/os/Bundle;Ljava/util/List;)Z

    move-result p1

    return p1
.end method

.method public newSession(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)Z
    .locals 5

    .line 105
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V

    const/4 v1, 0x0

    .line 107
    :try_start_0
    new-instance v2, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1$1;

    invoke-direct {v2, p0, v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1$1;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)V

    .line 113
    iget-object v3, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    invoke-static {v3}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;)Ljava/util/Map;

    move-result-object v3

    monitor-enter v3
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    .line 114
    :try_start_1
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object v4

    invoke-interface {v4, v2, v1}, Landroid/os/IBinder;->linkToDeath(Landroid/os/IBinder$DeathRecipient;I)V

    .line 115
    iget-object v4, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    invoke-static {v4}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;)Ljava/util/Map;

    move-result-object v4

    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    invoke-interface {v4, p1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 116
    monitor-exit v3
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 117
    :try_start_2
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->newSession(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Z

    move-result p1
    :try_end_2
    .catch Landroid/os/RemoteException; {:try_start_2 .. :try_end_2} :catch_0

    return p1

    :catchall_0
    move-exception p1

    .line 116
    :try_start_3
    monitor-exit v3
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    :try_start_4
    throw p1
    :try_end_4
    .catch Landroid/os/RemoteException; {:try_start_4 .. :try_end_4} :catch_0

    :catch_0
    return v1
.end method

.method public postMessage(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Ljava/lang/String;Landroid/os/Bundle;)I
    .locals 2

    .line 150
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V

    invoke-virtual {v0, v1, p2, p3}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->postMessage(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;Ljava/lang/String;Landroid/os/Bundle;)I

    move-result p1

    return p1
.end method

.method public requestPostMessageChannel(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/net/Uri;)Z
    .locals 2

    .line 144
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V

    invoke-virtual {v0, v1, p2}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->requestPostMessageChannel(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;Landroid/net/Uri;)Z

    move-result p1

    return p1
.end method

.method public updateVisuals(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)Z
    .locals 2

    .line 137
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V

    invoke-virtual {v0, v1, p2}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->updateVisuals(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;Landroid/os/Bundle;)Z

    move-result p1

    return p1
.end method

.method public validateRelationship(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;ILandroid/net/Uri;Landroid/os/Bundle;)Z
    .locals 2

    .line 157
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-direct {v1, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V

    invoke-virtual {v0, v1, p2, p3, p4}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->validateRelationship(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;ILandroid/net/Uri;Landroid/os/Bundle;)Z

    move-result p1

    return p1
.end method

.method public warmup(J)Z
    .locals 1

    .line 100
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    invoke-virtual {v0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->warmup(J)Z

    move-result p1

    return p1
.end method
