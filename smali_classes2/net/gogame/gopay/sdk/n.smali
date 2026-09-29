.class public final Lnet/gogame/gopay/sdk/n;
.super Ljava/lang/Object;


# instance fields
.field public final a:Ljava/lang/String;

.field public final b:Lorg/json/JSONObject;

.field public final c:Lnet/gogame/gopay/sdk/f;


# direct methods
.method public constructor <init>(Ljava/lang/String;Lorg/json/JSONObject;)V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/n;->a:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/n;->b:Lorg/json/JSONObject;

    const/4 p1, 0x0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/n;->c:Lnet/gogame/gopay/sdk/f;

    return-void
.end method

.method public constructor <init>(Lnet/gogame/gopay/sdk/f;)V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/n;->a:Ljava/lang/String;

    iput-object v0, p0, Lnet/gogame/gopay/sdk/n;->b:Lorg/json/JSONObject;

    iput-object p1, p0, Lnet/gogame/gopay/sdk/n;->c:Lnet/gogame/gopay/sdk/f;

    return-void
.end method
