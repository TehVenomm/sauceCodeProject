.class public final Lnet/gogame/gopay/sdk/iab/f;
.super Lnet/gogame/gopay/sdk/iab/a;


# instance fields
.field final a:Ljava/lang/String;

.field final b:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    invoke-direct {p0, v0, p1, v0}, Lnet/gogame/gopay/sdk/iab/a;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/f;->a:Ljava/lang/String;

    const/4 p1, 0x1

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/f;->b:Z

    return-void
.end method
