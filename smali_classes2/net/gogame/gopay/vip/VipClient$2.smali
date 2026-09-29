.class Lnet/gogame/gopay/vip/VipClient$2;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gopay/vip/VipClient;->checkVipStatus(Ljava/lang/String;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic a:Ljava/lang/String;

.field final synthetic b:Lnet/gogame/gopay/vip/VipClient;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/vip/VipClient;Ljava/lang/String;)V
    .locals 0

    .line 161
    iput-object p1, p0, Lnet/gogame/gopay/vip/VipClient$2;->b:Lnet/gogame/gopay/vip/VipClient;

    iput-object p2, p0, Lnet/gogame/gopay/vip/VipClient$2;->a:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5

    const/4 v0, 0x0

    :try_start_0
    const-string v1, "goPay"

    const-string v2, "Checking VIP status..."

    .line 166
    invoke-static {v1, v2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 167
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient$2;->b:Lnet/gogame/gopay/vip/VipClient;

    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient$2;->a:Ljava/lang/String;

    invoke-static {v1, v2}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/VipClient;Ljava/lang/String;)Lnet/gogame/gopay/vip/VipStatus;

    move-result-object v1

    .line 168
    iget-object v2, p0, Lnet/gogame/gopay/vip/VipClient$2;->b:Lnet/gogame/gopay/vip/VipClient;

    invoke-static {v2, v1}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/VipClient;Lnet/gogame/gopay/vip/VipStatus;)V
    :try_end_0
    .catch Lnet/gogame/gopay/vip/UnauthorizedException; {:try_start_0 .. :try_end_0} :catch_3
    .catch Lnet/gogame/gopay/vip/HttpException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const-string v1, "goPay"

    const-string v2, "JSON error checking VIP status"

    .line 186
    invoke-static {v1, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 188
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient$2;->b:Lnet/gogame/gopay/vip/VipClient;

    invoke-static {v1, v0}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/VipClient;Lnet/gogame/gopay/vip/VipStatus;)V

    goto :goto_0

    :catch_1
    const-string v1, "goPay"

    const-string v2, "I/O error checking VIP status"

    .line 179
    invoke-static {v1, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 181
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient$2;->b:Lnet/gogame/gopay/vip/VipClient;

    invoke-static {v1, v0}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/VipClient;Lnet/gogame/gopay/vip/VipStatus;)V

    goto :goto_0

    :catch_2
    move-exception v1

    const-string v2, "goPay"

    .line 173
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "HTTP response: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Lnet/gogame/gopay/vip/HttpException;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v2, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 174
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient$2;->b:Lnet/gogame/gopay/vip/VipClient;

    invoke-static {v1, v0}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/VipClient;Lnet/gogame/gopay/vip/VipStatus;)V

    goto :goto_0

    :catch_3
    move-exception v1

    const-string v2, "goPay"

    .line 170
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Unauthorized: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Lnet/gogame/gopay/vip/UnauthorizedException;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v2, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 171
    iget-object v1, p0, Lnet/gogame/gopay/vip/VipClient$2;->b:Lnet/gogame/gopay/vip/VipClient;

    invoke-static {v1, v0}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/VipClient;Lnet/gogame/gopay/vip/VipStatus;)V

    :goto_0
    return-void
.end method
