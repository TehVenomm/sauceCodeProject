.class Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;
.super Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;
.source "CustomTabsSessionToken.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)V
    .locals 0

    .line 84
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;-><init>()V

    return-void
.end method


# virtual methods
.method public extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 1

    .line 98
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    move-result-object v0

    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const-string p1, "CustomTabsSessionToken"

    const-string p2, "RemoteException during ICustomTabsCallback transaction"

    .line 100
    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public onMessageChannelReady(Landroid/os/Bundle;)V
    .locals 1

    .line 107
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    move-result-object v0

    invoke-interface {v0, p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->onMessageChannelReady(Landroid/os/Bundle;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const-string p1, "CustomTabsSessionToken"

    const-string v0, "RemoteException during ICustomTabsCallback transaction"

    .line 109
    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public onNavigationEvent(ILandroid/os/Bundle;)V
    .locals 1

    .line 89
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    move-result-object v0

    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->onNavigationEvent(ILandroid/os/Bundle;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const-string p1, "CustomTabsSessionToken"

    const-string p2, "RemoteException during ICustomTabsCallback transaction"

    .line 91
    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public onPostMessage(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 1

    .line 116
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    move-result-object v0

    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->onPostMessage(Ljava/lang/String;Landroid/os/Bundle;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const-string p1, "CustomTabsSessionToken"

    const-string p2, "RemoteException during ICustomTabsCallback transaction"

    .line 118
    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public onRelationshipValidationResult(ILandroid/net/Uri;ZLandroid/os/Bundle;)V
    .locals 1

    .line 126
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;->access$000(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    move-result-object v0

    invoke-interface {v0, p1, p2, p3, p4}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->onRelationshipValidationResult(ILandroid/net/Uri;ZLandroid/os/Bundle;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const-string p1, "CustomTabsSessionToken"

    const-string p2, "RemoteException during ICustomTabsCallback transaction"

    .line 128
    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method
