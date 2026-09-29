.class public interface abstract Lcom/helpshift/common/platform/Jsonifier;
.super Ljava/lang/Object;
.source "Jsonifier.java"


# virtual methods
.method public abstract jsonify(Ljava/util/Collection;)Ljava/lang/String;
.end method

.method public abstract jsonify(Ljava/util/Map;)Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation
.end method

.method public abstract jsonifyAnalyticsDTOList(Ljava/util/List;)Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/analytics/dto/AnalyticsEventDTO;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation
.end method

.method public abstract jsonifyBreadCrumbDTOList(Ljava/util/List;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/meta/dto/BreadCrumbDTO;",
            ">;)",
            "Ljava/lang/Object;"
        }
    .end annotation
.end method

.method public abstract jsonifyCustomIssueFieldDTOList(Ljava/util/List;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/cif/dto/CustomIssueFieldDTO;",
            ">;)",
            "Ljava/lang/Object;"
        }
    .end annotation
.end method

.method public abstract jsonifyCustomMetaMap(Ljava/util/Map;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)",
            "Ljava/lang/Object;"
        }
    .end annotation
.end method

.method public abstract jsonifyDebugLogDTOList(Ljava/util/List;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/meta/dto/DebugLogDTO;",
            ">;)",
            "Ljava/lang/Object;"
        }
    .end annotation
.end method

.method public abstract jsonifyListToJsonArray(Ljava/util/List;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/lang/Object;"
        }
    .end annotation
.end method

.method public abstract jsonifyLogModelList(Ljava/util/List;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/logger/model/LogModel;",
            ">;)",
            "Ljava/lang/Object;"
        }
    .end annotation
.end method

.method public abstract jsonifyToArray(Ljava/lang/String;)Ljava/lang/Object;
.end method

.method public abstract jsonifyToObject(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/Object;
.end method

.method public abstract jsonifyToObject(Ljava/util/Map;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/lang/Object;"
        }
    .end annotation
.end method

.method public abstract removeKeyFromJsonObjString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
.end method
