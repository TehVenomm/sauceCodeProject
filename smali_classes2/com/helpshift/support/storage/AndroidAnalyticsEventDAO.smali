.class public Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;
.super Ljava/lang/Object;
.source "AndroidAnalyticsEventDAO.java"

# interfaces
.implements Lcom/helpshift/analytics/AnalyticsEventDAO;


# static fields
.field private static final KEY_UNSENT_ANALYTICS_EVENTS:Ljava/lang/String; = "unsent_analytics_events"


# instance fields
.field private kvStore:Lcom/helpshift/common/platform/KVStore;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/KVStore;)V
    .locals 0

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 16
    iput-object p1, p0, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    return-void
.end method

.method private getUnSentAnalyticFromDB()Ljava/util/HashMap;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation

    .line 48
    iget-object v0, p0, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "unsent_analytics_events"

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/KVStore;->getSerializable(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_0

    .line 51
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    goto :goto_0

    .line 54
    :cond_0
    check-cast v0, Ljava/util/HashMap;

    :goto_0
    return-object v0
.end method


# virtual methods
.method public getUnsentAnalytics()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation

    .line 44
    invoke-direct {p0}, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;->getUnSentAnalyticFromDB()Ljava/util/HashMap;

    move-result-object v0

    return-object v0
.end method

.method public removeAnalyticsData(Ljava/lang/String;)V
    .locals 2

    .line 28
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 32
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;->getUnSentAnalyticFromDB()Ljava/util/HashMap;

    move-result-object v0

    .line 33
    invoke-virtual {v0, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 34
    invoke-virtual {v0}, Ljava/util/HashMap;->size()I

    move-result p1

    if-nez p1, :cond_1

    .line 35
    iget-object p1, p0, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v0, "unsent_analytics_events"

    const/4 v1, 0x0

    invoke-interface {p1, v0, v1}, Lcom/helpshift/common/platform/KVStore;->setSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    goto :goto_0

    .line 38
    :cond_1
    iget-object p1, p0, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "unsent_analytics_events"

    invoke-interface {p1, v1, v0}, Lcom/helpshift/common/platform/KVStore;->setSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    :goto_0
    return-void
.end method

.method public saveUnsentAnalyticsData(Ljava/lang/String;Ljava/util/HashMap;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 21
    invoke-direct {p0}, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;->getUnSentAnalyticFromDB()Ljava/util/HashMap;

    move-result-object v0

    .line 22
    invoke-virtual {v0, p1, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 23
    iget-object p1, p0, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string p2, "unsent_analytics_events"

    invoke-interface {p1, p2, v0}, Lcom/helpshift/common/platform/KVStore;->setSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    return-void
.end method
