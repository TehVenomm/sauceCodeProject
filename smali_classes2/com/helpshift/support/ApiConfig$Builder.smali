.class public Lcom/helpshift/support/ApiConfig$Builder;
.super Ljava/lang/Object;
.source "ApiConfig.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/ApiConfig;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field private conversationPrefillText:Ljava/lang/String;

.field private customContactUsFlows:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation
.end field

.field private customIssueFields:Ljava/util/Map;
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

.field private customMetadata:Lcom/helpshift/support/Metadata;

.field private enableContactUs:Ljava/lang/Integer;

.field private enableFullPrivacy:Z

.field private enableTypingIndicator:Z

.field private extras:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private gotoConversationAfterContactUs:Z

.field private hideNameAndEmail:Z

.field private isGotoConversationAfterContactUsSet:Z

.field private requireEmail:Z

.field private showConversationInfoScreen:Z

.field private showConversationResolutionQuestion:Z

.field private showSearchOnNewConversation:Z

.field private toolbarId:I

.field private withTagsMatching:Lcom/helpshift/support/FaqTagFilter;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 154
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 130
    sget-object v0, Lcom/helpshift/support/Support$EnableContactUs;->ALWAYS:Ljava/lang/Integer;

    iput-object v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->enableContactUs:Ljava/lang/Integer;

    const/4 v0, 0x0

    .line 131
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->gotoConversationAfterContactUs:Z

    .line 132
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->requireEmail:Z

    .line 133
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->hideNameAndEmail:Z

    .line 135
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->enableFullPrivacy:Z

    .line 136
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->showSearchOnNewConversation:Z

    .line 137
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->showConversationResolutionQuestion:Z

    .line 142
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->showConversationInfoScreen:Z

    .line 144
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->isGotoConversationAfterContactUsSet:Z

    .line 145
    iput-boolean v0, p0, Lcom/helpshift/support/ApiConfig$Builder;->enableTypingIndicator:Z

    return-void
.end method


# virtual methods
.method public build()Lcom/helpshift/support/ApiConfig;
    .locals 20

    move-object/from16 v0, p0

    .line 420
    new-instance v18, Lcom/helpshift/support/ApiConfig;

    move-object/from16 v1, v18

    iget-object v2, v0, Lcom/helpshift/support/ApiConfig$Builder;->enableContactUs:Ljava/lang/Integer;

    iget-boolean v3, v0, Lcom/helpshift/support/ApiConfig$Builder;->gotoConversationAfterContactUs:Z

    iget-boolean v4, v0, Lcom/helpshift/support/ApiConfig$Builder;->requireEmail:Z

    iget-boolean v5, v0, Lcom/helpshift/support/ApiConfig$Builder;->hideNameAndEmail:Z

    iget-object v6, v0, Lcom/helpshift/support/ApiConfig$Builder;->conversationPrefillText:Ljava/lang/String;

    iget-boolean v7, v0, Lcom/helpshift/support/ApiConfig$Builder;->enableFullPrivacy:Z

    iget-boolean v8, v0, Lcom/helpshift/support/ApiConfig$Builder;->showSearchOnNewConversation:Z

    iget-boolean v9, v0, Lcom/helpshift/support/ApiConfig$Builder;->showConversationResolutionQuestion:Z

    iget-object v10, v0, Lcom/helpshift/support/ApiConfig$Builder;->customContactUsFlows:Ljava/util/List;

    iget-object v11, v0, Lcom/helpshift/support/ApiConfig$Builder;->withTagsMatching:Lcom/helpshift/support/FaqTagFilter;

    iget-object v12, v0, Lcom/helpshift/support/ApiConfig$Builder;->customMetadata:Lcom/helpshift/support/Metadata;

    iget v13, v0, Lcom/helpshift/support/ApiConfig$Builder;->toolbarId:I

    iget-boolean v14, v0, Lcom/helpshift/support/ApiConfig$Builder;->showConversationInfoScreen:Z

    iget-boolean v15, v0, Lcom/helpshift/support/ApiConfig$Builder;->enableTypingIndicator:Z

    move-object/from16 v19, v1

    iget-object v1, v0, Lcom/helpshift/support/ApiConfig$Builder;->customIssueFields:Ljava/util/Map;

    move-object/from16 v16, v1

    iget-object v1, v0, Lcom/helpshift/support/ApiConfig$Builder;->extras:Ljava/util/Map;

    move-object/from16 v17, v1

    move-object/from16 v1, v19

    invoke-direct/range {v1 .. v17}, Lcom/helpshift/support/ApiConfig;-><init>(Ljava/lang/Integer;ZZZLjava/lang/String;ZZZLjava/util/List;Lcom/helpshift/support/FaqTagFilter;Lcom/helpshift/support/Metadata;IZZLjava/util/Map;Ljava/util/Map;)V

    return-object v18
