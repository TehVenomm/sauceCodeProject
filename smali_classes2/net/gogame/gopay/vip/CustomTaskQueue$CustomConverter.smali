.class public Lnet/gogame/gopay/vip/CustomTaskQueue$CustomConverter;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gopay/vip/CustomTaskQueue;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "CustomConverter"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter<",
        "Lnet/gogame/gopay/vip/BaseEvent;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 35
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public bridge synthetic from([B)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 35
    invoke-virtual {p0, p1}, Lnet/gogame/gopay/vip/CustomTaskQueue$CustomConverter;->from([B)Lnet/gogame/gopay/vip/BaseEvent;

    move-result-object p1

    return-object p1
.end method

.method public from([B)Lnet/gogame/gopay/vip/BaseEvent;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 40
    :try_start_0
    new-instance v0, Ljava/lang/String;

    const-string v1, "UTF-8"

    invoke-direct {v0, p1, v1}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    .line 41
    new-instance p1, Lorg/json/JSONObject;

    invoke-direct {p1, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v0, "@eventType"

    const/4 v1, 0x0

    .line 42
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    return-object v1

    :cond_0
    const/4 v2, -0x1

    .line 46
    invoke-virtual {v0}, Ljava/lang/String;->hashCode()I

    move-result v3

    const v4, -0x20b65a07

    if-eq v3, v4, :cond_1

    goto :goto_0

    :cond_1
    const-string v3, "PurchaseEvent"

    invoke-virtual {v0, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    const/4 v2, 0x0

    :cond_2
    :goto_0
    if-eqz v2, :cond_3

    return-object v1

    .line 48
    :cond_3
    new-instance v0, Lnet/gogame/gopay/vip/PurchaseEvent;

    invoke-direct {v0}, Lnet/gogame/gopay/vip/PurchaseEvent;-><init>()V

    .line 49
    invoke-virtual {v0, p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->unmarshal(Lorg/json/JSONObject;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 56
    new-instance v0, Ljava/io/IOException;

    invoke-direct {v0, p1}, Ljava/io/IOException;-><init>(Ljava/lang/Throwable;)V

    throw v0
.end method

.method public bridge synthetic toStream(Ljava/lang/Object;Ljava/io/OutputStream;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 35
    check-cast p1, Lnet/gogame/gopay/vip/BaseEvent;

    invoke-virtual {p0, p1, p2}, Lnet/gogame/gopay/vip/CustomTaskQueue$CustomConverter;->toStream(Lnet/gogame/gopay/vip/BaseEvent;Ljava/io/OutputStream;)V

    return-void
.end method

.method public toStream(Lnet/gogame/gopay/vip/BaseEvent;Ljava/io/OutputStream;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 63
    :try_start_0
    invoke-interface {p1}, Lnet/gogame/gopay/vip/BaseEvent;->marshal()Lorg/json/JSONObject;

    move-result-object p1

    .line 64
    invoke-virtual {p1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    const-string v0, "UTF-8"

    invoke-virtual {p1, v0}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object p1

    invoke-virtual {p2, p1}, Ljava/io/OutputStream;->write([B)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    .line 66
    new-instance p2, Ljava/io/IOException;

    invoke-direct {p2, p1}, Ljava/io/IOException;-><init>(Ljava/lang/Throwable;)V

    throw p2
.end method
