.class public Lcom/helpshift/account/dao/AndroidClearedUserDAO;
.super Ljava/lang/Object;
.source "AndroidClearedUserDAO.java"

# interfaces
.implements Lcom/helpshift/account/dao/ClearedUserDAO;


# instance fields
.field private final userDB:Lcom/helpshift/account/dao/UserDB;


# direct methods
.method public constructor <init>(Lcom/helpshift/account/dao/UserDB;)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 15
    iput-object p1, p0, Lcom/helpshift/account/dao/AndroidClearedUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    return-void
.end method


# virtual methods
.method public deleteClearedUser(Ljava/lang/Long;)Z
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return p1

    .line 47
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidClearedUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->deleteClearedUser(Ljava/lang/Long;)Z

    move-result p1

    return p1
.end method

.method public fetchClearedUsers()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/domainmodel/ClearedUserDM;",
            ">;"
        }
    .end annotation

    .line 29
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidClearedUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDB;->fetchClearedUsers()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public insertClearedUser(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Lcom/helpshift/account/domainmodel/ClearedUserDM;
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 24
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidClearedUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->insertClearedUser(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Lcom/helpshift/account/domainmodel/ClearedUserDM;

    move-result-object p1

    return-object p1
.end method

.method public updateSyncState(Ljava/lang/Long;Lcom/helpshift/account/dao/ClearedUserSyncState;)Z
    .locals 1

    if-eqz p1, :cond_1

    if-nez p2, :cond_0

    goto :goto_0

    .line 38
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidClearedUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/account/dao/UserDB;->updateClearedUserSyncState(Ljava/lang/Long;Lcom/helpshift/account/dao/ClearedUserSyncState;)Z

    move-result p1

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method
