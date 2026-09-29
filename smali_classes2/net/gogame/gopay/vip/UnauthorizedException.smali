.class public Lnet/gogame/gopay/vip/UnauthorizedException;
.super Ljava/lang/Exception;
.source "SourceFile"


# instance fields
.field private a:I

.field private b:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;ILjava/lang/String;)V
    .locals 0

    .line 9
    invoke-direct {p0, p1}, Ljava/lang/Exception;-><init>(Ljava/lang/String;)V

    .line 10
    iput p2, p0, Lnet/gogame/gopay/vip/UnauthorizedException;->a:I

    .line 11
    iput-object p3, p0, Lnet/gogame/gopay/vip/UnauthorizedException;->b:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getResponseCode()I
    .locals 1

    .line 15
    iget v0, p0, Lnet/gogame/gopay/vip/UnauthorizedException;->a:I

    return v0
.end method

.method public getResponseMessage()Ljava/lang/String;
    .locals 1

    .line 23
    iget-object v0, p0, Lnet/gogame/gopay/vip/UnauthorizedException;->b:Ljava/lang/String;

    return-object v0
.end method

.method public setResponseCode(I)V
    .locals 0

    .line 19
    iput p1, p0, Lnet/gogame/gopay/vip/UnauthorizedException;->a:I

    return-void
.end method

.method public setResponseMessage(Ljava/lang/String;)V
    .locals 0

    .line 27
    iput-object p1, p0, Lnet/gogame/gopay/vip/UnauthorizedException;->b:Ljava/lang/String;

    return-void
.end method
