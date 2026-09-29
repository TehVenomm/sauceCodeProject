.class public Lcom/helpshift/common/platform/AndroidMetadataDAO;
.super Ljava/lang/Object;
.source "AndroidMetadataDAO.java"

# interfaces
.implements Lcom/helpshift/meta/dao/MetaDataDAO;


# static fields
.field private static final BREAD_CRUMB_KEY:Ljava/lang/String; = "key_bread_crumb_storage"

.field private static final CUSTOM_META_KEY:Ljava/lang/String; = "key_custom_meta_storage"


# instance fields
.field private kvStore:Lcom/helpshift/common/platform/KVStore;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/KVStore;)V
    .locals 0

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 17
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidMetadataDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    return-void
.end method


# virtual methods
.method public getBreadCrumbs()Ljava/util/ArrayList;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/meta/dto/BreadCrumbDTO;",
            ">;"
        }
    .end annotation

    .line 22
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidMetadataDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "key_bread_crumb_storage"

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/KVStore;->getSerializable(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 24
    check-cast v0, Ljava/util/ArrayList;

    return-object v0

    :cond_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public getCustomMetaData()Ljava/util/HashMap;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation

    .line 41
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidMetadataDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "key_custom_meta_storage"

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/KVStore;->getSerializable(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 43
    check-cast v0, Ljava/util/HashMap;

    return-object v0

    :cond_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public saveCustomMetaData(Ljava/util/HashMap;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)V"
        }
    .end annotation

    .line 36
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidMetadataDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "key_custom_meta_storage"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/common/platform/KVStore;->setSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    return-void
.end method

.method public setBreadCrumbs(Ljava/util/ArrayList;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/meta/dto/BreadCrumbDTO;",
            ">;)V"
        }
    .end annotation

    .line 31
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidMetadataDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "key_bread_crumb_storage"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/common/platform/KVStore;->setSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    return-void
.end method
