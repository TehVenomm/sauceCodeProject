.class public Lnet/gogame/gowrap/VipStatus;
.super Ljava/lang/Object;
.source "VipStatus.java"


# instance fields
.field private suspended:Z

.field private suspensionMessage:Ljava/lang/String;

.field private vip:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 3
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getSuspensionMessage()Ljava/lang/String;
    .locals 1

    .line 26
    iget-object v0, p0, Lnet/gogame/gowrap/VipStatus;->suspensionMessage:Ljava/lang/String;

    return-object v0
.end method

.method public isSuspended()Z
    .locals 1

    .line 18
    iget-boolean v0, p0, Lnet/gogame/gowrap/VipStatus;->suspended:Z

    return v0
.end method

.method public isVip()Z
    .locals 1

    .line 10
    iget-boolean v0, p0, Lnet/gogame/gowrap/VipStatus;->vip:Z

    return v0
.end method

.method public setSuspended(Z)V
    .locals 0

    .line 22
    iput-boolean p1, p0, Lnet/gogame/gowrap/VipStatus;->suspended:Z

    return-void
.end method

.method public setSuspensionMessage(Ljava/lang/String;)V
    .locals 0

    .line 30
    iput-object p1, p0, Lnet/gogame/gowrap/VipStatus;->suspensionMessage:Ljava/lang/String;

    return-void
.end method

.method public setVip(Z)V
    .locals 0

    .line 14
    iput-boolean p1, p0, Lnet/gogame/gowrap/VipStatus;->vip:Z

    return-void
.end method
