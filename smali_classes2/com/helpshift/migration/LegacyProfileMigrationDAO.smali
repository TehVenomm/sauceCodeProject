.class public interface abstract Lcom/helpshift/migration/LegacyProfileMigrationDAO;
.super Ljava/lang/Object;
.source "LegacyProfileMigrationDAO.java"


# virtual methods
.method public abstract deleteLegacyProfile(Ljava/lang/String;)V
.end method

.method public abstract fetchLegacyProfile(Ljava/lang/String;)Lcom/helpshift/migration/legacyUser/LegacyProfile;
.end method

.method public abstract storeLegacyProfiles(Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/migration/legacyUser/LegacyProfile;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract updateMigrationState(Ljava/lang/String;Lcom/helpshift/migration/MigrationState;)Z
.end method
