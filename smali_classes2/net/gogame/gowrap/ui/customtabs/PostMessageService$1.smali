.class Lnet/gogame/gowrap/ui/customtabs/PostMessageService$1;
.super Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub;
.source "PostMessageService.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/customtabs/PostMessageService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/customtabs/PostMessageService;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/PostMessageService;)V
    .locals 0

    .line 29
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/PostMessageService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/PostMessageService;

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub;-><init>()V

    return-void
.end method


# virtual methods
.method public onMessageChannelReady(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 34
    invoke-interface {p1, p2}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->onMessageChannelReady(Landroid/os/Bundle;)V

    return-void
.end method

.method public onPostMessage(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 40
    invoke-interface {p1, p2, p3}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->onPostMessage(Ljava/lang/String;Landroid/os/Bundle;)V

    return-void
.end method
