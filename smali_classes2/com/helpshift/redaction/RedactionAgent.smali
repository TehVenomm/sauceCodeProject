.class public Lcom/helpshift/redaction/RedactionAgent;
.super Ljava/lang/Object;
.source "RedactionAgent.java"


# instance fields
.field private domain:Lcom/helpshift/common/domain/Domain;

.field private platform:Lcom/helpshift/common/platform/Platform;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;)V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 18
    iput-object p1, p0, Lcom/helpshift/redaction/RedactionAgent;->platform:Lcom/helpshift/common/platform/Platform;

    .line 19
    iput-object p2, p0, Lcom/helpshift/redaction/RedactionAgent;->domain:Lcom/helpshift/common/domain/Domain;

    return-void
.end method

.method private isConversationRedacted(Ljava/lang/Long;Ljava/lang/Long;)Z
    .locals 5

    const/4 v0, 0x1

    const/4 v1, 0x0

    if-nez p1, :cond_1

    :cond_0
    const/4 v0, 0x0

    goto :goto_0

    :cond_1
    if-nez p2, :cond_2

    goto :goto_0

    .line 115
    :cond_2
    invoke-virtual {p2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide p1

    cmp-long v4, v2, p1

    if-gez v4, :cond_0

    :goto_0
    return v0
.end method

.method private isUserRedacted(Ljava/lang/Long;Ljava/lang/Long;)Z
    .locals 5

    const/4 v0, 0x1

    const/4 v1, 0x0

    if-nez p2, :cond_1

    :cond_0
    const/4 v0, 0x0

    goto :goto_0

    :cond_1
    if-nez p1, :cond_2

    goto :goto_0

    .line 90
    :cond_2
    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-virtual {p2}, Ljava/lang/Long;->longValue()J

    move-result-wide p1

    cmp-long v4, v2, p1

    if-lez v4, :cond_0

    :goto_0
    return v0
.end method

.method private updateRedaction(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/redaction/RedactionType;)V
    .locals 4

    .line 56
    new-instance v0, Lcom/helpshift/redaction/RedactionDetail;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    sget-object v3, Lcom/helpshift/redaction/RedactionState;->PENDING:Lcom/helpshift/redaction/RedactionState;

    invoke-direct {v0, v1, v2, v3, p2}, Lcom/helpshift/redaction/RedactionDetail;-><init>(JLcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionType;)V

    .line 57
    iget-object p2, p0, Lcom/helpshift/redaction/RedactionAgent;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getRedactionDAO()Lcom/helpshift/redaction/RedactionDAO;

    move-result-object p2

    .line 58
    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {p2, v1, v2}, Lcom/helpshift/redaction/RedactionDAO;->getRedactionDetail(J)Lcom/helpshift/redaction/RedactionDetail;

    move-result-object p1

    if-nez p1, :cond_0

    .line 61
    invoke-interface {p2, v0}, Lcom/helpshift/redaction/RedactionDAO;->insertRedactionDetail(Lcom/helpshift/redaction/RedactionDetail;)V

    goto :goto_0

    .line 65
    :cond_0
    invoke-interface {p2, v0}, Lcom/helpshift/redaction/RedactionDAO;->updateRedactionRedail(Lcom/helpshift/redaction/RedactionDetail;)V

    :goto_0
    return-void
.end method


# virtual methods
.method public checkAndUpdateRedactionState(Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/Long;Ljava/lang/Long;)V
    .locals 2

    .line 30
    iget-object v0, p0, Lcom/helpshift/redaction/RedactionAgent;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v0

    .line 31
    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getConversationInboxDM(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    .line 33
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getOldestConversationCreatedAtTime()Ljava/lang/Long;

    move-result-object v1

    .line 34
    invoke-direct {p0, p2, v1}, Lcom/helpshift/redaction/RedactionAgent;->isUserRedacted(Ljava/lang/Long;Ljava/lang/Long;)Z

    move-result p2

    if-eqz p2, :cond_0

    .line 36
    sget-object p2, Lcom/helpshift/redaction/RedactionType;->USER:Lcom/helpshift/redaction/RedactionType;

    invoke-direct {p0, p1, p2}, Lcom/helpshift/redaction/RedactionAgent;->updateRedaction(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/redaction/RedactionType;)V

    goto :goto_0

    .line 41
    :cond_0
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getLastConversationsRedactionTime()Ljava/lang/Long;

    move-result-object p2

    .line 42
    invoke-direct {p0, p3, p2}, Lcom/helpshift/redaction/RedactionAgent;->isConversationRedacted(Ljava/lang/Long;Ljava/lang/Long;)Z

    move-result p2

    if-eqz p2, :cond_1

    .line 44
    sget-object p2, Lcom/helpshift/redaction/RedactionType;->CONVERSATION:Lcom/helpshift/redaction/RedactionType;

    invoke-direct {p0, p1, p2}, Lcom/helpshift/redaction/RedactionAgent;->updateRedaction(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/redaction/RedactionType;)V

    :cond_1
    :goto_0
    if-eqz p3, :cond_2

    .line 50
    invoke-virtual {p3}, Ljava/lang/Long;->longValue()J

    move-result-wide p1

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveLastConversationsRedactionTime(J)V

    :cond_2
    return-void
.end method
