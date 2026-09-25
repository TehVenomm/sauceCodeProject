.class public interface abstract Lnet/gogame/gopay/vip/BaseEvent;
.super Ljava/lang/Object;
.source "SourceFile"


# virtual methods
.method public abstract marshal()Lorg/json/JSONObject;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation
.end method

.method public abstract unmarshal(Lorg/json/JSONObject;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation
.end method
