.class public interface abstract Lcom/helpshift/meta/dao/MetaDataDAO;
.super Ljava/lang/Object;
.source "MetaDataDAO.java"


# virtual methods
.method public abstract getBreadCrumbs()Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/meta/dto/BreadCrumbDTO;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getCustomMetaData()Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation
.end method

.method public abstract saveCustomMetaData(Ljava/util/HashMap;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract setBreadCrumbs(Ljava/util/ArrayList;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/meta/dto/BreadCrumbDTO;",
            ">;)V"
        }
    .end annotation
.end method
