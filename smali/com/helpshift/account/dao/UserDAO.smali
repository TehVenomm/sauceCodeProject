.class public interface abstract Lcom/helpshift/account/dao/UserDAO;
.super Ljava/lang/Object;
.source "UserDAO.java"


# virtual methods
.method public abstract activateUser(Ljava/lang/Long;)Z
.end method

.method public abstract createUser(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/account/domainmodel/UserDM;
.end method

.method public abstract deleteUser(Ljava/lang/Long;)Z
.end method

.method public abstract fetchUser(Ljava/lang/Long;)Lcom/helpshift/account/domainmodel/UserDM;
.end method

.method public abstract fetchUser(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;
.end method

.method public abstract fetchUsers()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;
.end method

.method public abstract getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;
.end method

.method public abstract updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z
.end method
