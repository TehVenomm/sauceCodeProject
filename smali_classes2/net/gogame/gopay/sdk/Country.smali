.class public Lnet/gogame/gopay/sdk/Country;
.super Lnet/gogame/gopay/sdk/iab/a;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    invoke-direct {p0, p1, p2, p3}, Lnet/gogame/gopay/sdk/iab/a;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public getCode()Ljava/lang/String;
    .locals 1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/Country;->getId()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/Country;->getDisplayName()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
