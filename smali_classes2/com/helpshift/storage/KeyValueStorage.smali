.class public interface abstract Lcom/helpshift/storage/KeyValueStorage;
.super Ljava/lang/Object;
.source "KeyValueStorage.java"


# virtual methods
.method public abstract get(Ljava/lang/String;)Ljava/lang/Object;
.end method

.method public abstract removeAllKeys()V
.end method

.method public abstract removeKey(Ljava/lang/String;)V
.end method

.method public abstract set(Ljava/lang/String;Ljava/io/Serializable;)Z
.end method

.method public abstract setKeyValues(Ljava/util/Map;)Z
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)Z"
        }
    .end annotation
.end method
