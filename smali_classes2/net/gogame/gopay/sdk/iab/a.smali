.class public Lnet/gogame/gopay/sdk/iab/a;
.super Ljava/lang/Object;


# instance fields
.field private final a:Ljava/lang/String;

.field private final b:Ljava/lang/String;

.field private final c:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/a;->a:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/a;->b:Ljava/lang/String;

    iput-object p3, p0, Lnet/gogame/gopay/sdk/iab/a;->c:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getDisplayIcon()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/a;->c:Ljava/lang/String;

    return-object v0
.end method

.method public getDisplayName()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/a;->b:Ljava/lang/String;

    return-object v0
.end method

.method public getId()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/a;->a:Ljava/lang/String;

    return-object v0
.end method
