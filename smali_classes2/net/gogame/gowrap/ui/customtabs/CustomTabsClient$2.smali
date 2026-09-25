.class Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;
.super Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;
.source "CustomTabsClient.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->newSession(Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private mHandler:Landroid/os/Handler;

.field final synthetic this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;

.field final synthetic val$callback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;)V
    .locals 0

    .line 183
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->val$callback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;-><init>()V

    .line 184
    new-instance p1, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object p2

    invoke-direct {p1, p2}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->mHandler:Landroid/os/Handler;

    return-void
.end method


# virtual methods
.method public extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 200
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->val$callback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

    if-nez v0, :cond_0

    return-void

    .line 201
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->mHandler:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$2;

    invoke-direct {v1, p0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$2;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;Ljava/lang/String;Landroid/os/Bundle;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public onMessageChannelReady(Landroid/os/Bundle;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 212
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->val$callback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

    if-nez v0, :cond_0

    return-void

    .line 213
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->mHandler:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$3;

    invoke-direct {v1, p0, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$3;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;Landroid/os/Bundle;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public onNavigationEvent(ILandroid/os/Bundle;)V
    .locals 2

    .line 188
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->val$callback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

    if-nez v0, :cond_0

    return-void

    .line 189
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->mHandler:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$1;

    invoke-direct {v1, p0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$1;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;ILandroid/os/Bundle;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public onPostMessage(Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 224
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->val$callback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

    if-nez v0, :cond_0

    return-void

    .line 225
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->mHandler:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$4;

    invoke-direct {v1, p0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$4;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;Ljava/lang/String;Landroid/os/Bundle;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public onRelationshipValidationResult(ILandroid/net/Uri;ZLandroid/os/Bundle;)V
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 237
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->val$callback:Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;

    if-nez v0, :cond_0

    return-void

    .line 238
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;->mHandler:Landroid/os/Handler;

    new-instance v7, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$5;

    move-object v1, v7

    move-object v2, p0

    move v3, p1

    move-object v4, p2

    move v5, p3

    move-object v6, p4

    invoke-direct/range {v1 .. v6}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2$5;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;ILandroid/net/Uri;ZLandroid/os/Bundle;)V

    invoke-virtual {v0, v7}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
