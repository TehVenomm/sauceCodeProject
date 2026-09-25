.class public interface abstract Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;
.super Ljava/lang/Object;
.source "LegacyProfileDAO.java"


# virtual methods
.method public abstract deleteProfiles()V
.end method

.method public abstract fetchProfiles()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/account/dao/ProfileDTO;",
            ">;"
        }
    .end annotation
.end method
