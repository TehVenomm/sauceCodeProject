.class public Lcom/helpshift/support/ApiConfig;
.super Ljava/lang/Object;
.source "ApiConfig.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/ApiConfig$Builder;
    }
.end annotation


# instance fields
.field private final conversationPrefillText:Ljava/lang/String;

.field private final customContactUsFlows:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation
.end field

.field private final customIssueFields:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "[",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private final customMetadata:Lcom/helpshift/support/Metadata;

.field private final enableContactUs:Ljava/lang/Integer;

.field private final enableFullPrivacy:Z

.field private final enableTypingIndicator:Z

.field private final extras:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private final gotoConversationAfterContactUs:Z

.field private final hideNameAndEmail:Z

.field private final requireEmail:Z

.field private final showConversationInfoScreen:Z

.field private final showConversationResolutionQuestion:Z

.field private final showSearchOnNewConversation:Z

.field private final toolbarId:I

.field private final withTagsMatching:Lcom/helpshift/support/FaqTagFilter;


# direct methods
.method constructor <init>(Ljava/lang/Integer;ZZZLjava/lang/String;ZZZLjava/util/List;Lcom/helpshift/support/FaqTagFilter;Lcom/helpshift/support/Metadata;IZZLjava/util/Map;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Integer;",
            "ZZZ",
            "Ljava/lang/String;",
            "ZZZ",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;",
            "Lcom/helpshift/support/FaqTagFilter;",
            "Lcom/helpshift/support/Metadata;",
            "IZZ",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "[",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    move-object v0, p0

    .line 50
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    move-object v1, p1

    .line 51
    iput-object v1, v0, Lcom/helpshift/support/ApiConfig;->enableContactUs:Ljava/lang/Integer;

    move v1, p2

    .line 52
    iput-boolean v1, v0, Lcom/helpshift/support/ApiConfig;->gotoConversationAfterContactUs:Z

    move v1, p3

    .line 53
    iput-boolean v1, v0, Lcom/helpshift/support/ApiConfig;->requireEmail:Z

    move v1, p4

    .line 54
    iput-boolean v1, v0, Lcom/helpshift/support/ApiConfig;->hideNameAndEmail:Z

    move-object v1, p5

    .line 55
    iput-object v1, v0, Lcom/helpshift/support/ApiConfig;->conversationPrefillText:Ljava/lang/String;

    move v1, p6

    .line 56
    iput-boolean v1, v0, Lcom/helpshift/support/ApiConfig;->enableFullPrivacy:Z

    move v1, p7

    .line 57
    iput-boolean v1, v0, Lcom/helpshift/support/ApiConfig;->showSearchOnNewConversation:Z

    move v1, p8

    .line 58
    iput-boolean v1, v0, Lcom/helpshift/support/ApiConfig;->showConversationResolutionQuestion:Z

    move-object v1, p9

    .line 59
    iput-object v1, v0, Lcom/helpshift/support/ApiConfig;->customContactUsFlows:Ljava/util/List;

    move-object v1, p10

    .line 60
    iput-object v1, v0, Lcom/helpshift/support/ApiConfig;->withTagsMatching:Lcom/helpshift/support/FaqTagFilter;

    move-object v1, p11

    .line 61
    iput-object v1, v0, Lcom/helpshift/support/ApiConfig;->customMetadata:Lcom/helpshift/support/Metadata;

    move v1, p12

    .line 62
    iput v1, v0, Lcom/helpshift/support/ApiConfig;->toolbarId:I

    move v1, p13

    .line 63
    iput-boolean v1, v0, Lcom/helpshift/support/ApiConfig;->showConversationInfoScreen:Z

    move/from16 v1, p14

    .line 64
    iput-boolean v1, v0, Lcom/helpshift/support/ApiConfig;->enableTypingIndicator:Z

    move-object/from16 v1, p15

    .line 65
    iput-object v1, v0, Lcom/helpshift/support/ApiConfig;->customIssueFields:Ljava/util/Map;

    move-object/from16 v1, p16

    .line 66
    iput-object v1, v0, Lcom/helpshift/support/ApiConfig;->extras:Ljava/util/Map;

    return-void
.end method


# virtual methods
.method public toMap()Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 73
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "enableContactUs"

    .line 74
    iget-object v2, p0, Lcom/helpshift/support/ApiConfig;->enableContactUs:Ljava/lang/Integer;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "gotoConversationAfterContactUs"

    .line 75
    iget-boolean v2, p0, Lcom/helpshift/support/ApiConfig;->gotoConversationAfterContactUs:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "requireEmail"

    .line 76
    iget-boolean v2, p0, Lcom/helpshift/support/ApiConfig;->requireEmail:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "hideNameAndEmail"

    .line 77
    iget-boolean v2, p0, Lcom/helpshift/support/ApiConfig;->hideNameAndEmail:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "enableFullPrivacy"

    .line 78
    iget-boolean v2, p0, Lcom/helpshift/support/ApiConfig;->enableFullPrivacy:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "showSearchOnNewConversation"

    .line 79
    iget-boolean v2, p0, Lcom/helpshift/support/ApiConfig;->showSearchOnNewConversation:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "showConversationResolutionQuestion"

    .line 80
    iget-boolean v2, p0, Lcom/helpshift/support/ApiConfig;->showConversationResolutionQuestion:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "showConversationInfoScreen"

    .line 81
    iget-boolean v2, p0, Lcom/helpshift/support/ApiConfig;->showConversationInfoScreen:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "enableTypingIndicator"

    .line 82
    iget-boolean v2, p0, Lcom/helpshift/support/ApiConfig;->enableTypingIndicator:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 84
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->conversationPrefillText:Ljava/lang/String;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->conversationPrefillText:Ljava/lang/String;

    .line 85
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    if-lez v1, :cond_0

    const-string v1, "conversationPrefillText"

    .line 86
    iget-object v2, p0, Lcom/helpshift/support/ApiConfig;->conversationPrefillText:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 89
    :cond_0
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->customContactUsFlows:Ljava/util/List;

    if-eqz v1, :cond_1

    const-string v1, "customContactUsFlows"

    .line 90
    iget-object v2, p0, Lcom/helpshift/support/ApiConfig;->customContactUsFlows:Ljava/util/List;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 93
    :cond_1
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->withTagsMatching:Lcom/helpshift/support/FaqTagFilter;

    if-eqz v1, :cond_2

    .line 94
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->withTagsMatching:Lcom/helpshift/support/FaqTagFilter;

    invoke-virtual {v1}, Lcom/helpshift/support/FaqTagFilter;->toMap()Ljava/util/Map;

    move-result-object v1

    if-eqz v1, :cond_2

    const-string v2, "withTagsMatching"

    .line 96
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 100
    :cond_2
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->customMetadata:Lcom/helpshift/support/Metadata;

    if-eqz v1, :cond_3

    .line 101
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->customMetadata:Lcom/helpshift/support/Metadata;

    invoke-virtual {v1}, Lcom/helpshift/support/Metadata;->toMap()Ljava/util/Map;

    move-result-object v1

    .line 102
    invoke-interface {v1}, Ljava/util/Map;->size()I

    move-result v2

    if-lez v2, :cond_3

    const-string v2, "hs-custom-metadata"

    .line 103
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 107
    :cond_3
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->customIssueFields:Ljava/util/Map;

    if-eqz v1, :cond_4

    const-string v1, "hs-custom-issue-field"

    .line 108
    iget-object v2, p0, Lcom/helpshift/support/ApiConfig;->customIssueFields:Ljava/util/Map;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 111
    :cond_4
    iget v1, p0, Lcom/helpshift/support/ApiConfig;->toolbarId:I

    if-eqz v1, :cond_5

    const-string v1, "toolbarId"

    .line 112
    iget v2, p0, Lcom/helpshift/support/ApiConfig;->toolbarId:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 115
    :cond_5
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->extras:Ljava/util/Map;

    if-eqz v1, :cond_7

    .line 116
    iget-object v1, p0, Lcom/helpshift/support/ApiConfig;->extras:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_6
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_7

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 117
    iget-object v3, p0, Lcom/helpshift/support/ApiConfig;->extras:Ljava/util/Map;

    invoke-interface {v3, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    if-eqz v3, :cond_6

    .line 118
    iget-object v3, p0, Lcom/helpshift/support/ApiConfig;->extras:Ljava/util/Map;

    invoke-interface {v3, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_7
    return-object v0
.end method
