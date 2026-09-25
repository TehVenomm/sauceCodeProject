.class public Lcom/helpshift/conversation/activeconversation/UIConversation;
.super Ljava/lang/Object;
.source "UIConversation.java"


# instance fields
.field public final createdAt:Ljava/lang/String;

.field public final epochCreateTime:J

.field public final index:I

.field public final isInPreIssueMode:Z

.field public final isRedacted:Z

.field public final issueState:Lcom/helpshift/conversation/dto/IssueState;

.field public final localID:J

.field public final publishId:Ljava/lang/String;


# direct methods
.method public constructor <init>(JILjava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/dto/IssueState;Z)V
    .locals 0

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 29
    iput-wide p1, p0, Lcom/helpshift/conversation/activeconversation/UIConversation;->localID:J

    .line 30
    iput p3, p0, Lcom/helpshift/conversation/activeconversation/UIConversation;->index:I

    .line 31
    iput-object p4, p0, Lcom/helpshift/conversation/activeconversation/UIConversation;->createdAt:Ljava/lang/String;

    .line 32
    iput-object p7, p0, Lcom/helpshift/conversation/activeconversation/UIConversation;->publishId:Ljava/lang/String;

    .line 33
    iput-boolean p8, p0, Lcom/helpshift/conversation/activeconversation/UIConversation;->isInPreIssueMode:Z

    .line 34
    iput-object p9, p0, Lcom/helpshift/conversation/activeconversation/UIConversation;->issueState:Lcom/helpshift/conversation/dto/IssueState;

    .line 35
    iput-boolean p10, p0, Lcom/helpshift/conversation/activeconversation/UIConversation;->isRedacted:Z

    .line 36
    iput-wide p5, p0, Lcom/helpshift/conversation/activeconversation/UIConversation;->epochCreateTime:J

    return-void
.end method