.end method

.method public setConversationPrefillText(Ljava/lang/String;)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0

    .line 241
    iput-object p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->conversationPrefillText:Ljava/lang/String;

    return-object p0
.end method

.method public setCustomContactUsFlows(Ljava/util/List;)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;)",
            "Lcom/helpshift/support/ApiConfig$Builder;"
        }
    .end annotation

    .line 315
    iput-object p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->customContactUsFlows:Ljava/util/List;

    return-object p0
.end method

.method public setCustomIssueFields(Ljava/util/Map;)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "[",
            "Ljava/lang/String;",
            ">;)",
            "Lcom/helpshift/support/ApiConfig$Builder;"
        }
    .end annotation

    .line 404
    iput-object p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->customIssueFields:Ljava/util/Map;

    return-object p0
.end method

.method public setCustomMetadata(Lcom/helpshift/support/Metadata;)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0

    .line 349
    iput-object p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->customMetadata:Lcom/helpshift/support/Metadata;

    return-object p0
.end method

.method public setEnableContactUs(Ljava/lang/Integer;)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 1

    if-eqz p1, :cond_0

    .line 167
    sget-object v0, Lcom/helpshift/support/Support$EnableContactUs;->valueSet:Ljava/util/HashSet;

    .line 168
    invoke-virtual {v0, p1}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 169
    iput-object p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->enableContactUs:Ljava/lang/Integer;

    :cond_0
    return-object p0
.end method

.method public setEnableFullPrivacy(Z)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0

    .line 256
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->enableFullPrivacy:Z

    return-object p0
.end method

.method public setEnableTypingIndicator(Z)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 393
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->enableTypingIndicator:Z

    return-object p0
.end method

.method public setExtras(Ljava/util/Map;)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/support/ApiConfig$Builder;"
        }
    .end annotation

    .line 409
    iput-object p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->extras:Ljava/util/Map;

    return-object p0
.end method

.method public setGotoConversationAfterContactUs(Z)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 189
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->gotoConversationAfterContactUs:Z

    const/4 p1, 0x1

    .line 190
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->isGotoConversationAfterContactUsSet:Z

    return-object p0
.end method

.method public setHideNameAndEmail(Z)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 227
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->hideNameAndEmail:Z

    return-object p0
.end method

.method public setRequireEmail(Z)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 208
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->requireEmail:Z

    return-object p0
.end method

.method public setShowConversationInfoScreen(Z)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0

    .line 375
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->showConversationInfoScreen:Z

    return-object p0
.end method

.method public setShowConversationResolutionQuestion(Z)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 296
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->showConversationResolutionQuestion:Z

    return-object p0
.end method

.method public setShowSearchOnNewConversation(Z)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 277
    iput-boolean p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->showSearchOnNewConversation:Z

    return-object p0
.end method

.method public setToolbarId(I)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0

    .line 361
    iput p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->toolbarId:I

    return-object p0
.end method

.method public setWithTagsMatching(Lcom/helpshift/support/FaqTagFilter;)Lcom/helpshift/support/ApiConfig$Builder;
    .locals 0

    .line 337
    iput-object p1, p0, Lcom/helpshift/support/ApiConfig$Builder;->withTagsMatching:Lcom/helpshift/support/FaqTagFilter;

    return-object p0
.end method
