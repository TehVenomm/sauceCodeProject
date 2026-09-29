.class Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub$Proxy;
.super Ljava/lang/Object;
.source "IPostMessageService.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/customtabs/IPostMessageService;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "Proxy"
.end annotation


# instance fields
.field private mRemote:Landroid/os/IBinder;


# direct methods
.method constructor <init>(Landroid/os/IBinder;)V
    .locals 0

    .line 94
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 95
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    return-void
.end method


# virtual methods
.method public asBinder()Landroid/os/IBinder;
    .locals 1

    .line 99
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    return-object v0
.end method

.method public getInterfaceDescriptor()Ljava/lang/String;
    .locals 1

    const-string v0, "net.gogame.gowrap.ui.customtabs.IPostMessageService"

    return-object v0
.end method

.method public onMessageChannelReady(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 107
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 108
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.IPostMessageService"

    .line 110
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 111
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeStrongBinder(Landroid/os/IBinder;)V

    const/4 p1, 0x0

    if-eqz p2, :cond_1

    const/4 v2, 0x1

    .line 113
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 114
    invoke-virtual {p2, v0, p1}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 117
    :cond_1
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 119
    :goto_1
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v2, 0x2

    invoke-interface {p2, v2, v0, v1, p1}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 120
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 123
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 124
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return-void

    :catchall_0
    move-exception p1

    .line 123
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 124
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 125
    throw p1
.end method

.method public onPostMessage(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 129
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 130
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.IPostMessageService"

    .line 132
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 133
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeStrongBinder(Landroid/os/IBinder;)V

    .line 134
    invoke-virtual {v0, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    const/4 p1, 0x0

    if-eqz p3, :cond_1

    const/4 p2, 0x1

    .line 136
    invoke-virtual {v0, p2}, Landroid/os/Parcel;->writeInt(I)V

    .line 137
    invoke-virtual {p3, v0, p1}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 140
    :cond_1
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 142
    :goto_1
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 p3, 0x3

    invoke-interface {p2, p3, v0, v1, p1}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 143
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 146
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 147
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return-void

    :catchall_0
    move-exception p1

    .line 146
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 147
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 148
    throw p1
.end method
