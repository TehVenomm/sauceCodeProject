.class public interface abstract Lcom/helpshift/common/platform/KVStore;
.super Ljava/lang/Object;
.source "KVStore.java"


# virtual methods
.method public abstract getBoolean(Ljava/lang/String;)Ljava/lang/Boolean;
.end method

.method public abstract getBoolean(Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;
.end method

.method public abstract getFloat(Ljava/lang/String;)Ljava/lang/Float;
.end method

.method public abstract getFloat(Ljava/lang/String;Ljava/lang/Float;)Ljava/lang/Float;
.end method

.method public abstract getInt(Ljava/lang/String;)Ljava/lang/Integer;
.end method

.method public abstract getInt(Ljava/lang/String;Ljava/lang/Integer;)Ljava/lang/Integer;
.end method

.method public abstract getLong(Ljava/lang/String;)Ljava/lang/Long;
.end method

.method public abstract getLong(Ljava/lang/String;Ljava/lang/Long;)Ljava/lang/Long;
.end method

.method public abstract getSerializable(Ljava/lang/String;)Ljava/lang/Object;
.end method

.method public abstract getString(Ljava/lang/String;)Ljava/lang/String;
.end method

.method public abstract getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
.end method

.method public abstract removeAllKeys()V
.end method

.method public abstract setBoolean(Ljava/lang/String;Ljava/lang/Boolean;)V
.end method

.method public abstract setFloat(Ljava/lang/String;Ljava/lang/Float;)V
.end method

.method public abstract setInt(Ljava/lang/String;Ljava/lang/Integer;)V
.end method

.method public abstract setKeyValues(Ljava/util/Map;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract setLong(Ljava/lang/String;Ljava/lang/Long;)V
.end method

.method public abstract setSerializable(Ljava/lang/String;Ljava/io/Serializable;)V
.end method

.method public abstract setString(Ljava/lang/String;Ljava/lang/String;)V
.end method
