.class public interface abstract Lcom/helpshift/logger/database/LogStorage;
.super Ljava/lang/Object;
.source "LogStorage.java"


# virtual methods
.method public abstract deleteAll()V
.end method

.method public abstract getAll()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/logger/model/LogModel;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getCount(Ljava/util/List;)I
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)I"
        }
    .end annotation
.end method

.method public abstract insert(Lcom/helpshift/logger/model/LogModel;)V
.end method
