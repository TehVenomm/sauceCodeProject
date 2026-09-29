.class public abstract Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;
.super Landroid/os/Binder;
.source "ICustomTabsCallback.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x409
    name = "Stub"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub$Proxy;
    }
.end annotation


# static fields
.field private static final DESCRIPTOR:Ljava/lang/String; = "net.gogame.gowrap.ui.customtabs.ICustomTabsCallback"

.field static final TRANSACTION_extraCallback:I = 0x3

.field static final TRANSACTION_onMessageChannelReady:I = 0x4

.field static final TRANSACTION_onNavigationEvent:I = 0x2

.field static final TRANSACTION_onPostMessage:I = 0x5

.field static final TRANSACTION_onRelationshipValidationResult:I = 0x6


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 18
    invoke-direct {p0}, Landroid/os/Binder;-><init>()V

    const-string v0, "net.gogame.gowrap.ui.customtabs.ICustomTabsCallback"

    .line 19
    invoke-virtual {p0, p0, v0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;->attachInterface(Landroid/os/IInterface;Ljava/lang/String;)V

    return-void
.end method

.method public static asInterface(Landroid/os/IBinder;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;
    .locals 2

    if-nez p0, :cond_0

    const/4 p0, 0x0

    return-object p0

    :cond_0
    const-string v0, "net.gogame.gowrap.ui.customtabs.ICustomTabsCallback"

    .line 30
    invoke-interface {p0, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 31
    instance-of v1, v0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    if-eqz v1, :cond_1

    .line 32
    check-cast v0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;

    return-object v0

    .line 34
    :cond_1
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub$Proxy;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub$Proxy;-><init>(Landroid/os/IBinder;)V

    return-object v0
.end method


# virtual methods
.method public asBinder()Landroid/os/IBinder;
    .locals 0

    return-object p0
.end method

.method public onTransact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    const-string v0, "net.gogame.gowrap.ui.customtabs.ICustomTabsCallback"

    const v1, 0x5f4e5446

    const/4 v2, 0x1

    if-eq p1, v1, :cond_7

    const/4 v1, 0x0

    packed-switch p1, :pswitch_data_0

    .line 139
    invoke-super {p0, p1, p2, p3, p4}, Landroid/os/Binder;->onTransact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result p1

    return p1

    .line 114
    :pswitch_0
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 116
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result p1

    .line 118
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result p4

    if-eqz p4, :cond_0

    .line 119
    sget-object p4, Landroid/net/Uri;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {p4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object p4

    check-cast p4, Landroid/net/Uri;

    goto :goto_0

    :cond_0
    move-object p4, v1

    .line 125
    :goto_0
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v0

    if-eqz v0, :cond_1

    const/4 v0, 0x1

    goto :goto_1

    :cond_1
    const/4 v0, 0x0

    .line 127
    :goto_1
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result v3

    if-eqz v3, :cond_2

    .line 128
    sget-object v1, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {v1, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object p2

    move-object v1, p2

    check-cast v1, Landroid/os/Bundle;

    .line 133
    :cond_2
    invoke-virtual {p0, p1, p4, v0, v1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;->onRelationshipValidationResult(ILandroid/net/Uri;ZLandroid/os/Bundle;)V

    .line 134
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    return v2

    .line 98
    :pswitch_1
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 100
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    .line 102
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result p4

    if-eqz p4, :cond_3

    .line 103
    sget-object p4, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {p4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object p2

    move-object v1, p2

    check-cast v1, Landroid/os/Bundle;

    .line 108
    :cond_3
    invoke-virtual {p0, p1, v1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;->onPostMessage(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 109
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    return v2

    .line 84
    :pswitch_2
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 86
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result p1

    if-eqz p1, :cond_4

    .line 87
    sget-object p1, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {p1, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object p1

    move-object v1, p1

    check-cast v1, Landroid/os/Bundle;

    .line 92
    :cond_4
    invoke-virtual {p0, v1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;->onMessageChannelReady(Landroid/os/Bundle;)V

    .line 93
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    return v2

    .line 68
    :pswitch_3
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 70
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    .line 72
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result p4

    if-eqz p4, :cond_5

    .line 73
    sget-object p4, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {p4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object p2

    move-object v1, p2

    check-cast v1, Landroid/os/Bundle;

    .line 78
    :cond_5
    invoke-virtual {p0, p1, v1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;->extraCallback(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 79
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    return v2

    .line 52
    :pswitch_4
    invoke-virtual {p2, v0}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 54
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result p1

    .line 56
    invoke-virtual {p2}, Landroid/os/Parcel;->readInt()I

    move-result p4

    if-eqz p4, :cond_6

    .line 57
    sget-object p4, Landroid/os/Bundle;->CREATOR:Landroid/os/Parcelable$Creator;

    invoke-interface {p4, p2}, Landroid/os/Parcelable$Creator;->createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;

    move-result-object p2

    move-object v1, p2

    check-cast v1, Landroid/os/Bundle;

    .line 62
    :cond_6
    invoke-virtual {p0, p1, v1}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback$Stub;->onNavigationEvent(ILandroid/os/Bundle;)V

    .line 63
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    return v2

    .line 47
    :cond_7
    invoke-virtual {p3, v0}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    return v2

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
