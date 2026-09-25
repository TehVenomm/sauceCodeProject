.class Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;
.super Ljava/lang/Object;
.source "ICustomTabsService.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub;
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

    .line 208
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 209
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    return-void
.end method


# virtual methods
.method public asBinder()Landroid/os/IBinder;
    .locals 1

    .line 213
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    return-object v0
.end method

.method public extraCommand(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 290
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 291
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    .line 294
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    .line 295
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    const/4 p1, 0x0

    if-eqz p2, :cond_0

    const/4 v2, 0x1

    .line 297
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 298
    invoke-virtual {p2, v0, p1}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_0

    .line 301
    :cond_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 303
    :goto_0
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v2, 0x5

    invoke-interface {p2, v2, v0, v1, p1}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 304
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 305
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result p1

    if-eqz p1, :cond_1

    .line 306
    sget-object p1, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {p1, v1}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/os/Bundle;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_1

    :cond_1
    const/4 p1, 0x0

    .line 313
    :goto_1
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 314
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return-object p1

    :catchall_0
    move-exception p1

    .line 313
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 314
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 315
    throw p1
.end method

.method public getInterfaceDescriptor()Ljava/lang/String;
    .locals 1

    const-string v0, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    return-object v0
.end method

.method public mayLaunchUrl(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/net/Uri;Landroid/os/Bundle;Ljava/util/List;)Z
    .locals 3
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

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 257
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 258
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    .line 261
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 262
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeStrongBinder(Landroid/os/IBinder;)V

    const/4 p1, 0x1

    const/4 v2, 0x0

    if-eqz p2, :cond_1

    .line 264
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 265
    invoke-virtual {p2, v0, v2}, Landroid/net/Uri;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 268
    :cond_1
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    :goto_1
    if-eqz p3, :cond_2

    .line 271
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 272
    invoke-virtual {p3, v0, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_2

    .line 275
    :cond_2
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 277
    :goto_2
    invoke-virtual {v0, p4}, Landroid/os/Parcel;->writeTypedList(Ljava/util/List;)V

    .line 278
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 p3, 0x4

    invoke-interface {p2, p3, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 279
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 280
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result p2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p2, :cond_3

    goto :goto_3

    :cond_3
    const/4 p1, 0x0

    .line 283
    :goto_3
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 284
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return p1

    :catchall_0
    move-exception p1

    .line 283
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 284
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 285
    throw p1
.end method

.method public newSession(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)Z
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 239
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 240
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    .line 243
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 244
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeStrongBinder(Landroid/os/IBinder;)V

    .line 245
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v2, 0x3

    const/4 v3, 0x0

    invoke-interface {p1, v2, v0, v1, v3}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 246
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 247
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p1, :cond_1

    const/4 v3, 0x1

    .line 250
    :cond_1
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 251
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return v3

    :catchall_0
    move-exception p1

    .line 250
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 251
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 252
    throw p1
.end method

.method public postMessage(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Ljava/lang/String;Landroid/os/Bundle;)I
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 370
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 371
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    .line 374
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 375
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeStrongBinder(Landroid/os/IBinder;)V

    .line 376
    invoke-virtual {v0, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    const/4 p1, 0x0

    if-eqz p3, :cond_1

    const/4 p2, 0x1

    .line 378
    invoke-virtual {v0, p2}, Landroid/os/Parcel;->writeInt(I)V

    .line 379
    invoke-virtual {p3, v0, p1}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 382
    :cond_1
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 384
    :goto_1
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/16 p3, 0x8

    invoke-interface {p2, p3, v0, v1, p1}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 385
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 386
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 389
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 390
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return p1

    :catchall_0
    move-exception p1

    .line 389
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 390
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 391
    throw p1
.end method

.method public requestPostMessageChannel(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/net/Uri;)Z
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 345
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 346
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    .line 349
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 350
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeStrongBinder(Landroid/os/IBinder;)V

    const/4 p1, 0x1

    const/4 v2, 0x0

    if-eqz p2, :cond_1

    .line 352
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 353
    invoke-virtual {p2, v0, v2}, Landroid/net/Uri;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 356
    :cond_1
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 358
    :goto_1
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v3, 0x7

    invoke-interface {p2, v3, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 359
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 360
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result p2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p2, :cond_2

    goto :goto_2

    :cond_2
    const/4 p1, 0x0

    .line 363
    :goto_2
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 364
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return p1

    :catchall_0
    move-exception p1

    .line 363
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 364
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 365
    throw p1
.end method

.method public updateVisuals(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/os/Bundle;)Z
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 320
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 321
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    .line 324
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 325
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeStrongBinder(Landroid/os/IBinder;)V

    const/4 p1, 0x1

    const/4 v2, 0x0

    if-eqz p2, :cond_1

    .line 327
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 328
    invoke-virtual {p2, v0, v2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 331
    :cond_1
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInt(I)V

    .line 333
    :goto_1
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 v3, 0x6

    invoke-interface {p2, v3, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 334
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 335
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result p2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p2, :cond_2

    goto :goto_2

    :cond_2
    const/4 p1, 0x0

    .line 338
    :goto_2
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 339
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return p1

    :catchall_0
    move-exception p1

    .line 338
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 339
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 340
    throw p1
.end method

.method public validateRelationship(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;ILandroid/net/Uri;Landroid/os/Bundle;)Z
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 396
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 397
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    .line 400
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 401
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;->asBinder()Landroid/os/IBinder;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeStrongBinder(Landroid/os/IBinder;)V

    .line 402
    invoke-virtual {v0, p2}, Landroid/os/Parcel;->writeInt(I)V

    const/4 p1, 0x1

    const/4 p2, 0x0

    if-eqz p3, :cond_1

    .line 404
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 405
    invoke-virtual {p3, v0, p2}, Landroid/net/Uri;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 408
    :cond_1
    invoke-virtual {v0, p2}, Landroid/os/Parcel;->writeInt(I)V

    :goto_1
    if-eqz p4, :cond_2

    .line 411
    invoke-virtual {v0, p1}, Landroid/os/Parcel;->writeInt(I)V

    .line 412
    invoke-virtual {p4, v0, p2}, Landroid/os/Bundle;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_2

    .line 415
    :cond_2
    invoke-virtual {v0, p2}, Landroid/os/Parcel;->writeInt(I)V

    .line 417
    :goto_2
    iget-object p3, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/16 p4, 0x9

    invoke-interface {p3, p4, v0, v1, p2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 418
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 419
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result p3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p3, :cond_3

    goto :goto_3

    :cond_3
    const/4 p1, 0x0

    .line 422
    :goto_3
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 423
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return p1

    :catchall_0
    move-exception p1

    .line 422
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 423
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 424
    throw p1
.end method

.method public warmup(J)Z
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    .line 221
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 222
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v1

    :try_start_0
    const-string v2, "net.gogame.gowrap.ui.customtabs.ICustomTabsService"

    .line 225
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->writeInterfaceToken(Ljava/lang/String;)V

    .line 226
    invoke-virtual {v0, p1, p2}, Landroid/os/Parcel;->writeLong(J)V

    .line 227
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub$Proxy;->mRemote:Landroid/os/IBinder;

    const/4 p2, 0x2

    const/4 v2, 0x0

    invoke-interface {p1, p2, v0, v1, v2}, Landroid/os/IBinder;->transact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    .line 228
    invoke-virtual {v1}, Landroid/os/Parcel;->readException()V

    .line 229
    invoke-virtual {v1}, Landroid/os/Parcel;->readInt()I

    move-result p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p1, :cond_0

    const/4 v2, 0x1

    .line 232
    :cond_0
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 233
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    return v2

    :catchall_0
    move-exception p1

    .line 232
    invoke-virtual {v1}, Landroid/os/Parcel;->recycle()V

    .line 233
    invoke-virtual {v0}, Landroid/os/Parcel;->recycle()V

    .line 234
    throw p1
.end method
