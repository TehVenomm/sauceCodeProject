.class public interface abstract Lcom/helpshift/account/dao/ClearedUserDAO;
.super Ljava/lang/Object;
.source "ClearedUserDAO.java"


# virtual methods
.method public abstract deleteClearedUser(Ljava/lang/Long;)Z
.end method

.method public abstract fetchClearedUsers()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/domainmodel/ClearedUserDM;",
            ">;"
        }
    .end annotation
.end method

.method public abstract insertClearedUser(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Lcom/helpshift/account/domainmodel/ClearedUserDM;
.end method

.method public abstract updateSyncState(Ljava/lang/Long;Lcom/helpshift/account/dao/ClearedUserSyncState;)Z
.end method
