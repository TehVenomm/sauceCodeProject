.class public Lnet/gogame/gowrap/sdk/VipStatus;
.super Ljava/lang/Object;
.source "VipStatus.java"


# instance fields
.field private suspended:Z

.field private suspensionMessage:Ljava/lang/String;

.field private vip:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 6
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getSuspensionMessage()Ljava/lang/String;
    .locals 1

    .line 54
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/VipStatus;->suspensionMessage:Ljava/lang/String;

    return-object v0
.end method

.method public isSuspended()Z
    .locals 1

    .line 36
    iget-boolean v0, p0, Lnet/gogame/gowrap/sdk/VipStatus;->suspended:Z

    return v0
.end method

.method public isVip()Z
    .locals 1

    .line 18
    iget-boolean v0, p0, Lnet/gogame/gowrap/sdk/VipStatus;->vip:Z

    return v0
.end method

.method public setSuspended(Z)V
    .locals 0

    .line 45
    iput-boolean p1, p0, Lnet/gogame/gowrap/sdk/VipStatus;->suspended:Z

    return-void
.end method

.method public setSuspensionMessage(Ljava/lang/String;)V
    .locals 0

    .line 63
    iput-object p1, p0, Lnet/gogame/gowrap/sdk/VipStatus;->suspensionMessage:Ljava/lang/String;

    return-void
.end method

.method public setVip(Z)V
    .locals 0

    .line 27
    iput-boolean p1, p0, Lnet/gogame/gowrap/sdk/VipStatus;->vip:Z

    return-void
.end method
