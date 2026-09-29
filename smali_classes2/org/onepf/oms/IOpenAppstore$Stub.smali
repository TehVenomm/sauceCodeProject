.class public abstract Lorg/onepf/oms/IOpenAppstore$Stub;
.super Landroid/os/Binder;
.source "IOpenAppstore.java"

# interfaces
.implements Lorg/onepf/oms/IOpenAppstore;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/IOpenAppstore;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x409
    name = "Stub"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lorg/onepf/oms/IOpenAppstore$Stub$Proxy;
    }
.end annotation


# static fields
.field private static final DESCRIPTOR:Ljava/lang/String; = "org.onepf.oms.IOpenAppstore"

.field static final TRANSACTION_areOutsideLinksAllowed:I = 0x9

.field static final TRANSACTION_getAppstoreName:I = 0x1

.field static final TRANSACTION_getBillingServiceIntent:I = 0x5

.field static final TRANSACTION_getPackageVersion:I = 0x4

.field static final TRANSACTION_getProductPageIntent:I = 0x6

.field static final TRANSACTION_getRateItPageIntent:I = 0x7

.field static final TRANSACTION_getSameDeveloperPageIntent:I = 0x8

.field static final TRANSACTION_isBillingAvailable:I = 0x3

.field static final TRANSACTION_isPackageInstaller:I = 0x2


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 20
    invoke-direct {p0}, Landroid/os/Binder;-><init>()V

    const-string v0, "org.onepf.oms.IOpenAppstore"

    .line 21
    invoke-virtual {p0, p0, v0}, Lorg/onepf/oms/IOpenAppstore$Stub;->attachInterface(Landroid/os/IInterface;Ljava/lang/String;)V

    return-void
.end method

.method public static asInterface(Landroid/os/IBinder;)Lorg/onepf/oms/IOpenAppstore;
    .locals 2

    if-nez p0, :cond_0

    const/4 p0, 0x0

    return-object p0

    :cond_0
    const-string v0, "org.onepf.oms.IOpenAppstore"

    .line 32
    invoke-interface {p0, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 33
    instance-of v1, v0, Lorg/onepf/oms/IOpenAppstore;

    if-eqz v1, :cond_1

    .line 34
    check-cast v0, Lorg/onepf/oms/IOpenAppstore;

    return-object v0

    .line 36
    :cond_1
    new-instance v0, Lorg/onepf/oms/IOpenAppstore$Stub$Proxy;

    invoke-direct {v0, p0}, Lorg/onepf/oms/IOpenAppstore$Stub$Proxy;-><init>(Landroid/os/IBinder;)V

    return-object v0
.end method


# virtual methods
.method public asBinder()Landroid/os/IBinder;
    .locals 0

    return-object p0
.end method

.method public onTransact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    const v0, 0x5f4e5446

    const/4 v1, 0x1

    if-eq p1, v0, :cond_4

    const/4 v0, 0x0

    packed-switch p1, :pswitch_data_0

    .line 160
    invoke-super {p0, p1, p2, p3, p4}, Landroid/os/Binder;->onTransact(ILandroid/os/Parcel;Landroid/os/Parcel;I)Z

    move-result p1

    return p1

    :pswitch_0
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 153
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 154
    invoke-virtual {p0}, Lorg/onepf/oms/IOpenAppstore$Stub;->areOutsideLinksAllowed()Z

    move-result p1

    .line 155
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 156
    invoke-virtual {p3, p1}, Landroid/os/Parcel;->writeInt(I)V

    return v1

    :pswitch_1
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 137
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 139
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    .line 140
    invoke-virtual {p0, p1}, Lorg/onepf/oms/IOpenAppstore$Stub;->getSameDeveloperPageIntent(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1

    .line 141
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    if-eqz p1, :cond_0

    .line 143
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 144
    invoke-virtual {p1, p3, v1}, Landroid/content/Intent;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_0

    .line 147
    :cond_0
    invoke-virtual {p3, v0}, Landroid/os/Parcel;->writeInt(I)V

    :goto_0
    return v1

    :pswitch_2
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 121
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 123
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    .line 124
    invoke-virtual {p0, p1}, Lorg/onepf/oms/IOpenAppstore$Stub;->getRateItPageIntent(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1

    .line 125
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    if-eqz p1, :cond_1

    .line 127
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 128
    invoke-virtual {p1, p3, v1}, Landroid/content/Intent;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_1

    .line 131
    :cond_1
    invoke-virtual {p3, v0}, Landroid/os/Parcel;->writeInt(I)V

    :goto_1
    return v1

    :pswitch_3
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 105
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 107
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    .line 108
    invoke-virtual {p0, p1}, Lorg/onepf/oms/IOpenAppstore$Stub;->getProductPageIntent(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1

    .line 109
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    if-eqz p1, :cond_2

    .line 111
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 112
    invoke-virtual {p1, p3, v1}, Landroid/content/Intent;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_2

    .line 115
    :cond_2
    invoke-virtual {p3, v0}, Landroid/os/Parcel;->writeInt(I)V

    :goto_2
    return v1

    :pswitch_4
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 91
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 92
    invoke-virtual {p0}, Lorg/onepf/oms/IOpenAppstore$Stub;->getBillingServiceIntent()Landroid/content/Intent;

    move-result-object p1

    .line 93
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    if-eqz p1, :cond_3

    .line 95
    invoke-virtual {p3, v1}, Landroid/os/Parcel;->writeInt(I)V

    .line 96
    invoke-virtual {p1, p3, v1}, Landroid/content/Intent;->writeToParcel(Landroid/os/Parcel;I)V

    goto :goto_3

    .line 99
    :cond_3
    invoke-virtual {p3, v0}, Landroid/os/Parcel;->writeInt(I)V

    :goto_3
    return v1

    :pswitch_5
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 81
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 83
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    .line 84
    invoke-virtual {p0, p1}, Lorg/onepf/oms/IOpenAppstore$Stub;->getPackageVersion(Ljava/lang/String;)I

    move-result p1

    .line 85
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 86
    invoke-virtual {p3, p1}, Landroid/os/Parcel;->writeInt(I)V

    return v1

    :pswitch_6
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 71
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 73
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    .line 74
    invoke-virtual {p0, p1}, Lorg/onepf/oms/IOpenAppstore$Stub;->isBillingAvailable(Ljava/lang/String;)Z

    move-result p1

    .line 75
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 76
    invoke-virtual {p3, p1}, Landroid/os/Parcel;->writeInt(I)V

    return v1

    :pswitch_7
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 61
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 63
    invoke-virtual {p2}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object p1

    .line 64
    invoke-virtual {p0, p1}, Lorg/onepf/oms/IOpenAppstore$Stub;->isPackageInstaller(Ljava/lang/String;)Z

    move-result p1

    .line 65
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 66
    invoke-virtual {p3, p1}, Landroid/os/Parcel;->writeInt(I)V

    return v1

    :pswitch_8
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 53
    invoke-virtual {p2, p1}, Landroid/os/Parcel;->enforceInterface(Ljava/lang/String;)V

    .line 54
    invoke-virtual {p0}, Lorg/onepf/oms/IOpenAppstore$Stub;->getAppstoreName()Ljava/lang/String;

    move-result-object p1

    .line 55
    invoke-virtual {p3}, Landroid/os/Parcel;->writeNoException()V

    .line 56
    invoke-virtual {p3, p1}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    return v1

    :cond_4
    const-string p1, "org.onepf.oms.IOpenAppstore"

    .line 48
    invoke-virtual {p3, p1}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    return v1

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_8
        :pswitch_7
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
