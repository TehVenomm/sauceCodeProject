.class public final Lnet/gogame/gopay/sdk/k;
.super Lnet/gogame/gopay/sdk/iab/a;


# instance fields
.field public final a:Ljava/util/List;

.field private final b:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/List;)V
    .locals 0

    invoke-direct {p0, p1, p3, p4}, Lnet/gogame/gopay/sdk/iab/a;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    iput-object p2, p0, Lnet/gogame/gopay/sdk/k;->b:Ljava/lang/String;

    iput-object p5, p0, Lnet/gogame/gopay/sdk/k;->a:Ljava/util/List;

    return-void
.end method
