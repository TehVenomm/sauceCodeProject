.class public Lcom/helpshift/configuration/response/RootServerConfig;
.super Ljava/lang/Object;
.source "RootServerConfig.java"


# instance fields
.field public final allowUserAttachments:Z

.field public final autoFillFirstPreissueMessage:Z

.field public final breadcrumbLimit:I

.field public final conversationGreetingMessage:Ljava/lang/String;

.field public final conversationalIssueFiling:Z

.field public final customerSatisfactionSurvey:Z

.field public final debugLogLimit:I

.field public final disableHelpshiftBranding:Z

.field public final disableInAppConversation:Z

.field public final enableTypingIndicator:Z

.field public final issueExists:Z

.field public final lastRedactionAt:Ljava/lang/Long;

.field public final periodicFetchInterval:J

.field public final periodicReview:Lcom/helpshift/configuration/response/PeriodicReview;

.field public final preissueResetInterval:J

.field public final profileCreatedAt:Ljava/lang/Long;

.field public final profileFormEnable:Z

.field public final requireNameAndEmail:Z

.field public final reviewUrl:Ljava/lang/String;

.field public final shouldShowConversationHistory:Z

.field public final showAgentName:Z

.field public final showConversationResolutionQuestion:Z


# direct methods
.method public constructor <init>(ZZZZZZZIILjava/lang/String;Lcom/helpshift/configuration/response/PeriodicReview;ZLjava/lang/String;ZZZLjava/lang/Long;Ljava/lang/Long;ZJJZ)V
    .locals 3

    move-object v0, p0

    .line 63
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    move v1, p1

    .line 64
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->requireNameAndEmail:Z

    move v1, p2

    .line 65
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->profileFormEnable:Z

    move v1, p3

    .line 66
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->showAgentName:Z

    move v1, p4

    .line 67
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->customerSatisfactionSurvey:Z

    move v1, p5

    .line 68
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->disableInAppConversation:Z

    move v1, p6

    .line 69
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->disableHelpshiftBranding:Z

    move v1, p7

    .line 70
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->issueExists:Z

    move v1, p8

    .line 71
    iput v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->debugLogLimit:I

    move v1, p9

    .line 72
    iput v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->breadcrumbLimit:I

    move-object v1, p10

    .line 73
    iput-object v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->reviewUrl:Ljava/lang/String;

    move-object v1, p11

    .line 74
    iput-object v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->periodicReview:Lcom/helpshift/configuration/response/PeriodicReview;

    move v1, p12

    .line 75
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->conversationalIssueFiling:Z

    move-object/from16 v1, p13

    .line 76
    iput-object v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->conversationGreetingMessage:Ljava/lang/String;

    move/from16 v1, p14

    .line 77
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->enableTypingIndicator:Z

    move/from16 v1, p15

    .line 78
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->showConversationResolutionQuestion:Z

    move/from16 v1, p16

    .line 79
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->shouldShowConversationHistory:Z

    move-object/from16 v1, p17

    .line 80
    iput-object v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->lastRedactionAt:Ljava/lang/Long;

    move-object/from16 v1, p18

    .line 81
    iput-object v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->profileCreatedAt:Ljava/lang/Long;

    move/from16 v1, p19

    .line 82
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->allowUserAttachments:Z

    move-wide/from16 v1, p20

    .line 83
    iput-wide v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->periodicFetchInterval:J

    move-wide/from16 v1, p22

    .line 84
    iput-wide v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->preissueResetInterval:J

    move/from16 v1, p24

    .line 85
    iput-boolean v1, v0, Lcom/helpshift/configuration/response/RootServerConfig;->autoFillFirstPreissueMessage:Z

    return-void
.end method
