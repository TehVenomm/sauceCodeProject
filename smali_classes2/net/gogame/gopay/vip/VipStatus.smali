.class public Lnet/gogame/gopay/vip/VipStatus;
.super Ljava/lang/Object;
.source "SourceFile"


# instance fields
.field private final a:Ljava/lang/String;

.field private final b:Z

.field private final c:Z

.field private final d:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;ZZLjava/lang/String;)V
    .locals 0

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 14
    iput-object p1, p0, Lnet/gogame/gopay/vip/VipStatus;->a:Ljava/lang/String;

    .line 15
    iput-boolean p2, p0, Lnet/gogame/gopay/vip/VipStatus;->b:Z

    .line 16
    iput-boolean p3, p0, Lnet/gogame/gopay/vip/VipStatus;->c:Z

    .line 17
    iput-object p4, p0, Lnet/gogame/gopay/vip/VipStatus;->d:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getGuid()Ljava/lang/String;
    .locals 1

    .line 21
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipStatus;->a:Ljava/lang/String;

    return-object v0
.end method

.method public getSuspensionMessage()Ljava/lang/String;
    .locals 1

    .line 33
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipStatus;->d:Ljava/lang/String;

    return-object v0
.end method

.method public isSuspended()Z
    .locals 1

    .line 29
    iget-boolean v0, p0, Lnet/gogame/gopay/vip/VipStatus;->c:Z

    return v0
.end method

.method public isVip()Z
    .locals 1

    .line 25
    iget-boolean v0, p0, Lnet/gogame/gopay/vip/VipStatus;->b:Z

    return v0
.end method
