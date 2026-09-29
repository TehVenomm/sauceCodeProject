.class public Lcom/helpshift/logger/logmodels/LogExtrasModelFactory;
.super Ljava/lang/Object;
.source "LogExtrasModelFactory.java"

# interfaces
.implements Lcom/helpshift/logger/logmodels/ILogExtrasModelFactory;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 5
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public fromMap(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;
    .locals 1

    .line 14
    new-instance v0, Lcom/helpshift/logger/logmodels/MapExtrasModel;

    invoke-direct {v0, p1, p2}, Lcom/helpshift/logger/logmodels/MapExtrasModel;-><init>(Ljava/lang/String;Ljava/util/Map;)V

    return-object v0
.end method

.method public fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;
    .locals 1

    .line 9
    new-instance v0, Lcom/helpshift/logger/logmodels/StringExtrasModel;

    invoke-direct {v0, p1, p2}, Lcom/helpshift/logger/logmodels/StringExtrasModel;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    return-object v0
.end method
