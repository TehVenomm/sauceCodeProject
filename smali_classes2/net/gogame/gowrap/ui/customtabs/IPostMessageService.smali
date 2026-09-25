.class public interface abstract Lnet/gogame/gowrap/ui/customtabs/IPostMessageService;
.super Ljava/lang/Object;
.source "IPostMessageService.java"

# interfaces
.implements Landroid/os/IInterface;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub;
    }
.end annotation


# virtual methods
.method public abstract onMessageChannelReady(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation
.end method

.method public abstract onPostMessage(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Ljava/lang/String;Landroid/os/Bundle;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation
.end method
