.class final Lnet/gogame/gopay/sdk/iab/g;
.super Ljava/lang/Object;


# instance fields
.field private a:I

.field private b:Ljava/lang/String;

.field private c:Ljava/lang/String;

.field private d:Lnet/gogame/gopay/sdk/iab/h;


# direct methods
.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final CloseWindow()V
    .locals 0
    .annotation runtime Landroid/webkit/JavascriptInterface;
    .end annotation

    return-void
.end method

.method public final canRetry()Z
    .locals 2
    .annotation runtime Landroid/webkit/JavascriptInterface;
    .end annotation

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    const/4 v1, -0x1

    if-eq v0, v1, :cond_1

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    const/4 v1, -0x2

    if-eq v0, v1, :cond_1

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    const/4 v1, -0x6

    if-eq v0, v1, :cond_1

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    const/4 v1, -0x8

    if-eq v0, v1, :cond_1

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    const/4 v1, -0x7

    if-eq v0, v1, :cond_1

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    const/16 v1, -0xf

    if-eq v0, v1, :cond_1

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    const/16 v1, -0xb

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    return v0

    :cond_1
    :goto_0
    const/4 v0, 0x1

    return v0
.end method

.method public final getErrorCode()I
    .locals 1
    .annotation runtime Landroid/webkit/JavascriptInterface;
    .end annotation

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    return v0
.end method

.method public final getErrorMessage()Ljava/lang/String;
    .locals 1
    .annotation runtime Landroid/webkit/JavascriptInterface;
    .end annotation

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/g;->b:Ljava/lang/String;

    return-object v0
.end method

.method public final getFailedUrl()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/g;->c:Ljava/lang/String;

    return-object v0
.end method

.method public final onButtonClick()V
    .locals 1
    .annotation runtime Landroid/webkit/JavascriptInterface;
    .end annotation

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/g;->d:Lnet/gogame/gopay/sdk/iab/h;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/g;->d:Lnet/gogame/gopay/sdk/iab/h;

    invoke-interface {v0}, Lnet/gogame/gopay/sdk/iab/h;->a()V

    :cond_0
    return-void
.end method

.method public final setCallback(Lnet/gogame/gopay/sdk/iab/h;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/g;->d:Lnet/gogame/gopay/sdk/iab/h;

    return-void
.end method

.method public final setError(ILjava/lang/String;)V
    .locals 0

    iput p1, p0, Lnet/gogame/gopay/sdk/iab/g;->a:I

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/g;->b:Ljava/lang/String;

    return-void
.end method

.method public final setFailedUrl(Ljava/lang/String;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/g;->c:Ljava/lang/String;

    return-void
.end method
