.class Lcom/helpshift/faq/FaqsDM$1;
.super Lcom/helpshift/common/domain/F;
.source "FaqsDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/faq/FaqsDM;->markHelpful(Ljava/lang/String;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/faq/FaqsDM;

.field final synthetic val$faqId:Ljava/lang/String;

.field final synthetic val$isHelpful:Z


# direct methods
.method constructor <init>(Lcom/helpshift/faq/FaqsDM;Ljava/lang/String;Z)V
    .locals 0

    .line 52
    iput-object p1, p0, Lcom/helpshift/faq/FaqsDM$1;->this$0:Lcom/helpshift/faq/FaqsDM;

    iput-object p2, p0, Lcom/helpshift/faq/FaqsDM$1;->val$faqId:Ljava/lang/String;

    iput-boolean p3, p0, Lcom/helpshift/faq/FaqsDM$1;->val$isHelpful:Z

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 56
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/faq/FaqsDM$1;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$1;->val$faqId:Ljava/lang/String;

    iget-boolean v2, p0, Lcom/helpshift/faq/FaqsDM$1;->val$isHelpful:Z

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/faq/FaqsDM;->send(Ljava/lang/String;Z)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 59
    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v1, v2, :cond_0

    :goto_0
    return-void

    .line 61
    :cond_0
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$1;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v1, v1, Lcom/helpshift/faq/FaqsDM;->faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

    iget-object v2, p0, Lcom/helpshift/faq/FaqsDM$1;->val$faqId:Ljava/lang/String;

    iget-boolean v3, p0, Lcom/helpshift/faq/FaqsDM$1;->val$isHelpful:Z

    invoke-interface {v1, v2, v3}, Lcom/helpshift/faq/dao/FaqEventDAO;->insertFaqMarkHelpfulEvent(Ljava/lang/String;Z)V

    .line 64
    iget-object v1, p0, Lcom/helpshift/faq/FaqsDM$1;->this$0:Lcom/helpshift/faq/FaqsDM;

    iget-object v1, v1, Lcom/helpshift/faq/FaqsDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v1

    sget-object v2, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->FAQ:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0}, Lcom/helpshift/common/exception/RootAPIException;->getServerStatusCode()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleRetryTaskForEventType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;I)V

    .line 65
    throw v0
.end method
