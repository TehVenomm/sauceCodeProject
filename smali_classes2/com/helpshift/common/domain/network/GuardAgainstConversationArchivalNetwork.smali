.class public Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;
.super Ljava/lang/Object;
.source "GuardAgainstConversationArchivalNetwork.java"

# interfaces
.implements Lcom/helpshift/common/domain/network/Network;


# instance fields
.field private final network:Lcom/helpshift/common/domain/network/Network;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/network/Network;)V
    .locals 0

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 14
    iput-object p1, p0, Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;->network:Lcom/helpshift/common/domain/network/Network;

    return-void
.end method


# virtual methods
.method public makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    .locals 2

    .line 19
    iget-object v0, p0, Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;->network:Lcom/helpshift/common/domain/network/Network;

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    .line 20
    iget v0, p1, Lcom/helpshift/common/platform/network/Response;->status:I

    const/16 v1, 0x19a

    if-eq v0, v1, :cond_0

    return-object p1

    :cond_0
    const/4 p1, 0x0

    .line 22
    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->CONVERSATION_ARCHIVED:Lcom/helpshift/common/exception/NetworkException;

    invoke-static {p1, v0}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;Lcom/helpshift/common/exception/ExceptionType;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method
