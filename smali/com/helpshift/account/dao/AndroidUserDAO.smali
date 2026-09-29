.class public Lcom/helpshift/account/dao/AndroidUserDAO;
.super Ljava/lang/Object;
.source "AndroidUserDAO.java"

# interfaces
.implements Lcom/helpshift/account/dao/UserDAO;


# instance fields
.field private final userDB:Lcom/helpshift/account/dao/UserDB;


# direct methods
.method public constructor <init>(Lcom/helpshift/account/dao/UserDB;)V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 11
    iput-object p1, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    return-void
.end method


# virtual methods
.method public activateUser(Ljava/lang/Long;)Z
    .locals 2

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 68
    :cond_0
    iget-object v1, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v1, p1}, Lcom/helpshift/account/dao/UserDB;->fetchUser(Ljava/lang/Long;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    if-nez v1, :cond_1

    return v0

    .line 75
    :cond_1
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->activateUser(Ljava/lang/Long;)Z

    move-result p1

    return p1
.end method

.method public createUser(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 20
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->createUser(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p1

    return-object p1
.end method

.method public deleteUser(Ljava/lang/Long;)Z
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return p1

    .line 83
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->deleteUser(Ljava/lang/Long;)Z

    move-result p1

    return p1
.end method

.method public fetchUser(Ljava/lang/Long;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 37
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->fetchUser(Ljava/lang/Long;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p1

    return-object p1
.end method

.method public fetchUser(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;
    .locals 1

    if-nez p1, :cond_0

    if-nez p2, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 45
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/account/dao/UserDB;->fetchUser(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p1

    return-object p1
.end method

.method public fetchUsers()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            ">;"
        }
    .end annotation

    .line 60
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDB;->fetchUsers()Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;
    .locals 1

    .line 50
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDB;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    return-object v0
.end method

.method public getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;
    .locals 1

    .line 55
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0}, Lcom/helpshift/account/dao/UserDB;->getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v0

    return-object v0
.end method

.method public updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return p1

    .line 29
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidUserDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->updateUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result p1

    return p1
.end method
