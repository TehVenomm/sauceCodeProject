.class public interface abstract Lcom/helpshift/common/platform/network/NetworkRequestDAO;
.super Ljava/lang/Object;
.source "NetworkRequestDAO.java"


# virtual methods
.method public abstract clearSuccessfulRequestIds()V
.end method

.method public abstract deletePendingRequestId(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract getAllSuccessfulRequestIds()Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getETag(Ljava/lang/String;)Ljava/lang/String;
.end method

.method public abstract getPendingRequestId(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
.end method

.method public abstract getPendingRequestIdMapForRoute(Ljava/lang/String;)Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getServerTimeDelta()F
.end method

.method public abstract removeETag(Ljava/lang/String;)V
.end method

.method public abstract storeETag(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract storePendingRequestId(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract storeServerTimeDelta(F)V
.end method

.method public abstract storeSuccessfulRequestId(Ljava/lang/String;)V
.end method
