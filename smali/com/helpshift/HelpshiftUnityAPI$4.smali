.class final Lcom/helpshift/HelpshiftUnityAPI$4;
.super Ljava/lang/Object;
.source "HelpshiftUnityAPI.java"

# interfaces
.implements Lcom/helpshift/support/Callable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/HelpshiftUnityAPI;->setMetaData(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$metaDataString:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 322
    iput-object p1, p0, Lcom/helpshift/HelpshiftUnityAPI$4;->val$metaDataString:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public call()Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation

    .line 326
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    iget-object v1, p0, Lcom/helpshift/HelpshiftUnityAPI$4;->val$metaDataString:Ljava/lang/String;

    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 327
    invoke-static {v0}, Lcom/helpshift/util/HSJSONUtils;->toMap(Lorg/json/JSONObject;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "hs-tags"

    .line 328
    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    .line 329
    instance-of v2, v1, Ljava/util/ArrayList;

    if-eqz v2, :cond_0

    .line 330
    check-cast v1, Ljava/util/ArrayList;

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/util/ArrayList;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v1

    check-cast v1, [Ljava/lang/String;

    const-string v2, "hs-tags"

    .line 331
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 334
    :cond_0
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 335
    invoke-interface {v0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 336
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/io/Serializable;

    invoke-interface {v1, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/ClassCastException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :cond_1
    return-object v1

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_UnityAPI"

    const-string v2, "Error setting metaData "

    .line 344
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_1

    :catch_1
    move-exception v0

    const-string v1, "Helpshift_UnityAPI"

    const-string v2, "Error setting metaData "

    .line 341
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_1
    const/4 v0, 0x0

    return-object v0
.end method
