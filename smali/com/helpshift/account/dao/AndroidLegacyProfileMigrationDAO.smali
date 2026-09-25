.class public Lcom/helpshift/account/dao/AndroidLegacyProfileMigrationDAO;
.super Ljava/lang/Object;
.source "AndroidLegacyProfileMigrationDAO.java"

# interfaces
.implements Lcom/helpshift/migration/LegacyProfileMigrationDAO;


# instance fields
.field private final userDB:Lcom/helpshift/account/dao/UserDB;


# direct methods
.method public constructor <init>(Lcom/helpshift/account/dao/UserDB;)V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 18
    iput-object p1, p0, Lcom/helpshift/account/dao/AndroidLegacyProfileMigrationDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    return-void
.end method


# virtual methods
.method public deleteLegacyProfile(Ljava/lang/String;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 36
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidLegacyProfileMigrationDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->deleteLegacyProfile(Ljava/lang/String;)V

    return-void
.end method

.method public fetchLegacyProfile(Ljava/lang/String;)Lcom/helpshift/migration/legacyUser/LegacyProfile;
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 45
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidLegacyProfileMigrationDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->fetchLegacyProfile(Ljava/lang/String;)Lcom/helpshift/migration/legacyUser/LegacyProfile;

    move-result-object p1

    return-object p1
.end method

.method public storeLegacyProfiles(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/migration/legacyUser/LegacyProfile;",
            ">;)V"
        }
    .end annotation

    .line 24
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 27
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidLegacyProfileMigrationDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->storeLegacyProfiles(Ljava/util/List;)V

    return-void
.end method

.method public updateMigrationState(Ljava/lang/String;Lcom/helpshift/migration/MigrationState;)Z
    .locals 1

    if-eqz p1, :cond_1

    if-nez p2, :cond_0

    goto :goto_0

    .line 54
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidLegacyProfileMigrationDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/account/dao/UserDB;->updateUserMigrationState(Ljava/lang/String;Lcom/helpshift/migration/MigrationState;)Z

    move-result p1

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method
