.class public Lnet/gogame/gopay/vip/BaseBillingResponse;
.super Ljava/lang/Object;
.source "SourceFile"


# instance fields
.field private a:Z

.field private b:I

.field private c:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 3
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getStatusCode()I
    .locals 1

    .line 18
    iget v0, p0, Lnet/gogame/gopay/vip/BaseBillingResponse;->b:I

    return v0
.end method

.method public getStatusMessage()Ljava/lang/String;
    .locals 1

    .line 26
    iget-object v0, p0, Lnet/gogame/gopay/vip/BaseBillingResponse;->c:Ljava/lang/String;

    return-object v0
.end method

.method public isStatus()Z
    .locals 1

    .line 10
    iget-boolean v0, p0, Lnet/gogame/gopay/vip/BaseBillingResponse;->a:Z

    return v0
.end method

.method public setStatus(Z)V
    .locals 0

    .line 14
    iput-boolean p1, p0, Lnet/gogame/gopay/vip/BaseBillingResponse;->a:Z

    return-void
.end method

.method public setStatusCode(I)V
    .locals 0

    .line 22
    iput p1, p0, Lnet/gogame/gopay/vip/BaseBillingResponse;->b:I

    return-void
.end method

.method public setStatusMessage(Ljava/lang/String;)V
    .locals 0

    .line 30
    iput-object p1, p0, Lnet/gogame/gopay/vip/BaseBillingResponse;->c:Ljava/lang/String;

    return-void
.end method
